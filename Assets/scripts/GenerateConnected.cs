using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class GenerateConnected : MonoBehaviour
{
    public Spline Orbit;
    // Start is called before the first frame update
    public void GenerateStars(Spline L, GameObject par)
    {
        Orbit = L;
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        transform.Rotate(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360));
        Vector3 splinePos = Orbit.EvaluatePosition(Random.Range(0,1f));
        transform.position = transform.TransformPoint(splinePos);
        transform.parent = par.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
