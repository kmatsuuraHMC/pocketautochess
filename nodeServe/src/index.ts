import koa from "koa";
const app = new koa();

app.use(async (ctx: any) => {
  ctx.body = "Hello World";
});

app.listen(3000);

export function hoge() {
  console.log("hgoe");
}
