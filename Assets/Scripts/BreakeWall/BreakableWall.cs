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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Breaker") && !isDead)
        {
            var rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                isDead = true;
                rb.isKinematic = false;
                Vector3 contactPoint = collision.contacts[0].point;
                rb.AddExplosionForce(explosionForce, contactPoint, explosionRadius, upwardsModifier, ForceMode.Impulse);
                Destroy(this.gameObject,destroyAfter);
            }
        }
    }
}
