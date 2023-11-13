using System;
using RiptideNetworking;
using RiptideNetworking.Utils;
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
      if (singleton==null)
      {
        singleton = value;
      }
      else if(singleton!=value)
      {
         Debug.Log($"{nameof(NetworkManager)}instance already exits,destroy duplicate");
         Destroy(value);
      }
    }
  }

  public Server Server { get; private set; }
  [SerializeField] private ushort port;
  [SerializeField] private ushort maxClientCount;
  
  private void Awake()
  {
    Singleton = this;
    DontDestroyOnLoad(gameObject);
  }

  private void Start()
  {
    Application.targetFrameRate = 60;
    RiptideLogger.Initialize(Debug.Log,Debug.Log,Debug.LogWarning,Debug.LogError,false);
    Server = new Server();
    Server.Start(port,maxClientCount);
    Server.ClientDisconnected += PlayerLeft;
    Server.MessageReceived += OnMessageReceived;
  }

  private void FixedUpdate()
  {
    Server.Tick();
  }

  private void OnApplicationQuit()
  {
   Server.Stop();
  }

  private void PlayerLeft(object sender,ClientDisconnectedEventArgs e)
  {
//    Destroy(Player.list[e.Id].gameObject);
  }
  
  private void OnMessageReceived(object sender, ServerMessageReceivedEventArgs e)
  {
   
  }
}
