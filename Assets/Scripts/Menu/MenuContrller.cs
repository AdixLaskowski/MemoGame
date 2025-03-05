using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuContrller : MonoBehaviour
{
    // Menus
    [SerializeField] private GameObject singleplayerPanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject makingofPanel;
    [SerializeField] private GameObject gameModeSelectionPanel;



    [SerializeField] private GameObject exitNotification;
    [SerializeField] private Transform centralPoint;

    private void ShowPanel(GameObject panelToShow)
    {
        panelToShow.SetActive(true);
        panelToShow.transform.DOLocalMoveX(0, 0.3f);
    }

    private void ShowPanel_Y(GameObject panelToShow)
    {
        panelToShow.SetActive(true);
        panelToShow.transform.DOLocalMoveY(0, 0.3f);
    }


    //Left panels
    public void OpenSinglePlayerPanel()
    {
        Debug.Log("OpenSinglePlayerPanel");
        ShowPanel(singleplayerPanel);
    }

    public void OpenMultiPlayerPanel()
    {
        //ShowPanel(multiplayerPanel);
    }

    public void OpenHowToPlayPanel()
    {
        //ShowPanel(howtoplayPanel);
    }

    public void OpenGameModeSelection()
    {
        ShowPanel_Y(gameModeSelectionPanel);
    }

    public void OpenExitPanel()
    {
        ShowPanel(exitPanel);
        exitNotification.SetActive(true);
        exitNotification.transform.DOLocalMoveY(0, 0.4f);
    }


    //Right panels
    public void OpenSettingsPanel()
    {
        ShowPanel(settingsPanel);
    }

    public void OpenStatisticsPanel()
    {
       // ShowPanel(statisticsPanel);
    }

    public void OpenCreditsPanel()
    {
        ShowPanel(creditsPanel);
    }

    public void OpenMakingOfPanel()
    {
        ShowPanel(makingofPanel);
    }

    


}
