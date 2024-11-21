# Use the ASP.NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the .csproj file and restore any dependencies (via dotnet restore)
COPY SCM1.csproj ./
RUN dotnet restore "./SCM1.csproj"

# Copy the rest of the application and build it
COPY . ./
WORKDIR /src
RUN dotnet build "SCM1.csproj" -c Release -o /app/build

# Publish the application for release
FROM build AS publish
RUN dotnet publish "SCM1.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: copy the published app to the base image and define entrypoint
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "SCM1.dll"]
