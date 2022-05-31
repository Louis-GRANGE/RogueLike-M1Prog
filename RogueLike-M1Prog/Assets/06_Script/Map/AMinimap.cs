using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMinimap : MonoBehaviour
{
    public bool IsActiveMinimap;

    public virtual void ActiveMinimap(bool NewActive)
    {
        IsActiveMinimap = NewActive;
        SpriteRenderer[] Minimap = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in Minimap)
        {
            sprite.enabled = NewActive;
        }
    }
}
