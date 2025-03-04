#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Macaner.Ecomerce.Services.WebApi/Macaner.Ecomerce.Services.WebApi.csproj", "Macaner.Ecomerce.Services.WebApi/"]
COPY ["Macaner.Ecomerce.Application.DTO/Macaner.Ecomerce.Application.DTO.csproj", "Macaner.Ecomerce.Application.DTO/"]
COPY ["Macaner.Ecomerce.Application.Interface/Macaner.Ecomerce.Application.Interface.csproj", "Macaner.Ecomerce.Application.Interface/"]
COPY ["Macaner.Ecomerce.Transversal.Common/Macaner.Ecomerce.Transversal.Common.csproj", "Macaner.Ecomerce.Transversal.Common/"]
COPY ["Macaner.Ecomerce.Application.Main/Macaner.Ecomerce.Application.Main.csproj", "Macaner.Ecomerce.Application.Main/"]
COPY ["Macaner.Ecomerce.Domain.Entity/Macaner.Ecomerce.Domain.Entity.csproj", "Macaner.Ecomerce.Domain.Entity/"]
COPY ["Macaner.Ecomerce.Domain.Interface/Macaner.Ecomerce.Domain.Interface.csproj", "Macaner.Ecomerce.Domain.Interface/"]
COPY ["Macaner.Ecomerce.Domain.Core/Macaner.Ecomerce.Domain.Core.csproj", "Macaner.Ecomerce.Domain.Core/"]
COPY ["Macaner.Ecomerce.Infrastructure.Interface/Macaner.Ecomerce.Infrastructure.Interface.csproj", "Macaner.Ecomerce.Infrastructure.Interface/"]
COPY ["Macaner.Ecomerce.Infrastructure.Data/Macaner.Ecomerce.Infrastructure.Data.csproj", "Macaner.Ecomerce.Infrastructure.Data/"]
COPY ["Macaner.Ecomerce.Infrastructure.Repository/Macaner.Ecomerce.Infrastructure.Repository.csproj", "Macaner.Ecomerce.Infrastructure.Repository/"]
COPY ["Macaner.Ecomerce.Transversal.Logging/Macaner.Ecomerce.Transversal.Logging.csproj", "Macaner.Ecomerce.Transversal.Logging/"]
COPY ["Macaner.Ecomerce.Transversal.Mapper/Macaner.Ecomerce.Transversal.Mapper.csproj", "Macaner.Ecomerce.Transversal.Mapper/"]
RUN dotnet restore "Macaner.Ecomerce.Services.WebApi/Macaner.Ecomerce.Services.WebApi.csproj"
COPY . .
WORKDIR "/src/Macaner.Ecomerce.Services.WebApi"
RUN dotnet build "Macaner.Ecomerce.Services.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Macaner.Ecomerce.Services.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Macaner.Ecomerce.Services.WebApi.dll"]