# Kubernetes Setup for Asklepios

This directory contains the Kubernetes manifests and bash scripts necessary to run the Asklepios application locally using `minikube`.

## Prerequisites

Before you begin, ensure you have the following installed on your machine:
* [Docker](https://docs.docker.com/get-docker/)
* [Minikube](https://minikube.sigs.k8s.io/docs/start/)
* [kubectl](https://kubernetes.io/docs/tasks/tools/)

## Directory Structure

| File | Description |
|------|-------------|
| `secrets.yaml` | Contains base64 encoded passwords and connection strings. |
| `postgres-pvc.yaml` | PersistentVolumeClaim for PostgreSQL data. Not deleted by `stop_services.sh`, so database data survives restarts. |
| `infrastructure.yaml` | Deployments and Services for databases and logging (PostgreSQL, Redis, Seq). |
| `asklepios-api.yaml` | Deployment and Service for the .NET Backend API. |
| `asklepios-web.yaml` | Deployment and Service (`NodePort`) for the Vue/Nginx Frontend. |
| `build-images.sh` | Script to build Docker images locally. |
| `start_services.sh` | Script to load images into Minikube and apply all manifests in the correct order. |
| `stop_services.sh` | Script to delete all deployed resources from the cluster. |

## Quick Start

### 1. Build the images
First, build the Docker images for the API and the Web frontend on your local machine. From the `k8s` directory, run:
```bash
sh build-images.sh
```

### 2. Start the services
Run the startup script. It will automatically start Minikube (if it's not already running), load your locally built images directly into the Minikube environment, and deploy all necessary .yaml manifests in the correct order (Secrets -> Infrastructure -> Apps).
```bash
sh start_services.sh
```

### 3. Stop the services
Run the stop script.
```bash
sh stop_services.sh
```