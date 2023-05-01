FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["challenge.csproj", "."]
RUN dotnet restore "./challenge.csproj"


RUN apt-get update && \
    apt-get install -y wget && \
    wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y dotnet-sdk-7.0

RUN dotnet add package HotChocolate.AspNetCore --version 13.0.5
RUN dotnet add package Microsoft.AspNetCore.OpenApi --version 7.0.5
RUN dotnet add package Microsoft.EntityFrameworkCore --version 7.0.5
RUN dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.5
RUN dotnet add package Microsoft.VisualStudio.Azure.Containers.Tools.Targets --version 1.17.2
RUN dotnet add package MySql.Data --version 8.0.33
RUN dotnet add package MySql.Data.EntityFramework --version 8.0.33
RUN dotnet add package MySql.EntityFrameworkCore --version 7.0.2
RUN dotnet add package Swashbuckle.AspNetCore --version 6.4.0

COPY . .
WORKDIR "/src/."
RUN dotnet build "challenge.csproj" -c Release -o /app/build

# Install EF Core tools
RUN dotnet tool install --global dotnet-ef --version 7.0.0

# Run EF Core migrations
RUN dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.0
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet --version 2.0.3


FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
COPY --from=build /app/build .
COPY wait-for.sh /app/wait-for.sh
RUN chmod +x /app/wait-for.sh

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "challenge.dll"]
