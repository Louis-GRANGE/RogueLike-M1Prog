using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    Image _iconWeapon;
    TextMeshProUGUI _ammoText;
    Animator _animator;

    private void Awake()
    {
        _iconWeapon = transform.GetChild(0).GetComponent<Image>();
        _ammoText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _animator = transform.GetComponent<Animator>();
    }

    public void UpdateWeapon(Sprite newWeaponSprite, int ammo)
    {
        _iconWeapon.sprite = newWeaponSprite;
        if(ammo > 30)
            _ammoText.text = "<sprite=0> <color=green>" + ammo + "</color>";
        else if (ammo > 0)
            _ammoText.text = "<sprite=0> <color=yellow>" + ammo + "</color>";
        else
            _ammoText.text = "<sprite=0> <color=red>" + ammo + "</color>";

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
