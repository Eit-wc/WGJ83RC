using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    [ColorUsageAttribute(false,true,0f,8f,0.125f,3f)]
    public Color colorPushed;
    [ColorUsageAttribute(false,true,0f,8f,0.125f,3f)]
    public Color colorDone;

    [ColorUsageAttribute(false,true,0f,8f,0.125f,3f)]
    public Color currentColor;
    public UnityEvent switchAction;

    // Start is called before the first frame update
    public float chargeTime = 2;
    float chargeTimeCound;
    bool charging = false;

    public MeshRenderer mr;
    public float deepSwitch = 0.1f;

    void Start()
    {
        if(switchAction == null)
        {
            switchAction = new UnityEvent();
        }
        mr = GetComponent<MeshRenderer>();

        charging = false;
        currentColor = this.colorDone;
        chargeTimeCound = 0.0f;
        mr.material.SetColor("_EmissionColor",currentColor);
        mr.material.EnableKeyword("_EMISSION");
      
        this.transform.localPosition = Vector3.zero;
    }

    public void push()
    {
        if(!charging)
        {
            charging = true;
            currentColor = this.colorPushed;
            chargeTimeCound = 0.0f;
            mr.material.SetColor("_EmissionColor",currentColor);
            mr.material.EnableKeyword("_EMISSION");
            this.transform.localPosition = Vector3.down * deepSwitch;
            switchAction.Invoke();
        }
    }
    // Update is called once per frame
    private void FixedUpdate() {

        if(charging)
        {
            chargeTimeCound += Time.deltaTime;
            currentColor = Color.Lerp(colorPushed,colorDone,chargeTimeCound/chargeTime);
            if(chargeTimeCound >= chargeTime)
            {
                charging = false;
                currentColor = this.colorDone;
                this.transform.localPosition = Vector3.zero;
            }
            mr.material.SetColor("_EmissionColor",currentColor);
            mr.material.EnableKeyword("_EMISSION");
        }
    }
}
