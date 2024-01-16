using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestOfCard1 : MonoBehaviour
{
    public List<int> cards = new List<int>();
    private int previousChildCount = 0;

    void Start()
    {
        PopulateCardIDs();
    }

    
    void Update()
    {
        PopulateCardIDs();
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


}
