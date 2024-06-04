# Запускаем сборку проекта

try {

# Определите исходный и целевой пути
$sourcePath = "$env:APPDATA\ASP.NET\https"
$destinationPath = ".\https"

# Создайте целевую папку, если она еще не существует
if (-not (Test-Path -Path $destinationPath)) {
    New-Item -ItemType Directory -Path $destinationPath
}

Write-Host "Starting frontend build"

Start-Process npm -ArgumentList "run build" -Wait

# Копируйте содержимое из исходной папки в целевую папку
Copy-Item -Path $sourcePath\* -Destination $destinationPath -Recurse -Force

# Выведите сообщение об успешном копировании
Write-Host "Completed Transfer local development https key to $destinationPath"

Write-Host "Starting docker image build"

# Запускаем docker build
Start-Process docker -ArgumentList "build -t my-nginx-image ." -Wait

Write-Host "Starting docker container"

docker rm -f OOP_PROJECT.Nginx

docker rm -f OOP_PROJECT.Sql

#docker network create OOP_PROJECT

#docker network connect OOP_PROJECT OOP_PROJECT.Server

# Запускаем docker container с ожиданием окончания
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=PaSsWoRd!@3." -p 1433:1433 --name OOP_PROJECT.Sql -d mcr.microsoft.com/mssql/server:2019-latest

docker run -p 80:80 -p 443:443 --name OOP_PROJECT.Nginx -d my-nginx-image

Remove-Item -Path "./https" -Recurse

#Write-Host "Docker container started, wait for terminal close"

#Start-Process docker -ArgumentList "logs -f OOP_PROJECT.Nginx" -Wait

# Запускаем команду остановки docker container
#docker stop OOP_PROJECT.Nginx
#Write-Host "Docker container terminal closed, stoping container"

# Закрываем терминал
exit
}
catch {
    Write-Host "Произошла ошибка: $_"
}
