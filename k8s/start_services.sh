#!/bin/bash
cd "$(dirname "$0")"

# Start minikube
minikube status || minikube start

# Loading loal images from Docker to the Minikube
echo "Loading images into Minikube..."
minikube image load asklepios-api:local
minikube image load asklepios-web:local

# Start infrastructure
echo "Starting the infrastructure (Postgres, Redis, Seq)..."
kubectl apply -f infrastructure.yaml

echo "I'm waiting for the infrastructure to be launched..."
sleep 10

# Running app
echo "Launching the Asklepios app..."
kubectl apply -f asklepios-api.yaml
kubectl apply -f asklepios-web.yaml

echo "All deployed! Run the command below to open the frontend in your browser:"
echo "minikube service asklepios-web"