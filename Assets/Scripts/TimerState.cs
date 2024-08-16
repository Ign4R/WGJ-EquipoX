using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerState : MonoBehaviour
{
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


    private void Update()
    {
        if (lifeManager.lives < 1)
        {
            timeSlider.gameObject.SetActive(false);
        }
      
    }

    private void ResetState()
    {
        currentState = GameState.State1;
        timeSlider.value = maxTimeState1;
        timeSlider.fillRect.GetComponent<Image>().color = estado1Color;
        Debug.Log("Estados reseteados");
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
                    lifeManager.LoseLife(true);  
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

    public void GrabItem()
    {
        StopAllCoroutines(); // Detiene todos los coroutines en ejecución
        ResetState(); // Resetea los estados y variables
        StartCoroutine(StateLoop()); // Reinicia el ciclo de estados
        Debug.Log("Estado Normal");
    }
}
