using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorFromButtonByColor : MonoBehaviour
{
    public ButtonPressedController buttonPressedController;

    public Vector3 startRotation, endRotation;
    public float rotationSpeed = 1.0f;

    private Quaternion startQuaternion, endQuaternion;

    void Start()
    {
        startQuaternion = Quaternion.Euler(startRotation);
        endQuaternion = Quaternion.Euler(endRotation);
    }

    void Update()
    {
        if (buttonPressedController.IsPressed)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, endQuaternion, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startQuaternion, Time.deltaTime * rotationSpeed);
        }
    }
}
