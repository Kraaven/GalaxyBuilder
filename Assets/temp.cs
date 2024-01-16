using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class temp : MonoBehaviour
{

    float deltaRot;
    float deltayRot;
    
    float x;
    float y;
    void Update()
    {
        deltaRot = Input.GetAxis("Vertical");
        deltayRot = Input.GetAxis("Horizontal") * -1;
        y += deltayRot * 2.3f;
        if (y > 360) {y = 0;}
        x += deltaRot * 2.3f;
        x = Mathf.Clamp(x, -85, 85);
        transform.rotation = Quaternion.Euler(x,y,0);
    }
}
