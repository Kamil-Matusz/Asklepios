#!/bin/bash
cd "$(dirname "$0")"

# Start minikube
minikube status || minikube start

# Loading loal images from Docker to the Minikube
echo "Loading images into Minikube..."
minikube image load asklepios-api:local
minikube image load asklepios-web:local

# Apply secrets
echo "Applying secrets..."
kubectl apply -f secrets.yaml

# Apply persistent storage for Postgres (kept out of stop_services.sh so data survives restarts)
kubectl apply -f postgres-pvc.yaml

# Start infrastructure
echo "Starting the infrastructure (Postgres, Redis, Seq)..."
kubectl apply -f infrastructure.yaml

echo "I'm waiting for the infrastructure to be launched..."
kubectl rollout status deployment/postgres --timeout=120s
kubectl rollout status deployment/redis --timeout=120s
kubectl rollout status deployment/seq --timeout=120s

# Running app
echo "Launching the Asklepios app..."
kubectl apply -f asklepios-api.yaml
kubectl apply -f asklepios-web.yaml

echo "All deployed! Run the command below to open the frontend in your browser:"
echo "minikube service asklepios-web"