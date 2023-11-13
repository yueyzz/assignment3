using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager singleton;

    public static UIManager Singleton
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
                Debug.Log($"{nameof(UIManager)}instance already exits,destroy duplicate");
                Destroy(value);
            }
        }
    }

    [Header("Connect")]
    [SerializeField] private GameObject connetUI;
    [SerializeField] private InputField usernameField;
    private void Awake()
    {
        Singleton = this;
    }

    public void ConnectClick()
    {
        usernameField.interactable = false;
        connetUI.SetActive(false);
        NetworkManager.Singleton.Connect();
    }

    public void BackToMain()
    {
        usernameField.interactable = true;
        connetUI.SetActive(true);
    }
    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.reliable, (ushort)ClientToServerId.name);
        message.AddString(usernameField.text);
        NetworkManager.Singleton.Client.Send(message);
    }
}
