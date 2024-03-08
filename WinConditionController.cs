using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionController : MonoBehaviour
{
    public bool[] TVsCorrect = new bool[3];
    public bool buttonPressed;

    public int playerHealth = 3;

    public bool gameWon;

    AudioSource errorSound;
    AudioSource correctSound;

    // Start is called before the first frame update
    void Start()
    {
        errorSound = GameObject.Find("Error Sound - 3D").GetComponent<AudioSource>();
        correctSound = GameObject.Find("Correct Sound - 3D").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
        {
            if (TVsCorrect[0] && TVsCorrect[1] && TVsCorrect[2])
            {
                gameWon = true;
                correctSound.Play();
            }
            else
            {
                if (playerHealth >= 1)
                {
                    playerHealth--;
                    errorSound.Play();
                }

            }
            buttonPressed = false;
        }
    }
}
