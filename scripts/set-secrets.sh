#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";
SECRETS_PATH="$ROOT_DIR/secrets.json";

if [ ! -f "$SECRETS_PATH" ]; then
  echo "[OZDS] Could not find secrets.json"
  exit 1;
fi;
secrets="$(cat "$SECRETS_PATH")";

printf "\n[OZDS] Setting secrets...\n";
for project in \
    "$ROOT_DIR/test/Ozds.Elasticsearch.Test/Ozds.Elasticsearch.Test.csproj" \
    "$ROOT_DIR/src/Ozds.Web/Ozds.Web.csproj" \
    ; do
  dotnet user-secrets clear --project "$project";
  echo "$secrets" | dotnet user-secrets set --project "$project";
done
