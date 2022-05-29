FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY sisVentas_producto_ms.sln ./
COPY SysVentas.Product.Domain/*.csproj ./SysVentas.Product.Domain/
COPY SysVentas.Product.WebApi/*.csproj ./SysVentas.Product.WebApi/
COPY SysVentas.Product.Application/*.csproj ./SysVentas.Product.Application/
COPY SysVentas.Product.Infrastructure.Data/*.csproj ./SysVentas.Product.Infrastructure.Data/

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SysVentas.Product.WebApi.dll"]