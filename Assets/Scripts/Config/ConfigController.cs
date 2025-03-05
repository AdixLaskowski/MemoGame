using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ConfigController : MonoBehaviour
{
    Vector3 startPosition = new Vector3(0, 0, 0);
    private float animationSpeed = 0.3f;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private int direction;

    private void Awake()
    {
        startPosition = transform.position;
    }
    public void ClosePanel()
    {
        transform.DOLocalMoveX(direction*2000, animationSpeed).OnComplete(()=>{ gameObject.SetActive(false); });
    }

    public void LoadGameScene()
    {
        loadingPanel.transform.DOLocalMoveY(0, 0.3f).OnComplete(() => { StartCoroutine("LoadNewScene");});
    }

    private IEnumerator LoadNewScene() {

        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(1);


    } 
}
