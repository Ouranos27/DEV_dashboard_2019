FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src    
COPY ["dashboard/dashboard.csproj", "dashboard/"]
RUN dotnet restore "dashboard/dashboard.csproj"
COPY . .
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm
WORKDIR "/src/dashboard/"
RUN dotnet build "dashboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dashboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","dashboard.dll"]
