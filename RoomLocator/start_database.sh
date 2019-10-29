#!/usr/bin/env bash

CURR_DIR="$(dirname $0)"
IMAGE="mcr.microsoft.com/mssql/server:2017-latest"
PORT="1433"
PASSWORD="myStrong()Password"
DB_CONTAINER_NAME="db"

source ${CURR_DIR}/ensure_network.sh
export CONNETION_STRING="Server=${DB_CONTAINER_NAME},${PORT};Database=RoomLocator;User Id=SA;Password=${PASSWORD}"

echo "Ensuring database is started..."
[[ $(docker ps -f "name=${DB_CONTAINER_NAME}" --format "{{.Names}}") == ${DB_CONTAINER_NAME} ]] || docker run \
-e 'ACCEPT_EULA=Y' \
-e "SA_PASSWORD=${PASSWORD}" \
--network ${NETWORK_NAME} \
--name ${DB_CONTAINER_NAME} \
-p ${PORT}:${PORT} \
--restart always \
-d ${IMAGE}
