
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build


WORKDIR /app


COPY *.csproj ./
RUN dotnet restore


COPY . ./


RUN dotnet publish -c Release -o /out


FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime


WORKDIR /app


COPY --from=build /out .

COPY data/questions.json ./data/questions.json


ENTRYPOINT ["dotnet", "QuizApp.dll"]
