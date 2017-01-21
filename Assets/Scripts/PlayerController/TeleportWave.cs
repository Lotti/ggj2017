using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWave : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public LayerMask floorMask;

    // Update is called once per frame
    void Update ()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red);

        //TELEPORT 
        if (Input.GetMouseButtonDown(0))
        {
           

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f, floorMask))
            {
                

                Debug.Log(hit.point);
                //if (VoiceLightShader2.Instance.isThisPositionOk(hit.point))
                this.transform.position = hit.point;
            }
        }

        //FORZO ALTEZZA
       /* if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 10f, floorMask))
        {
            Debug.DrawLine(this.transform.position, hit.point, Color.green);
            this.transform.position=new Vector3(this.transform.position.x,0,this.transform.position.z);
        }
        */
    }
}
