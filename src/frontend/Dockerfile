FROM node:14.15.4 as node

WORKDIR /app

COPY . .

WORKDIR /app/src/frontend

RUN npm i -g @angular/cli

RUN npm ci

RUN ng build --configuration=production

FROM nginx:alpine
COPY --from=node /app/src/frontend/dist/ingoodhands /usr/share/nginx/html
