#!/usr/bin/env bash

curl_result=$(curl -s \
  --cacert config/certs/ca/ca.crt \
  https://localhost:9200);

if [[ ! $(echo "$curl_result" | \
    grep 'missing authentication credentials') ]]; then
  echo "Credentials missing.";
  exit 1;
fi;

exit 0;
