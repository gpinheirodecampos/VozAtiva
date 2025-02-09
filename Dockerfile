# Step 1: Build Stage (using SDK image)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source

# Copy only the .csproj files to restore dependencies first
COPY . .

# Restore dependencies
RUN dotnet restore "./VozAtiva.API/VozAtiva.API.csproj"
RUN dotnet publish "./VozAtiva.API/VozAtiva.API.csproj" -c Release -o /app --no-restore

# Step 2: Runtime Stage (using ASP.NET runtime image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

#ENV ASPNETCORE_HTTP_PORT=https://+:5001
ENV ASPNETCORE_URLS=http://+:5000;

WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

# Set the entry point to run the published application
ENTRYPOINT ["dotnet", "VozAtiva.API.dll"]

#To run container we use the command  docker run -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 hanyerkek/vozativa