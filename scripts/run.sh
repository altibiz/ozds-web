#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Production;
export DOTNET_ENVIRONMENT=Production;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

echo -e "
[OZDS] Building with 'yarn'...
";
yarn --cwd "$ROOT_DIR" build;

echo -e "
[OZDS] Running with 'dotnet' in production...
";
dotnet \
  run \
  --configuration Release \
  --property:consoleLoggerParameters=ErrorsOnly \
  --project "$ROOT_DIR/src/Ozds.Web/Ozds.Web.csproj";