#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";
CONTENT_ROOT="$ROOT_DIR/artifacts";
SECRETS_SH="$ROOT_DIR/secrets.sh";
WD=`pwd`;

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
[OZDS] Publishing with 'dotnet'...
";
dotnet \
  publish \
  --output "$CONTENT_ROOT" \
  --property:consoleLoggerParameters=ErrorsOnly \
  --property:IsWebConfigTransformDisabled=true \
  --configuration Release;

cd "$CONTENT_ROOT";
source "$SECRETS_SH";
echo -e "
[OZDS] Running with 'dotnet'...
";
dotnet "Ozds.Web.dll";
cd "$WD";
