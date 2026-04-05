import { readFileSync } from "node:fs";
import { resolve } from "node:path";

import { describe, expect, it } from "vitest";

import { parseCountryPackJson } from "../src/content/countryPackLoader.js";
import { validateCountryPack } from "../src/world/countryPack.js";

describe("first-country content pack", () => {
  it("satisfies the deep-top-two and playable-club contract", () => {
    const jsonPath = resolve(
      import.meta.dirname,
      "..",
      "content",
      "countries",
      "first-country.json"
    );
    const rawJson = readFileSync(jsonPath, "utf-8");
    const countryPack = parseCountryPackJson(rawJson);
    const validation = validateCountryPack(countryPack);

    expect(validation.valid).toBe(true);
    expect(validation.errors).toEqual([]);
  });
});
