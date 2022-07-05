#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Production;
export DOTNET_ENVIRONMENT=Production;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

printf "\n[OZDS] Building with 'yarn'...\n";
yarn --cwd "$ROOT_DIR" build;

printf "\n[OZDS] Running with 'dotnet' in production...\n";
dotnet \
  run \
  --configuration Release \
  --property:consoleLoggerParameters=ErrorsOnly \
  --project "$ROOT_DIR/src/Ozds.Web/Ozds.Web.csproj";
