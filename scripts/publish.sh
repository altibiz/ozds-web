#!/usr/bin/env sh

#shellcheck disable=SC1090

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";
CONTENT_ROOT="$ROOT_DIR/artifacts";
SECRETS_SH="$ROOT_DIR/secrets.sh";
WD=$(pwd);

export ASPNETCORE_ENVIRONMENT=Production;
export DOTNET_ENVIRONMENT=Production;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

printf "\n[OZDS] Building with 'yarn'...\n";
yarn --cwd "$ROOT_DIR" build;

printf "\n[OZDS] Publishing with 'dotnet'...\n";
dotnet \
  publish \
  --output "$CONTENT_ROOT" \
  --property:consoleLoggerParameters=ErrorsOnly \
  --property:IsWebConfigTransformDisabled=true \
  --configuration Release;

cd "$CONTENT_ROOT" || exit;
. "$SECRETS_SH";
printf "\n[OZDS] Running with 'dotnet'...\n";
dotnet "Ozds.Web.dll";
cd "$WD" || exit;
