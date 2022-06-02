using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    Player _Player;

    public Image healthBar;
    public Image healthBarMemory;

    [Header("HealthBar Memory")]
    public float _memorizedHealth;
    Animator _healthBarMemoryAnim;
    float _memorizeLatency;

    private void Awake()
    {
        _healthBarMemoryAnim = healthBarMemory.GetComponent<Animator>();
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
