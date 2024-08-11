using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndMove : MonoBehaviour
{
    public bool hasObjectGrabbed;
    public bool hasObjectToGrab;
    public GameObject grabbedObject;
    public Rigidbody playerRigidBody;

    public Animator anim;

    public string cajaAudioClip;

    void Update()
    {
        if(hasObjectToGrab && Input.GetKeyDown(KeyCode.F) && grabbedObject != null && !hasObjectGrabbed)
        {
            hasObjectGrabbed = true;
            grabbedObject.AddComponent<HingeJoint>().connectedBody = playerRigidBody;
            anim.SetBool("IsPushing", true);
            bool randomBool = Random.value > 0.5f;
            cajaAudioClip = randomBool ? "Caja1" : "Caja2";
            AudioManager.main.Play(cajaAudioClip);
        } 
        else if(hasObjectGrabbed && Input.GetKeyDown(KeyCode.F))
        {
            hasObjectGrabbed = false;
            hasObjectToGrab = false;
            Destroy(grabbedObject.GetComponent<HingeJoint>());
            grabbedObject = null;
            anim.SetBool("IsPushing", false);
            AudioManager.main.Stop(cajaAudioClip);
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
