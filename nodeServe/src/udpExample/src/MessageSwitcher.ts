import { Utils } from "./Util";
import { MatchingRoom } from "./MatchingRoom";
import { InputRelay } from "./InputRelay";
import * as dgram from "dgram";
var utils = new Utils();

export class MessageSwitcher {
  server: dgram.Socket;
  matchingRoom = new MatchingRoom(this.server);
  inputRelay = new InputRelay(this.server);
  switchMessage(msg: string, port: number, address: string) {
    try {
      var json = JSON.parse(msg);
      if (!json["type"]) {
        utils.sendErrorJson(
          "it is not include type",
          port,
          address,
          this.server
        );
        return false;
      }

      switch (json.type) {
        case "match": {
          utils.writeLogWithDate(`server got: ${msg} from ${address}:${port}`);
          this.matchingRoom.join(json, port, address);
          break;
        }
        case "input": {
          //ログの容量が大きくなってしまうから出力しない
          //utils.writeLogWithDate(`server got: ${msg} from ${address}:${port}`);
          this.inputRelay.relay(json, port, address);
          break;
        }
        case "hit-bullet": {
          //ログの容量が大きくなってしまうから出力しない
          //utils.writeLogWithDate(`server got: ${msg} from ${address}:${port}`);
          this.inputRelay.hitInfoRelay(json, port, address);
          break;
        }
        default: {
          utils.writeLogWithDate(`server got: ${msg} from ${address}:${port}`);
          utils.sendErrorJson("unknown type", port, address, this.server);
          break;
        }
      }
    } catch (e) {
      utils.sendErrorJson(e.message, port, address, this.server);
      return false;
    }
    return true;
  }

  constructor(server: dgram.Socket) {
    this.server = server;
  }
}
