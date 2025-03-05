using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesConfig : MonoBehaviour
{
    // Lives
    [SerializeField] private Button livesIncreaseButton;
    [SerializeField] private Button livesDecreaseButton;
    [SerializeField] private TMP_Text livesText;
    private static int lives = 3;
    private int maxLives = 5;


    private void UpdateLivesText() { livesText.text = $" {lives} "; GameConfig.lives = lives; }

    public void IncreaseLives()
    {

        lives += 1;
        UpdateLivesText();
        livesDecreaseButton.gameObject.SetActive(true);

        if (lives == maxLives) livesIncreaseButton.gameObject.SetActive(false);
        else { livesDecreaseButton.gameObject.SetActive(true); }
    }

    public void DecreaseLives()
    {
        lives -= 1;
        UpdateLivesText();
        livesIncreaseButton.gameObject.SetActive(true);

        if (lives == 1) { livesDecreaseButton.gameObject.SetActive(false);}
        else { livesDecreaseButton.gameObject.SetActive(true); }
    }
}
