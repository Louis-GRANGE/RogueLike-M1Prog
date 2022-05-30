using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("External References")]
    Camera _mainCamera;

    [Header("Components")]
    public Transform canon;
    ParticleSystem _cannonFire;

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
        _cannonFire = canon.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        _laserPool = new GameObject().transform;
        _laserPool.parent = transform;
        _laserPool.gameObject.SetActive(false);
        for (int i = 0; i < 50; i++)
        {
            GameObject newLaser = Instantiate(laserFX, transform.position, transform.rotation, _laserPool);
            newLaser.GetComponent<LaserEffect>().Initiate(_laserPool);
        }

        _hitPool = new GameObject().transform;
        _hitPool.parent = transform;
        _hitPool.gameObject.SetActive(false);
        for (int i = 0; i < 50; i++)
        {
            GameObject newHit = Instantiate(hitFX, transform.position, transform.rotation, _hitPool);
            newHit.GetComponent<HitEffect>().Initiate(_hitPool);
        }

        _fireRateTime = fireRateLatency;
    }

    private void Update()
    {
        Shoot();
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

            Vector3 shootDirection = (pointDirection - canon.transform.position).normalized;
            if (Physics.Raycast(canon.transform.position, shootDirection, out hit, 1000))
            {
                _laserPool.GetChild(0).GetComponent<LaserEffect>().DrawLine(_cannonFire.transform.position, hit.point);
                _hitPool.GetChild(0).GetComponent<HitEffect>().DrawParticle(hit.point);
            }

            _cannonFire.Play();
        }
    }
}
