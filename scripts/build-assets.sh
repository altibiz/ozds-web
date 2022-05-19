#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

echo -e "
[OZDS] Building with 'yarn'...
";
yarn --cwd "$ROOT_DIR" build;
