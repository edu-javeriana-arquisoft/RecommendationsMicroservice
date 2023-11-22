import express from 'express';
import { Server } from 'socket.io';
import { createServer } from 'node:http';
import { Kafka } from 'kafkajs';

const app = express();
const server = createServer(app);
const io = new Server(server);
const kafka = new Kafka({
	clientId: 'kafka-ws',
	brokers: [process.env.KAFKA_SERVER]
});

const producer = kafka.producer();

await producer.connect();
console.log("Connection stablished with broker");

io.on('connection', (socket) => {
	console.log("New connection detected");
	socket.on("disconnect", () => {
		console.log("User left the connection");
	});

	socket.on("interaction", async (e) => {
		console.log("Interaction request arrived");
		console.log(e);

		producer.send({
			topic: process.env.KAFKA_TOPIC,
			messages: [
				{ value: `${e.user}:${e.category}` },
			]
		});
	});
})

app.get('/', (req, res) => {
	res.json({
		info: "This is a Kafka websocket implementation",
		server: process.env.KAFKA_SERVER,
		topic: process.env.KAFKA_TOPIC
	});
});

server.listen(process.env.USE_PORT, () => {
	console.log(`Server is listening at http://localhost:${process.env.USE_PORT}`);
});