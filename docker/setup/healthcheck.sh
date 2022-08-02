#!/usr/bin/env sh

check_instance_certificate() {
  if [ ! -f "config/certs/$1/$1.crt" ]; then
    printf "Failed creating certificate for instance %s.\n" "$1";
    exit 1;
  fi;
};

check_instance_certificate elasticsearch;
check_instance_certificate kibana;

exit 0;
