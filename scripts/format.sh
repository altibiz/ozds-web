#!/usr/bin/env bash

export NODE_OPTIONS="--no-warnings";

echo -e "
[OZDS] Formatting with 'yarn'...
";
yarn format;

echo -e "
[OZDS] Formatting with 'dotnet'...
";
dotnet format whitespace;
