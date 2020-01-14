FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /build
COPY app/back.csproj .
RUN dotnet restore "back.csproj"
COPY app/. .
RUN dotnet build "back.csproj" -c Release -o /app



FROM build AS publish

RUN dotnet publish "back.csproj" -c Release -o /app



FROM mcr.microsoft.com/dotnet/core/runtime:3.1 as final

EXPOSE 8080
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT [ "dotnet", "back.dll" ]
