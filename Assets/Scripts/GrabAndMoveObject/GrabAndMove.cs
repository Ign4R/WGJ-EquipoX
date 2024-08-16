using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndMove : MonoBehaviour
{
    public CursedController cursed;
    public bool hasObjectGrabbed;
    public bool hasObjectToGrab;
    public GameObject grabbedObject;
    public Rigidbody playerRigidBody;
    public bool isGhost;
    public Collider checker;

    public Animator anim;

    public string cajaAudioClip;
    private bool shouldCheckForBoxes=true;

    void Update()
    {
        if (cursed.isGhost)
        {
            hasObjectGrabbed = false;
            hasObjectToGrab = false;
            if (grabbedObject != null)
            {
                Destroy(grabbedObject?.GetComponent<HingeJoint>());

            }
       
            grabbedObject = null;
            anim.SetBool("IsPushing", false);
            AudioManager.main.Stop(cajaAudioClip);
            shouldCheckForBoxes = true;

        }
       
        if (!cursed.isGhost)
        {
            if (Input.GetKeyDown(KeyCode.F) && shouldCheckForBoxes)
            {
                CheckForNearbyBoxes();
                shouldCheckForBoxes = false; // Desactiva la verificación después de agarrar el objeto.
            }
            if (Input.GetKeyDown(KeyCode.F) && grabbedObject != null && !hasObjectGrabbed)
            {
              
                hasObjectGrabbed = true;
                grabbedObject.AddComponent<HingeJoint>().connectedBody = playerRigidBody;
                anim.SetBool("IsPushing", true);
                bool randomBool = Random.value > 0.5f;
                cajaAudioClip = randomBool ? "Caja1" : "Caja2";
                AudioManager.main.Play(cajaAudioClip);
            }


            else if (hasObjectGrabbed && Input.GetKeyDown(KeyCode.F))
            {
                hasObjectGrabbed = false;
                hasObjectToGrab = false;
                Destroy(grabbedObject.GetComponent<HingeJoint>());
                grabbedObject = null;
                anim.SetBool("IsPushing", false);
                AudioManager.main.Stop(cajaAudioClip);
                shouldCheckForBoxes = true;
            }
        }
      
    }

    private void CheckForNearbyBoxes()
    {
        if (!hasObjectToGrab && !isGhost)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, checker.bounds.extents.magnitude);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Box"))
                {
                    hasObjectToGrab = true;
                    grabbedObject = collider.gameObject;
                    break;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box") && !hasObjectGrabbed && !isGhost )
        {     
            hasObjectToGrab = true;
            grabbedObject = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        //hasObjectToGrab = true;
 
    }
}
