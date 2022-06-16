using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum MenuState { Main, Settings, NewGame };

public class MainMenuManager : MonoBehaviour
{
    [HideInInspector] public static MainMenuManager Instance;

    MenuState _menuState;

    public GameObject main;
    public GameObject newGame;
    public GameObject settings;
    public TMP_InputField inputFieldSeed;

    bool isLaunching = false;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        Time.timeScale = 1;
        SelectMenu("Main");

        //Show Run Seed
        inputFieldSeed.text = GameManager.instance.RunSeed.ToString();
    }

    public void SelectMenu(string menuName)
    {
        if (!isLaunching)
        {
            switch (menuName)
            {
                case "Main":
                    newGame.SetActive(false);
                    settings.SetActive(false);
                    main.SetActive(true);
                    break;
                case "Settings":
                    newGame.SetActive(false);
                    main.SetActive(false);
                    settings.SetActive(true);
                    break;
                case "NewGame":
                    main.SetActive(false);
                    settings.SetActive(false);
                    newGame.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    public void LaunchGame(TextMeshProUGUI seedText)
    {
        if (!isLaunching)
        {
            isLaunching = true;
            
            string seedString = "";
            string numbers = "-0123456789";
            foreach (char mchar in seedText.text)
                if (numbers.Contains(mchar))
                    seedString += mchar;

            int seed = 0;

            if (int.TryParse(seedString, out seed))
            {
                GameManager.instance.SetRunSeed(seed);
                SceneManager.LoadSceneAsync(Constants.GameLevel);
            }
            else
                SceneManager.LoadSceneAsync(Constants.GameLevel);

            GameManager.instance.StartGame();
        }
    }

    public void Quit() => Application.Quit();
}
