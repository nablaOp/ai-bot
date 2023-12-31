FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AiBot/AiBot.csproj", "AiBot/"]
RUN dotnet restore "AiBot/AiBot.csproj"
COPY . .
WORKDIR "/src/AiBot"
RUN dotnet build "" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AiBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AiBot.dll"]
