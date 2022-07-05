#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Production;
export DOTNET_ENVIRONMENT=Production;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

printf "\n[OZDS] Linting with 'yarn'...\n";
yarn --cwd "$ROOT_DIR" lint;

printf "\n[OZDS] Building with 'dotnet'...\n";
dotnet \
  build \
  --output "$ROOT_DIR/artifacts" \
  --configuration Release;
