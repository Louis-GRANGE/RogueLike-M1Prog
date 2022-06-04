using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(BoxCollider), typeof(Rigidbody))]
public abstract class AItem : MonoBehaviour
{
    [Header("External")]
    public FollowingUI prefFollowingUI;
    //public Sprite _icon;

    [Header("FollowingUI")]
    protected FollowingUI _followingUI;
    protected GameObject _equipText;

    private void Start()
    {

    }


    public virtual void ActualizeShown()
    {

    }
    public virtual void HideShown()
    {

    }

    public virtual void Desactivate()
    {
        if (_followingUI)
            Destroy(_followingUI.gameObject);
        Destroy(gameObject);
    }
}
