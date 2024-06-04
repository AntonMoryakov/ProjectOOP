#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["oop_project.client/nuget.config", "oop_project.client/"]
COPY ["OOP_PROJECT.Server/OOP_PROJECT.Server.csproj", "OOP_PROJECT.Server/"]
ENV ASPNETCORE_ENVIRONMENT=$BUILD_CONFIGURATION
#COPY ["oop_project.client/oop_project.client.esproj", "oop_project.client/"]
#ENV NODE_VERSION=20.13.0
#RUN apt install -y curl
#RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.7/install.sh | bash
#ENV NVM_DIR=/root/.nvm
#RUN . "$NVM_DIR/nvm.sh" && nvm install ${NODE_VERSION}
#RUN . "$NVM_DIR/nvm.sh" && nvm use v${NODE_VERSION}
#RUN . "$NVM_DIR/nvm.sh" && nvm alias default v${NODE_VERSION}
#ENV PATH="/root/.nvm/versions/node/v${NODE_VERSION}/bin/:${PATH}"
#RUN node --version
#RUN npm --version
#RUN dotnet tool install --global dotnet-ef
#ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet restore "./OOP_PROJECT.Server/OOP_PROJECT.Server.csproj"
COPY . .
WORKDIR "/src/OOP_PROJECT.Server"
RUN dotnet build "./OOP_PROJECT.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OOP_PROJECT.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OOP_PROJECT.Server.dll"]