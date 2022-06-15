using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;

    // Start is called before the first frame update
    void Awake()
    {
        PausePanel.SetActive(false);
    }

    public void TogglePause()
    {
        Pause(Time.timeScale != 0);
    }

    public void Pause(bool pause)
    {
        if(pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        PausePanel.SetActive(pause);
    }

    public void BackToMenu() => SceneManager.LoadSceneAsync(0);
}
