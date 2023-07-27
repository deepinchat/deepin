#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG NODE_IMAGE=node:18.16.0
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM ${NODE_IMAGE} as node-build
WORKDIR /client 
COPY Services/Identity/DeepIn.Identity.Server.Web/ClientApp ./
RUN npm install && npm run build

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Identity/DeepIn.Identity.Server.Web/DeepIn.Identity.Server.Web.csproj", "Services/Identity/DeepIn.Identity.Server.Web/"]
RUN dotnet restore "Services/Identity/DeepIn.Identity.Server.Web/DeepIn.Identity.Server.Web.csproj"
COPY . .
WORKDIR "/src/Services/Identity/DeepIn.Identity.Server.Web"
RUN dotnet build "DeepIn.Identity.Server.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeepIn.Identity.Server.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node-build /client/dist wwwroot
ENTRYPOINT ["dotnet", "DeepIn.Identity.Server.Web.dll"]