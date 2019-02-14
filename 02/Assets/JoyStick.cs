using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public Vector2 maxHorizontal;
    public Vector2 maxVerticle;

    public Quaternion rotage;

    public GameObject controlObj;
    CharControl cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = controlObj.GetComponent<CharControl>();
    }

    public Vector2 Control(float x, float y)
    {
        if(x < maxHorizontal.x)
            x = maxHorizontal.x;
        else if(x > maxHorizontal.y)
            x = maxHorizontal.y;

        if(y < maxVerticle.x)
            y = maxVerticle.x;
        else if(y > maxVerticle.y)
            y = maxVerticle.y;


        rotage = Quaternion.Euler(-y,0,x);
        transform.parent.localRotation = rotage;
        cc.v = y/maxVerticle.x;
        cc.h = x/maxHorizontal.x;
        return new Vector2(x,y);
    }

    public void Reset() {
        transform.parent.rotation = Quaternion.identity;
    }
    // Update is called once per frame
    private void FixedUpdate() {
        
    }
}
