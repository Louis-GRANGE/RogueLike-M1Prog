using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer))
        {
            GameManager.instance.NextLevel();
            Destroy(gameObject);
        }
    }
}
