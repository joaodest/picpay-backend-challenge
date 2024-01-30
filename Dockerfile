FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /App/out .

RUN apt-get update && apt-get install -y wait-for-it

CMD ["wait-for-it", "db:3306", "--", "dotnet", "PicpayChallenge.dll", "--urls", "http://*:8080"]

ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080