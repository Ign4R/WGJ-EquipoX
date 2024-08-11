using UnityEngine;
using UnityEngine.UI;


public class LifeManager : MonoBehaviour
{
    public int lives = 3;
    public Image[] lifeImages;
    private PlayerMovement pm;
    public ThirdPersonCamera tpc;
    public AttackBehaviour ab;

    private void Start()
    {
        UpdateLivesUI();  
        pm = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LoseLife();  
        }
    }

    private void LoseLife()
    {
        if (lives > 0)
        {
            lives--;  
            UpdateLivesUI(); 

            if (lives <= 0)
            {
                pm.anim.SetBool("IsDead", true);
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
        pm.enabled = false;
        tpc.enabled = false;
        ab.enabled = false;
        Debug.Log("Game Over");
    }

}


