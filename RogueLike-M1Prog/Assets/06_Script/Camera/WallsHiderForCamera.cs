using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallsHiderForCamera : MonoBehaviour
{
    Vector3 _offset;
    Player _player;
    Transform Feets;
    public float AlphaHide = 0.5f;

    private List<Renderer> Hides;


    private void Start()
    {
        _player = Player.Instance;
        Hides = new List<Renderer>();
        Feets = Player.Instance.transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        if (!_player)
            return;

        List<Renderer> CurrentToHides;
        Vector3 pointDirection = Camera.main.ScreenToWorldPoint(Player.Instance.playerMovement.lookInput);
        //hits = Physics.RaycastAll(transform.position, Feets.position - transform.position, Vector3.Distance(Feets.position, transform.position) - 2).ToList();
        //hits.AddRange(Physics.RaycastAll(pointDirection, Camera.main.transform.forward, 1000, -5, QueryTriggerInteraction.Ignore).ToList());
        CurrentToHides = AddingIfNotAdded(Physics.SphereCastAll(transform.position, 1.3f, Feets.position - transform.position, Vector3.Distance(Feets.position, transform.position) - 2).ToList(), Physics.RaycastAll(pointDirection, Camera.main.transform.forward, 1000, -5, QueryTriggerInteraction.Ignore).ToList());

        for (int i = Hides.Count - 1; i >= 0; i--) // SHOW
        {
            if (Hides[i])
            {
                Hides[i].material.color = Hides[i].material.color + new Color(0, 0, 0, AlphaHide);
                Hides.RemoveAt(i);
            }
        }

        for (int i = 0; i < CurrentToHides.Count; i++) // HIDE
        {
            if (CurrentToHides[i])
            {
                Hides.Add(CurrentToHides[i]);
                CurrentToHides[i].material.color = CurrentToHides[i].material.color - new Color(0, 0, 0, AlphaHide);
            }
        }
    }


    List<Renderer> AddingIfNotAdded(List<RaycastHit> first, List<RaycastHit> second)
    {
        List<Renderer> tmp = new List<Renderer>();

        foreach (RaycastHit hit in first)
        {
            if (hit.transform.gameObject.layer == Constants.LayerIgnoreMouseHit)
            {
                Renderer rend = hit.transform.GetComponent<Renderer>();
                if (rend && !tmp.Contains(rend))
                {
                    tmp.Add(rend);
                }
            }
        }

        foreach (RaycastHit hit in second)
        {
            if (hit.transform.gameObject.layer == Constants.LayerIgnoreMouseHit)
            {
                Renderer rend = hit.transform.GetComponent<Renderer>();
                if (rend && !tmp.Contains(rend))
                {
                    tmp.Add(rend);
                }
            }
        }
        return tmp;
    }
}
