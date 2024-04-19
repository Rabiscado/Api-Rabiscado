
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./Rabiscado.Api/Rabiscado.Api.csproj ./Rabiscado.Api/Rabiscado.Api.csproj
COPY ./Rabiscado.Application/Rabiscado.Application.csproj ./Rabiscado.Application/Rabiscado.Application.csproj
COPY ./Rabiscado.Core/Rabiscado.Core.csproj ./Rabiscado.Core/Rabiscado.Core.csproj
COPY ./Rabiscado.Domain/Rabiscado.Domain.csproj ./Rabiscado.Domain/Rabiscado.Domain.csproj
COPY ./Rabiscado.Infra/Rabiscado.Infra.csproj ./Rabiscado.Infra/Rabiscado.Infra.csproj
RUN dotnet restore ./Rabiscado.Api/Rabiscado.Api.csproj
COPY ./Rabiscado.Api ./Rabiscado.Api
COPY ./Rabiscado.Application ./Rabiscado.Application
COPY ./Rabiscado.Core ./Rabiscado.Core
COPY ./Rabiscado.Domain ./Rabiscado.Domain
COPY ./Rabiscado.Infra ./Rabiscado.Infra
RUN dotnet build ./Rabiscado.Api/Rabiscado.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
RUN dotnet publish ./Rabiscado.Api/Rabiscado.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rabiscado.Api.dll"]
