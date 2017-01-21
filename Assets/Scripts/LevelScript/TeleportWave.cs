using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWave : Targettable
{
    Ray ray;
    RaycastHit hit;

    // Update is called once per frame
    void Update ()
    {     
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000f))
            {
               
                GameObject.Find("PlayerVR").transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
            }
        }
        
    }
}
