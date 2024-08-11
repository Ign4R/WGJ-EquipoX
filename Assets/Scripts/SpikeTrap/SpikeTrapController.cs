using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{
    public Transform[] spikes; // Array de espinas
    public Vector3 spikeMoveDirection = Vector3.up; // Dirección en la que se moverán las espinas
    public float spikeMoveDistance = 2.0f; // Distancia que recorrerán las espinas
    public float spikeExtendSpeed = 2.0f; // Velocidad de extensión de las espinas
    public float spikeRetractSpeed = 2.0f; // Velocidad de retracción de las espinas
    public float rateOfFire = 1.0f; // Tiempo entre cada salida de espinas

    private Vector3[] initialPositions; // Posiciones iniciales de las espinas

    void Start()
    {
        // Guardar las posiciones iniciales de las espinas
        initialPositions = new Vector3[spikes.Length];
        for (int i = 0; i < spikes.Length; i++)
        {
            initialPositions[i] = spikes[i].position;
        }

        // Comenzar la secuencia de activación de las espinas
        StartCoroutine(ActivateSpikes());
    }

    IEnumerator ActivateSpikes()
    {
        while (true)
        {
            // Extender las espinas
            foreach (var spike in spikes)
            {
                StartCoroutine(MoveSpike(spike, spikeMoveDirection * spikeMoveDistance, spikeExtendSpeed));
                yield return new WaitForSeconds(rateOfFire);
            }

            // Esperar un momento antes de retraer las espinas
            yield return new WaitForSeconds(1.0f);

            // Retraer las espinas
            foreach (var spike in spikes)
            {
                StartCoroutine(MoveSpike(spike, Vector3.zero, spikeRetractSpeed));
                yield return new WaitForSeconds(rateOfFire);
            }

            // Esperar un momento antes de repetir
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator MoveSpike(Transform spike, Vector3 targetOffset, float speed)
    {
        Vector3 targetPosition = initialPositions[System.Array.IndexOf(spikes, spike)] + targetOffset;
        while (spike.position != targetPosition)
        {
            spike.position = Vector3.MoveTowards(spike.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
