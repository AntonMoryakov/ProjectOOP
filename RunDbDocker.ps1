docker rm -f OOP_PROJECT.Sql

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=PaSsWoRd!@3." -p 1433:1433 --name OOP_PROJECT.Sql -d mcr.microsoft.com/mssql/server:2019-latest

Start-Process docker -ArgumentList "logs -f OOP_PROJECT.Sql" -Wait

docker stop OOP_PROJECT.Sql