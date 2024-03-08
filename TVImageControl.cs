using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVImageControl : MonoBehaviour
{
    Texture2D inScriptOffImageTexture;
    public Texture2D textureBase;

    public Texture2D[] imageTextures = new Texture2D[3];
    int[] currentImageID = new int[3];

    public Camera playerCamera;

    public int individualTVIDNum;
    public int invividualTVCorrectNum;

    WinConditionController WinScript;

    Animator buttonAnimator;
    GeneralMenuController menuScript;

    AudioSource TVChannelChangeSound;
    AudioSource buttonClickSound;

    // Start is called before the first frame update
    void Start()
    {
        inScriptOffImageTexture = textureBase;
        WinScript = GameObject.Find("Canvas").GetComponent<WinConditionController>();
        buttonAnimator = GameObject.Find("SymbolsConfirmedButton").GetComponent<Animator>();
        menuScript = GameObject.Find("Canvas").GetComponent<GeneralMenuController>();
        TVChannelChangeSound = GameObject.Find("TV Channel Change").GetComponent<AudioSource>();
        buttonClickSound = GameObject.Find("Button Click - 3D").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetTexture("_CurrentImage", inScriptOffImageTexture);

        RaycastHit result;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if (buttonAnimator.GetBool("ButtonShouldPress") == true)
        {
            StartCoroutine(WaitThenShouldntPress(0.5f));
        }

        if (Physics.Raycast(ray, out result, Mathf.Infinity, 1 << LayerMask.NameToLayer("TV Interact Layer"))) //if we're pointing at an interactable object
        {
            GameObject g = result.collider.gameObject;



            if (Input.GetKeyDown(KeyCode.E) && !menuScript.inPauseMenu)
            {
                if (g.name == "TV Model With Image (" + individualTVIDNum + ")")
                {
                    TVChannelChangeSound.Play();
                    inScriptOffImageTexture = imageTextures[currentImageID[individualTVIDNum - 1]];

                    if (currentImageID[individualTVIDNum - 1] == invividualTVCorrectNum - 1)
                    {
                        WinScript.TVsCorrect[individualTVIDNum - 1] = true;
                    }
                    else
                    {
                        WinScript.TVsCorrect[individualTVIDNum - 1] = false;
                    }

                    currentImageID[individualTVIDNum - 1]++;
                    if (currentImageID[individualTVIDNum - 1] >= 3)
                    {
                        currentImageID[individualTVIDNum - 1] = 0;
                    }

                }



                if (g.name == "SymbolsConfirmedButton")
                {
                    buttonClickSound.Play();
                    buttonAnimator.SetBool("ButtonShouldPress", true);
                    WinScript.buttonPressed = true;
                }



            }// end of e pressed
        } //end of if hitting interactable

    } //end of update


    IEnumerator WaitThenShouldntPress(float num)
    {
        yield return new WaitForSeconds(num);
        buttonAnimator.SetBool("ButtonShouldPress", false);
    }
}
