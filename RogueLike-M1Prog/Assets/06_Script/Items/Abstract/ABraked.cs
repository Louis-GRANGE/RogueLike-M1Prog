using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABraked : MonoBehaviour
{
    [Header("External")]
    public float ImpluseExplode = 75;
    public float RadiusExplode = 50;
    public float TimeToDesapear = 2;
    public float TimeBeforeDesapear = 5;


    Rigidbody[] rigidbodies;
    Renderer[] renderers;

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        renderers = GetComponentsInChildren<Renderer>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.AddExplosionForce(ImpluseExplode, transform.position, RadiusExplode);
            rigidbody.AddTorque(Vector3.up * 100000, ForceMode.Impulse);
        }
        StartCoroutine(Desapear());
        Destroy(gameObject, TimeBeforeDesapear);
    }

    IEnumerator Desapear()
    {
        yield return new WaitForSeconds(TimeBeforeDesapear - (TimeToDesapear + 1));
        float alpha = 1;
        while (alpha >= 0)
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = new Color(1, 1, 1, alpha);
            }
            alpha -= 1 / TimeToDesapear * 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = new Color(1, 1, 1, 0);
        }
    }
}
