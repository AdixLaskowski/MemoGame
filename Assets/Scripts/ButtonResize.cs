using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonResize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button MyButton;

    [SerializeField] private float newScale = 0.9f;
    [SerializeField] private float basicScale = 0.7f;
    
   

    public void OnPointerEnter(PointerEventData EvenntData)
    {
        
        MyButton.transform.localScale = new Vector3(newScale, newScale, newScale);
        
    }

    public void OnPointerExit(PointerEventData EventData)
    {
        MyButton.transform.localScale = new Vector3(basicScale, basicScale, basicScale);
    }
}
