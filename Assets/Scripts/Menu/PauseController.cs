using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class PauseController : MonoBehaviour
{

    public static event Action OnPauseMenuClosed;
    [SerializeField] private GameObject leaveGamePanel;

    public void BackToMenu()
    {
        leaveGamePanel.SetActive(true);
        leaveGamePanel.transform.DOLocalMoveY(0, 0.3f);
    }

    public void ClosePauseMenu()
    {
        transform.DOLocalMoveY(-2000, 0.3f).OnComplete(() => OnPauseMenuClosed?.Invoke());
    }
}
