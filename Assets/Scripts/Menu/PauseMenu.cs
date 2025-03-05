using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Buttons
    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button menuButton;

    //Menu and Shadow panel
    [SerializeField] private GameObject shadowPanel;


    public void Resume()
    {
        shadowPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Settings()
    { 
    
    }


    public void OpenSettings()
    { 
    
    }

    public void CloseSettings()
    { 
    
    }

    public void GoToMenu()
    { 
    
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
