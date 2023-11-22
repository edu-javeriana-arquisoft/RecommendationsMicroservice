FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /Application

COPY ../RecommendationsMicroservice/ ./RecommendationsMicroservice

WORKDIR /Application/RecommendationsMicroservice

RUN dotnet restore
RUN dotnet publish -c Release
RUN dotnet tool install --global dotnet-ef --version 7.0.14

ENV PATH "$PATH:/root/.dotnet/tools"

ARG PORT=8089
EXPOSE $PORT

WORKDIR /Application/RecommendationsMicroservice

ENTRYPOINT ["/bin/bash"]
CMD ["entrypoint.sh"]

