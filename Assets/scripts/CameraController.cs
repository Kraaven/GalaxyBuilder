using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private float rot;
    public GameObject pivot;
    
    // Sensitivity for mouse rotation
    public float sensitivity = 2.0f;

    // Variable to check if right mouse button is held down
    private bool isRotating = false;

    private float scalingSensitivity = 0.5f;

    public void Start()
    {
        pivot = gameObject.transform.parent.gameObject;
        pivot.transform.eulerAngles = new Vector3(Mathf.Clamp(pivot.transform.eulerAngles.x, -20f, 60f), pivot.transform.eulerAngles.y, pivot.transform.eulerAngles.z);
    }

    public void Update()
    {
        rot += 0.01f;
        if (rot > 100)
        {
            rot = 0;
        }
        
        RenderSettings.skybox.SetFloat("_Rotation",rot);
        
        
        // Check for right mouse button press and release
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.None;
            isRotating = false;
        }

        // Rotate the camera if the right mouse button is held down
        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            // Rotate the camera on the x and y axes
            transform.Rotate(Vector3.up * mouseX);
            transform.Rotate(Vector3.left * mouseY);
        }
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        scrollWheel *= -1;
        float newScale = pivot.transform.localScale.x + scrollWheel * scalingSensitivity;
        newScale = Mathf.Max(newScale, 0.1f);
        pivot.transform.localScale = new Vector3(newScale, newScale, newScale);
        
        
        
        
    }
}
