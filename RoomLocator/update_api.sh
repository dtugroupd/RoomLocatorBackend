#!/usr/bin/env bash

CURR_DIR="$(dirname $0)"
API_IMAGE="dtugroupd/room-locator:latest"
OUT_PORT="5000"
API_CONTAINER_NAME="api"

source ${CURR_DIR}/start_database.sh

start_api() {
    echo stuffs
    docker run \
    -e "ConnectionStrings__RoomLocator=${CONNETION_STRING}" \
    --network ${NETWORK_NAME} \
    -p ${OUT_PORT}:80 \
    --name ${API_CONTAINER_NAME} \
    --restart always \
    -d ${API_IMAGE}
}

echo -e "\n==== Stopping API ===="
[ "$(docker ps | grep ${API_CONTAINER_NAME})" ] && docker stop ${API_CONTAINER_NAME}
echo -e "==== Removing API ===="
[ "$(docker ps -a | grep ${API_CONTAINER_NAME})" ] && docker rm ${API_CONTAINER_NAME}

echo -e "\n==== Pulling latest API ===="
docker pull ${API_IMAGE}

echo -e "\n==== Starting new API ===="
start_api

until curl -s localhost:5000 | grep -q "This pod is healthy"; do
    echo "Restarting API..."
    sleep 2
    docker stop ${API_CONTAINER_NAME} &>/dev/null
    docker rm ${API_CONTAINER_NAME} &>/dev/null
    start_api &>/dev/null
done

echo;
docker ps

echo;
echo;
echo "Done! If the database was just started, you might need to "
