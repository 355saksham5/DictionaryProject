FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DictionaryApp/DictionaryApp.csproj", "DictionaryApp/"]
COPY ["DictionaryApi/DictionaryApi.csproj", "DictionaryApi/"]
RUN dotnet restore "DictionaryApp/DictionaryApp.csproj"
COPY . .

WORKDIR "/src/DictionaryApp"
RUN dotnet build "DictionaryApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DictionaryApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DictionaryApp.dll"]