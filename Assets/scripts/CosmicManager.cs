using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicManager : MonoBehaviour
{
    private float rot;
    public GameObject Pivot;
    public GameObject StartUI;



    public void StartGeneration()
    {
        StartUI.SetActive(false);
        Invoke("delayStart",1f);
    }

    private void delayStart()
    {
        Pivot.SetActive(true);
        Pivot.transform.parent.gameObject.GetComponent<GenerateSplines>().StartGen();
    }
    void Update()
    {
        rot += Time.deltaTime;
        if (rot > 100)
        {
            rot = 0;
        }
        
        RenderSettings.skybox.SetFloat("_Rotation",rot);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
