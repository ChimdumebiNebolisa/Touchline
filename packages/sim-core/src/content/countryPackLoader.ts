import type { CountryPack } from "../shared/types.js";

export function parseCountryPackJson(rawJson: string): CountryPack {
  return JSON.parse(rawJson) as CountryPack;
}
