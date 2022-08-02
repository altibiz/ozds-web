#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";
SECRETS_PATH="$ROOT/secrets.json";

if [ ! -f "$SECRETS_PATH" ]; then
  echo "[OZDS] Could not find secrets.json"
  exit 1;
fi;
secrets="$(cat "$SECRETS_PATH")";

printf "[OZDS] Setting secrets...\n";
for project in \
    "$ROOT/test/Ozds.Elasticsearch.Test/Ozds.Elasticsearch.Test.csproj" \
    "$ROOT/src/Ozds.Web/Ozds.Web.csproj" \
    ; do
  dotnet user-secrets clear --project "$project";
  echo "$secrets" | dotnet user-secrets set --project "$project";
done
