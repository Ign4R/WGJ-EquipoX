using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int lives = 3;
    public Image[] lifeImages;
    public GameObject deathPanel;

    private CursedController cursedC;
    private PlayerMovement pm;
    public ThirdPersonCamera tpc;
    public AttackBehaviour ab;

    public Transform respawnPoint; 
    public float delayBeforeGameOverPanel = 0.1f;  

    private void Start()
    {
        UpdateLivesUI();
        pm = GetComponent<PlayerMovement>();
        cursedC = GetComponent<CursedController>();


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
        if (gameObject.layer == 9) return;
        if (lives > 0)
        {
            lives--;
            UpdateLivesUI();

            if (lives <= 0)
            {
                pm.anim[pm.indexAnim].SetBool("IsDead", true);
                Invoke("GameOver", 0); 
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
        pm.anim[pm.indexAnim].SetBool("IsDead", true);
        cursedC.enabled = false;
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
    public void SceneMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void ResetLevel()
    {

        lives = 2;
        UpdateLivesUI();


        pm.anim[pm.indexAnim].SetBool("IsDead", false);

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
