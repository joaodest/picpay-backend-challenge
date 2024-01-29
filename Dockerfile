FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet public -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /App
COPY --from=build /App/out .

ENV ASPNETCORE_ENVIRONTMENT=Development

EXPOSE 8080
ENTRYPOINT ["dotnet", "App.dll", "PicpayChallenge.dll", "--urls", "http://*:8080"]