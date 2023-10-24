using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlideElements : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public float xOffset = 0f; // Optional offset for adjusting the x position

    public RectTransform[] uiElements; // Array of UI elements to align with the camera

    private float initialCameraPositionX;

    void Start()
    {
        initialCameraPositionX = mainCamera.transform.position.x;
    }

    void LateUpdate()
    {
        float cameraMovement = mainCamera.transform.position.x - initialCameraPositionX;

        foreach (RectTransform uiElement in uiElements)
        {
            Vector3 newPosition = uiElement.position;
            newPosition.x += cameraMovement + xOffset;
            uiElement.position = newPosition;
        }

        initialCameraPositionX = mainCamera.transform.position.x;
    }
}
