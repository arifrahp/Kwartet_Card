using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHover1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardObject1 cardObject;
    public Button thisButton;
    public Canvas cardButtonCanvas;

    void Start()
    {
        cardObject = GetComponentInParent<CardObject1>();
        thisButton = GetComponent<Button>();
        cardButtonCanvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        if(thisButton.interactable)
        {
            cardButtonCanvas.sortingOrder = 1;
        }
        else if (!thisButton.interactable)
        {
            cardButtonCanvas.sortingOrder = 0;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardObject.cardTouchButton.interactable)
            cardObject.OnCardHoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cardObject.cardTouchButton.interactable)
            cardObject.OnCardHoverExit();
    }

    public bool PointerOnHover()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            return false;
        else
            return true;
    }
}
