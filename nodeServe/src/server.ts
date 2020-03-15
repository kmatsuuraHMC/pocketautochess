import { Server } from "ws";
var wss = new Server({
  port: 8080
});

wss.on("connection", (ws) => {
  ws.on("message", function(message) {
    console.log("received: %s", message);
  });
  ws.send("This is server");
});
