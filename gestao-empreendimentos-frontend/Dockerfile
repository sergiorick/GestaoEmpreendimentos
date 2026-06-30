# Etapa 1: Build do Angular
FROM node:20-alpine AS build
WORKDIR /app

# Copia package.json e instala dependências
COPY package*.json ./
RUN npm ci

# Copia o restante do código e faz o build de produção
COPY . .
RUN npm run build

# Etapa 2: Servir com Nginx
FROM nginx:alpine AS final
COPY --from=build /app/dist/gestao-empreendimentos-frontend/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80