#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";

export ASPNETCORE_ENVIRONMENT=Development;
export DOTNET_ENVIRONMENT=Development;
export ORCHARD_APP_DATA="$ROOT/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="development";

printf "[OZDS] Building with 'yarn'...\n";
yarn --cwd "$ROOT" build;

printf "\n[OZDS] Running with 'dotnet' in development...\n";
dotnet \
  watch run \
  --configuration Debug \
  --property:consoleLoggerParameters=ErrorsOnly \
  --project "$ROOT/src/Ozds.Web/Ozds.Web.csproj";
