#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export NODE_OPTIONS="--no-warnings";

printf "\n[OZDS] Installing dependencies with 'yarn'...\n";
yarn install;

printf "\n[OZDS] Installing dependencies with 'dotnet'...\n";
dotnet restore;

printf "\n[OZDS] Setting up git hooks with 'husky'...\n";
if [ ! "$CI" ]; then
  yarn husky install;
fi;

printf "\n[OZDS] Generating 'secrets.json'...\n";
if [ ! -f "$ROOT_DIR/secrets.json" ]; then
  cp "$ROOT_DIR/example.secrets.json" "$ROOT_DIR/secrets.json"
fi;

printf "\n[OZDS] Generating 'secrets.sh'...\n";
if [ ! -f "$ROOT_DIR/secrets.sh" ]; then
  cp "$ROOT_DIR/example.secrets.sh" "$ROOT_DIR/secrets.sh"
fi;
