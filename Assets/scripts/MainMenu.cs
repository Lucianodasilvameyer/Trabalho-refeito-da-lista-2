using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void playGame()
   {
        SceneManager.LoadScene("Fase1");   
   }
   public void quit()
   {
        Debug.Log("Saiu");
        Application.Quit();
   }
}
