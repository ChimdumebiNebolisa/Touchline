using Godot;
using System;
using System.IO;
using System.Text.Json;

public partial class AuditCommandBridge : Node
{
    private sealed class AuditCommand
    {
        public string Id { get; init; } = string.Empty;
        public string Action { get; init; } = string.Empty;
        public string Path { get; init; } = string.Empty;
        public string Value { get; init; } = string.Empty;
        public string ExpectedScene { get; init; } = string.Empty;
    }

    private sealed class AuditCommandResult
    {
        public required string Id { get; init; }
        public required string TimestampUtc { get; init; }
        public required string SceneName { get; init; }
        public required bool Success { get; init; }
        public required string Message { get; init; }
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    private static readonly string CommandPath =
        ProjectSettings.GlobalizePath("res://../docs/audit/active-playtest/logs/audit-command.json");

    private static readonly string ResultPath =
        ProjectSettings.GlobalizePath("res://../docs/audit/active-playtest/logs/audit-command-result.json");

    private string _lastCommandId = string.Empty;
    private double _elapsed;
    private AuditCommand? _pendingScreenshotCommand;
    private int _screenshotWarmupFrames;

    public override void _Process(double delta)
    {
        _elapsed += delta;
        if (_elapsed < 0.1d)
        {
            return;
        }

        _elapsed = 0.0d;
        TryCompletePendingScreenshot();
        TryProcessCommand();
    }

    private void TryProcessCommand()
    {
        if (!File.Exists(CommandPath))
        {
            return;
        }

        try
        {
            var commandJson = File.ReadAllText(CommandPath);
            var command = JsonSerializer.Deserialize<AuditCommand>(commandJson, JsonOptions);
            if (command == null || string.IsNullOrWhiteSpace(command.Id) || command.Id == _lastCommandId)
            {
                return;
            }

            if (command.Action.Equals("capture_screenshot", StringComparison.Ordinal))
            {
                _pendingScreenshotCommand = command;
                _screenshotWarmupFrames = 3;
                _lastCommandId = command.Id;
                File.Delete(CommandPath);
                return;
            }

            var result = TryExecute(command);
            if (result == null)
            {
                return;
            }

            _lastCommandId = command.Id;
            File.WriteAllText(ResultPath, JsonSerializer.Serialize(result, JsonOptions));
            File.Delete(CommandPath);
        }
        catch (Exception exception)
        {
            WriteFailureResult(string.Empty, $"Audit command processing failed: {exception.Message}");
        }
    }

    private void TryCompletePendingScreenshot()
    {
        if (_pendingScreenshotCommand == null)
        {
            return;
        }

        if (_screenshotWarmupFrames > 0)
        {
            _screenshotWarmupFrames--;
            return;
        }

        try
        {
            var result = TryExecute(_pendingScreenshotCommand);
            if (result == null)
            {
                return;
            }

            File.WriteAllText(ResultPath, JsonSerializer.Serialize(result, JsonOptions));
            _pendingScreenshotCommand = null;
        }
        catch (Exception exception)
        {
            var pendingId = _pendingScreenshotCommand?.Id ?? string.Empty;
            WriteFailureResult(pendingId, $"Deferred screenshot failed: {exception.Message}");
            _pendingScreenshotCommand = null;
        }
    }

    private AuditCommandResult? TryExecute(AuditCommand command)
    {
        var currentScene = ResolveCurrentScene();
        var sceneName = currentScene?.Name.ToString() ?? string.Empty;
        if (currentScene == null)
        {
            return null;
        }

        if (!string.IsNullOrWhiteSpace(command.ExpectedScene) &&
            !sceneName.Equals(command.ExpectedScene, StringComparison.Ordinal))
        {
            return null;
        }

        return command.Action switch
        {
            "press_button" => ExecuteButtonPress(command, currentScene),
            "capture_screenshot" => ExecuteScreenshotCapture(command, currentScene),
            "select_option" => ExecuteOptionSelection(command, currentScene),
            "set_line_edit" => ExecuteLineEdit(command, currentScene),
            "set_spin_box" => ExecuteSpinBox(command, currentScene),
            _ => BuildResult(command.Id, sceneName, false, $"Unsupported action {command.Action}.")
        };
    }

    private Node? ResolveCurrentScene()
    {
        var currentScene = GetTree().CurrentScene;
        if (currentScene != null)
        {
            return currentScene;
        }

        var root = GetTree().Root;
        for (var index = root.GetChildCount() - 1; index >= 0; index--)
        {
            var child = root.GetChild(index);
            if (child == this || child == GetParent())
            {
                continue;
            }

            if (child is Window)
            {
                continue;
            }

            return child;
        }

        return null;
    }

