# ================================
# BUILD NOTIFICATION WORKER
# ================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-notifications
WORKDIR /src

COPY ./Contracts/ ./Contracts/
COPY ./NotificationService.Infrastructure/ ./NotificationService.Infrastructure/
COPY ./NotificationService.Worker/ ./NotificationService.Worker/

WORKDIR /src/NotificationService.Worker
RUN dotnet restore
RUN dotnet build -c Release -o /app/notifications

# ================================
# BUILD USER SERVICE API
# ================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-users
WORKDIR /src

COPY ./UserService.API/ ./UserService.API/
COPY ./UserService.Application/ ./UserService.Application/
COPY ./UserService.Domain/ ./UserService.Domain/
COPY ./UserService.Infrastructure/ ./UserService.Infrastructure/
COPY ./NotificationService.Infrastructure/ ./NotificationService.Infrastructure/
COPY ./NotificationService.Worker/ ./NotificationService.Worker/
COPY ./Contracts/ ./Contracts/
COPY ./UserService.API/UserService.API.sln ./UserService.API/

WORKDIR /src/UserService.API
RUN dotnet restore
RUN dotnet build -c Release -o /app/users

# ================================
# FINAL IMAGE FOR NOTIFICATIONS WORKER
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS notifications-worker
WORKDIR /app
COPY --from=build-notifications /app/notifications ./
ENTRYPOINT ["dotnet", "NotificationService.Worker.dll"]

# ================================
# FINAL IMAGE FOR USER SERVICE API
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS users-service
WORKDIR /app
COPY --from=build-users /app/users ./
ENTRYPOINT ["dotnet", "UserService.API.dll"]