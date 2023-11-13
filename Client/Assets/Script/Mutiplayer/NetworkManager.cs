using System;
using System.Collections;
using RiptideNetworking.Utils;
using RiptideNetworking;
using UnityEngine;

public enum ServerToClientId : ushort
{
    playerSpawned=1,
    printNum=2
}
public enum ClientToServerId : ushort
{
    name=1,
    Num=2,
    str=3,
    btn=4
}

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager singleton;

    public static NetworkManager Singleton
    {
        get => singleton;
        private set
        {
            if (singleton == null)
            {
                singleton = value;
            }
            else if (singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)}instance already exits,destroy duplicate");
                Destroy(value);
            }
        }
    }

    public Client Client { get; private set; }
    [SerializeField] private string ip;
    [SerializeField] private ushort port;
    private void Awake()
    {
        Singleton = this;
    }
/// <summary>
///
/// Connected事件会在客户端成功连接到服务器时触发，你可以在DidConnect方法中编写处理连接成功的逻辑。
 //    ConnectionFailed事件会在客户端连接服务器失败时触发，你可以在FailedToConnect方法中编写处理连接失败的逻辑。
  //  Disconnected事件会在客户端与服务器断开连接时触发，你可以在DidDisconnect方法中编写处理断开连接的逻辑。
  //   ClientDisconnected事件会在其他客户端从服务器断开连接时触发，你可以在PlayerLeft方法中编写处理其他客户端断开连接的逻辑。*/
/// </summary>
    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log,Debug.Log,Debug.LogWarning,Debug.LogError,false);
        Client = new Client();
        Client.Connect($"{ip}:{port}");
        Client.Connected+=DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.Disconnected += DidDisconnect;
        Client.ClientDisconnected += PlayerLeft;
    }

    private void FixedUpdate()
    {
        Client.Tick();
       
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    public void Connect()
    {
        Client.Connect($"{ip}:{port}");
        
    }

    private void DidConnect(object sender, EventArgs e)
    {
      //  UIManager.Singleton.SendName();
    }

    private void FailedToConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.BackToMain();
    }

    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        Destroy(Player.list[e.Id].gameObject);
    }
    private void DidDisconnect(object sender,EventArgs e)
    {
//        UIManager.Singleton.BackToMain();
    }

 
    public void SendNumber(bool put)
    {
        Message message = Message.Create(MessageSendMode.reliable,(ushort)ClientToServerId.Num);
        message.AddBool(put);
        Client.Send(message);
    }

    public void SendInput(string str)
    {
        Message message = Message.Create(MessageSendMode.reliable,(ushort)ClientToServerId.str);
        message.AddString(str);
        Client.Send(message);
    }

    public void SendBtnInput(string str)
    {
        Message message = Message.Create(MessageSendMode.reliable,(ushort)ClientToServerId.btn);
        message.AddString(str);
        Client.Send(message);
    }
}

   

