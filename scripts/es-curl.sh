#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

if [ ! -f "$ROOT_DIR/certs/ca/ca.crt" ]; then
  printf "Make sure that a CA exists at 'certs/ca/ca.crt'";
  exit 1;
fi;

if [ ! -f "$ROOT_DIR/.env" ]; then
  printf "Please update your '.env' file";
  exit 1;
fi;
. "$ROOT_DIR/.env";

curl -s -X "$1" \
  --cacert certs/ca/ca.crt \
  -u "elastic:${ELASTIC_PASSWORD}" \
  -H "Accept: application/json" \
  -H "Content-Type: application/json" \
  "https://localhost:9200/$2" \
  -d "$3"