    private AuditCommandResult ExecuteButtonPress(AuditCommand command, Node currentScene)
    {
        var node = currentScene.GetNodeOrNull(command.Path);
        if (node is not BaseButton button)
        {
            return BuildResult(command.Id, currentScene.Name, false, $"Button not found: {command.Path}");
        }

        if (button.Disabled)
        {
            return BuildResult(command.Id, currentScene.Name, false, $"Button disabled: {command.Path}");
        }

        button.EmitSignal(BaseButton.SignalName.Pressed);
        return BuildResult(command.Id, currentScene.Name, true, $"Pressed {command.Path}");
    }

    private AuditCommandResult ExecuteOptionSelection(AuditCommand command, Node currentScene)
    {
        var node = currentScene.GetNodeOrNull(command.Path);
        if (node is not OptionButton option)
        {
            return BuildResult(command.Id, currentScene.Name, false, $"OptionButton not found: {command.Path}");
        }

        for (var index = 0; index < option.ItemCount; index++)
        {
            if (!option.GetItemText(index).Equals(command.Value, StringComparison.Ordinal))
            {
                continue;
            }

            option.Select(index);
            option.EmitSignal(OptionButton.SignalName.ItemSelected, index);
            return BuildResult(command.Id, currentScene.Name, true, $"Selected {command.Value} on {command.Path}");
        }

        return BuildResult(command.Id, currentScene.Name, false, $"Option value not found: {command.Value}");
    }

    private AuditCommandResult ExecuteScreenshotCapture(AuditCommand command, Node currentScene)
    {
        if (string.IsNullOrWhiteSpace(command.Path))
        {
            return BuildResult(command.Id, currentScene.Name, false, "Screenshot path is required.");
        }

        var screenshotPath = Path.GetFullPath(command.Path);
        var directory = Path.GetDirectoryName(screenshotPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var image = GetViewport().GetTexture().GetImage();
        if (image == null)
        {
            return BuildResult(command.Id, currentScene.Name, false, "Viewport image is unavailable.");
        }

        var saveError = image.SavePng(screenshotPath);
        return saveError == Error.Ok
            ? BuildResult(command.Id, currentScene.Name, true, $"Saved screenshot to {screenshotPath}")
            : BuildResult(command.Id, currentScene.Name, false, $"Screenshot save failed: {saveError}");
    }

    private AuditCommandResult ExecuteLineEdit(AuditCommand command, Node currentScene)
    {
        var node = currentScene.GetNodeOrNull(command.Path);
        if (node is not LineEdit lineEdit)
        {
            return BuildResult(command.Id, currentScene.Name, false, $"LineEdit not found: {command.Path}");
        }

        lineEdit.Text = command.Value;
        lineEdit.EmitSignal(LineEdit.SignalName.TextChanged, command.Value);
        return BuildResult(command.Id, currentScene.Name, true, $"Updated {command.Path}");
    }

    private AuditCommandResult ExecuteSpinBox(AuditCommand command, Node currentScene)
    {
        var node = currentScene.GetNodeOrNull(command.Path);
        if (node is not SpinBox spinBox)
        {
            return BuildResult(command.Id, currentScene.Name, false, $"SpinBox not found: {command.Path}");
        }

        if (!double.TryParse(command.Value, out var parsed))
        {
            return BuildResult(command.Id, currentScene.Name, false, $"Invalid spin box value: {command.Value}");
        }

        spinBox.Value = parsed;
        spinBox.EmitSignal(SpinBox.SignalName.ValueChanged, parsed);
        return BuildResult(command.Id, currentScene.Name, true, $"Set {command.Path} to {parsed}");
    }

    private static AuditCommandResult BuildResult(string id, string sceneName, bool success, string message)
    {
        return new AuditCommandResult
        {
            Id = id,
            TimestampUtc = DateTime.UtcNow.ToString("O"),
            SceneName = sceneName,
            Success = success,
            Message = message
        };
    }

    private static void WriteFailureResult(string id, string message)
    {
        try
        {
            var result = BuildResult(id, string.Empty, false, message);
            File.WriteAllText(ResultPath, JsonSerializer.Serialize(result, JsonOptions));
        }
        catch
        {
            // Keep the bridge silent if the audit result file is also unavailable.
        }
    }
}
