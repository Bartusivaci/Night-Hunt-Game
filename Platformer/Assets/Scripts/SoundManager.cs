using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource attack1;
    public AudioSource attack2;
    public AudioSource attack3;
    public AudioSource crouch;
    public AudioSource skeletonDying;
    public AudioSource damageHitSound;
    public AudioSource swordFlying;

    public AudioSource gameMusic;


    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    public void StartPlayingGameMusic()
    {
        gameMusic.Play();
    }

    public void StopPlayingGameMusic()
    {
        gameMusic.Stop();
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
            case "Crouch":
                crouch.Play();
                break;
            case "SkeletonDying":
                skeletonDying.Play();
                break;
            case "DamageHitSound":
                damageHitSound.Play();
                break;
            case "SwordFlying":
                swordFlying.Play();
                break;
        }
    }
}
