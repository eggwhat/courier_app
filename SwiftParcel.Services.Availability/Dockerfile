# Use the .NET 6.0 SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish src/SwiftParcel.Services.Availability.Api/SwiftParcel.Services.Availability.Api  -c Release -o out

# Use the .NET 6.0 ASP.NET Core runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_ENVIRONMENT=docker
ENTRYPOINT ["dotnet", "SwiftParcel.Services.Availability.Api.dll"]
