using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPlayer : MonoBehaviour
{
    CameraEffect _cameraEffect;

    private AMainData ownerMainData;
    public ParticleSystem VFX_Explosion;
    public int ExplodeDamage;
    private List<AHealth> ToDealDamage;
    public GameObject Character;
    public Collider Collider;

    private void Start()
    {
        _cameraEffect = CameraEffect.Instance;

        ownerMainData = transform.parent.GetComponent<AMainData>();
        ToDealDamage = new List<AHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AHealth aHealth;
        if (other.TryGetComponent<AHealth>(out aHealth))
        {
            ToDealDamage.Add(aHealth);
        }
        if (other.CompareTag(Constants.TagPlayer))
        {
            ownerMainData.stateManager.ClearStates();
            VFX_Explosion.Play();
            _cameraEffect.Explosion();
            foreach (AHealth health in ToDealDamage)
            {
                int DamageToDeal = Mathf.RoundToInt(ExplodeDamage / Vector3.Distance(health.transform.position, transform.position));
                health.TakeDamage(DamageToDeal, transform.parent.gameObject);
                Instantiate(VFX_Explosion, transform.position, Quaternion.identity);
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AHealth aHealth;
        if (other.TryGetComponent<AHealth>(out aHealth))
        {
            ToDealDamage.Remove(aHealth);
        }
    }
}
