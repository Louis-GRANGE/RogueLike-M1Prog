using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : AHealth
{
    AMainData ownerMainData;
    DamageTextPool _damageTextPool;

    [Header("HealthBar")]
    Image _healthBar;

    [Header("HealthBar Memory")]
    float _memorizedHealth;
    Image _healthBarMemory;
    Animator _healthBarMemoryAnim;
    float _memorizeLatency;

    private void Awake()
    {
        ownerMainData = GetComponent<AMainData>();
        ownerMainData.HealthManager = this;
    }

    protected override void Start()
    {
        base.Start();
        _memorizedHealth = health;

        FollowingUI followingUI = Instantiate(Resources.Load<GameObject>("UI/HealthPanel"), FollowingUIPanel.Instance.transform.GetChild(0)).GetComponent<FollowingUI>();
        followingUI.followedRenderer = transform.GetComponentInChildren<Renderer>();

        _healthBarMemory = followingUI.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        _healthBar = followingUI.transform.GetChild(0).GetChild(2).GetComponent<Image>();

        _healthBarMemoryAnim = _healthBarMemory.GetComponent<Animator>();

        _damageTextPool = DamageTextPool.Instance;
    }

    public override void TakeDamage(int damage, GameObject Sender)
    {
        base.TakeDamage(damage, Sender);
        _healthBar.fillAmount = (float)health / (float)maxHealth;
        _healthBarMemoryAnim.SetTrigger("Hit");

        _memorizeLatency = 0;

        _damageTextPool.RequestDamageText(transform.position, damage);
        
    }

    private void Update()
    {
        if(_memorizedHealth > health)
        {
            if(_memorizeLatency < 0.5f)
                _memorizeLatency += Time.deltaTime;
            else
            {
                _memorizedHealth -= (_memorizedHealth - health) / 10;
                _healthBarMemory.fillAmount = _memorizedHealth / (float)maxHealth;
            }
        }
    }

    public override void OnDeath(GameObject Sender)
    {
        Destroy(_healthBar.transform.parent.parent.gameObject);

        GameObject ragdoll = Instantiate(Resources.Load<GameObject>("Ragdoll/EnemyRagdoll"), transform.position, transform.rotation);

        ragdoll.transform.position -= new Vector3(0, 1, 0);

        ragdoll.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().AddForce(-(Sender.transform.position - transform.position).normalized * 500, ForceMode.Impulse);

        if(Random.Range(0, 3) == 0 && ownerMainData.WeaponManager._weaponData)
            DropWeapon(ownerMainData.WeaponManager._weaponData, Random.Range(50, 100));

        Destroy(gameObject);
    }

    void DropWeapon(WeaponData _droppedWeapon, int munitions)
    {
        Vector3 dropPosition = transform.position + transform.forward;
        GameObject droppedWeapon = Instantiate(_droppedWeapon.weaponItem, dropPosition, transform.rotation);

        droppedWeapon.GetComponent<WeaponItem>().munitions = munitions;

        droppedWeapon.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
    }
}
