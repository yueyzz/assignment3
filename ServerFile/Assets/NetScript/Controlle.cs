using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Controlle : MonoBehaviour
{
    public static Text InputText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdeatInput(string str)
    {
        InputText.text = str;
    }
}
