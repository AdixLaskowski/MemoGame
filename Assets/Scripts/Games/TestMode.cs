using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TestMode : MonoBehaviour
{

    // Board
    [SerializeField] protected List<GameObject> allActiveButtons;
    [SerializeField] protected List<Button> buttonsList;
    [SerializeField] protected GameObject grid2x2;
    [SerializeField] protected GameObject grid3x3;
    [SerializeField] protected GameObject grid4x4;
    [SerializeField] protected GameObject grid5x5;

    protected GameObject grid;

    // Compared lists
    [SerializeField] protected List<int> buttonsToClick;
    [SerializeField] public List<int> buttonsClicked;

    // Game
    protected int buttonsCount;
    protected int level = 1;
    protected int nextButtonId;
    protected bool isGameReady = false;
    protected bool isCorutineFinished = true;
    [SerializeField] protected AudioSource clickSound;

    // Buttons
    protected GameObject buttonToHighlight;
    protected Image buttonImage;
    protected double highlightTime = 0.4;
    public int lastClickedButtonId;

    // Sounds
    [SerializeField] protected AudioSource worngSound;
    [SerializeField] protected AudioSource successSound;

    // Flags
    public bool clicked = false;
    protected bool canAddButton = true;

    // Debuging
    [SerializeField] protected TMP_Text levelText;

    // Lives
    [SerializeField] protected List<GameObject> heartsImages;
    [SerializeField] protected GameObject heartsParent;
    public int maxLifes = 1;
    protected int lifes = 3;


    //Points
    protected int basicPoints = 50;
    protected int score = 0;
    [SerializeField] protected TMP_Text scoreValue;


    // Other
    [SerializeField] protected GameObject darkPanel;
    //[SerializeField] private Messa

    //Loading
    [SerializeField] protected GameObject loadingPanel;
    [SerializeField] protected GameObject countdownPanel;
    [SerializeField] protected TMP_Text countdown;

    //Pause
    [Header("Pause")]
    [SerializeField] protected GameObject pausePanel;

    protected GameObject actualGrid;

    [SerializeField] protected int currentModeId = 0;

    #region Buttons
    protected void GetButtonsCount() => buttonsCount = actualGrid.transform.childCount;

    protected void GetListOfButtons()
    {
        for (int i = 0; i < buttonsCount; i++)
        {
            buttonsList.Add(allActiveButtons[i].transform.GetChild(1).GetComponent<Button>());
            allActiveButtons[i].GetComponent<ButtonController>().buttonId = i;
        }
    }

    protected void GetAllButtons()
    {
        for (int i = 0; i < buttonsCount; i++)
        {
            allActiveButtons.Add(actualGrid.transform.GetChild(i).gameObject);
            allActiveButtons[i].GetComponent<ButtonController>().buttonId = i;
        }
    }

    protected void SwitchInteractable(bool state)
    {
        Debug.Log("<Color=Orange>Switch Interactable</Color>");
        for (int i = 0; i < buttonsCount; i++)
        {
            buttonsList[i].interactable = state;
        }
    }

    protected IEnumerator Highlite(int buttonId)
    {
        Debug.Log("Highlite");

        yield return new WaitForSeconds(0.5f);
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

    protected void WaitForClick()
    {
        Debug.Log("Setting varriable clicked as false");
        clicked = false;

    }

    protected bool CompareId(int idToCompare)
    {

        if (lastClickedButtonId != buttonsToClick[idToCompare])
        {
            Debug.Log($" {lastClickedButtonId} != {buttonsToClick[idToCompare]}");
            return false;

        }
        else { return true; }

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

    #endregion

    #region Updates
    protected void UpdateLevelText()
    {
        levelText.text = $"Level {level}";
    }

    protected void UpdateScore()
    {
        Debug.Log("Update Score");
        score = score + basicPoints * buttonsToClick.Count;
        scoreValue.text = score.ToString();

    }
    #endregion

    protected void EndGuessing()
    {
        Debug.Log("End guessing");
        buttonsClicked.Clear();
        StartCoroutine("StartNextRound");
        Debug.Log("____________");
    }

    private void DisplayLostMessage()
    {
        //foregroundPanel.SetActive(true);
        //EndGuessing();
        //Instantiate(Message, new Vector3 (0, 0, 0), Quaternion.identity);
    }

    #region Win/Lose
    protected void Lose()
    {
        SwitchInteractable(false);
        if (lifes > 0) { LoseLife(); SetLivesList(); }
        if (lifes == 0) { DisplayLostMessage(); }

        StartCoroutine("DisplayLose");
    }

    protected void Win()
    {
        SwitchInteractable(false);
        StartCoroutine("DisplayWin");
    }

    protected IEnumerator DisplayWin()
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

    protected IEnumerator DisplayLose()
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

    #endregion

    #region Lives
    protected void ActiveLivesIcons(bool status)
    {
        for (int i = 0; i < GameConfig.lives; i++)
        {
            heartsImages[i].SetActive(status);
        }
    }

    private void LoseLife()
    {
        Debug.Log("Lose life");
        heartsImages[lifes - 1].SetActive(false);
        lifes -= 1;

    }

    protected void SetLivesList()
    {
        for (int i = 0; i < heartsParent.transform.childCount; i++)
        {
            heartsImages.Add(heartsParent.transform.GetChild(i).gameObject);
        }
    }

    #endregion

    #region Preparation
    private IEnumerator CountdownToStart()
    {
        Debug.Log("Uruchamiam korutynê");
        for (int i = 3; i >= 0; i--)
        {
            Debug.Log($"Korutyna: {i}");
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("Koniec korutyny");
        countdownPanel.transform.DOLocalMoveY(1200, 0.3f).OnComplete(() => { StartCoroutine("StartTheGame"); });
    }
    protected void HideLoadingScreen()
    {
        countdownPanel.transform.DOLocalMoveY(1200, 0.3f).OnComplete(() => { StartCoroutine("StartTheGame"); });
    }
    protected GameObject PrepareGrid()
    {
        GameObject grid = grid5x5;

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

        return grid;
    }

    #endregion


    public void Start()
    {
        Debug.Log("Start klasy bazowej");
        currentModeId = GameManager.gameModeId;
        loadingPanel.transform.DOLocalMoveY(-1200, 0.3f).OnComplete(() => {

            PrepareGrid();




            StartCoroutine("CountdownToStart"); });

       // Debug.Log($"<color=orange> gridId = {GameConfig.gridId} <color/>");
    }

    protected void PauseGame()
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

    protected void HideExternalObject()
    {
        darkPanel.SetActive(false);
    }

    

    


}
