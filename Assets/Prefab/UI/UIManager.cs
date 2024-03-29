using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] AudioClip gameOverSound;
    [Header("Pause")]
    [SerializeField] GameObject pauseScreen;



    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region Game Over

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //exits play mode (will only be executed in the editor)
        #endif
    }
    #endregion

    #region Pause

    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(.2f);
    }
    #endregion

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
