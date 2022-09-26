using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DevionGames;


public class SceneChanger : MonoBehaviour
{
    [SerializeField] GameObject player, uiCanvas, endUI;
    [SerializeField] DevionGames.ThirdPersonCamera cameraController;

    int levelIndex;
    float loadTime;
    bool runOnce, paused;

    Rigidbody playerRb;
    Collider playerCol;
    


    private void Awake()
    {
        if(!SceneManager.GetActiveScene().name.Contains("StartScene")) DontDestroyOnLoad(this.gameObject);
        levelIndex = 1;
        playerCol = player.GetComponent<CapsuleCollider>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && Vector3.Distance(player.transform.position, transform.position) <= 1.5f)
            {
                AudioListener.volume = 0;
                cameraController.enabled = false;
                paused = true;
                playerCol.isTrigger = true;
                playerRb.isKinematic = true;
                ShowMouse();
                uiCanvas.SetActive(true);
            }
        }

        if (SceneManager.GetActiveScene().name.Contains("LoadScene"))
        {
            if (!runOnce)
            {
                loadTime = Time.time + 8;
                //Time.timeScale = 1;
                runOnce = true;
            }

            if (Time.time > loadTime)
            {
                MoveToNextScene();
            }
        }

        
        
    }

    public void MoveToNextScene() 
    {
        if (levelIndex < 3)
        {
            //Time.timeScale = 1;
            MovePlayer();
            CloseMenu();
            HideMouse();
            levelIndex++;
            runOnce = false;
            SceneManager.LoadScene("Level " + levelIndex.ToString());
        }
        else if (levelIndex == 3)
        {
            SceneManager.LoadScene("EndScene");

            if (player != null)
            {
                player.GetComponent<ThirdPersonController>().enabled = false;
                MovePlayer();
                CloseMenu();
                endUI.SetActive(true);
                ShowMouse();
                
            }
        }
    }

    //Call from teleport button to go to load scene
    public void GoToLoadScene()
    {
        MovePlayer();
        CloseMenu();
        //Time.timeScale = 1;
        SceneManager.LoadScene("LoadScene");
    }

    

    public void CloseMenu() 
    {
        if (levelIndex != 3)
            cameraController.enabled = true;
        
        AudioListener.volume = 1;
        paused = false;
        uiCanvas.SetActive(false);
        playerCol.isTrigger = false;
        playerRb.isKinematic = false;
        
    }

    void ShowMouse() 
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void HideMouse() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MovePlayer() 
    {
        player.transform.position = new Vector3(0, -27.1700001f, 0.649999976f);
        player.transform.rotation = Quaternion.Euler(Vector3.zero);
        cameraController.transform.position = new Vector3(-0.0866289735f, -26.9265347f, -1.85045981f);
        cameraController.transform.rotation = Quaternion.Euler(new Vector3(0.225000039f, 4.2750001f, 0));

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}
