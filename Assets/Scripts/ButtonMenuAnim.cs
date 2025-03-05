using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonMenuAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float animationSpeed = 0.1f;
    private float basicY = 0f;
    private float newY = 3f;

    private void Start() {

        Debug.Log("Awake");
       
    }

    public void OnPointerEnter(PointerEventData EvenntData)
    {
        basicY = transform.position.y;
        Debug.Log($"basicY: {basicY}");
        newY = basicY + 25f;
        transform.DOMoveY(newY, animationSpeed);
        

    }

    public void OnPointerExit(PointerEventData EventData)
    {
        Debug.Log("On Pointer Exit");
        transform.DOMoveY(basicY, animationSpeed);
    }
}
