using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagic : MonoBehaviour
{
    public TimerState lifePlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            lifePlayer.GrabItem();
            gameObject.SetActive(false);
        }
    }

}
