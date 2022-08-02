#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";

export NODE_OPTIONS="--no-warnings";

printf "[OZDS] Installing dependencies with 'yarn'...\n";
yarn install;

printf "\n[OZDS] Installing dependencies with 'dotnet'...\n";
dotnet restore;

if [ ! "$CI" ] && [ ! -f "$ROOT/.husky/_/husky.sh" ]; then
  printf "\n[OZDS] Setting up git hooks with 'husky'...\n";
  yarn husky install;
fi;
