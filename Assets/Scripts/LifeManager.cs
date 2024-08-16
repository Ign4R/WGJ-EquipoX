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
    public Slider lifeSlider;
    public int damageAmount=10;

    private void Start()
    {
        
        pm = GetComponent<PlayerMovement>();
        cursedC = GetComponent<CursedController>();


        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }          
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoseLife(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            if (gameObject.CompareTag("Player"))
            {
                LoseLife(false);
            }
        }
    }


    public void LoseLife(bool cursed)
    {

        if (gameObject.layer == 9 && !cursed) return;
        if (lives > 0)
        {
            lives -= damageAmount; // Resta la cantidad de daño
            if (lives < 0) lives = 0; // Asegúrate de que las vidas no sean negativas

            // Actualiza el Slider
            // Convierte las vidas a un rango de 0 a 1
            lifeSlider.value = (float)lives / 100;

            if (lives <= 0)
            {
                pm.anim[pm.indexAnim].SetBool("IsDead", true);
                Invoke("GameOver", 0);
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
