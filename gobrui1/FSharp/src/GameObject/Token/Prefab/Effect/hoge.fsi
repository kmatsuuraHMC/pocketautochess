
let ret   x   = fun _ -> x
let (>>=) m f = fun x -> f (m x) x

type FuncBuilder() =
    member __.Return(x)  = ret x
    member __.Bind(m, f) = m >>= f
let func = FuncBuilder()

let test = func {
    let! a = fun x -> x + 1
    let! b = fun x -> x * 2
    return (a, b) }

do
    printfn "%A" (test 3)
    printfn "%A" (test 5)