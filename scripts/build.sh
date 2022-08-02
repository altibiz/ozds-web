#!/usr/bin/env bash

SCRIPTS="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT="$(dirname "$SCRIPTS")";

export ASPNETCORE_ENVIRONMENT=Production;
export DOTNET_ENVIRONMENT=Production;
export ORCHARD_APP_DATA="$ROOT/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

echo -e "
[OZDS] Building with 'yarn'...
";
yarn --cwd "$ROOT" build;

echo -e "
[OZDS] Building with 'dotnet'...
";
dotnet \
  build \
  --no-incremental \
  --output "$ROOT/build" \
  --property:consoleLoggerParameters=ErrorsOnly \
  --configuration Release;
