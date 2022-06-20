using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseMenu : MonoBehaviour
{
    public TMPro.TMP_Text Difficulty;
    public TMPro.TMP_Text Kills;
    public TMPro.TMP_Text DamageDeal;
    public TMPro.TMP_Text DamageTaked;
    private void Start()
    {
        MenuManager.instance.looseMenu = this;
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        Debug.Log("BACK TO MENU");
        GameManager.instance.Save(true);
        SceneManager.LoadSceneAsync(Constants.MainMenu);
    }

    public void ShowMenu()
    {
        Difficulty.text += GameManager.instance.Difficulty;
        Kills.text += Player.Instance.playerStats.NumberKills;
        DamageDeal.text += Player.Instance.playerStats.DamagesDeals;
        DamageTaked.text += Player.Instance.playerStats.DamageTaked;
        gameObject.SetActive(true);
    }
}
