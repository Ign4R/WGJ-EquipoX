using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedController : MonoBehaviour
{
    public bool isGhost;
    public TimerState timerState;
    public Vector3 cursedPosition; // TODO: Restaruar a esta posicion cuando toma el agua luego de ser fantasma
    public Rigidbody rigidbody;
    public Collider collider;

    public GameObject[] nonGhostPrefabs;
    public GameObject[] ghostPrefabs;
    public List<GameObject> itemsMagic;

    public PlayerMovement playerMovement;

    void Update()
    {
        if(timerState.currentState == GameState.State1 && isGhost)
        {
            ViewItemMagic(false);
            gameObject.layer = 6;
            rigidbody.isKinematic = false;
            playerMovement.indexAnim = 0;
            playerMovement.canJump = true;
            rigidbody.useGravity = true;
            collider.isTrigger = false;
            isGhost = false;
            foreach (var item in nonGhostPrefabs)
            {
                item.SetActive(true);
            }
            foreach (var item in ghostPrefabs)
            {
                item.SetActive(false);
            }
            RenderSettings.fog = false;
        }

        if(timerState.currentState == GameState.State2 && !isGhost)
        {
            ViewItemMagic(true);
            gameObject.layer = 9;
            playerMovement.indexAnim = 1;
            playerMovement.canJump = true;
            cursedPosition = timerState.gameObject.transform.position;
            isGhost = true;
 
           
            foreach (var item in nonGhostPrefabs)
            {
                item.SetActive(false);
            }
            foreach (var item in ghostPrefabs)
            {
                item.SetActive(true);
            }
            RenderSettings.fog = true;

        }


       
    }
    public void ViewItemMagic(bool value)
    {
        foreach (var item in itemsMagic)
        {
            if (item != null)
            {
                item.SetActive(value);
            }
        }
    }
}
