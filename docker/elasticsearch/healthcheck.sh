#!/usr/bin/env sh

curl_result="$(curl -s \
  --cacert config/certs/ca/ca.crt \
  https://localhost:9200)"

if ! echo "$curl_result" | grep -q 'missing authentication credentials'; then
  exit 1
fi

exit 0
