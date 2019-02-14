using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private void OnParticleCollision(GameObject other) {

        if(other.gameObject.layer == LayerMask.NameToLayer(global.missleLayer))
        {
            SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
        }
    }
}
