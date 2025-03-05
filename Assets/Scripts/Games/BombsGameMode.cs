using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BombsGameMode : TestMode
{

    private int timeToRememberBoard = 10;
    private int bombsToGuess = 0;
    [SerializeField] private List<int> buttonsWithBombs;

    int buttonToAdd = 0;

    private IEnumerator CountdownToGuessing()
    {
        Debug.Log("Start of countdown");
        for (int i = 5; i >= 0; i--)
        {
            Debug.Log($"Odliczanie: {i}");
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("End of countdown");
        countdownPanel.transform.DOLocalMoveY(1200, 0.3f).OnComplete(() => { SetBombs(); });
    }

    public void StartTheGame()
    { 
        
    
    }


    private int GetRandomButtonId() => Random.Range(0, allActiveButtons.Count);

    private void SetBombs()
    {
        bombsToGuess = Random.Range(7, 15);

        for (int i = 0; i <= bombsToGuess; i++)
        {        

            buttonToAdd = GetRandomButtonId();

            if (buttonsWithBombs.Contains(buttonToAdd))
            { 
                
            }

            buttonsWithBombs.Add(buttonToAdd);
        }

    }

    private void StartTimmer()
    { 
    
    }

    private void Guess()
    { 
        
    }

    // Start is called before the first frame update
    void Start()
    {
        grid = PrepareGrid();
        actualGrid = grid.transform.GetChild(0).GetChild(0).gameObject;
        GetButtonsCount();
        Debug.Log($"Buttons: {buttonsCount}");
        GetAllButtons();
        GetListOfButtons();
        
        SetBombs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
