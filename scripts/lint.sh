#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Production;
export DOTNET_ENVIRONMENT=Production;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

echo -e "
[OZDS] Linting with 'yarn'...
";
yarn --cwd "$ROOT_DIR" lint;

echo -e "
[OZDS] Building with 'dotnet'...
";
dotnet \
  build \
  --output "$ROOT_DIR/artifacts" \
  --configuration Release;