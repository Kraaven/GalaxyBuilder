using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;


public class CreateSatalites : MonoBehaviour
{
    // Start is called before the first frame update
    public float angleTilt;
    public int Size;
    public Material StarMat1;
    public Material StarMat2;
    private Material StarMat;
    public GenerateConnected AnchorPrefab;
    private List<GameObject> AnchorList;
    private Spline Ellipse;

    public List<GameObject> anchors;
    public List<float> Location;
    public Mesh CUBE;
    private Matrix4x4[] matrices;

    public float speed;
    
    public void Create(int size)
    {
        anchors = new List<GameObject>();
        Location = new List<float>();

        switch (Random.Range(0,20))
        {
            case 19:
                StarMat = StarMat2;
                break;
            default:
                StarMat = StarMat1;
                break;
        }
        
        Size = size;
        transform.Rotate(Random.Range(-3f,3f),angleTilt,Random.Range(-3f,3f));
        transform.localScale = new Vector3(1, 1, 1.2f);

        StarMat.enableInstancing = true;
        Ellipse = GetComponent<SplineContainer>().Splines[0];

        int anchorNum = Random.Range((int)size/2, 3*size);
        matrices = new Matrix4x4[anchorNum];
        
        
        for (int i = 0; i < anchorNum; i++)
        {
            //Create the Cube point
            GameObject anchor = new GameObject();
            //anchor.GetComponent<MeshRenderer>().material = StarMat;

            //Set Cube point configurations
            var anchorT = anchor.transform;
            anchorT.parent = gameObject.transform;
            float StarScale = Random.Range(0.5f, 2.2f);
            anchorT.localScale = new Vector3(StarScale, StarScale, StarScale);
            anchorT.Rotate(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360));
            
            //Find Cube point position and set it
            float spawnPoint = Random.Range(0, 1f);
            Vector3 splinePos = Ellipse.EvaluatePosition(spawnPoint);
            anchor.transform.position = transform.TransformPoint(splinePos);
            
            anchors.Add(anchor);
            Location.Add(spawnPoint);

            //set the matrix
            Transform starstrans = anchors[i].transform;
            matrices[i] = Matrix4x4.TRS(starstrans.position,starstrans.rotation,starstrans.localScale);
            
            // GenerateConnected anchor = Instantiate(AnchorPrefab, gameObject.transform);
            // anchor.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            // Vector3 splinePos = GetComponent<SplineContainer>().Splines[0].EvaluatePosition(Random.Range(0,1f));
            // anchor.transform.position = transform.TransformPoint(splinePos);

            //StartCoroutine(Revolve());
            //StartCoroutine(RenderRevolve());
        }
        
        
        
    }

    //Move the Cubes to their new positions
    IEnumerator Revolve()
    {
        float offset = 0.05f/(Size*Size);
        while (true)
        {
            for (int i = 0; i < anchors.Count; i++)
            {
                Location[i] += offset;
                if (Location[i] > 1)
                {
                    Location[i]--;
                }
                Vector3 splinePos = Ellipse.EvaluatePosition(Location[i]);
                anchors[i].transform.position = transform.TransformPoint(splinePos);
            }

            yield return new WaitForSeconds(0.08f);    
        }
        
    }

    IEnumerator RenderRevolve()
    {
        //Render Cube Mesh on each Cube Point
        float offset = 0.05f/(Size*Size);
        while (true)
        {
            for (int i = 0; i < anchors.Count; i++)
            {
                Location[i] += offset;
                if (Location[i] > 1)
                {
                    Location[i]--;
                }
                Vector3 splinePos = Ellipse.EvaluatePosition(Location[i]);
                anchors[i].transform.position = transform.TransformPoint(splinePos);
                
                Transform starstrans = anchors[i].transform;
                matrices[i] = Matrix4x4.TRS(starstrans.position,starstrans.rotation,starstrans.localScale);
            }
            
            Graphics.DrawMeshInstanced(CUBE,0,StarMat,matrices);
            yield return new WaitForSeconds(0.08f);   
        }
    }

    private void Update()
    {
        float offset = 0.05f/(Size*Size)*speed;
        
            for (int i = 0; i < anchors.Count; i++)
            {
                Location[i] += offset;
                if (Location[i] > 1)
                {
                    Location[i]--;
                }
                Vector3 splinePos = Ellipse.EvaluatePosition(Location[i]);
                anchors[i].transform.position = transform.TransformPoint(splinePos);
                
                Transform starstrans = anchors[i].transform;
                matrices[i] = Matrix4x4.TRS(starstrans.position,starstrans.rotation,starstrans.localScale);
            }
            
            Graphics.DrawMeshInstanced(CUBE,0,StarMat,matrices);
             
        
    }
}
