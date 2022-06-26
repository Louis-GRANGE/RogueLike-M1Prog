using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    Transform playerPos;
    public float TimeToGoOnTarget = 1f;
    float timer, angle;

    private void Awake()
    {
        angle = Random.Range(90, 270);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer))
        {
            playerPos = other.transform;
            StartCoroutine(anim());
        }
    }

    IEnumerator anim()
    {
        while (true)
        {
            if ((TimeToGoOnTarget - 1) == 0 || !playerPos)
                break;
            timer += 1 / (TimeToGoOnTarget - 1) * 0.01f;
            transform.position = Tools.RotatePointAroundPivot(transform.position, playerPos.position, new Vector3(0, angle * timer, 0), 1 - timer);

            if (Vector3.Distance(transform.position, playerPos.position) < 0.5f)
            {
                SaveManager.instance.GetSave<SOSaveDatas>().ChipsNumber++;
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
