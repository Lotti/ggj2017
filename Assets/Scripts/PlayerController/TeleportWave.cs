using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWave : MonoBehaviour
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
                Debug.Log(hit.point);
                GameObject.Find("PlayerVR").transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
            }
        }
        
    }
}
