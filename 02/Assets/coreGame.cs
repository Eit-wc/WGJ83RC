using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreGame : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
 
    public Vector3 DeltaPos;
    bool controlJoy;
    public float sensitivity = 1.0f;
    GameObject joyTraget;
    
    public GameObject HPanel;
    public GameObject H1,H2,H3;
    public GameOver gameOver;
    GameObject canvas;



    // Start is called before the first frame update
    void Start()
    {
        DeltaPos = Vector2.zero;
        HPanel = GameObject.Find("HPanel");
        H1 = HPanel.transform.FindChild("H1").gameObject;
        H2 = HPanel.transform.FindChild("H2").gameObject;
        H3 = HPanel.transform.FindChild("H3").gameObject;

        canvas = GameObject.Find("Canvas");

        global.winFlag = false;

    }

    public void setHelth(int h)
    {
        if(h>0)
        {
            H1.SetActive(true);
        }else
        {
            H1.SetActive(false);    
        }

        if(h>1)
        {
            H2.SetActive(true);
        }else
        {
            H2.SetActive(false);    
        }

        if(h>2)
        {
            H3.SetActive(true);
        }else
        {
            H3.SetActive(false);    
        }
        
    }
    bool insGameOver = false;
    // Update is called once per frame
    private void FixedUpdate() {

        setHelth(global.Helth);

        

        if(controlJoy && Input.GetButton("Fire1"))
        {
            DeltaPos.x -= Input.GetAxis("Mouse X")*sensitivity;
            DeltaPos.y -= Input.GetAxis("Mouse Y")*sensitivity;
           
            DeltaPos = joyTraget.GetComponent<JoyStick>().Control(DeltaPos.x,DeltaPos.y);

        }
        else if(Input.GetButton("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            
            if( Physics.Raycast( ray, out hit, 100,LayerMask.GetMask(global.buttonLayer )  | LayerMask.GetMask(global.joyLayer) ) )
            {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer(global.buttonLayer))
                {
                    hit.transform.GetComponent<ButtonScript>().push();
                    //Debug.Log( hit.transform.gameObject.name );
                }else if(hit.transform.gameObject.layer == LayerMask.NameToLayer(global.joyLayer))
                {
                    controlJoy = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    joyTraget = hit.transform.gameObject;
                    DeltaPos = Vector2.zero;
                }
            }
        }else
        {
            DeltaPos = Vector2.zero;
            if(joyTraget != null)
            {
                joyTraget.GetComponent<JoyStick>().Control(DeltaPos.x,DeltaPos.y);
            }
            controlJoy = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            joyTraget = null;
        }

        if(global.Helth<=0 &&(!insGameOver))
        {
            //end game
            Instantiate(gameOver,canvas.transform);
            insGameOver = true;
        }

    }
}
