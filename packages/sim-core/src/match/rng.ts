export interface RandomSource {
  next: () => number;
}

export function createSeededRandom(seed: number): RandomSource {
  let state = seed >>> 0;
  return {
    next: () => {
      state += 0x6d2b79f5;
      let value = Math.imul(state ^ (state >>> 15), 1 | state);
      value ^= value + Math.imul(value ^ (value >>> 7), 61 | value);
      return ((value ^ (value >>> 14)) >>> 0) / 4294967296;
    }
  };
}
