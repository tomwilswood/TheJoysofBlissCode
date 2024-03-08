using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using EvolveGames;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GeneralMenuController : MonoBehaviour
{

    GameObject startMenu;
    GameObject peacefulScene;
    public bool inStartMenu = true;
    bool cursorShouldBeVisible;

    GameObject player;
    GameObject playerCamera;

    WinConditionController WinScript;

    Image damageRedImage;

    GameObject gameOverScreen;
    GameObject winScreen;
    bool onWinScreen;

    GameObject turnOffEffectP1;
    Animator blackCircleOffAnimator;

    GameObject turnOffEffectP2;
    Animator turnOffEffectP2Animator;

    GameObject turnOffEffectP0;


    GameObject turnOnEffectPart1;
    Animator turnOnP1Animator;

    GameObject turnOnEffectPart2;
    Animator blackCircleOnAnimator;

    bool turnOnP1Done;

    public bool inPauseMenu;
    GameObject pauseMenu;
    GameObject pauseTooltip;

    GameObject extraClueLights;

    GameObject disconnectText;

    public bool inMainGame;

    Image peacefulSceneOnTop;

    public bool peacefulSceneStartShouldFade;
    float currentPeacefulSceneFadeAlpha;

    public float startFadeTime = 3.0f;

    public bool startButtonPressedBool;

    bool gameOverSoundsPlayed;
    AudioSource gameOverSound;
    AudioSource flatLineSound;
    public bool gameIsOver;
    // Start is called before the first frame update
    void Start()
    {

        peacefulScene = GameObject.Find("PeacefulScene");
        startMenu = GameObject.Find("Start Menu");
        winScreen = GameObject.Find("Win Screen");


        turnOffEffectP1 = GameObject.Find("Turn Off Effect Part 1");
        blackCircleOffAnimator = GameObject.Find("Black Blurred Circle 1 - Off").GetComponent<Animator>();
        turnOffEffectP2 = GameObject.Find("Turn Off Effect Part 2");
        turnOffEffectP2Animator = GameObject.Find("Black Background - Off").GetComponent<Animator>();

        turnOffEffectP0 = GameObject.Find("Turn Off Effect Part 0");
        disconnectText = GameObject.Find("Disconnect Text");


        turnOnEffectPart1 = GameObject.Find("Turn On Effect Part 1");
        turnOnP1Animator = GameObject.Find("Black Background - On").GetComponent<Animator>();
        turnOnEffectPart2 = GameObject.Find("Turn On Effect Part 2");
        blackCircleOnAnimator = GameObject.Find("Black Blurred Circle 1 - On").GetComponent<Animator>();

        peacefulSceneOnTop = GameObject.Find("PeacefulSceneImage - OnTop").GetComponent<Image>();



        player = GameObject.Find("PlayerController");

        pauseMenu = GameObject.Find("Pause Menu");
        pauseTooltip = GameObject.Find("Pause Tooltip");

        extraClueLights = GameObject.Find("Clue Extra Lights");

        WinScript = GameObject.Find("Canvas").GetComponent<WinConditionController>();
        damageRedImage = GameObject.Find("The Red").GetComponent<Image>();
        gameOverScreen = GameObject.Find("Game Over Screen");

        goToStartMenu();

        peacefulSceneOnTop.color = new Vector4(peacefulSceneOnTop.color.r, peacefulSceneOnTop.color.g, peacefulSceneOnTop.color.b, 0.0f);
        peacefulSceneOnTop.enabled = false;
        gameOverSound = GameObject.Find("Game Over Sound").GetComponent<AudioSource>();
        flatLineSound = GameObject.Find("Flat Line Sound").GetComponent<AudioSource>();

        pauseMenu.SetActive(false);
        pauseTooltip.SetActive(false);

        extraClueLights.SetActive(false);

        turnOffEffectP1.SetActive(false);
        turnOffEffectP2.SetActive(false);
        turnOffEffectP0.SetActive(false);

        turnOnEffectPart1.SetActive(false);
        turnOnEffectPart2.SetActive(false);

        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (turnOffEffectP0.activeInHierarchy)
        {
            disconnectText.transform.localPosition = new Vector3(Random.Range(-75, -45), Random.Range(-15, 15));
        }

        if (cursorShouldBeVisible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        damageScreenControl();

        if (WinScript.gameWon && !onWinScreen)
        {
            inMainGame = false;
            if (inPauseMenu)
            {
                leavePauseMenu();
            }
            if (turnOnP1Done)
            {
                goToWinScreen();
                onWinScreen = true;

            }
            else
            {
                turnOnEffectPart1.SetActive(true);
            }

        }

        if (blackCircleOffAnimator.GetCurrentAnimatorStateInfo(0).IsName("Grown Circle Idle Animation"))
        {
            turnOffEffectP1.SetActive(false);
            turnOffEffectP2.SetActive(true);
        }

        if (turnOffEffectP2Animator.GetCurrentAnimatorStateInfo(0).IsName("TnOffP2Idle")) //game acc starts
        {
            turnOffEffectP2.SetActive(false);
            pauseTooltip.SetActive(true);
            inMainGame = true;
        }

        if (turnOnP1Animator.GetCurrentAnimatorStateInfo(0).IsName("TuOnP1Idle") && !turnOnP1Done)
        {
            turnOnEffectPart2.SetActive(true);
            turnOnEffectPart1.SetActive(false);
            turnOnP1Done = true;
        }

        if (blackCircleOnAnimator.GetCurrentAnimatorStateInfo(0).IsName("Turn On Idle"))
        {
            turnOnEffectPart2.SetActive(false);
            cursorShouldBeVisible = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !inPauseMenu && inMainGame)
        {
            goToPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && inPauseMenu)
        {
            leavePauseMenu();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            extraClueLights.SetActive(true);
        }
        else
        {
            extraClueLights.SetActive(false);
        }

        if (inMainGame)
        {
            pauseTooltip.SetActive(true);
        }
        else
        {
            pauseTooltip.SetActive(false);

        }

        if (peacefulSceneStartShouldFade && peacefulSceneOnTop.color.a < 1.0f)
        {
            peacefulSceneOnTop.color = new Vector4(peacefulSceneOnTop.color.r, peacefulSceneOnTop.color.g, peacefulSceneOnTop.color.b, currentPeacefulSceneFadeAlpha);
            currentPeacefulSceneFadeAlpha += 0.5f * Time.deltaTime;
        }
    } //end of update

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void StartButtonPressed()
    {
        cursorShouldBeVisible = false;

        peacefulSceneOnTop.enabled = true;
        peacefulSceneStartShouldFade = true;
        startButtonPressedBool = true;
        StartCoroutine(WaitThenActivateOffP0AndDisableStartMenu(startFadeTime));
        StartCoroutine(WaitThenActivateOffP1(startFadeTime + 0.5f));
    }

    void damageScreenControl()
    {
        if (WinScript.playerHealth < 3 && WinScript.playerHealth != 0)
        {
            damageRedImage.color = new Vector4(damageRedImage.color.r, damageRedImage.color.g, damageRedImage.color.b, (0.03f / (WinScript.playerHealth)));
        }
        else if (WinScript.playerHealth == 3)
        {
            damageRedImage.color = new Vector4(damageRedImage.color.r, damageRedImage.color.g, damageRedImage.color.b, 0.0f);
        }
        else if (WinScript.playerHealth <= 0)
        {
            goToGameOver();
        }

    }

    public void goToStartMenu()
    {
        cursorShouldBeVisible = true;
        inStartMenu = true;


        player.GetComponent<PlayerController>().enabled = false;

        peacefulScene.SetActive(true);
        startMenu.SetActive(true);

        player.GetComponent<PlayerController>().enabled = false;


    }

    public void goToGameOver()
    {
        cursorShouldBeVisible = true;
        gameOverScreen.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
        inMainGame = false;
        if (inPauseMenu)
        {
            leavePauseMenu();
        }

        if (!gameOverSoundsPlayed)
        {
            gameOverSound.Play();
            flatLineSound.Play();
            gameOverSoundsPlayed = true;
        }
        gameIsOver = true;
    }

    public void restartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void goToWinScreen()
    {
        peacefulScene.SetActive(true);
        winScreen.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
    }

    void goToPauseMenu()
    {
        cursorShouldBeVisible = true;
        player.GetComponent<PlayerController>().enabled = false;
        inPauseMenu = true;
        pauseMenu.SetActive(true);
        pauseTooltip.SetActive(false);
        inMainGame = false;
    }

    public void leavePauseMenu()
    {
        cursorShouldBeVisible = false;
        player.GetComponent<PlayerController>().enabled = true;
        inPauseMenu = false;
        pauseMenu.SetActive(false);
        pauseTooltip.SetActive(true);
        inMainGame = true;
    }

    IEnumerator WaitThenActivateOffP1(float num)
    {
        yield return new WaitForSeconds(num);
        turnOffEffectP1.SetActive(true);
        turnOffEffectP0.SetActive(false);
    }

    IEnumerator WaitThenActivateOffP0AndDisableStartMenu(float num)
    {
        yield return new WaitForSeconds(num);
        peacefulScene.SetActive(false);
        startMenu.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
        inStartMenu = false;
        turnOffEffectP0.SetActive(true);
        peacefulSceneOnTop.enabled = false;
    }

}
