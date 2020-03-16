import { Server } from "ws";
import * as dgram from "dgram";

export class Utils {
  constructor() {}
  //エラーがあった場合、クライアントにエラーメッセージを送信するための関数
  sendErrorJson(
    errMsg: string,
    port: number,
    address: string,
    server: dgram.Socket
  ): void {
    const data = { type: "error", msg: errMsg };
    server.send(JSON.stringify(data), port, address);
    console.log(
      this.getDateStr() +
        `: server send: ${JSON.stringify(data)} to ${address}:${port}`
    );
  }

  //JSONの送信とログの書き込みを行う
  //ログの書き込みが不要な場合はisWriteLogをfalseにする
  sendJsonAndWriteLog(
    json: any,
    port: number,
    address: string,
    server: dgram.Socket,
    isWriteLog = true
  ) {
    var jsonStr = JSON.stringify(json);
    server.send(jsonStr, port, address);
    if (isWriteLog)
      console.log(
        this.getDateStr() + `: server send: ${jsonStr} to ${address}:${port}`
      );
  }

  //ユニークな文字列を返す
  getUniqueStr(myStrong = 1000) {
    return (
      new Date().getTime().toString(16) +
      Math.floor(myStrong * Math.random()).toString(16)
    );
  }

  //現在の日時を文字列で返す
  getDateStr() {
    var dt = new Date();
    var formatted = dt.toString();
    return formatted;
  }

  //日時と連結してログを出力する
  writeLogWithDate(str: string) {
    console.log(this.getDateStr() + ": " + str);
  }
}
