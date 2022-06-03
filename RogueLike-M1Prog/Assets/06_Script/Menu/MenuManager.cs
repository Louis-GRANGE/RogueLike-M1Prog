using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState { Main, Settings, NewGame };

public class MenuManager : MonoBehaviour
{
    [HideInInspector] public static MenuManager Instance;

    MenuState _menuState;

    public GameObject main;
    public GameObject newGame;
    public GameObject settings;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SelectMenu("Main");
    }

    public void SelectMenu(string menuName)
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
