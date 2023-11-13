using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();
    
    public ushort Id { get; private set; }
    public string Username {get; private set; }

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    public static void Spawn(ushort id, string username)
    {
        foreach (Player otherPlayer in list.Values)
        {
            otherPlayer.SendSpawned(id);
        }
        Player player=Instantiate(GameLogic.Singleton.PlayerPrefab,new Vector3(0f,0f,1f),Quaternion.identity).GetComponent<Player>();
        player.name = $"Player{id}({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
        player.Id = id;
        player.Username = string.IsNullOrEmpty(username) ? $"Guest{id}" : username;
        player.SendSpawned();
        list.Add(id,player);
    }

    private void SendSpawned()
    {
        
        
        NetworkManager.Singleton.Server.SendToAll(AddSpawnData( Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.playerSpawned)));
    }

    private void SendSpawned(ushort toCliented)
    {
        NetworkManager.Singleton.Server.Send(AddSpawnData( Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.playerSpawned)),toCliented);
    }

    private Message AddSpawnData(Message message)
    {
        message.AddUShort(Id);
        message.AddString(Username);
        message.AddVector3(transform.position);
        return message;
    }
    
    [MessageHandler((ushort)ClientToServerId.name)]
    private static void Name(ushort fromClientId,Message message)
    {
        Spawn(fromClientId, message.ToString());
    }
}
