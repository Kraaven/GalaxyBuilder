using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CreateSatalites : MonoBehaviour
{
    // Start is called before the first frame update
    public float angleTilt;
    public void Create()
    {
        transform.Rotate(0,angleTilt,0);
        transform.localScale = new Vector3(1, 1, 1.2f);
        
        
        GameObject anchor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        anchor.transform.parent = gameObject.transform;
        anchor.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        anchor.transform.position = transform.TransformPoint(GetComponent<SplineContainer>().Splines[0].EvaluatePosition(0));
        
        GameObject a2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        a2.transform.parent = gameObject.transform;
        a2.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        a2.transform.position = transform.TransformPoint(GetComponent<SplineContainer>().Splines[0].EvaluatePosition(0.5f));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
