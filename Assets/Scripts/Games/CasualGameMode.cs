using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CasualGameMode : TestMode
{
    protected GameObject grid;
    int step = 0;
    [SerializeField] private GameObject replayButton;

    private int replays;
    private int replaysToUse;
    [SerializeField] private TMP_Text replaysText;

    //Pause Menu
    [SerializeField] private GameObject pauseMenu;

    private IEnumerator ClassicMode()
    {
        SwitchInteractable(false);
        SwitchReplayButtonInteractable(false);
        Debug.Log("<Color=red>Start showing</Color>");
        if (buttonsToClick.Count > 0)
        {
            for (int i = 0; i < buttonsToClick.Count; i++)
            {
                Debug.Log("Oddtwarzam poprzednie przyciski");
                yield return StartCoroutine(Highlite(buttonsToClick[i]));

            }
        }

        if (canAddButton)
        {
            Debug.Log("Dodaje nowy przycisk"); int x = Random.Range(0, buttonsCount);
            StartCoroutine(Highlite(x)); buttonsToClick.Add(x);
        }

        yield return new WaitForSeconds((float)highlightTime);
        SwitchInteractable(true);
        SwitchReplayButtonInteractable(true);
    }

    private void SwitchReplayButtonInteractable(bool state)
    {
        replayButton.transform.GetChild(4).GetComponent<Button>().interactable = state;
    }

    private void StartTheGame()
    {
        lifes = GameConfig.lives;
        highlightTime = GameConfig.highlightTime;
        Debug.Log($"<color=orange> highlightTime is now set to: {highlightTime} </color>");
        Debug.Log($"<Color=pink> Lives: {lifes} </Color>");

        grid = PrepareGrid();
        actualGrid = grid.transform.GetChild(0).GetChild(0).gameObject;
        //HideLoadingScreen();

        GetButtonsCount();
        Debug.Log($"Buttons: {buttonsCount}");
        GetAllButtons();
        GetListOfButtons();
        isGameReady = true;
        PlayClassicMode();
        SetLivesList();
        ActiveLivesIcons(true);
        SetMarks();

        if (GameConfig.replays)
        {
            replayButton.SetActive(true);
            replays = GameConfig.replaysLimit;
            replaysToUse = replays;
            replaysText.text = $"x{replays}";
            if (replays == 0)
            {
                replayButton.transform.GetChild(2).gameObject.SetActive(false);
            }

        }
    }

    public void PlayClassicMode()
    {
        StartCoroutine("ClassicMode");
    }

    private IEnumerator StartNextRound()
    {
        yield return new WaitForSeconds(1f);
        TestNextlevel();
    }

    public void TestNextlevel()
    {
        if (canAddButton) {
            level += 1;
            UpdateLevelText();
        }
        PlayClassicMode();
    }

    public void Pause() => PauseGame();

    private void Guess()
    {

        if (step <= buttonsToClick.Count)
        {
            if (CompareId(step))
            {

                WaitForClick();
                if (buttonsClicked.Count == buttonsToClick.Count)
                {
                    canAddButton = true;
                    Win();
                    UpdateScore();
                    
                    step = 0;

                    EndGuessing();
                    return;
                }
            }
            else
            {
                
                canAddButton = false;
                Lose();
                if (lifes != 0) EndGuessing();
                step = 0;
                return;
            }

            // Jeœli koniec
            if (step > buttonsToClick.Count)
            {
                step = 0;

                return;
            }
            step++;

        }

    }

    private void SetMarks()
    {
        //if (PlayerPrefs.HasKey("Marks"))
        //{
        //    if (PlayerPrefs.GetInt("Marks") != 1) return;
        //    for (int i = 0, j = buttonsList.Count; i < j; i++)
        //    {
        //        buttonsList[i].transform.GetChild(0).gameObject.SetActive(true);
        //    }

        //}

        if (GameConfig.useMarks) {

            for (int i = 0, j = buttonsList.Count; i < j; i++)
            {
                buttonsList[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }

    public void Reapeat()
    {
        canAddButton = false;

        if (replays != 0)
        {
            replaysToUse--;
            replaysText.text = $"x{replaysToUse}";
        }

        if (replays != 0 && replaysToUse == 0)
        {
            replayButton.SetActive(false);
        }
            
        PlayClassicMode();
    }

    private void Start()
    {
        //base.StartCoroutine("StartTheGame");
        //StartTheGame();
        Run();
    }

    private void Run()
    {
        base.Start();
        
    }

    public void OpenPauseMenu()
    {
        darkPanel.SetActive(true);
        pauseMenu.SetActive(true);
    }

    void Update()
    {
        if (clicked == true)
        {
            clicked = false;
            Guess();


        }
    }
}
