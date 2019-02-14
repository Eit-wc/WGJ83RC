using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject monitor;
    private void Start() {
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("sth enter");
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            monitor.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
