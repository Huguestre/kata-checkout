FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["checkout/checkout.csproj", "checkout/"]
RUN dotnet restore "checkout/checkout.csproj"
COPY . .
WORKDIR "/src/checkout"
RUN dotnet build "checkout.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "checkout.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "checkout.dll"]