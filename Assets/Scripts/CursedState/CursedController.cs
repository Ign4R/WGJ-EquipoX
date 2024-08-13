using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedController : MonoBehaviour
{
    public bool isGhost;
    public TimerState timerState;
    public Vector3 cursedPosition;
    public Rigidbody rigidbody;
    public CapsuleCollider capsuleCollider;

    public GameObject[] nonGhostPrefabs;
    public GameObject[] ghostPrefabs;

    public PlayerMovement playerMovement;

    void Update()
    {
        if(timerState.currentState == GameState.State1 && isGhost)
        {
            playerMovement.canJump = true;
            rigidbody.useGravity = true;
            capsuleCollider.isTrigger = false;
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
            playerMovement.canJump = false;
            cursedPosition = timerState.gameObject.transform.position;
            isGhost = true;
            rigidbody.useGravity = false;
            capsuleCollider.isTrigger = true;
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
}
