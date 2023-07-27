#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateways/Bff/DeepIn.WebSPA.HttpAggregator/DeepIn.WebSPA.HttpAggregator.csproj", "ApiGateways/Bff/DeepIn.WebSPA.HttpAggregator/"]
RUN dotnet restore "ApiGateways/Bff/DeepIn.WebSPA.HttpAggregator/DeepIn.WebSPA.HttpAggregator.csproj"
COPY . .
WORKDIR "/src/ApiGateways/Bff/DeepIn.WebSPA.HttpAggregator"
RUN dotnet build "DeepIn.WebSPA.HttpAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeepIn.WebSPA.HttpAggregator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeepIn.WebSPA.HttpAggregator.dll"]