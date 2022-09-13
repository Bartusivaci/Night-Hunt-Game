using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource attack1;
    public AudioSource attack2;
    public AudioSource attack3;


    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }


    public void PlaySFX(string sound)
    {
        switch (sound)
        {
            case "Attack1":
                attack1.Play();
                break;
            case "Attack2":
                attack2.Play();
                break;
            case "Attack3":
                attack3.Play();
                break;
        }
    }
}
