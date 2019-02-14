using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject winText;
    private void Start() {
        
        

    }
    public void restartLevel()
    {
         Application.LoadLevel(Application.loadedLevel);
    }

    public void checkWin()
    {
        if(global.winFlag)
        {
            winText.SetActive(true);
        }
    }

}
