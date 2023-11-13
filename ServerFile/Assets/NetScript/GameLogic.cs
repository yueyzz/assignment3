using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private static GameLogic singleton;

    public static GameLogic Singleton
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
                Debug.Log($"{nameof(GameLogic)}instance already exits,destroy duplicate");
                Destroy(value);
            }
        }
    }

    public bool Jump;
    public SceneCompletion sc;
   
    public Controlle con;
    public GameObject PlayerPrefab=>playerPrefab;
    [Header("Prefab")] 
    [SerializeField] private GameObject playerPrefab;
    private void Awake()
    {
        Singleton = this;
    }
    
    [MessageHandler((ushort)ClientToServerId.Num)]
    public static void Name(ushort fromClientId,Message message)
    {
        bool input=  message.GetBool();
        GameLogic instance = FindObjectOfType<GameLogic>();
        instance.SetJump(input);
    }
    public Text textComponent;

    [MessageHandler((ushort)ClientToServerId.str)]
    public static void GetInput(ushort fromClientId, Message message)
    {
        bool input = message.GetBool();
        print(input);
        GameLogic instance = FindObjectOfType<GameLogic>();
        instance.SetLode();

    }

    public void SetLode()
    {
        SceneManager.LoadScene(0);
    }
    public void SetJump(bool put)
    {
        Jump = put;
    }
    public Text btn;

    [MessageHandler((ushort)ClientToServerId.btn)]
    public static void GetbtnInput(ushort fromClientId, Message message)
    {
        string str = message.GetString();
        GameLogic instance = FindObjectOfType<GameLogic>();
        instance.SetbtnText(str);
    }
    public void SetbtnText(string str)
    {
        print(str);
    }

    private void Update()
    {
        if (Jump)
        {
           
        }
    }
}
