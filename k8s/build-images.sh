#!/bin/bash
set -e
cd "$(dirname "$0")/.."

echo "Building the API image..."
docker build -t asklepios-api:local -f Asklepios/Asklepios.Api/Dockerfile .

echo "Building the Web image..."
docker build -t asklepios-web:local -f Asklepios.Web/Dockerfile .

echo "Images built successfully!"