import * as dgram from "dgram";
import { MessageSwitcher } from "./src/MessageSwitcher";

const PORT = 33333;

const server = dgram.createSocket("udp4");
var switcher = new MessageSwitcher(server);

server.on("error", (err) => {
  console.log(`server error:\n${err.stack}`);
  server.close();
});

server.on("message", (msg: string, rinfo) => {
  switcher.switchMessage(msg, rinfo.port, rinfo.address);
});

server.on("listening", () => {
  const address = server.address();
  console.log(`server listening ${address.address}:${address.port}`);
});

server.bind(PORT);
