using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleUI : MonoBehaviour
{
    public void changeScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
