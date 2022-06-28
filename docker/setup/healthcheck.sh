#!/usr/bin/env bash

function check_instance_certificate() {
  if [[ ! -f "config/certs/$1/$1.crt" ]]; then
    echo "Failed creating certificate for instance $1.";
    exit 1;
  fi;
};

check_instance_certificate "elasticsearch01";
check_instance_certificate "elasticsearch02";
check_instance_certificate "elasticsearch03";
check_instance_certificate "kibana";

exit 0;
