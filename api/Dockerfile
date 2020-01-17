FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine

WORKDIR /app

# Copy csproj file and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./

RUN dotnet publish -c Debug -o build

EXPOSE 5001
EXPOSE 5000

CMD ["dotnet", "build/dashboard.dll"]
