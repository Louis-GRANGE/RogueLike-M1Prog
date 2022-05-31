using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("External References")]
    Camera _mainCamera;

    [Header("External Components")]
    public Transform weaponHandler;
    Transform _canon;
    Weapon _weapon;

    [Header("Internal Components")]
    ParticleSystem _cannonFire;
    Animator _animator;

    [Header("Effects")]
    public GameObject laserFX;
    Transform _laserPool;
    public GameObject hitFX;
    Transform _hitPool;

    [Header("FireRate")]
    public float fireRateLatency;
    float _fireRateTime;

    private void Awake()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        //EquipWeapon(Resources.Load<WeaponData>("WeaponData/Automatic"));
    }

    private void Update()
    {
        if(_weapon)
            Shoot();

        Interact();
    }

    public void Shoot()
    {
        if(_fireRateTime < fireRateLatency)
        {
            _fireRateTime += Time.deltaTime;
        }

        else if (Input.GetMouseButton(0))
        {
            _fireRateTime = 0;
            Vector3 pointDirection = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(pointDirection, _mainCamera.transform.forward, out hit, 1000))
                pointDirection = hit.point;

            pointDirection += new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
            Vector3 shootDirection = (pointDirection - _canon.transform.position).normalized;
            if (Physics.Raycast(_canon.transform.position, shootDirection, out hit, 1000))
            {
                
                _laserPool.GetChild(0).GetComponent<LaserEffect>().DrawLine(_cannonFire.transform.position, hit.point);
                _hitPool.GetChild(0).GetComponent<HitEffect>().DrawParticle(hit.point);
            }
            
            _cannonFire.Play();
        }
    }

    public void EquipWeapon(WeaponData _newWeapon)
    {
        _weapon = Instantiate(_newWeapon.weaponPrefab, weaponHandler).GetComponent<Weapon>();
        _canon = _weapon.canon;
        _cannonFire = _canon.GetChild(0).GetComponent<ParticleSystem>();

        fireRateLatency = _newWeapon.fireLatency;

        if(_laserPool)
            Destroy(_laserPool.gameObject);

        _laserPool = new GameObject().transform;
        _laserPool.parent = transform;
        _laserPool.gameObject.SetActive(false);
        for (int i = 0; i < 50; i++)
        {
            GameObject newLaser = Instantiate(laserFX, transform.position, transform.rotation, _laserPool);
            newLaser.GetComponent<LaserEffect>().Initiate(_laserPool);
        }

        if(_hitPool)
            Destroy(_hitPool.gameObject);

        _hitPool = new GameObject().transform;
        _hitPool.parent = transform;
        _hitPool.gameObject.SetActive(false);
        for (int i = 0; i < 50; i++)
        {
            GameObject newHit = Instantiate(hitFX, transform.position, transform.rotation, _hitPool);
            newHit.GetComponent<HitEffect>().Initiate(_hitPool);
        }

        _fireRateTime = fireRateLatency;

        _animator.SetTrigger("Equip");
    }

    public void Interact()
    {
        Vector3 pointDirection = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(pointDirection, _mainCamera.transform.forward, out hit, 1000))
        {
            if (hit.collider.GetComponent<WeaponItem>() && Vector3.Distance(transform.position, hit.point) < 3f && Input.GetKeyDown(KeyCode.E))
            {
                Destroy(_weapon);
                EquipWeapon(hit.collider.GetComponent<WeaponItem>().weaponData);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
