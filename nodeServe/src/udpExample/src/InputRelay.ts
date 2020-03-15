import { Utils } from "./Util";
import * as dgram from "dgram";
var utils = new Utils();

export class InputRelay {
  constructor(server: dgram.Socket) {
    this.server = server;
  }
  server: dgram.Socket;
  relay(json: any, port: number, address: string) {
    var to = JSON.parse(json["rival"]);
    const sendData = {
      type: "rival-input",
      requireNextFrame: json["requireNextFrame"],
      inputObjects: json["inputObjects"]
    };
    utils.sendJsonAndWriteLog(
      sendData,
      to["port"],
      to["address"],
      this.server,
      false
    );
  }
  hitInfoRelay(json: any, port: number, address: string) {
    var to = JSON.parse(json["rival"]);
    var sendData = {
      type: "Hit-bullet",
      bulletType: json["buletType"],
      fireFrame: json["fireFrame"],
      own: json["rival"],
      rival: json["own"]
    };
    utils.sendJsonAndWriteLog(
      sendData,
      to["port"],
      to["address"],
      this.server,
      false
    );
  }
}
