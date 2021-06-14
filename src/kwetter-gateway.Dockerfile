#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Gateway/Web.Gateway/Kwetter.Services.Gateway.Web.Gateway.csproj", "Services/Gateway/Web.Gateway/"]
RUN dotnet restore "Services/Gateway/Web.Gateway/Kwetter.Services.Gateway.Web.Gateway.csproj"
COPY . .
WORKDIR "/src/Services/Gateway/Web.Gateway"
RUN dotnet build "Kwetter.Services.Gateway.Web.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kwetter.Services.Gateway.Web.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.Services.Gateway.Web.Gateway.dll"]