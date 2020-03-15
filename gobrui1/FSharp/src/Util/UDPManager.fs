namespace UDPManager

open System.Collections
open System.Collections.Generic
open UnityEngine
open System.Net
open System.Net.Sockets
open System.Text
open System.Threading
open MiniJSON

type MessageReceived = JsonNode * string

type UDPManager() =
    inherit MonoBehaviour()
    let host = "localhost"
    let port = 33333
    let mutable messageReceived = new Event<MessageReceived>()
    static let mutable instance: option<UDPManager> = None
    let mutable client: UdpClient = null
    let mutable thread: Thread = null

    member this.messageReceivedI
        with get () = messageReceived
        and set v = messageReceived <- v

    static member Instance =
        match instance with
        | Some inst -> inst
        | None ->
            let inst = GameObject.FindObjectOfType<UDPManager>()
            if (box inst
                |> isNull
                |> not) then
                inst
            else
                let newObj = GameObject()
                newObj.name <- "UDPManager"
                newObj.AddComponent<UDPManager>() |> ignore
                GameObject.Instantiate(newObj) |> ignore
                newObj.GetComponent<UDPManager>()

    member this.Awake() =
        if (this <> UDPManager.Instance) then
            GameObject.Destroy(this.gameObject)
            ()
        else
            GameObject.DontDestroyOnLoad(this.gameObject)

    member this.ThreadMethod() =
        while (true) do
            if (not client.Client.Connected) then client.Connect(host, port)
            let remoteEP: IPEndPoint = null
            let data: byte [] = client.Receive(ref remoteEP)
            let text = Encoding.UTF8.GetString(data)
            if (box messageReceived <> null) then
                let jsonNode = JsonNode.Parse(text)
                messageReceived.Trigger(jsonNode, text)
            Debug.Log("Get:" + text)

    member this.Start() =
        client <- new UdpClient()
        client.Connect(host, port)
        thread <- Thread(ThreadStart(this.ThreadMethod))
        thread.Start()

    member this.OnApplicationFocus(focus) =
        if isNull (client) then ()
        else if (focus) then client.Connect(host, port)




    member this.OnApplicationQuit() =
        client.Close()
        thread.Abort()
        if (this = UDPManager.Instance) then instance <- None

    member this.SendJson(jsonStr: string) =
        if (not client.Client.Connected) then client.Connect(host, port)
        let dgram = Encoding.UTF8.GetBytes(jsonStr)
        client.Send(dgram, dgram.Length) |> ignore
        Debug.Log("SEND:" + jsonStr)
