using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LeaveGame : MonoBehaviour
{

    [SerializeField] private GameObject loadingPanel;
    
    public void SureLeaveGame()
    {
        // W��czenie ekranu �adowania
        loadingPanel.SetActive(true);

        // Animacja ekranu �adowania
        loadingPanel.transform.DOLocalMoveY(0, 0.3f);
        // W��czenie sceny z menu
        SceneManager.LoadScene(0);
    }

    public void Cancel()
    {
        transform.DOLocalMoveY(-1000, 0.3f).OnComplete(() => { gameObject.SetActive(false); });
    }
}
