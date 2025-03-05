using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighlightConfig : MonoBehaviour
{

    // Higlight
    [SerializeField] private Button higlightIncreaseButton;
    [SerializeField] private Button higlightDecreaseButton;
    [SerializeField] private TMP_Text higlightTimeText;

    //Color
    [SerializeField] private Color newColor;

    private double higlightTime = 0.7;
    private double maxHiglightTime = 0.9;
    private double minimumHighlightTime = 0.2;

    private void UpdateHighlightTimeText() { higlightTimeText.text = $" {higlightTime}S "; GameConfig.highlightTime = higlightTime; }

    public void IncreaseHighlightTime()
    {
        //if (higlightTime == 1.0) return;

        higlightTime += 0.1;
        UpdateHighlightTimeText();
        higlightDecreaseButton.gameObject.SetActive(true);

        if (higlightTime >= maxHiglightTime) { Debug.Log("Wy¹cz increase button"); higlightIncreaseButton.gameObject.SetActive(false); }
        else { higlightIncreaseButton.gameObject.SetActive(true); }
    }

    public void DecreaseHighlightTime()
    {
        higlightTime -= 0.1;
        UpdateHighlightTimeText();
        higlightIncreaseButton.gameObject.SetActive(true);

        Debug.Log($"higlightTime: {higlightTime}");

        if (higlightTime != 0.1) { Debug.Log($"{higlightTime} != {minimumHighlightTime}"); }

        if (higlightTime <= minimumHighlightTime) { higlightDecreaseButton.gameObject.SetActive(false); Debug.Log("Wy¹cz decrease button"); }
        else { higlightDecreaseButton.gameObject.SetActive(true); }
    }

    public void SelectThisColor()
    {
        newColor.r = GetComponent<Image>().color.r;
    }
}
