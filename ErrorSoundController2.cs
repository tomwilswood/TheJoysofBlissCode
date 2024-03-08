using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorSoundController2 : MonoBehaviour
{

    bool audioShouldPlay;
    AudioSource testErrorSound;

    public float individualID;

    GeneralMenuController menuScript;
    GameObject turnOffEffectP0;
    bool soundsLoaded;
    // Start is called before the first frame update
    void Start()
    {
        testErrorSound = gameObject.GetComponent<AudioSource>();

        menuScript = GameObject.Find("Canvas").GetComponent<GeneralMenuController>();
        turnOffEffectP0 = GameObject.Find("Turn Off Effect Part 0");
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.name == "ErrorSound (-1)") && !soundsLoaded && menuScript.peacefulSceneStartShouldFade)
        {
            StartCoroutine(WaitThenPlay(menuScript.startFadeTime - 1.0f));
            soundsLoaded = true;
        }

        if (audioShouldPlay)
        {
            if (!testErrorSound.isPlaying)
            {
                StartCoroutine(WaitThenPlay(individualID * 0.1f));
            }
        }

        if (menuScript.peacefulSceneStartShouldFade && !soundsLoaded && !(gameObject.name == "ErrorSound (-1)"))
        {
            StartCoroutine(waitThenPlayMultiple(menuScript.startFadeTime - 0.1f));
            StartCoroutine(stopWaitThenPlayMultiple(menuScript.startFadeTime + 0.2f));
            soundsLoaded = true;
        }

    }

    IEnumerator WaitThenPlay(float num)
    {
        yield return new WaitForSeconds(num);
        testErrorSound.Play();

    }

    IEnumerator waitThenPlayMultiple(float num)
    {
        yield return new WaitForSeconds(num);
        audioShouldPlay = true;
    }

    IEnumerator stopWaitThenPlayMultiple(float num)
    {
        yield return new WaitForSeconds(num);
        audioShouldPlay = false;
    }
}
