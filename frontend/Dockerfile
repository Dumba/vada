FROM node:14.17.0 AS build-env
WORKDIR /usr/src/app
COPY ./package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx
COPY --from=build-env /usr/src/app/dist /www/data
