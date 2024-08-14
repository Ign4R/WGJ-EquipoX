using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public bool isDead;
    public float destroyAfter;
    public float explosionForce = 20; // Fuerza de la explosión
    public float explosionRadius = 5f; // Radio de la explosión
    public float upwardsModifier = 1f; // Ajuste para levantar un poco los objetos en la explosión
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Breaker") && !isDead)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                isDead = true;
                rb.isKinematic = false;

                // Usar la posición del otro objeto como punto de explosión
                Vector3 explosionPoint = other.transform.position;
                rb.AddExplosionForce(explosionForce, explosionPoint, explosionRadius, upwardsModifier, ForceMode.Impulse);

                Destroy(gameObject, destroyAfter);
                AudioManager.main.Play("BreakWood");
            }
        }
    }

}
