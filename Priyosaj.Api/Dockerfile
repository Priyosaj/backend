FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Priyosaj.Api/Priyosaj.Api.csproj", "Priyosaj.Api/"]
COPY ["Priyosaj.Business/Priyosaj.Business.csproj", "Priyosaj.Business/"]
COPY ["Priyosaj.Contacts/Priyosaj.Contacts.csproj", "Priyosaj.Contacts/"]
RUN dotnet restore "Priyosaj.Api/Priyosaj.Api.csproj"
COPY . .
WORKDIR "/src/Priyosaj.Api"
RUN dotnet build "Priyosaj.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Priyosaj.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Priyosaj.Api.dll"]
