#!/bin/bash
export ASPNETCORE_ENVIRONMENT=Development

cd ../

docker build -t swift-parcel-web:latest .

docker tag swift-parcel-web:latest adrianvsaint/swift-parcel-web:latest

docker push adrianvsaint/swift-parcel-web
