using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInThrow1 : MonoBehaviour
{
    public List<int> cards = new List<int>();
    private int previousChildCount = 0;
    private Player1 player;

    void Start()
    {
        player = GetComponentInParent<Player1>();
    }

    void Update()
    {
        PopulateCardIDs();
        SetChildCardNotInteractable();
    }

    public void PopulateCardIDs()
    {
        int currentChildCount = transform.childCount;

        if (currentChildCount != previousChildCount)
        {
            cards.Clear();

            for (int i = 0; i < currentChildCount; i++)
            {
                Transform child = transform.GetChild(i);
                CardObject1 cardObject = child.GetComponent<CardObject1>();
                if (cardObject != null)
                {
                    int cardID = cardObject.idCard;
                    cards.Add(cardID);

                    Vector3 localPosition = Vector3.zero;
                    localPosition.x = i * 0.2f;
                    localPosition.y = 0f;
                    localPosition.z = 0f;

                    Quaternion localRotation = Quaternion.Euler(0f, 35f, 0f);

                    child.localPosition = localPosition;
                    child.localRotation = localRotation;

                    cardObject.GetRotationAndPosition();
                }

                previousChildCount = currentChildCount;
            }
        }
    }
    public void SetChildCardNotInteractable()
    {
        foreach (Transform card in transform)
        {
            Button cardButton = card.GetComponentInChildren<Button>();

            if (cardButton != null)
            {
                cardButton.interactable = false;
            }
        }
    }
}
