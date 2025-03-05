using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarksController : MonoBehaviour
{
    [SerializeField] private Toggle marksToggle;


    private void OnToggleOn()
    {
        //PlayerPrefs.SetInt("Marks", 1);
        Debug.Log("W³¹cz litery");
        GameConfig.useMarks = true;
    }


    private void OnToggleOff()
    {
        //PlayerPrefs.SetInt("Marks", 0);

        GameConfig.useMarks = false;
    }

    public void SwitchToggle()
    {
        if (marksToggle.isOn) { GameConfig.useMarks = true; }
        if (!marksToggle.isOn) { GameConfig.useMarks = false; }

    }

}
