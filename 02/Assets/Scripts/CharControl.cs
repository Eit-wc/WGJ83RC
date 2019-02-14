using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CharControl : MonoBehaviour
{
    public GameObject objMissle;
    ParticleSystem parMissleL,parMissleR;

    private Rigidbody rb;
    public float rotage_speed = 100;
    public float move_speed = 100;
    public float max_speed = 100;
    public ForceMode fm = ForceMode.Force;
    private Vector3 rotageVec;

    // Start is called before the first frame update
    Quaternion deltaRotation;
    public float h,v;

    GameObject objWheelR,objWheelL;
    public GameObject handL,handR;
    Material matWheelR,matWheelL;

    Quaternion turnForce;
    Vector3 moveForce;
    public float animeSpeed = 5;

    public int Helth;

    
    bool takeDmg = false;
    float timeFree = 2;
    float timeFreeCount =0;

    public bool manualMove = false;
    public GameObject boomEffect;
    AudioSource auds;

    public GameObject takeDmgAudio;
            
    void Start()
    {
        auds = GetComponent<AudioSource>();
        auds.volume = 0.0f;
        auds.loop = true;
        auds.Play();

        rb = GetComponent<Rigidbody>();
        rotageVec = new Vector3(0,1,0);
        
        objWheelR = this.transform.Find("WheelR").gameObject;
        objWheelL = this.transform.Find("WheelL").gameObject;

        handR = this.transform.Find("HandR").Find("hand").gameObject;
        handL = this.transform.Find("HandL").Find("hand").gameObject;

        //objMissle = this.transform.Find("Missle").gameObject;


        // wheel
        matWheelR = objWheelR.GetComponent<MeshRenderer>().materials[0];
        matWheelL = objWheelL.GetComponent<MeshRenderer>().materials[0];
        
        matWheelR.SetFloat(global.shdWheelName,0);
        matWheelL.SetFloat(global.shdWheelName,0);
        global.Helth = this.Helth;
    }

    // Update is called once per frame

    public void ShootL()
    {
        Instantiate(objMissle,handL.transform.position,handL.transform.rotation).GetComponent<ParticleSystem>();
    }

    public void ShootR()
    {
        Instantiate(objMissle,handR.transform.position,handR.transform.rotation).GetComponent<ParticleSystem>();
    }

    public void takeControl(float x, float y)
    {
        h = x;
        v = y;
    }

    private void FixedUpdate() {
        
        if(manualMove)
        {
            // read inputs
            h = Input.GetAxis(global.hor);
            v = Input.GetAxis(global.ver);
            
            if(Input.GetButton("Fire1"))
            {
                if(parMissleL == null)
                {
                    parMissleL = Instantiate(objMissle,handL.transform.position,handL.transform.rotation).GetComponent<ParticleSystem>();
                }
                //    missleL.Play(true);
            }
            if(Input.GetButton("Fire2"))
            {
                if(parMissleR == null)
                {
                    parMissleR = Instantiate(objMissle,handR.transform.position,handR.transform.rotation).GetComponent<ParticleSystem>();
                }
            }
        }

        matWheelR.SetFloat(global.shdWheelName,(v - h)*animeSpeed);
        matWheelL.SetFloat(global.shdWheelName,(v + h)*animeSpeed);

        deltaRotation = Quaternion.Euler( (rotageVec * rotage_speed) * h * Time.deltaTime);
        turnForce = rb.rotation * deltaRotation;
        moveForce = (transform.right * move_speed) * v * Time.deltaTime;

        
        
        
        rb.MoveRotation(turnForce);
        if(rb.velocity.magnitude < max_speed)
        {
            rb.AddForce(moveForce ,fm);
        }


        if((h>0.01f) ||(h<-0.01f) || (v>0.01f) ||(v<-0.01f))
        {
            auds.volume = Mathf.Lerp(auds.volume,0.5f,Time.deltaTime*10.0f);
            
        }else
        {
            auds.volume = Mathf.Lerp(auds.volume,0.0f,Time.deltaTime*10.0f);
        }

        if(takeDmg)
        {
            timeFreeCount += Time.deltaTime;
            if(timeFreeCount >= timeFree)
            {
                takeDmg = false;
            }
        }
    }

    private void OnParticleCollision(GameObject other) {

        if(other.gameObject.layer == LayerMask.NameToLayer(global.boomLayer))
        {
            if(!takeDmg)
            {
                takeDmgAudio.GetComponent<AudioSource>().Play();
                // camera shake
                CameraShaker.Instance.ShakeOnce(8f ,4f,.1f,1f);

                takeDmg = true;
                timeFreeCount = 0.0f;
                global.Helth -= 1;
                if(global.Helth <= 0)
                {
                    Debug.Log("Die");
                    Instantiate(boomEffect,this.transform.position,Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }

        }
    }
}
