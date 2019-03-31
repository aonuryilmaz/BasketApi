FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY . ./
RUN dotnet restore

WORKDIR /app/Basket.Api
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime as runtime
WORKDIR /app
COPY --from=build /app/Basket.Api/out ./
ENTRYPOINT ["dotnet", "Basket.Api.dll"]