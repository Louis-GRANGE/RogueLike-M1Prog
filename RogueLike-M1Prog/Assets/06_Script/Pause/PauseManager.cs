using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public TMPro.TMP_InputField inputFieldSeed;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        inputFieldSeed.text = GameManager.instance.RunSeed.ToString();
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

        gameObject.SetActive(pause);
    }

    public void BackToMenu()
    {
        Debug.Log("BACK TO MENU");
        SceneManager.LoadSceneAsync(0);
    }
}
