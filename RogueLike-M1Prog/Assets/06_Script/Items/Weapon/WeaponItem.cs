using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponItem : AItem
{
    [Header("Data")]
    public WeaponData weaponData;
    public int munitions;
    Player _player;

    private void Start()
    {
        _player = Player.Instance;
    }

    public override void ActualizeShown()
    {
        base.ActualizeShown();
        
        _followingUI.followedRenderer = GetComponentInChildren<Renderer>();
        
        _equipText = _followingUI.transform.GetChild(0).GetChild(5).gameObject;
        /*_icon =*/ _followingUI.transform.GetChild(0).GetChild(4).GetComponent<Image>().sprite = weaponData.icon;// _icon;
        
        //_iconWeapon.sprite = weaponData.icon;

        string damages = "";
        if (weaponData.damages > _player.playerWeaponManager.damages)
            damages += "<sprite=2> <color=green>" + weaponData.damages + "</color>";
        else if (weaponData.damages == _player.playerWeaponManager.damages)
            damages += "<sprite=2> <color=yellow>" + weaponData.damages + "</color>";
        else
            damages += "<sprite=2> <color=red>" + weaponData.damages + "</color>";

        _followingUI.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = damages;

        string fireRate = "";
        if (weaponData.fireLatency < _player.playerWeaponManager.weapon.fireRateLatency)
            fireRate += "<sprite=1> <color=green>" + 1 / weaponData.fireLatency + "</color> <color=yellow>/ sec</color>";
        else if (weaponData.fireLatency == _player.playerWeaponManager.weapon.fireRateLatency)
            fireRate += "<sprite=1> <color=yellow>" + 1 / weaponData.fireLatency + "</color> <color=yellow>/ sec</color>";
        else
            fireRate += "<sprite=1> <color=red>" + 1 / weaponData.fireLatency + "</color> <color=yellow>/ sec</color>";

        _followingUI.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = fireRate;

        string ammo = "";
        if (munitions > _player.playerWeaponManager.weapon._munitions)
            ammo += "<sprite=0> <color=green>" + munitions + "</color>";
        else if (munitions == _player.playerWeaponManager.weapon._munitions)
            ammo += "<sprite=0> <color=yellow>" + munitions + "</color>";
        else
            ammo += "<sprite=0> <color=red>" + munitions + "</color>";

        _followingUI.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = ammo;

        //_followingUI.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Update()
    {
        /*if (_player)
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            if (distance <= 3 && !_equipText.activeSelf)
                _equipText.SetActive(true);
            else if (distance > 3 && _equipText.activeSelf)
                _equipText.SetActive(false);
        }*/
    }

    //public override void HideShown() => _followingUI.transform.GetChild(0).gameObject.SetActive(false);
}
