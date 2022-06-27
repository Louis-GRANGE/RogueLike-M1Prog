using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public TMPro.TMP_InputField inputFieldSeed;

    private void Awake()
    {
        Player.Instance.playerInputs.inputs.Player.Pause.performed += TogglePause;
    }

    private void Start()
    {
        MenuManager.instance.pauseMenu = this;
        inputFieldSeed.text = GameManager.instance.RunSeed.ToString();

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Player.Instance.playerInputs.inputs.Player.Pause.performed -= TogglePause;
    }


    private void Update()
    {

    }

    public void TogglePause(UnityEngine.InputSystem.InputAction.CallbackContext context)
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
        GameManager.instance.Save(false);
        SceneManager.LoadSceneAsync(Constants.MainMenu);
        Destroy(Player.Instance.gameObject);
    }
}
