#!/bin/bash
cd "$(dirname "$0")"

kubectl delete -f asklepios-web.yaml
kubectl delete -f asklepios-api.yaml
kubectl delete -f infrastructure.yaml