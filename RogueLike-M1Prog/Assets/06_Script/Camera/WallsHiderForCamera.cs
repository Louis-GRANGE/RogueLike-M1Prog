using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsHiderForCamera : MonoBehaviour
{
    Vector3 _offset;
    Player _player;
    Transform Feets;

    private List<Renderer> Hides;

    private void Start()
    {
        _player = Player.Instance;
        Hides = new List<Renderer>();
        Feets = Player.Instance.transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, Feets.position - transform.position, Vector3.Distance(Feets.position, transform.position) - 2);

        for (int i = Hides.Count - 1; i > 0; i--)
        {
            if (Hides[i])
            {
                Hides[i].enabled = true;
                Hides.RemoveAt(i);
            }
        }

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if (rend)
            {
                Hides.Add(rend);
                rend.enabled = false;
            }
        }
    }
}
