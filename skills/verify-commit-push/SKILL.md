# verify-commit-push

## Purpose

Use this skill after every meaningful code change.

The goal is to ensure that work is:
- verified
- tracked
- committed cleanly
- pushed to GitHub
- resumable later

This skill is mandatory for autonomous work in this repo.

## Inputs

You need:
- the changed repository
- terminal access
- git access
- verification commands from the repo
- `AUTONOMOUS_PROGRESS.md`

## Required procedure

### 1. Inspect pending changes

Run:
- `git status`

Review all changed files.
Stage only the files that belong to the intended task.

### 2. Run verification

Run the strongest relevant checks for the changed area.

At minimum, use what exists in the repo for:
- tests
- typecheck
- lint
- build

If a changed subsystem has its own higher-signal checks, run those too.

Do not commit failing work unless the task is explicitly to capture a blocker and the failure is the blocker itself.

### 3. Update progress file

Update `AUTONOMOUS_PROGRESS.md` with:
- current active Plan step
- last completed verified task
- current subtask in progress
- next 1 to 3 queued subtasks
- known blockers
- last verification run
- last commit hash placeholder if commit has not happened yet
- resume instructions

### 4. Commit cleanly

Commit only the bounded intended change.

Commit message format:
- `step-N: <actual bounded change>`

Examples:
- `step-1: add post-match board fan and morale persistence`
- `step-2: implement contextual board expectation evaluation`

Do not:
- combine unrelated work
- leave extra debug edits
- amend older commits
- create noisy “misc fixes” commits

### 5. Push immediately

Push to the configured remote immediately after the commit.

This repo’s autonomous workflow assumes:
- verified change
- commit
- push
- continue

Do not keep piling up local-only commits during autonomous flow.

### 6. Confirm clean state

After push, run:
- `git status`

The worktree should be clean unless a known blocker is being documented.

## Failure handling

### Verification failure
If checks fail:
1. inspect the failure
2. fix if it is part of the current bounded task
3. rerun verification
4. only then commit

If fixing the failure would force unrelated scope, stop and document the blocker.

### Push failure
If push fails:
1. capture the exact command and output
2. write it into `AUTONOMOUS_PROGRESS.md`
3. do not pretend push succeeded
4. stop if auth/remote access blocks the required workflow

If push is rejected because the remote moved:
1. fetch
2. rebase carefully if safe
3. rerun relevant verification
4. push again

## Output format

Before commit, briefly state:
- verification being run
- files intended for commit

After push, briefly state:
- verification passed
- commit hash
- push result
- next subtask

## Hard rules

- No meaningful change without verification
- No meaningful verified change without commit
- No meaningful verified commit without push
- No fake “done” status without evidence
- No dirty worktree left behind when stopping unless a documented blocker requires it