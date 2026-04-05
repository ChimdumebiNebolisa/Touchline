import tsParser from "@typescript-eslint/parser";
import tsPlugin from "@typescript-eslint/eslint-plugin";

export default [
  {
    ignores: ["**/node_modules/**", "**/dist/**", "**/coverage/**"]
  },
  {
    files: ["**/*.ts"],
    languageOptions: {
      parser: tsParser,
      ecmaVersion: "latest",
      sourceType: "module"
    },
    plugins: {
      "@typescript-eslint": tsPlugin
    },
    rules: {
      "@typescript-eslint/consistent-type-imports": "error",
      "@typescript-eslint/no-unused-vars": [
        "error",
        { "argsIgnorePattern": "^_" }
      ]
    }
  }
];
