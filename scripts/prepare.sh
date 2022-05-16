#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export NODE_OPTIONS="--no-warnings";

echo -e "
[OZDS] Installing dependencies with 'yarn'...
";
yarn install;

echo -e "
[OZDS] Installing dependencies with 'dotnet'...
";
dotnet restore;

echo -e "
[OZDS] Setting up git hooks with 'husky'...
";
if [[ ! "$CI" ]]; then
  yarn husky install;
fi;

echo -e "
[OZDS] Generating 'secrets.json'...
";
if [[ ! -f "$ROOT_DIR/secrets.json" ]]; then
  cp "$ROOT_DIR/example.secrets.json" "$ROOT_DIR/secrets.json"
fi;

echo -e "
[OZDS] Generating 'secrets.sh'...
";
if [[ ! -f "$ROOT_DIR/secrets.sh" ]]; then
  cp "$ROOT_DIR/example.secrets.sh" "$ROOT_DIR/secrets.sh"
fi;