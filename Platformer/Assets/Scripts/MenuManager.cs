using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Animator playerAnimator;
    public float delayTime = 0.9f;
    public Image playButtonImage;
    public Image optionsButtonImage;

    public void Play()
    {
        playButtonImage.enabled = false;
        optionsButtonImage.enabled = false;
        playerAnimator.SetTrigger("Transform");
        SoundManager.instance.StartPlayingGameMusic();
        StartCoroutine(SceneLoadDelay());
    }

    public void Options()
    {
        SceneManager.LoadScene("Options Scene");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    IEnumerator SceneLoadDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("Game Scene");
    }
}
