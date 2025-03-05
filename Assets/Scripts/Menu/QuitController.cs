using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuitController : MonoBehaviour
{
    Vector3 backPos = new Vector3(0, -850, 0);
    private float animationSpeed = 0.3f;
    [SerializeField] private GameObject shadowedBackground;
    public void QuitGame() => Application.Quit();
    public void Cancel() => transform.DOLocalMoveY(-800, animationSpeed).OnComplete(()=> { gameObject.SetActive(false); shadowedBackground.SetActive(false); });
}
