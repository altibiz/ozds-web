#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";

glob() {
  find . -type f -wholename "$1";
}

wd="$(pwd)";
cd "$ROOT" || exit;
for secret in \
    "secrets.sh" \
    "secrets.json" \
    $(glob "./src/Ozds.Modules.Ozds/Migrations/Sensitive/*") \
    ; do
  secret="$(realpath "$secret")";
  out="$ROOT/secrets/$(realpath --relative-to="$ROOT" "$secret")";
  dir="$(dirname "$out")";
  printf "[DKOM] Extracting %s into %s\n" "$secret" "$out";
  mkdir --parents "$dir";
  cp "$secret" "$out";
done;
cd "$wd" || exit;
