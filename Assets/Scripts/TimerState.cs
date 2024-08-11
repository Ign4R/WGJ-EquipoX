using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerState : MonoBehaviour
{
    public enum GameState { State1, State2 }
    public GameState currentState;

    [SerializeField] private Slider timeSlider;
    [SerializeField] private float maxTimeState1 = 60f;
    [SerializeField] private float maxTimeState2 = 60f;
    [SerializeField] private float waitTimeBetweenStates = 2f;

    [SerializeField] private LifeManager lifeManager;  
    [SerializeField] private Color estado1Color = Color.green;
    [SerializeField] private Color estado2Color = Color.red;

    private void Start()
    {
        currentState = GameState.State1;
        timeSlider.maxValue = maxTimeState1;
        timeSlider.fillRect.GetComponent<Image>().color = estado1Color; 
        StartCoroutine(StateLoop());
    }

    private IEnumerator StateLoop()
    {
        while (true)
        {
            switch (currentState)
            {
                case GameState.State1:
                    Debug.Log("Estado Normal");
                    yield return StartCoroutine(StateTimer(maxTimeState1, 1f));
                    yield return new WaitForSeconds(waitTimeBetweenStates);
                    currentState = GameState.State2;
                    timeSlider.fillRect.GetComponent<Image>().color = estado2Color;  
                    break;

                case GameState.State2:
                    Debug.Log("Estado Fantasma");
                    yield return StartCoroutine(StateTimer(maxTimeState2, 1f)); 
                    yield return new WaitForSeconds(waitTimeBetweenStates);
                    currentState = GameState.State1;
                    timeSlider.fillRect.GetComponent<Image>().color = estado1Color;  
                    lifeManager.LoseLife();  
                    break;
            }
        }
    }

    private IEnumerator StateTimer(float time, float timeFactor)
    {
        while (time > 0)
        {
            timeSlider.value = time;
            yield return new WaitForSeconds(1f * timeFactor); 
            time -= 1f;
        }
        timeSlider.value = 0;
    }
}
