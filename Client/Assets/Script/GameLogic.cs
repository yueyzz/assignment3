using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private static GameLogic singleton;

    public static GameLogic Singleton
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
                Debug.Log($"{nameof(GameLogic)}instance already exits,destroy duplicate");
                Destroy(value);
            }
        }
    }

    public GameObject LocalPlayerPrefab => localPlayerPrefab;
    public GameObject PlayerPrefab => playerPrefab;

    public Button SpaceBtn;
    [Header("PlayerPrefab")] 
    [SerializeField]
    private GameObject localPlayerPrefab;
    [SerializeField]
    private GameObject playerPrefab;
    private void Awake()
    {
        Singleton = this;
    }

    public void sendnumber()
    {
       
    }

    public void sendInput(string str)
    {
        NetworkManager.Singleton.SendInput(str);
    }

    private void Update()
    {
        // 检测按钮是否被按住
        if (Input.GetButton("Fire1"))
        {
            // 执行你想要的方法
            NetworkManager.Singleton.SendNumber(true);
        }
        else
        {
            NetworkManager.Singleton.SendNumber(false);
        }
    }
}
