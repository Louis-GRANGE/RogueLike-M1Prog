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
    public SphereCollider TriggerAreaDamage;

    //[SerializeField]
    private float ExplodeSize;
    [HideInInspector]
    public GameObject DamageSender;

    protected virtual void Start()
    {
        _cameraEffect = CameraEffect.Instance;
        ToDealDamage = new List<AHealth>();
        ExplodeSize = TriggerAreaDamage.radius - 0.5f;
    }

    public virtual void Explode()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(ExplodeAfterTime());
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (ToDealDamage == null)
            return;
        AHealth aHealth;
        if (other.TryGetComponent<AHealth>(out aHealth) && !ToDealDamage.Contains(aHealth))
        {
            if (ToDealDamage != null)
                ToDealDamage.Add(aHealth);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (ToDealDamage == null)
            return;
        AHealth aHealth;
        if (other.TryGetComponent<AHealth>(out aHealth) && !ToDealDamage.Contains(aHealth))
        {
            if (ToDealDamage != null)
                ToDealDamage.Add(aHealth);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (ToDealDamage == null)
            return;
        AHealth aHealth;
        if (other.TryGetComponent<AHealth>(out aHealth) && ToDealDamage.Contains(aHealth))
        {
            if (ToDealDamage != null)
                ToDealDamage.Remove(aHealth);
        }
    }

    protected IEnumerator ExplodeAfterTime()
    {
        yield return new WaitForSeconds(TimeBeforeExplode);
        ParticleSystem VFX = Instantiate(VFX_Explosion, transform.position, Quaternion.identity);
        SoundManager.Instance.RequestSoundEffect(transform.position, SoundType.Item);
        VFX.transform.localScale = Vector3.one * ExplodeSize;
        _cameraEffect.Explosion();
        foreach (AHealth health in ToDealDamage)
        {
            if (health)
            {
                float dist = Vector3.Distance(health.transform.position, transform.position);
                int DamageToDeal = Mathf.RoundToInt(ExplodeDamage / dist);
                if(DamageSender)
                    health.TakeDamage(DamageToDeal, /*transform.parent.gameObject*/DamageSender, DamageType.Explosion);
                else
                    health.TakeDamage(DamageToDeal, transform.parent.gameObject, DamageType.Explosion);

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
