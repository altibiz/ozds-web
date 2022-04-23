#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Development;
export DOTNET_ENVIRONMENT=Development;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="development";

echo -e "
[OZDS] Building with 'yarn'...
";
yarn --cwd "$ROOT_DIR" build;

echo -e "
[OZDS] Running with 'dotnet' in development...
";
dotnet \
  run \
  --configuration Debug \
  --property:consoleLoggerParameters=ErrorsOnly \
  --project "$ROOT_DIR/src/Ozds.Web/Ozds.Web.csproj";
