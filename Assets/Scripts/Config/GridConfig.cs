using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridConfig : MonoBehaviour
{

    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;

    [SerializeField] private TMP_Text gridText;

    [SerializeField] private Image currentGridImage;
    [SerializeField] private Sprite Grid2Image;
    [SerializeField] private Sprite Grid3Image;
    [SerializeField] private Sprite Grid4Image;
    [SerializeField] private Sprite Grid5Image;

    int gridId = 3;

    private void UpdateGridText() { gridText.text = $" {gridId}x{gridId} "; GameConfig.gridId = gridId; }


    public void IncreaseHighlightTime()
    {

        gridId += 1;
        UpdateGridText();
        decreaseButton.gameObject.SetActive(true);

        if (gridId >= 5) { Debug.Log("Wy¹cz increase button"); increaseButton.gameObject.SetActive(false); }
        else { decreaseButton.gameObject.SetActive(true); }

        UpdateCurrentGridImage(gridId);
    }

    public void DecreaseHighlightTime()
    {
        gridId -= 1;
        UpdateGridText();
        increaseButton.gameObject.SetActive(true);

        if (gridId <= 2) { decreaseButton.gameObject.SetActive(false); Debug.Log("Wy¹cz decrease button"); }
        else { decreaseButton.gameObject.SetActive(true); }

        UpdateCurrentGridImage(gridId);
    }

    private void UpdateCurrentGridImage(int gridId)
    {
        switch (gridId)
        {
            case 2:
                currentGridImage.sprite = Grid2Image;
                break;

            case 3:
                currentGridImage.sprite = Grid3Image;
                break;

            case 4:
                currentGridImage.sprite = Grid4Image;
                break;

            case 5:
                currentGridImage.sprite = Grid5Image;
                break;

            default:
                currentGridImage.sprite = Grid3Image;
                break;

        }
    }
}
