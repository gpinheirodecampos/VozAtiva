FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

ENV DOTNET_CLI_HOME=/tmp
ENV PATH /usr/local/bin:$PATH

WORKDIR /app
COPY VozAtiva.API/VozAtiva.API.csproj VozAtiva.API/
COPY VozAtiva.Application/VozAtiva.Application.csproj VozAtiva.Application/
COPY VozAtiva.CrossCutting/VozAtiva.CrossCutting.csproj VozAtiva.CrossCutting/

RUN dotnet restore VozAtiva.API/VozAtiva.API.csproj

COPY . .

FROM base AS build

RUN dotnet publish VozAtiva.API/VozAtiva.API.csproj -c Release -o /app/publish

# Etapas relacionadas a testes e cobertura comentadas
# FROM base AS build_coverages
# 
# RUN apt-get update -qq && apt-get install -qq --no-install-recommends -y \
#     make \
#     && dotnet tool install --global coverlet.console \
#     && dotnet tool install --global dotnet-reportgenerator-globaltool \
#     && dotnet tool install --global dotnet-format
# 
# RUN dotnet restore VozAtiva.Tests/VozAtiva.Tests.csproj
# RUN dotnet build VozAtiva.Tests/VozAtiva.Tests.csproj
# RUN dotnet test VozAtiva.Tests/VozAtiva.Tests.csproj --collect:"Code Coverage" --no-build
# RUN reportgenerator "-reports:/app/VozAtiva.Tests/TestResults/*.xml" "-targetdir:/app/coverage" -reporttypes:Html
# RUN dotnet format VozAtiva.API/VozAtiva.API.csproj --check

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "VozAtiva.API.dll"]