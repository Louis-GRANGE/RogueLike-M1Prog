using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform canon;

    public AudioSource Sound;
    public bool isAuto;

    public void PlaySound()
    {
        if(isAuto && SoundManager.Instance.RequestAutoSound())
            Sound.Play();
        else if (!isAuto && SoundManager.Instance.RequestSemiAutoSound())
            Sound.Play();
    }
}
