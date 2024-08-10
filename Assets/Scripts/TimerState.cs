using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerState : MonoBehaviour
{
    [SerializeField] private enum GameState { Estado1, Estado2, Estado3 }
    [SerializeField] private GameState currentState;

    [SerializeField] private Slider timeSlider;
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private float waitTimeBetweenStates = 2f;

    private void Start()
    {
        currentState = GameState.Estado1;
        timeSlider.maxValue = maxTime;
        StartCoroutine(StateLoop());
    }

    private IEnumerator StateLoop()
    {
        while (true)
        {
            switch (currentState)
            {
                case GameState.Estado1:
                    yield return StartCoroutine(StateTimer(maxTime));
                    yield return new WaitForSeconds(waitTimeBetweenStates);
                    currentState = GameState.Estado2;
                    break;

                case GameState.Estado2:
                    yield return StartCoroutine(StateTimer(maxTime));
                    yield return new WaitForSeconds(waitTimeBetweenStates);
                    currentState = GameState.Estado3;
                    break;

                case GameState.Estado3:
                    yield return StartCoroutine(StateTimer(maxTime));
                    yield return new WaitForSeconds(waitTimeBetweenStates);
                    Debug.Log("Moriste");
                    yield break;
            }
        }
    }

    private IEnumerator StateTimer(float time)
    {
        while (time > 0)
        {
            timeSlider.value = time;
            yield return new WaitForSeconds(1f);
            time -= 1f;
        }
        timeSlider.value = 0;
    }
}


