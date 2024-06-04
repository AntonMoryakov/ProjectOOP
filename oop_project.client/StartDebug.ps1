# Запускаем сборку проекта

# Переходим в каталог с nginx
cd Nginx

# Определите исходный и целевой пути
$sourcePath = "$env:APPDATA\ASP.NET\https"
$destinationPath = ".\https"

# Создайте целевую папку, если она еще не существует
if (-not (Test-Path -Path $destinationPath)) {
    New-Item -ItemType Directory -Path $destinationPath
}

# Копируйте содержимое из исходной папки в целевую папку
Copy-Item -Path $sourcePath\* -Destination $destinationPath -Recurse -Force

# Выведите сообщение об успешном копировании
Write-Host "Completed Transfer local development key to $destinationPath"


Write-Host "Starting Nginx"

try {
Get-Process -Name 'nginx' | Stop-Process -Force
} catch {

}

# Запускаем nginx без ожидания окончания
Start-Process ./nginx.exe -ArgumentList "-c .\conf\nginx_debug.conf" -NoNewWindow

Write-Host "Nginx started"

cd ..

Write-Host "Starting Vite server"

# Запускаем npm с ожиданием окончания
Start-Process npm -ArgumentList "run dev" -Wait

Write-Host "Vite server closed, closing Nginx and sql"

Start-Process docker -ArgumentList "stop OOP_PROJECT.Nginx"

# Переходим в каталог с nginx
cd Nginx

# Запускаем команду остановки nginx
Start-Process ./nginx.exe -ArgumentList "-c .\conf\nginx_debug.conf -s stop" -NoNewWindow -Wait

Remove-Item -Path "./https" -Recurse

# Закрываем терминал
exit
