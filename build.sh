#!/usr/bin/env bash

RED='\033[0;31m'
NC='\033[0m' # No Color

function build {
    echo "building..."
    docker build -t basket-app .
    EXIT_CODE=$?
}

build

if [ $EXIT_CODE -ne 0 ]; then
    echo -e "${RED}Some tests failed!${NC}"
    exit $EXIT_CODE
fi
