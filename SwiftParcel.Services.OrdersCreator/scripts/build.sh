#!/bin/bash
export ASPNETCORE_ENVIRONMENT=Development
cd ../src/SwiftParcel.Services.OrdersCreator/SwiftParcel.Services.OrdersCreator
dotnet build -c release