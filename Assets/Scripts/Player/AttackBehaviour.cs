using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    public Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("ScytheAttackAnimation");
            bool randomBool = Random.value > 0.5f;
            AudioManager.main.Play(randomBool ? "Ataque1" : "Ataque2");
        }
    }
}
