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

    [Header("Saved Game")]
    public GameObject ContinueButton;
    public GameObject SavedGame;
    [Space]
    public TextMeshProUGUI DifficultyText;
    public TextMeshProUGUI SeedText;
    [Space]
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI WeaponText;
    public TextMeshProUGUI AmmoText;
    [Space]
    public TextMeshProUGUI KillsText;
    public TextMeshProUGUI DamagesDealtText;
    public TextMeshProUGUI DamagesTakenText;

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

        ShowSavedGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            GameManager.instance.Save(true);
            ShowSavedGame();
        }
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

    public void ContinueGame()
    {
        GameManager.instance.SetRunSeed(GameManager.instance.GameSave.Seed);
        GameManager.instance.Difficulty = GameManager.instance.GameSave.Difficulty;
        SceneManager.LoadSceneAsync(Constants.GameLevel);

        GameManager.instance.StartGame();
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

    public void ShowSavedGame()
    {
        SaveScriptableObject savedGame = GameManager.instance.GameSave;
        if (savedGame.CanContinue)
        {
            DifficultyText.text = "Difficulty : <color=white>" + savedGame.Difficulty + "</color>";
            SeedText.text = "Seed : <color=white>" + savedGame.Seed + "</color>";

            HealthText.text = "Health : <color=green>" + savedGame.Health + "</color>";
            if(savedGame.EquipedWeapon)
                WeaponText.text = "Weapon : <color=green>" + savedGame.EquipedWeapon.name + "</color>";
            AmmoText.text = "Ammo : <color=green>" + savedGame.Munitions + "</color>";

            KillsText.text = "Kills : <color=green>" + savedGame.NumberKills + "</color>";
            DamagesDealtText.text = "Damages dealt : <color=green>" + savedGame.DamagesDealt + "</color>";
            DamagesTakenText.text = "Damages taken : <color=red>" + savedGame.DamagesTaken + "</color>";

            SavedGame.SetActive(true);
            ContinueButton.SetActive(true);
        }
        else
        {
            SavedGame.SetActive(false);
            ContinueButton.SetActive(false);
        }
    }

    public void Quit() => Application.Quit();
}
