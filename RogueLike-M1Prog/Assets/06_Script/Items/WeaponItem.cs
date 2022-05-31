using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponItem : IItem
{
    Player _Player;

    public WeaponData weaponData;

    FollowingUI _followingUI;

    private void Start()
    {
        _Player = Player.Instance;

        _followingUI = Instantiate(Resources.Load<GameObject>("UI/WeaponPanel"), FollowingUIPanel.Instance.transform).GetComponent<FollowingUI>();
        _followingUI.followedRenderer = GetComponentInChildren<Renderer>();
        
        HideShown();
    }

    public void ActualizeShown()
    {
        string damages = "";
        if (weaponData.damages > _Player.playerShoot.damages)
            damages += "<color=green>" + weaponData.damages + "</color> <color=yellow>damages</color>";
        else if (weaponData.damages == _Player.playerShoot.damages)
            damages += "<color=yellow>" + weaponData.damages + "</color> <color=yellow>damages</color>";
        else
            damages += "<color=red>" + weaponData.damages + "</color> <color=yellow>damages</color>";

        _followingUI.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = damages;

        string fireRate = "";
        if (weaponData.fireLatency < _Player.playerShoot.fireRateLatency)
            fireRate += "<color=green>" + 1 / weaponData.fireLatency + "</color> <color=yellow>/ sec.</color>";
        else if (weaponData.fireLatency == _Player.playerShoot.fireRateLatency)
            fireRate += "<color=yellow>" + 1 / weaponData.fireLatency + "</color> <color=yellow>/ sec.</color>";
        else
            fireRate += "<color=red>" + 1 / weaponData.fireLatency + "</color> <color=yellow>/ sec.</color>";

        _followingUI.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = fireRate;

        _followingUI.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HideShown() => _followingUI.transform.GetChild(0).gameObject.SetActive(false);

    public void Desactivate()
    {
        Destroy(_followingUI.gameObject);
        Destroy(gameObject);
    }
}
