FROM microsoft/dotnet:2.0-sdk@sha256:16fbc9aec9cdef978cecf6255f61f9c7dec4ca03d00ba731da6ab5fa1bfdc340 AS build
WORKDIR /build
COPY . /dotnet-sources/.
RUN dotnet publish /dotnet-sources/Promitor.Scraper.csproj --configuration release -o output

FROM microsoft/aspnetcore:2.0@sha256:c0417ce94216da665dac35df9d6e5369ea63fbf493cfb0a0ab49d69b8e6a1cf8 as runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /dotnet-sources/output .

ENTRYPOINT ["dotnet", "Promitor.Scraper.dll"]