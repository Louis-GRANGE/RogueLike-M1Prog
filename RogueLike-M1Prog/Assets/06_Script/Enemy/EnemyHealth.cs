using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    int _health;

    [Header("HealthBar")]
    Image _healthBar;

    [Header("HealthBar Memory")]
    float _memorizedHealth;
    Image _healthBarMemory;
    Animator _healthBarMemoryAnim;
    float _memorizeLatency;

    private void Start()
    {
        _health = maxHealth;
        _memorizedHealth = _health;

        FollowingUI followingUI = Instantiate(Resources.Load<GameObject>("UI/HealthPanel"), FollowingUIPanel.Instance.transform).GetComponent<FollowingUI>();
        followingUI.followedRenderer = transform.GetComponentInChildren<Renderer>();

        _healthBarMemory = followingUI.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        _healthBar = followingUI.transform.GetChild(0).GetChild(2).GetComponent<Image>();

        _healthBarMemoryAnim = _healthBarMemory.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, maxHealth);

        _healthBar.fillAmount = (float)_health/(float)maxHealth;
        _healthBarMemoryAnim.SetTrigger("Hit");

        _memorizeLatency = 0;
    }

    private void Update()
    {
        if(_memorizedHealth > _health)
        {
            
            if(_memorizeLatency < 1f)
                _memorizeLatency += Time.deltaTime;
            else
            {
                _memorizedHealth -= (_memorizedHealth - _health) / 10;
                _healthBarMemory.fillAmount = _memorizedHealth / (float)maxHealth;
            }
        }
    }
}
