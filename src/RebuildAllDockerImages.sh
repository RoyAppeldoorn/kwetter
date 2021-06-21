#!/bin/bash
# Rebuild all the services that have changes
docker build -f kwetter-timeline-service.Dockerfile -t timeline-service .
docker build -f kwetter-user-service.Dockerfile -t user-service .
docker build -f kwetter-kweet-service.Dockerfile -t kweet-service .
docker build -f kwetter-authorization-service.Dockerfile -t authorization-service .
docker build -f kwetter-follow-service.Dockerfile -t follow-service .
docker build -f kwetter-gateway.Dockerfile -t gateway-service .