using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndMove : MonoBehaviour
{
    public bool hasObjectGrabbed;
    public bool hasObjectToGrab;
    public GameObject grabbedObject;
    public Rigidbody playerRigidBody;

    void Update()
    {
        if(hasObjectToGrab && Input.GetKeyDown(KeyCode.F) && grabbedObject != null && !hasObjectGrabbed)
        {
            hasObjectGrabbed = true;
            grabbedObject.AddComponent<HingeJoint>().connectedBody = playerRigidBody;
        } 
        else if(hasObjectGrabbed && Input.GetKeyDown(KeyCode.F))
        {
            hasObjectGrabbed = false;
            hasObjectToGrab = false;
            Destroy(grabbedObject.GetComponent<HingeJoint>());
            grabbedObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Box") && !hasObjectGrabbed)
        {
            hasObjectToGrab = true;
            grabbedObject = other.gameObject;
        }
    }
        private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Box") && !hasObjectGrabbed)
        {
            hasObjectToGrab = false;
            grabbedObject = null;
        }
    }
}
