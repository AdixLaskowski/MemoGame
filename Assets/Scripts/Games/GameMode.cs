using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class GameMode : MonoBehaviour
{
    // Board
    [SerializeField] private List<GameObject> allActiveButtons;
    [SerializeField] private List<Button> buttonsList;
    [SerializeField] private GameObject grid2x2;
    [SerializeField] private GameObject grid3x3;
    [SerializeField] private GameObject grid4x4;
    [SerializeField] private GameObject grid5x5;

    private GameObject grid;

    // Compared lists
    [SerializeField] private List<int> buttonsToClick;
    [SerializeField] public List<int> buttonsClicked;

    // Game
    private int buttonsCount;
    private int level = 1;
    private int nextButtonId;
    private bool isGameReady = false;
    private bool isCorutineFinished = true;
    [SerializeField] private AudioSource clickSound;

    // Buttons
    private GameObject buttonToHighlight;
    private Image buttonImage;
    private double highlightTime = 0.4;
    public int lastClickedButtonId;

    // Sounds
    [SerializeField] private AudioSource worngSound;
    [SerializeField] private AudioSource successSound;

    // Flags
    public bool clicked = false;
    private bool canAddButton = true;
    int step = 0;

    // Debuging
    [SerializeField] private TMP_Text levelDebugText;

    // Lives
    [SerializeField] private List<GameObject> heartsImages;
    [SerializeField] private GameObject heartsParent;
    public int maxLifes = 1;
    private int lifes = 0;


    //Points
    private int basicPoints = 50;
    private int score = 0;
    [SerializeField] private TMP_Text scoreValue;

    // Dificulty
    public enum dificulty { easy, medium, hard };
    [SerializeField] private TMP_Text difficultyLabel;
    dificulty currentDifficulty;

    // Other

    [SerializeField] private GameObject darkPanel;
    //[SerializeField] private Messa

    //Loading
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private TMP_Text countdown;

    //Pause
    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;

    private GameObject actualGrid;

    [SerializeField] private int currentModeId = 0;

    [SerializeField] private TMP_Text modeName;

   // public static event Action OnGameLose;

    private void GetButtonsCount() => buttonsCount = actualGrid.transform.childCount;

    private void GetListOfButtons()
    {
        for (int i = 0; i < buttonsCount; i++)
        {
            buttonsList.Add(allActiveButtons[i].transform.GetChild(1).GetComponent<Button>());
            allActiveButtons[i].GetComponent<ButtonController>().buttonId = i;
        }
    }

    public void TestNextlevel()
    {
        Debug.Log("<Color=green>Test Next level</Color>");
        if (canAddButton) level += 1;
        PlayClassicMode();
    }

    private void GetAllButtons()
    {
        for (int i = 0; i < buttonsCount; i++)
        {
            allActiveButtons.Add(actualGrid.transform.GetChild(i).gameObject);
            allActiveButtons[i].GetComponent<ButtonController>().buttonId = i;
        }
    }

    private void PrepareLevel(dificulty currentDificulty)
    {
        highlightTime = GameConfig.highlightTime;
        

        switch (currentDificulty)
        {
            case dificulty.easy:
                maxLifes = 5;
                basicPoints = 10;
                //highlightTime = 0.7f;
                break;

            case dificulty.medium:
                maxLifes = 3;
                basicPoints = 30;
                //highlightTime = 0.4f;
                break;

            case dificulty.hard:
                maxLifes = 1;
                basicPoints = 50;
                //highlightTime = 0.1f;
                break;

        }

    }

    private void SwitchInteractable(bool state)
    {
        Debug.Log("<Color=Orange>Switch Interactable</Color>");
        for (int i = 0; i < buttonsCount; i++)
        {
            buttonsList[i].interactable = state;
        }
    }

    private IEnumerator Highlite2(int buttonId)
    {
        Debug.Log("Highlite 2");

        yield return new WaitForSeconds(0.3f);

        //Debug.Log("i: " + i);
        yield return new WaitForSeconds(0.2f);
        nextButtonId = buttonId;
        buttonToHighlight = allActiveButtons[nextButtonId];
        buttonImage = buttonToHighlight.transform.GetChild(1).GetComponent<Image>();


        buttonImage.color = Color.yellow;
        clickSound.Play();
        float x = (float)highlightTime;
        yield return new WaitForSeconds(x);
        buttonImage.color = Color.white;
        isCorutineFinished = true;
        yield return null;
    }

    private void WaitForClick()
    {
        //lastClickedButtonId 
        clicked = false;

    }

    private IEnumerator StartNextRound()
    {
        Debug.Log("StartNextRound");
        yield return new WaitForSeconds(1f);
        TestNextlevel();
    }

    private bool CompareId(int idToCompare)
    {

        if (lastClickedButtonId != buttonsToClick[idToCompare])
        {
            Debug.Log($" {lastClickedButtonId} != {buttonsToClick[idToCompare]}");
            return false;

        }
        else { return true; }

    }

    private void LoseLife()
    {
        Debug.Log("Lose life");
        heartsImages[lifes - 1].SetActive(false);
        lifes -= 1;

    }

    private void UpdateScore()
    {
        Debug.Log("Update Score");
        score = score + basicPoints * buttonsToClick.Count;
        scoreValue.text = score.ToString();

    }

    private void Guess2()
    {

        if (step <= buttonsToClick.Count)
        {
            Debug.Log($"Zgadza siê: {step}"); Debug.Log($"Step: {step} Buttons to click: {buttonsToClick.Count}");


            // Porównuje ostatni przycisk z step
            if (CompareId(step)) {

                // W tym momencie musimy odebraæ nowy lastClickedButton, w celu kontynuowania pêtli
                WaitForClick();
                if (buttonsClicked.Count == buttonsToClick.Count)
                {
                    canAddButton = true;
                    Win();
                    UpdateScore();
                    Debug.Log("Ustawiam step na 0");
                    step = 0;

                    EndGuessing();
                    return;
                }

            }
            else {
                Debug.Log("Nie zgadza siê");
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

    private void EndGuessing()
    {
        Debug.Log("End guessing");
        buttonsClicked.Clear();
        StartCoroutine("StartNextRound");
        Debug.Log("____________");
    }

    private void SetAllButtonsColor(Color color)
    {
        for (int i = 0; i < buttonsCount; i++)
        {
            buttonToHighlight = allActiveButtons[i];
            buttonImage = buttonToHighlight.transform.GetChild(1).GetComponent<Image>();
            buttonImage.color = color;
        }
    }

    private void RestartGame()
    {
        Debug.Log("RestartGame");
        level = 0;
        buttonsToClick.Clear();
        canAddButton = true;
        lifes = 5;
        score = 0;
        scoreValue.text = score.ToString();
        PrepareLevel(currentDifficulty);
        ActiveLivesIcons(true);

    }

    private void DisplayLostMessage()
    {
        //foregroundPanel.SetActive(true);
        //EndGuessing();
        //Instantiate(Message, new Vector3 (0, 0, 0), Quaternion.identity);
    }

    private void ActiveLivesIcons(bool status)
    {
        for (int i = 0; i < GameConfig.lives; i++)
        {
            heartsImages[i].SetActive(status);
        }
    }

    public void Lose()
    {
        SwitchInteractable(false);
        if (lifes > 0) { LoseLife(); SetLivesList(); }
        if (lifes == 0) { DisplayLostMessage(); }

        StartCoroutine("DisplayLose");



    }

    public void Win()
    {
        SwitchInteractable(false);
        StartCoroutine("DisplayWin");
    }

    private IEnumerator DisplayWin()
    {
        successSound.Play();
        SetAllButtonsColor(Color.green);
        yield return new WaitForSeconds(0.3f);
        SetAllButtonsColor(Color.white);
        yield return new WaitForSeconds(0.3f);
        SetAllButtonsColor(Color.green);
        yield return new WaitForSeconds(0.3f);
        SetAllButtonsColor(Color.white);

    }

    private IEnumerator DisplayLose()
    {
        worngSound.Play();
        SetAllButtonsColor(Color.red);
        yield return new WaitForSeconds(0.3f);
        SetAllButtonsColor(Color.white);
        yield return new WaitForSeconds(0.3f);
        SetAllButtonsColor(Color.red);
        yield return new WaitForSeconds(0.3f);
        SetAllButtonsColor(Color.white);

    }

    public void PlayClassicMode()
    {
        StartCoroutine("ClassicMode");
    }

    private IEnumerator ClassicMode()
    {
        SwitchInteractable(false);
        Debug.Log("<Color=red>Start showing</Color>");
        if (buttonsToClick.Count > 0)
        {
            for (int i = 0; i < buttonsToClick.Count; i++)
            {
                Debug.Log("Oddtwarzam poprzednie przyciski");
                yield return StartCoroutine(Highlite2(buttonsToClick[i]));

            }
        }


        if (canAddButton) { Debug.Log("Dodaje nowy przycisk"); int x = UnityEngine.Random.Range(0, buttonsCount);
            StartCoroutine(Highlite2(x)); buttonsToClick.Add(x); }
        SwitchInteractable(true);
    }

    private void Test()
    {
        for (int i = 0; i < level; i++)
        {
            buttonsToClick.Add(UnityEngine.Random.Range(0, buttonsCount));
        }
    }

    private void SetLivesList()
    {
        for (int i = 0; i < heartsParent.transform.childCount; i++)
        {
            heartsImages.Add(heartsParent.transform.GetChild(i).gameObject);
        }
    }

    private IEnumerator CountdownToStart()
    {
        Debug.Log("Uruchamiam korutynê");
        for (int i = 5; i >= 0; i--)
        {
            Debug.Log($"Korutyna: {i}");
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("Koniec korutyny");
        countdownPanel.transform.DOLocalMoveY(1200, 0.3f).OnComplete(() => { StartCoroutine("StartTheGame"); });
    }

    private void GetMode()
    {
        switch (currentModeId)
        {
            case 0:
  
                PrepareCasualMode();
                break;

            case 1:
                PrepareDuelMode();
                break;

            case 2:
                PrepareBombsMode();
                break;

            case 3:
                PrepareFindingMode();
                break;
        
        }
    }

    private void UpdateModeName(string name) => modeName.text = $"{name} mode".ToUpper();


    protected void PrepareCasualMode()
    {
        UpdateModeName("casual");

        switch (GameConfig.gridId)
        {
            case 2:
                grid2x2.SetActive(true);
                grid = grid2x2;
                break;

            case 3:
                grid3x3.SetActive(true);
                grid = grid3x3;
                break;

            case 4:
                grid4x4.SetActive(true);
                grid = grid4x4;
                break;

            case 5:
                grid5x5.SetActive(true);
                grid = grid5x5;
                break;

        }

        loadingPanel.transform.DOLocalMoveY(-1200, 0.3f).OnComplete(() => { StartCoroutine("CountdownToStart"); });

    }

    private void PrepareDuelMode()
    {
        UpdateModeName("duel");

    }
      
    private void PrepareBombsMode()
    {
        UpdateModeName("bombs");

    }

    private void PrepareFindingMode()
    {
        UpdateModeName("finding");
    }

    void Start()
    {
        currentModeId = GameManager.gameModeId;
        GetMode();

        Debug.Log($"<Color=orange> gridId = {GameConfig.gridId} <Color/>");


    }

    void StartTheGame()
    {
        Debug.LogWarning("START THE GAME");
        currentDifficulty = dificulty.medium;
        difficultyLabel.text = currentDifficulty.ToString().ToUpper();

        PrepareLevel(currentDifficulty);

        //lifes = maxLifes;
        lifes = GameConfig.lives;
        Debug.Log($"<Color=pink> Lives: {lifes} </Color>");
        highlightTime = GameConfig.highlightTime;
        Debug.Log($"<color=orange> highlightTime is now set to: {highlightTime} </color>");
        actualGrid = grid.transform.GetChild(0).GetChild(0).gameObject;
        GetButtonsCount();
        Debug.Log($"Buttons: {buttonsCount}");
        GetAllButtons();
        GetListOfButtons();
        isGameReady = true;
        PlayClassicMode();
        SetLivesList();
        ActiveLivesIcons(true);

    }

    public void PauseGame()
    {
        // Wstrzymanie gry

        // Aktywowanie Dark panel
        darkPanel.SetActive(true);
        PauseController.OnPauseMenuClosed += HideExternalObject;

        // Aktywowanie Pause panel
        pausePanel.SetActive(true);

        // Animacja DOTween od dolu wlaœciwego pause manu
        pausePanel.transform.DOLocalMoveY(0, 0.3f);
    }

    private void HideExternalObject()
    {
        darkPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        levelDebugText.text = $"Level: {level}";

        if (clicked == true)
        {
            clicked = false;
            Debug.Log("Update");
            Guess2();


        }
    }
}
