#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InvoiceR.WebAPI/InvoiceR.WebAPI.csproj", "InvoiceR.WebAPI/"]
COPY ["InvoiceR.Infrastructure/InvoiceR.Infrastructure.csproj", "InvoiceR.Infrastructure/"]
COPY ["InvoiceR.Application/InvoiceR.Application.csproj", "InvoiceR.Application/"]
COPY ["InvoiceR.Domain/InvoiceR.Domain.csproj", "InvoiceR.Domain/"]
COPY ["InvoiceR.Presentation/InvoiceR.Presentation.csproj", "InvoiceR.Presentation/"]
RUN dotnet restore "./InvoiceR.WebAPI/./InvoiceR.WebAPI.csproj"
COPY . .
WORKDIR "/src/InvoiceR.WebAPI"
RUN dotnet build "./InvoiceR.WebAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InvoiceR.WebAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvoiceR.WebAPI.dll"]