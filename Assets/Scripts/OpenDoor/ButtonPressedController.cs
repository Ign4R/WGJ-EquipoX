using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressedController : MonoBehaviour
{
    public bool IsPressed;

    public Vector3 pressedPositionOffset;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (IsPressed)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition + pressedPositionOffset, Time.deltaTime * 10f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * 10f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box") && IsPressed) IsPressed = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box") && !IsPressed) IsPressed = true;
    }
}
