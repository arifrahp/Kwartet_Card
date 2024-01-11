using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardObject cardObject;

    void Start()
    {
        cardObject = GetComponentInParent<CardObject>();
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
}
