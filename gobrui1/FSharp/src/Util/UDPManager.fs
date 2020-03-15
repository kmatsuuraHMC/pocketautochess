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
    let messageReceived = new Event<MessageReceived>()
    static let mutable instance: UDPManager = unbox null

    static member Instance =
        if (box instance <> null) then
            instance
        else
            instance <- GameObject.FindObjectOfType<UDPManager>()
            if (box instance <> null) then
                let newObj = GameObject()
                newObj.name <- "UDPManager"
                newObj.AddComponent<UDPManager>() |> ignore
                GameObject.Instantiate(newObj) |> ignore
                instance <- newObj.GetComponent<UDPManager>()
            instance

    member private this.client: UdpClient = null
    member private this.thread: Thread = null

    member this.Awake() =
        if (this <> UDPManager.Instance) then
            GameObject.Destroy(this.gameObject)
            ()
        else
            GameObject.DontDestroyOnLoad(this.gameObject)

    member this.ThreadMethod() =
        while (true) do
            if (not this.client.Client.Connected) then this.client.Connect(host, port)
            let remoteEP: IPEndPoint = null
            let data: byte [] = this.client.Receive(ref remoteEP)
            let text = Encoding.UTF8.GetString(data)
            if (box messageReceived <> null) then
                let jsonNode = JsonNode.Parse(text)
                messageReceived.Trigger(jsonNode, text)
            Debug.Log("Get:" + text)

    member this.Start() =
        let client = new UdpClient()
        client.Connect(host, port)
        let thread = new Thread(new ThreadStart(this.ThreadMethod))
        thread.Start()

    member this.OnApplicationFocus(focus) =
        if isNull (this.client) then ()
        else if (focus) then this.client.Connect(host, port)






    member this.OnApplicationQuit() =
        this.client.Close()
        this.thread.Abort()
        if (this = UDPManager.Instance) then instance <- unbox null

    member this.SendJson(jsonStr: string) =
        if (not this.client.Client.Connected) then this.client.Connect(host, port)
        let dgram = Encoding.UTF8.GetBytes(jsonStr)
        this.client.Send(dgram, dgram.Length) |> ignore
        Debug.Log("SEND:" + jsonStr)
