using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
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

    [Header("Sound curve evolution")]
    public AnimationCurve SoundEvolution;

    [Header("Music")]
    public Slider MusicSlider;
    public TextMeshProUGUI MusicText;
    public AudioMixer MusicMixer;
    [Header("Sound")]
    public Slider SoundSlider;
    public TextMeshProUGUI SoundText;
    public AudioMixer SoundMixer;

    [Header("Music")]
    public Slider DifficultySlider;
    public TextMeshProUGUI DifficultySetText;

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

    [Header("Input type")]
    public TextMeshProUGUI InputTypeText;
    public Image InputTypeLogo;
    public Sprite Joystick, Keyboard;
    bool isKeyboard = true;

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
        LoadVolumes();
        LoadInput();
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
                    //if(GameManager.instance.GameSave.CanContinue)
                    if(SaveManager.instance.GetSave<SOSaveGame>().CanContinue)
                        SavedGame.SetActive(true);
                    break;
                case "Settings":
                    newGame.SetActive(false);
                    main.SetActive(false);
                    settings.SetActive(true);
                    SavedGame.SetActive(false);
                    break;
                case "NewGame":
                    main.SetActive(false);
                    settings.SetActive(false);
                    newGame.SetActive(true);
                    SavedGame.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    public void ContinueGame()
    {
        GameManager.instance.Difficulty = SaveManager.instance.GetSave<SOSaveGame>().Difficulty;
        GameManager.instance.SetRunSeed(SaveManager.instance.GetSave<SOSaveGame>().Seed);


        SceneManager.LoadSceneAsync(Constants.GameLevel);

        GameManager.instance.StartGame();
    }

    public void LaunchGame(TextMeshProUGUI seedText)
    {
        if (!isLaunching)
        {
            isLaunching = true;
            SaveManager.instance.GetSave<SOSaveGame>().SetValues();

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
        SOSaveGame savedGame = SaveManager.instance.GetSave<SOSaveGame>();
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

    public void LoadVolumes()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        MusicText.text = Mathf.RoundToInt(MusicSlider.value * 100f) + "%";
        MusicMixer.SetFloat("Volume", -80f * (1f - SoundEvolution.Evaluate(MusicSlider.value)));

        SoundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        SoundText.text = Mathf.RoundToInt(SoundSlider.value * 100f) + "%";
        SoundMixer.SetFloat("Volume", -80f * (1f - SoundEvolution.Evaluate(SoundSlider.value)));
    }

    public void MusicChangeVolume()
    {
        MusicText.text = Mathf.RoundToInt(MusicSlider.value * 100f) + "%";
        MusicMixer.SetFloat("Volume", -80f * (1f - SoundEvolution.Evaluate(MusicSlider.value)));
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
    }

    public void SoundChangeVolume()
    {
        SoundText.text = Mathf.RoundToInt(SoundSlider.value * 100f) + "%";
        SoundMixer.SetFloat("Volume", -80f * (1f - SoundEvolution.Evaluate(SoundSlider.value)));
        PlayerPrefs.SetFloat("SoundVolume", SoundSlider.value);
    }

    public void LoadInput()
    {
        isKeyboard = PlayerPrefs.GetInt("isKeyboard") == 1;

        if (!isKeyboard)
        {
            InputTypeLogo.sprite = Joystick;
            InputTypeText.text = "Joystick";
        }
        else
        {
            InputTypeLogo.sprite = Keyboard;
            InputTypeText.text = "Keyboard";
        }
    }

    public void ChangeInput()
    {
        if (isKeyboard)
        {
            InputTypeLogo.sprite = Joystick;
            InputTypeText.text = "Joystick";
            PlayerPrefs.SetInt("isKeyboard", 0);
        }
        else
        {
            InputTypeLogo.sprite = Keyboard;
            InputTypeText.text = "Keyboard";
            PlayerPrefs.SetInt("isKeyboard", 1);
        }

        isKeyboard = !isKeyboard;
    }

    public void ChangeDifficulty()
    {
        DifficultySetText.text = DifficultySlider.value.ToString();
        GameManager.instance.Difficulty = (int)DifficultySlider.value;
    }
}
