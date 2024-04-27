#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ReconcileService/ReconcileService.csproj", "ReconcileService/"]
COPY ["Reconcile.Entity/Reconcile.Entity.csproj", "Reconcile.Entity/"]
COPY ["Reconcile.Repository/Reconcile.Repository.csproj", "Reconcile.Repository/"]
COPY ["Reconcile.BusinessLogic/Reconcile.BusinessLogic.csproj", "Reconcile.BusinessLogic/"]
RUN dotnet restore "ReconcileService/ReconcileService.csproj"
COPY . .
WORKDIR "/src/ReconcileService"
RUN dotnet build "ReconcileService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReconcileService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReconcileService.dll"]
