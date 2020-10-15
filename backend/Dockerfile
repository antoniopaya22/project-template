############################################################
# Dockerfile Node.js - Express
############################################################

FROM node:lts-alpine


# Crear directorio de trabajo
RUN mkdir -p /src

WORKDIR /src

RUN npm install -g typescript ts-node

# Copia la Aplicación
COPY . .

RUN npm install --quiet

EXPOSE 5000

# Inicia la aplicación al iniciar al contenedor
CMD npm run prod