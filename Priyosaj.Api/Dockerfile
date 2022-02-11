FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Wateron.Api/Wateron.Api.csproj", "Wateron.Api/"]
COPY ["Wateron.Business/Wateron.Business.csproj", "Wateron.Business/"]
COPY ["Wateron.Contacts/Wateron.Contacts.csproj", "Wateron.Contacts/"]
RUN dotnet restore "Wateron.Api/Wateron.Api.csproj"
COPY . .
WORKDIR "/src/Wateron.Api"
RUN dotnet build "Wateron.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wateron.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wateron.Api.dll"]
