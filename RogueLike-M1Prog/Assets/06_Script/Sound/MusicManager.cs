using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [HideInInspector] public static MusicManager Instance;

    [Header("Calme")]
    public AudioSource Calme;
    public bool CalmePlaying;

    [Header("Combat")]
    public AudioSource Combat;
    public bool CombatPlaying;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CalmePlaying && Calme.volume < 1)
            Calme.volume += Time.fixedDeltaTime / 2;
        else if (!CalmePlaying && Calme.volume > 0)
            Calme.volume -= Time.fixedDeltaTime / 2;

        if (CombatPlaying && Combat.volume < 1)
            Combat.volume += Time.fixedDeltaTime / 2;
        else if (!CombatPlaying && Combat.volume > 0)
            Combat.volume -= Time.fixedDeltaTime / 2;
    }

    public void ChangeSituation(bool inCombat)
    {
        CalmePlaying = !inCombat;
        CombatPlaying = inCombat;
    }
}
