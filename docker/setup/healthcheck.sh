#!/usr/bin/env sh

check_instance_certificate() {
  if [ ! -f "config/certs/$1/$1.crt" ]; then
    echo "Failed creating certificate for instance $1.";
    exit 1;
  fi;
};

check_instance_certificate "elasticsearch";
check_instance_certificate "kibana";

exit 0;
