using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int lives = 3;
    public Image[] lifeImages;
    public GameObject deathPanel;  

    private PlayerMovement pm;
    public ThirdPersonCamera tpc;
    public AttackBehaviour ab;

    public Transform respawnPoint; 
    public float delayBeforeGameOverPanel = 2f;  

    private void Start()
    {
        UpdateLivesUI();
        pm = GetComponent<PlayerMovement>();

     
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }          
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("Enemy"))
        {         
            if (gameObject.CompareTag("Player"))
            {
                LoseLife();
            }
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
                pm.anim.SetBool("IsDead", true);
                Invoke("GameOver", delayBeforeGameOverPanel); 
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
        //ab.enabled = false;
        Debug.Log("Game Over");


        if (deathPanel != null)
        {
            deathPanel.SetActive(true);


            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void ResetLevel()
    {

        lives = 2;
        UpdateLivesUI();


        pm.anim.SetBool("IsDead", false);

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
        }


        pm.enabled = true;
        tpc.enabled = true;
        //ab.enabled = true;


        if (deathPanel != null)
        {
            deathPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
