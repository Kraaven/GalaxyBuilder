using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


public class CreateSatalites : MonoBehaviour
{
    // Start is called before the first frame update
    public float angleTilt;
    public int Size;
    public Material StarMat;
    public GenerateConnected AnchorPrefab;
    private List<GameObject> AnchorList;
    private Spline Ellipse;

    public List<GameObject> anchors;
    public List<float> Location;
    
    public void Create(int size)
    {
        anchors = new List<GameObject>();
        Location = new List<float>();
        
        
        Size = size;
        transform.Rotate(Random.Range(-3f,3f),angleTilt,Random.Range(-3f,3f));
        transform.localScale = new Vector3(1, 1, 1.2f);

        Ellipse = GetComponent<SplineContainer>().Splines[0];

        int anchorNum = Random.Range((int)size/2, 3*size);
        for (int i = 0; i < anchorNum; i++)
        {

            GameObject anchor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            anchor.GetComponent<MeshRenderer>().material = StarMat;

            var anchorT = anchor.transform;
            anchorT.parent = gameObject.transform;
            anchorT.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            anchorT.Rotate(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360));
            
            float spawnPoint = Random.Range(0, 1f);
            Vector3 splinePos = Ellipse.EvaluatePosition(spawnPoint);
            anchor.transform.position = transform.TransformPoint(splinePos);
            
            anchors.Add(anchor);
            Location.Add(spawnPoint);
            
            
            // GenerateConnected anchor = Instantiate(AnchorPrefab, gameObject.transform);
            // anchor.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            // Vector3 splinePos = GetComponent<SplineContainer>().Splines[0].EvaluatePosition(Random.Range(0,1f));
            // anchor.transform.position = transform.TransformPoint(splinePos);

            StartCoroutine(Revolve());
        }
        
        
        
    }

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
}
