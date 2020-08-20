FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["icxl_idsServer/icxl_idsServer.csproj", "icxl_idsServer/"]
#RUN dotnet restore "icxl_idsServer/icxl_idsServer.csproj"
COPY . .
WORKDIR "/src/icxl_idsServer"
RUN dotnet build "icxl_idsServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "icxl_idsServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "icxl_idsServer.dll"]