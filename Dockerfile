# Use the ASP.NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["SCM1.csproj", "./"]
RUN dotnet restore "./SCM1.csproj"

# Copy the rest of the application
COPY . .
WORKDIR "/src/."

# Build the application
RUN dotnet build "SCM1.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "SCM1.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCM1.dll"]
