using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
  /* private static controller singleton;

   public static controller Singleton
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
            Debug.Log($"{nameof(controller)}instance already exits,destroy duplicate");
            Destroy(value);
         }
      }
   }*/
   void Update()
   {
      if (Input.touchCount > 0)
      {
         Touch touch = Input.GetTouch(0);
        
         if (touch.phase == TouchPhase.Began)
         {
            float screenWidth = Screen.width;
            float touchPositionX = touch.position.x;
            
            if (touchPositionX < screenWidth / 2)
            {
               // 左半边
               Debug.Log("Left side clicked");
               NetworkManager.Singleton.SendInput("Left side clicked");
            }
            else
            {
               // 右半边
               Debug.Log("Right side clicked");
               NetworkManager.Singleton.SendInput("Right side clicked");
            }
         }
      }
   }
   public void btn()
   {
      GameLogic.Singleton.sendnumber();
   }

   public void ClickBtn(string name)
   {
      NetworkManager.Singleton.SendInput(name);
   }
  
}
