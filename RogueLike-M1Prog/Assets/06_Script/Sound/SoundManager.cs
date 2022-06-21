using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [HideInInspector] public static SoundManager Instance;

    public bool AutoPlaying = false;
    float Autotime = 0;

    public bool SemiAutoPlaying = false;
    float SemiAutotime = 0;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Update()
    {
        if (AutoPlaying)
        {
            Autotime += Time.deltaTime;
            if (Autotime > 0.03f)
                AutoPlaying = false;
        }

        if (SemiAutoPlaying)
        {
            SemiAutotime += Time.deltaTime;
            if (SemiAutotime > 0.03f)
                SemiAutoPlaying = false;
        }
    }

    public bool RequestAutoSound()
    {
        if (!AutoPlaying)
        {
            Autotime = 0;
            AutoPlaying = true;
            return true;
        }
        else
            return false;
    }

    public bool RequestSemiAutoSound()
    {
        if (!SemiAutoPlaying)
        {
            SemiAutotime = 0;
            SemiAutoPlaying = true;
            return true;
        }
        else
            return false;
    }
}
