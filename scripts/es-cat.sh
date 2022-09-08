#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

if [ ! -f "$ROOT_DIR/docker/certs/ca/ca.crt" ]; then
  printf "Make sure that a CA exists at 'docker/certs/ca/ca.crt'";
  exit 1;
fi;

if [ ! -f "$ROOT_DIR/.env" ]; then
  printf "Please update your '.env' file";
  exit 1;
fi;
. "$ROOT_DIR/.env";

curl -s \
  --cacert docker/certs/ca/ca.crt \
  -u "elastic:${ELASTIC_PASSWORD}" \
  "https://localhost:9200/_cat/$1"
