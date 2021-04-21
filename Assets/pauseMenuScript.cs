using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    private static bool isGameOver, isYouWon;
    public GameObject pasueUI;
    private static GameObject GameOver;
    private static GameObject YouWon;
    [SerializeField] public float x;
    // Update is called once per frame
    
    private void Start()
    {
        GameOver = this.transform.GetChild(1).gameObject;
        YouWon = this.transform.GetChild(2).gameObject;
        isGameOver = false;
        isYouWon = false;
        gameIsPaused = false;
        Time.timeScale = 1f;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&(!isGameOver&& !isYouWon))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                 Pause();
            }
        }
        if (Input.GetMouseButtonDown(0)&&(isGameOver||isYouWon))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void Resume()
    {
        pasueUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void Pause()
    {
        pasueUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;

    }
    public static void gameOver()
    {
        GameOver.SetActive(true);
        Time.timeScale = 0f;
        isGameOver = true;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public static void youWon()
    {
        YouWon.SetActive(true);
        Time.timeScale = 0f;
        isYouWon = true;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
