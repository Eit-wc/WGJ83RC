using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float startHelth = 1;
    public GameObject BoomEffect;
    
    float helth;
    Rigidbody rb;

    GameObject traget;
    public float speed = 1.0f;
    public float BoomDistance = 1.0f;

    AudioSource auds;
    
    // Start is called before the first frame update
    void Start()
    {
        auds = GetComponent<AudioSource>();
        helth = this.startHelth;
        traget =null;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if(traget != null)
        {
            this.transform.LookAt(traget.transform,Vector3.up);
            this.transform .position = Vector3.MoveTowards(this.transform.position, this.traget.transform.position,Time.deltaTime * speed);

            if( (this.transform.position - this.traget.transform.position).sqrMagnitude < BoomDistance * BoomDistance)
            {
                GameObject boomObj = Instantiate(BoomEffect,this.transform.position,Quaternion.identity);
                boomObj.transform.localScale = this.transform.localScale;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer(global.playerLayer))
        {
            traget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer(global.playerLayer))
        {
            traget = null;
        }
    }

    private void OnParticleCollision(GameObject other) {

        if(other.gameObject.layer == LayerMask.NameToLayer(global.missleLayer))
        {
            
            this.helth -= other.GetComponent<missileScript>().damage;
            
            if(this.helth<=0)
            {
                Instantiate(BoomEffect,this.transform.position,Quaternion.identity);
                auds.Play();
                Destroy(this.gameObject,0.9f);
            }
            
        }
    }
}
