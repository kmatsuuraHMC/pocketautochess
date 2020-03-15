import { Utils } from "./Util";
import { Server } from "ws";
import * as dgram from "dgram";
var utils = new Utils();

//現在の書き方だと２以外指定できない
const MATCH_NUM = 2;

export class MatchingRoom {
  server: dgram.Socket;
  waitingPlayer: any[];
  waitingPlayerTimeOut?: NodeJS.Timeout;
  constructor(server: dgram.Socket) {
    this.server = server;
    this.waitingPlayer = [];
  }
  join(json: any, port: number, address: string) {
    //マッチング開始したユーザー返すユーザー情報
    const data = {
      type: "playerInfo",
      id: utils.getUniqueStr(),
      name: json.name
    };

    utils.sendJsonAndWriteLog(data, port, address, this.server);

    const hoge = { ...data, port: port.toString(), address: address };

    this.waitingPlayer.push(hoge);

    if (this.waitingPlayer.length >= MATCH_NUM) {
      //マッチング成立
      for (const i of [0, 1]) {
        var otherIndex;
        if (i === 0) {
          otherIndex = 1;
        } else {
          otherIndex = 0;
        }

        //成立したマッチング情報を送る
        var matchData = {
          type: "success-match",
          rival: this.waitingPlayer[otherIndex]
        };

        const own = this.waitingPlayer[i];
        utils.sendJsonAndWriteLog(
          matchData,
          own["port"],
          own["address"],
          this.server
        );
      }

      //マッチングが成立しなかった場合のタイムアウト処理を削除する
      if (this.waitingPlayerTimeOut === null)
        clearTimeout(this.waitingPlayerTimeOut);

      this.waitingPlayer = [];
    } else {
      // マッチング不成立（５秒後）
      //５秒以内にマッチングが成立しなかった場合に成立しなかった旨の情報を送信
      this.waitingPlayerTimeOut = setTimeout(
        (d) => {
          if (this.waitingPlayer.length >= MATCH_NUM) return;

          //マッチング不成立情報
          const notMatch = { type: "not-match", msg: "sorry not matching" };
          utils.sendJsonAndWriteLog(
            notMatch,
            d["port"],
            d["address"],
            this.server
          );

          this.waitingPlayer.pop();
        },
        5000,
        hoge
      );
    }
  }
}

