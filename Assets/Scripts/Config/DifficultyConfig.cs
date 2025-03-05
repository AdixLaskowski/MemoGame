using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyConfig : MonoBehaviour
{
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;

    [SerializeField] private TMP_Text difficultyText;

    private int difficultyID = 0;
    private int maxDifficultyID = 3;
    private string difficultyName = "";

    // Difficulty IDs
    //
    // ID 0 -> EASY
    // ID 1 -> MEDIUM
    // ID 2 -> HARD
    // ID 3 -> CUSTOM

    private void UpdateDifficulty()
    {
        Debug.Log($"Difficulty ID is set to: {difficultyID}");

        switch (difficultyID)
        {
            case 0:
                difficultyName = "EASY";
                break;

            case 1:
                difficultyName = "MEDIUM";
                break;

            case 2:
                difficultyName = "HARD";
                break;

            case 3:
                difficultyName = "CUSTOM";
                break;
        
        }

        difficultyText.text = difficultyName;
        GameConfig.difficultyName = difficultyName;
    }

    public void IncreaseDifficulty()
    {
        difficultyID++;
        UpdateDifficulty();
        if (decreaseButton.IsActive() == false) { decreaseButton.gameObject.SetActive(true); }
        if (difficultyID == maxDifficultyID) { increaseButton.gameObject.SetActive(false); }
    }

    public void DecreaseDifficulty()
    {
        difficultyID--;
        UpdateDifficulty();
        if (increaseButton.IsActive() == false) { increaseButton.gameObject.SetActive(true); }
        if (difficultyID == 0) { decreaseButton.gameObject.SetActive(false); }
    }

}
