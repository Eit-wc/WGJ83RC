using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOver;
    GameObject canvas;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    private void OnParticleCollision(GameObject other) {


        if(other.gameObject.layer == LayerMask.NameToLayer(global.missleLayer))
        {
            //end game
            global.winFlag = true;
            Instantiate(gameOver,canvas.transform);
           
        }
    }
}
