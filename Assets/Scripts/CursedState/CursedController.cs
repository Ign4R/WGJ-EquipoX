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

    public PlayerMovement playerMovement;

    void Update()
    {
        if(timerState.currentState == GameState.State1 && isGhost)
        {
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
            playerMovement.indexAnim = 1;
            playerMovement.canJump = false;
            cursedPosition = timerState.gameObject.transform.position;
            isGhost = true;
            rigidbody.useGravity = false;
            collider.isTrigger = true;
           
            foreach (var item in nonGhostPrefabs)
            {
                item.SetActive(false);
            }
            foreach (var item in ghostPrefabs)
            {
                item.SetActive(true);
            }
            RenderSettings.fog = true;
           
            playerMovement.RespawnPosition();
            rigidbody.isKinematic = false;
        }
    }
}
