using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayPanelController : MonoBehaviour
{
    [Header("Main buttons")]
    // Buttons
    [SerializeField] private Button StartButton;
    [SerializeField] private Button ConfigButton;
    [SerializeField] private Button HowToPlayButton;

    [Header("Modes buttons")]
    // Modes buttons
    [SerializeField] private Button CasualModeButton;
    [SerializeField] private Button DuelModeButton;
    [SerializeField] private Button BombsModeButton;
    [SerializeField] private Button FindingModeButton;

    [SerializeField] private int currentModeId = 0;


    // Modes IDs
    //
    // Casual Mode -> 0
    // Duel Mode -> 1
    // Bombs Mode -> 2
    // Finding Mode -> 3

    // Text
    [SerializeField] private TMP_Text currentGameModeText;

    [Header("Panels")]
    // Panels
    [SerializeField] private GameObject configPanel;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject loadingPanel;


    public void StartGame()
    {
        GameManager.gameModeId = currentModeId;
        LoadGameScene();
    }


    private void UpdateGameModeId() => GameManager.gameModeId = currentModeId;
    public void LoadGameScene()
    {
        loadingPanel.transform.DOLocalMoveY(0, 0.3f).OnComplete(() => { StartCoroutine("LoadNewScene"); });
    }

    private IEnumerator LoadNewScene()
    {

        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(1);


    }

    public void ConfigGame()
    {
        configPanel.SetActive(true);
        configPanel.transform.DOLocalMoveY(0, 0.3f);
    }

    public void OpenInstructions()
    { 
    
    }

    public void SwitchToCasualMode()
    {
        currentModeId = 0;
        UpdateCurrentGameModeName();
        UpdateGameModeId();
    }

    public void SwitchToDuelMode()
    {
        currentModeId = 1;
        UpdateCurrentGameModeName();
        UpdateGameModeId();
    }


    public void SwitchToBombsMode()
    {
        currentModeId = 2;
        UpdateCurrentGameModeName();
        UpdateGameModeId();
    }

    public void SwitchToFindingMode()
    {
        currentModeId = 3;
        UpdateCurrentGameModeName();
        UpdateGameModeId();
    }

    private void UpdateCurrentGameModeName()
    {
        switch (currentModeId)
        {
            case 0:
                currentGameModeText.text = "Casual mode".ToUpper();
                break;

            case 1:
                currentGameModeText.text = "Duel mode".ToUpper();
                break;

            case 2:
                currentGameModeText.text = "Bombs mode".ToUpper();
                break;

            case 3:
                currentGameModeText.text = "Finding mode".ToUpper();
                break;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
