using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public int buttonId;
    [SerializeField] private TestMode gameMode;
    private Button currentButton;


    private void Start()
    {
        gameMode = FindFirstObjectByType<TestMode>();
        currentButton = transform.GetChild(1).GetComponent<Button>();
        currentButton.onClick.AddListener(SendId);
        currentButton.onClick.AddListener(ComfirmClick);
    }

    public void SendId()
    {
        Debug.Log("Button has been clicked");
        gameMode.lastClickedButtonId = buttonId;
        gameMode.buttonsClicked.Add(buttonId);
    }

    private void ComfirmClick()
    {
        gameMode.clicked = true;
    }

}
