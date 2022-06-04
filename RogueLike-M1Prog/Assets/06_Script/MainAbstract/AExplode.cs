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
                int DamageToDeal = Mathf.RoundToInt(ExplodeDamage / Vector3.Distance(health.transform.position, transform.position));
                Debug.Log("Damage deal" + DamageToDeal); 
                health.TakeDamage(DamageToDeal, transform.parent.gameObject);
            }
        }
        Destroy(transform.parent.gameObject);
    }
}
