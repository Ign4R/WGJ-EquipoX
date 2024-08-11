using UnityEngine;
using UnityEngine.UI;


public class LifeManager : MonoBehaviour
{
    public int lives = 3;
    public Image[] lifeImages;

    private void Start()
    {
        UpdateLivesUI();  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LoseLife();  
        }
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;  
            UpdateLivesUI(); 

            if (lives <= 0)
            {
                GameOver();  
            }
        }
    }

    private void UpdateLivesUI()
    {      
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i < lives)
            {
                lifeImages[i].enabled = true; 
            }
            else
            {
                lifeImages[i].enabled = false;  
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

}


