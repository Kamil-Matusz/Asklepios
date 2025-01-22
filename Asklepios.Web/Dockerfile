FROM node:18-alpine AS build

WORKDIR /app

# Kopiowanie zależności
COPY ./Asklepios.Web/package.json ./Asklepios.Web/package-lock.json ./

# Instalacja zależności
RUN npm install

# Kopiowanie całego projektu
COPY ./Asklepios.Web .

# Sprawdzenie typów (opcjonalnie ignoruje błędy)
RUN npm run build:types || echo "Ignoring TypeScript errors"

# Budowanie aplikacji
RUN npm run build:prod

FROM nginx:stable-alpine

# Usuwanie domyślnej konfiguracji Nginx
RUN rm /etc/nginx/conf.d/default.conf

# Kopiowanie konfiguracji Nginx
COPY ./Asklepios.Web/nginx.conf /etc/nginx/conf.d/

# Kopiowanie plików zbudowanej aplikacji
COPY --from=build /app/dist /usr/share/nginx/html

# Eksponowanie portu
EXPOSE 80

# Uruchomienie Nginx
CMD ["nginx", "-g", "daemon off;"]