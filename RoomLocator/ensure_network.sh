#!/usr/bin/env bash

export NETWORK_NAME="RoomLocator"

echo "Ensuring docker network..."
docker network inspect ${NETWORK_NAME} &>/dev/null || docker network create ${NETWORK_NAME}
