using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    public GameObject attackRange;
    public Rigidbody rb;
    public Animator _anim;   
    private float timer = 0f;
    public float maxTimer;
    public bool isAttacking = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
            AttackView();
            // rb.constraints = RigidbodyConstraints.FreezePositionY;
            attackRange.SetActive(true);
            timer = maxTimer;
        }

        if (isAttacking)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                // rb.constraints = RigidbodyConstraints.None;
                // rb.constraints = RigidbodyConstraints.FreezeRotation;
                attackRange.SetActive(false);
                isAttacking = false;
            }
        }

    }
    public void AttackView()
    {
        _anim.SetTrigger("ScytheAttackAnimation");
        bool randomBool = Random.value > 0.5f;
        AudioManager.main.Play(randomBool ? "Ataque1" : "Ataque2");
    }
}

    
    