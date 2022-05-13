#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

for project in \
    "$ROOT_DIR/test/Ozds.Elasticsearch.Test/Ozds.Elasticsearch.Test.csproj" \
    "$ROOT_DIR/src/Ozds.Web/Ozds.Web.csproj" \
    ; do
  echo -e "
[OZDS] Secrets for '$project':
";
  dotnet user-secrets list --project "$project";
done
