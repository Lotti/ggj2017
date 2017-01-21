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
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000f, floorMask))
            {
                Debug.Log(hit.point);
                if (VoiceLightShader2.Instance.isThisPositionOk(hit.point))
                    this.transform.position = hit.point;
            }
        }
        
    }
}
