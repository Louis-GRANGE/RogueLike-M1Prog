using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    Transform pool;

    AudioSource audiosource;
    bool used;

    float latency;
    float time;

    void Awake()
    {
        audiosource = GetComponent<AudioSource>();

        pool = transform.parent;   
    }

    private void Update()
    {
        if (used)
        {
            time += Time.deltaTime;
            if(time >= latency)
            {
                used = false;
                transform.SetParent(pool);
            }
        }
    }

    public void MakeSound(AudioClip clip)
    {
        audiosource.clip = clip;
        latency = audiosource.clip.length;
        time = 0;

        audiosource.Play();

        used = true;
    }
}
