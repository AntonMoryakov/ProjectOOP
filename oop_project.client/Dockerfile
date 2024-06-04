#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM node:20 as build

WORKDIR /app

COPY package.json package.json
COPY package-lock.json package-lock.json

RUN npm install

COPY . .

RUN npm run build

# Используем официальный образ Nginx
FROM nginx:latest
# Удаляем стандартную конфигурацию Nginx
RUN rm /etc/nginx/conf.d/default.conf
# Копируем нашу кастомную конфигурацию nginx
COPY Nginx/conf/nginx_prod.conf /etc/nginx/nginx.conf
# Копируем файлы index.html и статику из папки dist
COPY --from=build /app/dist/ /usr/share/nginx/html
COPY dist/ /usr/share/nginx/html/
COPY https /etc/nginx/
# Опционально, если у вас есть дополнительные настройки для Nginx
# COPY path/to/your/additional/nginx/configs /etc/nginx/conf.d/
# Порт, который будет прослушивать Nginx
EXPOSE 80
# Запускаем Nginx и указываем, что он должен оставаться в фоновом режиме
CMD ["nginx", "-g", "daemon off;"]
