FROM node:20-alpine

WORKDIR /app

COPY package.json yarn.lock ./
RUN yarn install

COPY . .

RUN yarn build

EXPOSE ${VITE_PREVIEW_PORT}
CMD [ "yarn", "preview" ]