using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class AuditUiStateWriter
{
    private sealed class AuditUiState
    {
        public required string SceneName { get; init; }
        public required string TimestampUtc { get; init; }
        public required string RoleText { get; init; }
        public required string[] Anchors { get; init; }
        public string SelectedNav { get; init; } = string.Empty;
    }

    private static readonly string RuntimeStatePath =
        ProjectSettings.GlobalizePath("res://../docs/audit/active-playtest/logs/current-ui-state.json");

    public static void Write(string sceneName, string roleText, TouchlineRailRoute selectedNav, params string[] anchors)
    {
        try
        {
            var uniqueAnchors = new List<string>();
            foreach (var anchor in anchors)
            {
                if (string.IsNullOrWhiteSpace(anchor))
                {
                    continue;
                }

                var normalized = anchor.Trim();
                if (!uniqueAnchors.Contains(normalized))
                {
                    uniqueAnchors.Add(normalized);
                }
            }

            var payload = new AuditUiState
            {
                SceneName = sceneName,
                TimestampUtc = DateTime.UtcNow.ToString("O"),
                RoleText = roleText ?? string.Empty,
                SelectedNav = selectedNav == TouchlineRailRoute.None ? string.Empty : selectedNav.ToString(),
                Anchors = uniqueAnchors.ToArray()
            };

            var directory = Path.GetDirectoryName(RuntimeStatePath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(
                RuntimeStatePath,
                JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch (Exception exception)
        {
            GD.PushWarning($"Audit UI state write failed: {exception.Message}");
        }
    }
}
