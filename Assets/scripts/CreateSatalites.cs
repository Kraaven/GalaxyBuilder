using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


public class CreateSatalites : MonoBehaviour
{
    // Start is called before the first frame update
    public float angleTilt;
    public int Size;
    public GenerateConnected AnchorPrefab;
    public void Create(int size)
    {
        Size = size;
        transform.Rotate(Random.Range(-3f,3f),angleTilt,Random.Range(-3f,3f));
        transform.localScale = new Vector3(1, 1, 1.2f);

        int anchorNum = Random.Range((int)size/2, 3*size);
        for (int i = 0; i < anchorNum; i++)
        {
            //GenerateConnected OrbitSat = Instantiate(AnchorPrefab);
            //OrbitSat.GenerateStars(GetComponent<SplineContainer>().Splines[0], gameObject);
            
            GameObject anchor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            anchor.transform.parent = gameObject.transform;
            anchor.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            Vector3 splinePos = GetComponent<SplineContainer>().Splines[0].EvaluatePosition(Random.Range(0,1f));
            anchor.transform.position = transform.TransformPoint(splinePos); 
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
