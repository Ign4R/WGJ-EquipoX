using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagic : MonoBehaviour
{
    public TimerState lifePlayer;
    public GameObject blockTable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            blockTable.SetActive(false);
            lifePlayer.GrabItem();
            Destroy(gameObject);
        }
    }

}
