using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    [Header("External Components")]
    Image _iconWeapon;
    TextMeshProUGUI _ammoText;
    TextMeshProUGUI _damageText;
    TextMeshProUGUI _fireRateText;
    Animator _animator;

    private void Start()
    {
        _iconWeapon = transform.GetChild(1).GetComponent<Image>();
        _ammoText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _damageText = _iconWeapon.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _fireRateText = _iconWeapon.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _animator = transform.GetComponent<Animator>();
    }

    public void UpdateWeapon(WeaponData data, int ammo)
    {
        _iconWeapon.sprite = data.icon;
        if(ammo > 30)
            _ammoText.text = "<sprite=0> <color=green>" + ammo + "</color>";
        else if (ammo > 0)
            _ammoText.text = "<sprite=0> <color=yellow>" + ammo + "</color>";
        else
            _ammoText.text = "<sprite=0> <color=red>" + ammo + "</color>";

        _damageText.text = "<sprite=2> <color=yellow>" + data.damages;
        _fireRateText.text = "<sprite=1> <color=yellow>" + (1 / data.fireLatency).ToString() + " / sec";

        _animator.SetTrigger("Weapon");
    }

    public void UpdateAmmo(int ammo)
    {
        if (ammo > 30)
            _ammoText.text = "<sprite=0> <color=green>" + ammo + "</color>";
        else if (ammo > 0)
            _ammoText.text = "<sprite=0> <color=yellow>" + ammo + "</color>";
        else
            _ammoText.text = "<sprite=0> <color=red>" + ammo + "</color>";

        _animator.SetTrigger("Ammo");
    }
}
