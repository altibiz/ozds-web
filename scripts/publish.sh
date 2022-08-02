#!/usr/bin/env sh

#shellcheck disable=SC1090

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";
CONTENT_ROOT="$ROOT/artifacts";
SECRETS_SH="$ROOT/secrets.sh";
WD=$(pwd);

export ASPNETCORE_ENVIRONMENT=Production;
export DOTNET_ENVIRONMENT=Production;
export ORCHARD_APP_DATA="$ROOT/App_Data";

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="production";

printf "[OZDS] Building with 'yarn'...\n";
yarn --cwd "$ROOT" build;

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
