using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType { Explosion, Item };

public class SoundManager : MonoBehaviour
{
    [HideInInspector] public static SoundManager Instance;

    public bool AutoPlaying = false;
    float Autotime = 0;

    public bool SemiAutoPlaying = false;
    float SemiAutotime = 0;

    [Header("Sound effects")]
    public AudioClip Explosion;
    public AudioClip Item;

    bool ready = false;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        GameObject model = transform.GetChild(0).gameObject;

        for (int i = 0; i < 19; i++)
        {
            Instantiate(model, transform);
        }
    }

    private void Start()
    {
        ready = true;
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

    public void RequestSoundEffect(Vector3 soundPosition, SoundType soundType)
    {
        if (ready && transform.childCount > 0)
        {
            GameObject choosen = transform.GetChild(0).gameObject;

            choosen.transform.SetParent(null);
            choosen.transform.position = soundPosition;

            switch (soundType)
            {
                case SoundType.Explosion:
                    choosen.GetComponent<ReturnToPool>().MakeSound(Explosion);
                    break;
                case SoundType.Item:
                    choosen.GetComponent<ReturnToPool>().MakeSound(Item);
                    break;
                default:
                    break;
            }
        }
    }
}
