using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;


public class GenerateSplines : MonoBehaviour
{
    public int layers;
    public float density;
    public float innerR;
    public GameObject OrbitPrefab;
    public List<GameObject> orbits;
    public float angleTilt;
    public GameObject Pivot;

    public float SpinSpeed;

    public void StartGen()
    {
        orbits = new List<GameObject>();
        StartCoroutine(CreateOrbits());

    }

    IEnumerator CreateOrbits()
    {
        for (int i = 0; i < layers; i++)
        {
            int NumOrbits = Random.Range(((int)i / 10)+1, (int)i / 7);
            if (i < 40)
            {
                NumOrbits++;
            }

            if (i<15)
            {
                NumOrbits+= 2;
            }
            for (int m = 0; m < NumOrbits; m++)
            {
                Debug.Log("Orbit Size: "+ i);
                GameObject Line = Instantiate(OrbitPrefab, transform.position, Quaternion.identity);
                orbits.Add(Line);
                Line.transform.parent = gameObject.transform;
            
                Spline Orbit = Line.GetComponent<SplineContainer>().AddSpline();
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

                //orbits[i].transform.localScale = new Vector3(1, 1, 1.2f);
                Line.GetComponent<CreateSatalites>().angleTilt = i * angleTilt;
                Line.GetComponent<CreateSatalites>().Create(i);
                Line.GetComponent<CreateSatalites>().speed = SpinSpeed;
            }
                
            
            
            


            yield return new WaitForSeconds(0.07f);

        }

        Pivot.GetComponent<temp>().enabled = true;
        Pivot.transform.GetChild(0).GetComponent<CameraController>().enabled = true;
    }
    
}
