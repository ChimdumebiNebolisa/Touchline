import { mkdir, readdir, readFile, rm, writeFile } from "node:fs/promises";
import { join } from "node:path";

import {
  createSaveEnvelopeV1,
  deserializeSaveEnvelope,
  SaveLoadError,
  serializeSaveEnvelope
} from "./codec.js";
import type { SaveEnvelopeV1, SaveGameStateV1 } from "./types.js";

const SLOT_FILE_SUFFIX = ".json";
const SLOT_ID_PATTERN = /^[a-z0-9][a-z0-9-_]{1,62}$/;

function assertValidSlotId(slotId: string): void {
  if (!SLOT_ID_PATTERN.test(slotId)) {
    throw new SaveLoadError(
      "MALFORMED_PAYLOAD",
      `Save slot id '${slotId}' is invalid. Use lowercase alphanumeric, '-' or '_'.`
    );
  }
}

function isNotFoundError(error: unknown): boolean {
  return (
    typeof error === "object" &&
    error !== null &&
    "code" in error &&
    typeof (error as { code?: unknown }).code === "string" &&
    (error as { code: string }).code === "ENOENT"
  );
}

export interface SaveSlotMetadata {
  slotId: string;
  savedAtIso: string;
  version: number;
  filePath: string;
}

export function buildSaveSlotFilePath(rootDirectory: string, slotId: string): string {
  assertValidSlotId(slotId);
  return join(rootDirectory, `${slotId}${SLOT_FILE_SUFFIX}`);
}

export async function writeSaveSlot(options: {
  rootDirectory: string;
  slotId: string;
  state: SaveGameStateV1;
  savedAtIso?: string;
}): Promise<SaveEnvelopeV1> {
  const filePath = buildSaveSlotFilePath(options.rootDirectory, options.slotId);
  const envelope = createSaveEnvelopeV1(options.state, options.savedAtIso);

  try {
    await mkdir(options.rootDirectory, { recursive: true });
    await writeFile(filePath, serializeSaveEnvelope(envelope), "utf-8");
    return envelope;
  } catch (error) {
    if (error instanceof SaveLoadError) {
      throw error;
    }

    throw new SaveLoadError(
      "IO_FAILURE",
      `Failed to write save slot '${options.slotId}' at '${filePath}'.`
    );
  }
}

export async function readSaveSlot(options: {
  rootDirectory: string;
  slotId: string;
}): Promise<SaveEnvelopeV1> {
  const filePath = buildSaveSlotFilePath(options.rootDirectory, options.slotId);

  try {
    const raw = await readFile(filePath, "utf-8");
    return deserializeSaveEnvelope(raw);
  } catch (error) {
    if (error instanceof SaveLoadError) {
      throw error;
    }

    if (isNotFoundError(error)) {
      throw new SaveLoadError("SLOT_NOT_FOUND", `Save slot '${options.slotId}' was not found.`);
    }

    throw new SaveLoadError(
      "IO_FAILURE",
      `Failed to read save slot '${options.slotId}' at '${filePath}'.`
    );
  }
}

export async function deleteSaveSlot(options: {
  rootDirectory: string;
  slotId: string;
}): Promise<void> {
  const filePath = buildSaveSlotFilePath(options.rootDirectory, options.slotId);

  try {
    await rm(filePath, { force: false });
  } catch (error) {
    if (isNotFoundError(error)) {
      throw new SaveLoadError("SLOT_NOT_FOUND", `Save slot '${options.slotId}' was not found.`);
    }

    throw new SaveLoadError(
      "IO_FAILURE",
      `Failed to delete save slot '${options.slotId}' at '${filePath}'.`
    );
  }
}

export async function listSaveSlots(rootDirectory: string): Promise<SaveSlotMetadata[]> {
  let entries: string[];
  try {
    entries = await readdir(rootDirectory);
  } catch (error) {
    if (isNotFoundError(error)) {
      return [];
    }

    throw new SaveLoadError("IO_FAILURE", `Failed to list save slots in '${rootDirectory}'.`);
  }

  const slotFiles = entries.filter((entry) => entry.endsWith(SLOT_FILE_SUFFIX));
  const metadata: SaveSlotMetadata[] = [];

  for (const slotFile of slotFiles) {
    const slotId = slotFile.slice(0, -SLOT_FILE_SUFFIX.length);
    const envelope = await readSaveSlot({ rootDirectory, slotId });
    metadata.push({
      slotId,
      savedAtIso: envelope.savedAtIso,
      version: envelope.version,
      filePath: join(rootDirectory, slotFile)
    });
  }

  return metadata.sort((first, second) => second.savedAtIso.localeCompare(first.savedAtIso));
}
