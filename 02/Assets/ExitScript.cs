using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    private void OnParticleCollision(GameObject other) {

        if(other.gameObject.layer == LayerMask.NameToLayer(global.missleLayer))
        {
            Debug.Log("Exit");
            Application.Quit();
        }
    }
}
