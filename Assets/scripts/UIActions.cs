using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIActions : MonoBehaviour
{

    public GameObject ExtraSettings;
    private bool showAdvanced;

    public void Start()
    {
        ExtraSettings.SetActive(false);
        showAdvanced = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Advanced()
    {
        showAdvanced = !showAdvanced;
        ExtraSettings.SetActive(showAdvanced);
    }
}
