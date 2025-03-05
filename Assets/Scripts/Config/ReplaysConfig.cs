using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReplaysConfig : MonoBehaviour
{
    //Toggle
    [SerializeField] private Toggle replaysToggle;

    // Replays limit
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button increaseButton;
    [SerializeField] private TMP_Text replaysLimitText;

    private int replaysLimitValue=0;

    private int highestLimit = 10;

    private void Start()
    {
        UpdateButtons();
    }

    public void SwitchToggleValue()
    {
        if (replaysToggle.isOn) { GameConfig.replays = true; }
        if (!replaysToggle.isOn) { GameConfig.replays = false; }
    }

    public void IncreaseReplaysLimitValue()
    {
        if (replaysLimitValue < highestLimit) replaysLimitValue++;
        UpdateReplaysLimitText();
    }

    public void DecreaseReplaysLimitValue()
    {
        if (replaysLimitValue > 0) replaysLimitValue--;
        UpdateReplaysLimitText();
    }

    private void UpdateButtons()
    {
        if (replaysLimitValue == 0) decreaseButton.gameObject.SetActive(false);
        else { decreaseButton.gameObject.SetActive(true); }


        if (replaysLimitValue == highestLimit) increaseButton.gameObject.SetActive(false);
        else { increaseButton.gameObject.SetActive(true); }
    }

    private void UpdateReplaysLimitText()
    {
        switch (replaysLimitValue)
        {
            case 0:
                replaysLimitText.text = "NO";
                break;

            default:
                replaysLimitText.text = replaysLimitValue.ToString();
                break;

        }


        UpdateButtons();
        GameConfig.replaysLimit = replaysLimitValue;
    }
}
