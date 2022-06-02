using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    Player _Player;

    Image healthBar;
    Image healthBarMemory;

    [Header("HealthBar Memory")]
    float _memorizedHealth;
    Animator _healthBarMemoryAnim;
    float _memorizeLatency;

    private void Awake()
    {
        healthBar = transform.GetChild(4).GetComponent<Image>();
        healthBarMemory = transform.GetChild(3).GetComponent<Image>();
    }

    private void Start()
    {
        _Player = Player.Instance;
    }

    public void UpdateHealth()
    {
        healthBar.fillAmount = Mathf.Clamp((float)_Player.HealthManager.health / (float)_Player.HealthManager.maxHealth, 0f, 1f);
        _healthBarMemoryAnim.SetTrigger("Hit");
        _memorizeLatency = 0;
    }

    private void Update()
    {
        if (_memorizedHealth > _Player.HealthManager.health)
        {
            if (_memorizeLatency < 0.5f)
                _memorizeLatency += Time.deltaTime;
            else
            {
                _memorizedHealth -= (_memorizedHealth - _Player.HealthManager.health) / 10;
                healthBarMemory.fillAmount = _memorizedHealth / (float)_Player.HealthManager.maxHealth;
            }
        }
    }
}
