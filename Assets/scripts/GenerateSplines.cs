using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class GenerateSplines : MonoBehaviour
{
    public int layers;
    public float density;
    public float innerR;
    public GameObject OrbitPrefab;
    public List<GameObject> orbits;
    public float angleTilt;

    public void Start()
    {
        orbits = new List<GameObject>();
        StartCoroutine(CreateOrbits());

    }

    IEnumerator CreateOrbits()
    {
        for (int i = 0; i < layers; i++)
        {
            orbits.Add(Instantiate(OrbitPrefab,transform.position,Quaternion.identity));
            orbits[i].transform.parent = gameObject.transform;
            
            Spline Orbit = orbits[i].GetComponent<SplineContainer>().AddSpline();
            float offset = i*density + innerR;
            var numPoints = 8;
            Orbit.Closed = true;
            for (int j = 0; j < numPoints; j++)
            {
                float angle = (float)j / numPoints * 2 * Mathf.PI; // Calculate angle in radians
                var x = offset * Mathf.Cos(angle); // Calculate x coordinate
                var z = offset * Mathf.Sin(angle); // Calculate z coordinate
                Orbit.Add(new BezierKnot(new float3(x, 0, z)), TangentMode.AutoSmooth);
            }

            orbits[i].transform.localScale = new Vector3(1, 1, 1.2f);
            orbits[i].transform.Rotate(0,i*angleTilt,0);

            yield return new WaitForSeconds(0.01f);

        } 
    }
    
    
}
