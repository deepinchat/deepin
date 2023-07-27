#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Chatting/DeepIn.Chatting.API/DeepIn.Chatting.API.csproj", "Services/Chatting/DeepIn.Chatting.API/"]
COPY ["BuildingBlocks/Common/DeepIn.Service.Common/DeepIn.Service.Common.csproj", "BuildingBlocks/Common/DeepIn.Service.Common/"]
COPY ["BuildingBlocks/Caching/DeepIn.Caching.Redis/DeepIn.Caching.Redis.csproj", "BuildingBlocks/Caching/DeepIn.Caching.Redis/"]
COPY ["BuildingBlocks/Caching/DeepIn.Caching/DeepIn.Caching.csproj", "BuildingBlocks/Caching/DeepIn.Caching/"]
COPY ["BuildingBlocks/EventBus/DeepIn.EventBus.RabbitMQ/DeepIn.EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus/DeepIn.EventBus.RabbitMQ/"]
COPY ["BuildingBlocks/EventBus/DeepIn.EventBus/DeepIn.EventBus.csproj", "BuildingBlocks/EventBus/DeepIn.EventBus/"]
COPY ["Services/Chatting/DeepIn.Chatting.Application/DeepIn.Chatting.Application.csproj", "Services/Chatting/DeepIn.Chatting.Application/"]
COPY ["BuildingBlocks/DDD/DeepIn.Application/DeepIn.Application.csproj", "BuildingBlocks/DDD/DeepIn.Application/"]
COPY ["BuildingBlocks/DDD/DeepIn.Infrastructure/DeepIn.Infrastructure.csproj", "BuildingBlocks/DDD/DeepIn.Infrastructure/"]
COPY ["BuildingBlocks/DDD/DeepIn.Domain/DeepIn.Domain.csproj", "BuildingBlocks/DDD/DeepIn.Domain/"]
COPY ["Services/Chatting/DeepIn.Chatting.Infrastructure/DeepIn.Chatting.Infrastructure.csproj", "Services/Chatting/DeepIn.Chatting.Infrastructure/"]
COPY ["Services/Chatting/DeepIn.Chatting.Domain/DeepIn.Chatting.Domain.csproj", "Services/Chatting/DeepIn.Chatting.Domain/"]
RUN dotnet restore "Services/Chatting/DeepIn.Chatting.API/DeepIn.Chatting.API.csproj"
COPY . .
WORKDIR "/src/Services/Chatting/DeepIn.Chatting.API"
RUN dotnet build "DeepIn.Chatting.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeepIn.Chatting.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeepIn.Chatting.API.dll"]