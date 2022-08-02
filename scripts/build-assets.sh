#!/usr/bin/env bash

SCRIPTS="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT="$(dirname "$SCRIPTS")";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

echo -e "
[OZDS] Building with 'yarn'...
";
yarn --cwd "$ROOT" build;
