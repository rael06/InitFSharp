FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InitFSharp/InitFSharp.fsproj", "InitFSharp/"]
RUN dotnet restore "InitFSharp/InitFSharp.fsproj"
COPY . .
WORKDIR "/src/InitFSharp"
RUN dotnet build "InitFSharp.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InitFSharp.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InitFSharp.dll"]
