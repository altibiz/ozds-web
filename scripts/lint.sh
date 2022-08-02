#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";

export ASPNETCORE_ENVIRONMENT=Production;
export DOTNET_ENVIRONMENT=Production;
export ORCHARD_APP_DATA="$ROOT/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

printf "[OZDS] Linting with 'yarn'...\n";
yarn --cwd "$ROOT" lint;

printf "\n[OZDS] Building with 'dotnet'...\n";
dotnet \
  build \
  --output "$ROOT/artifacts" \
  --configuration Release;
