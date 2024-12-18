#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Asklepios/Asklepios.Api/Asklepios.Api.csproj", "Asklepios/Asklepios.Api/"]
COPY ["Asklepios/Asklepios.Application/Asklepios.Application.csproj", "Asklepios/Asklepios.Application/"]
COPY ["Asklepios/Asklepios.Core/Asklepios.Core.csproj", "Asklepios/Asklepios.Core/"]
COPY ["Asklepios/Asklepios.Infrastructure/Asklepios.Infrastructure.csproj", "Asklepios/Asklepios.Infrastructure/"]
RUN dotnet restore "Asklepios/Asklepios.Api/Asklepios.Api.csproj"
COPY . .
WORKDIR "/src/Asklepios/Asklepios.Api"
RUN dotnet build "Asklepios.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Asklepios.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Asklepios.Api.dll"]