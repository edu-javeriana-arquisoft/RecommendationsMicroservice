FROM node:16-alpine
WORKDIR /app

COPY . .

ENV KAFKA_SERVER localhost:9094
ENV KAFKA_TOPIC interactions
ENV USE_PORT 9093

RUN npm install
ENTRYPOINT [ "node" ]
CMD ["index.js"]