import { execSync } from "node:child_process";

const commands = [
  "npm run -w @touchline/sim-core build",
  "npm run -w @touchline/save build",
  "npm run manual:step1",
  "npm run manual:step2",
  "npm run manual:step4",
  "npm run manual:step5",
  "npm run manual:step6",
  "npm run manual:step6:multiseason",
  "npm run manual:step7:calibration"
];

function runCommand(command) {
  console.log(`Running: ${command}`);
  execSync(command, {
    stdio: "inherit",
    env: process.env
  });
}

function main() {
  console.log("Step 7 Regression Check");
  console.log("- Running bounded regression command suite");

  for (const command of commands) {
    try {
      runCommand(command);
    } catch (error) {
      console.error(`Step 7 regression check failed while running '${command}'.`);
      if (error && typeof error === "object" && "status" in error) {
        console.error(`Exit status: ${String(error.status)}`);
      }
      process.exit(1);
    }
  }

  console.log("Step 7 regression check passed.");
}

main();
