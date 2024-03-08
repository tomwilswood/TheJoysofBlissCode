using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using EvolveGames;
using Unity.VisualScripting;

public class AudioController : MonoBehaviour
{
    GeneralMenuController menuScript;

    AudioSource birdsAndMusic;
    bool actionsCompleted1;

    AudioSource TVHum;
    AudioSource TVOff;
    AudioSource headSetSwoosh2;
    AudioSource hitSound;
    AudioSource evilRobotSounds;
    AudioSource creepyMusicSounds;
    AudioSource TVStaticSounds;

    AudioSource[] cameraMovementSounds = new AudioSource[3];
    float cameraSoundAimVol;

    GameObject player;

    AudioSource heartbeatSound;

    WinConditionController WinScript;
    bool actionsCompleted2;
    float timeToWinScreen = 0.5f;

    AudioSource TVOnSounds;

    // Start is called before the first frame update
    void Start()
    {
        menuScript = GameObject.Find("Canvas").GetComponent<GeneralMenuController>();
        birdsAndMusic = GameObject.Find("BirdsongWithMusicObject").GetComponent<AudioSource>();
        TVHum = GameObject.Find("TV General Hum").GetComponent<AudioSource>();
        TVOff = GameObject.Find("TV Off Sound").GetComponent<AudioSource>();
        TVOff.enabled = false;
        headSetSwoosh2 = GameObject.Find("Headset Take Off Swoosh (1)").GetComponent<AudioSource>();
        hitSound = GameObject.Find("Hit sound").GetComponent<AudioSource>();
        evilRobotSounds = GameObject.Find("Evil Robot Sounds").GetComponent<AudioSource>();
        creepyMusicSounds = GameObject.Find("Creepy Music/Ambient").GetComponent<AudioSource>();

        TVStaticSounds = GameObject.Find("TV static sound Object - 3D").GetComponent<AudioSource>();
        cameraMovementSounds[0] = GameObject.Find("Camera Movement Whirring").GetComponent<AudioSource>();
        cameraMovementSounds[1] = GameObject.Find("Camera Movement Whirring (1)").GetComponent<AudioSource>();
        cameraMovementSounds[2] = GameObject.Find("Camera Movement Whirring (2)").GetComponent<AudioSource>();
        player = GameObject.Find("PlayerController");
        heartbeatSound = GameObject.Find("Heartbeat Sound").GetComponent<AudioSource>();

        WinScript = GameObject.Find("Canvas").GetComponent<WinConditionController>();
        TVOnSounds = GameObject.Find("TV On Sound").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (menuScript.startButtonPressedBool && !actionsCompleted1)
        {
            StartCoroutine(WaitThenStop(birdsAndMusic, menuScript.startFadeTime - 0.8f));
            StartCoroutine(WaitThenChangeTVSoundsToOff(menuScript.startFadeTime - 0.55f));
            StartCoroutine(WaitThenPlay(headSetSwoosh2, menuScript.startFadeTime + 1.2f));
            StartCoroutine(WaitThenPlay(hitSound, menuScript.startFadeTime + 1.2f));
            StartCoroutine(WaitThenPlay(evilRobotSounds, menuScript.startFadeTime + 1.1f));
            StartCoroutine(WaitThenPlay(creepyMusicSounds, menuScript.startFadeTime + 1.1f));
            StartCoroutine(WaitThenPlay(TVStaticSounds, menuScript.startFadeTime + 1.1f));
            StartCoroutine(WaitThenPlay(heartbeatSound, menuScript.startFadeTime + 1.1f));
            actionsCompleted1 = true;
        }

        if (menuScript.gameIsOver)
        {
            heartbeatSound.Stop();
        }

        if (player.GetComponent<PlayerController>().enabled)
        {
            cameraSoundAimVol = (player.GetComponent<CharacterController>().velocity.magnitude) / 5;
        }
        else
        {
            cameraSoundAimVol = 0.0f;
        }

        for (int i = 0; i < 3; i++)
        {
            if (cameraMovementSounds[i].volume < cameraSoundAimVol)
            {
                cameraMovementSounds[i].volume += 1.0f * Time.deltaTime;
            }
            else if (cameraMovementSounds[i].volume > cameraSoundAimVol)
            {
                cameraMovementSounds[i].volume -= 1.0f * Time.deltaTime;
            }
        }


        if (WinScript.gameWon && !actionsCompleted2)
        {
            headSetSwoosh2.Play();
            StartCoroutine(WaitThenStop(evilRobotSounds, timeToWinScreen));
            StartCoroutine(WaitThenStop(creepyMusicSounds, timeToWinScreen));
            StartCoroutine(WaitThenStop(heartbeatSound, timeToWinScreen));
            StartCoroutine(WaitThenStop(TVStaticSounds, timeToWinScreen));
            StartCoroutine(WaitThenPlay(TVOnSounds, timeToWinScreen + 0.6f));
            StartCoroutine(WaitThenPlay(TVHum, timeToWinScreen + 1.5f));
            StartCoroutine(WaitThenPlay(birdsAndMusic, timeToWinScreen + 1.5f));
            actionsCompleted2 = true;
        }


    }

    IEnumerator WaitThenPlay(AudioSource audio, float num)
    {
        yield return new WaitForSeconds(num);
        audio.Play();
    }

    IEnumerator WaitThenChangeTVSoundsToOff(float num)
    {
        yield return new WaitForSeconds(num);
        TVOff.enabled = true;
        yield return new WaitForSeconds(0.3f);
        TVHum.Stop();
    }

    IEnumerator WaitThenStop(AudioSource audio, float num)
    {
        yield return new WaitForSeconds(num);
        audio.Stop();
    }
}
