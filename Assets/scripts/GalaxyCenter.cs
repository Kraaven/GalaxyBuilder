using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GalaxyCenter : MonoBehaviour
{
    public Vector3 originalSize;
    public void Start()
    {
        originalSize = gameObject.transform.localScale;
        gameObject.transform.localScale = originalSize / 100;
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        for (int i = 1; i < 101; i++)
        {
            gameObject.transform.localScale = originalSize * i / 100;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
