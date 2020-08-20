FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["icxl_api_oms/icxl_api_oms.csproj", "icxl_api_oms/"]
#RUN dotnet restore "icxl_api_oms/icxl_api_oms.csproj"
COPY . .
WORKDIR "/src/icxl_api_oms"
RUN dotnet build "icxl_api_oms.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "icxl_api_oms.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "icxl_api_oms.dll"]