using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class AExplode : MonoBehaviour
{
    protected CameraEffect _cameraEffect;
    protected List<AHealth> ToDealDamage;

    [Header("External")]
    public ParticleSystem VFX_Explosion;
    public int ExplodeDamage;
    public float TimeBeforeExplode = 0;
    public float ExplosionForce = 500000;

    [SerializeField]
    private float ExplodeSize;

    protected virtual void Start()
    {
        _cameraEffect = CameraEffect.Instance;
        ToDealDamage = new List<AHealth>();
        ExplodeSize = GetComponent<SphereCollider>().radius - 0.5f;
    }

    public virtual void Explode()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(ExplodeAfterTime());
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        AHealth aHealth;
        if (other.TryGetComponent<AHealth>(out aHealth))
        {
            ToDealDamage.Add(aHealth);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        AHealth aHealth;
        if (other.TryGetComponent<AHealth>(out aHealth))
        {
            ToDealDamage.Remove(aHealth);
        }
    }

    protected IEnumerator ExplodeAfterTime()
    {
        yield return new WaitForSeconds(TimeBeforeExplode);
        ParticleSystem VFX = Instantiate(VFX_Explosion, transform.position, Quaternion.identity);
        VFX.transform.localScale = Vector3.one * ExplodeSize;
        _cameraEffect.Explosion();
        foreach (AHealth health in ToDealDamage)
        {
            if (health)
            {
                float dist = Vector3.Distance(health.transform.position, transform.position);
                int DamageToDeal = Mathf.RoundToInt(ExplodeDamage / dist);
                health.TakeDamage(DamageToDeal, transform.parent.gameObject);

                Rigidbody rigidbody;
                if (health.gameObject.TryGetComponent<Rigidbody>(out rigidbody))
                {
                    rigidbody.AddExplosionForce(ExplosionForce / dist, transform.position, ExplodeSize);
                }
            }
        }
        Destroy(transform.parent.gameObject);
    }
}
