# Запускаем сборку проекта
try {
docker rm -f OOP_PROJECT.Sql
} catch {

}
Start-Process "docker" -ArgumentList 'run', '-e', '""ACCEPT_EULA=Y""', '-e', '""SA_PASSWORD=PaSsWoRd!@3.""', '-p', '1433:1433', '--name', 'OOP_PROJECT.Sql', '-d', 'mcr.microsoft.com/mssql/server:2019-latest' -NoNewWindow -Wait

Start-Process powershell -ArgumentList " -File ./StartDebug.ps1"

Start-sleep -Seconds 5