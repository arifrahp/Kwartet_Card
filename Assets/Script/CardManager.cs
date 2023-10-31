using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<GameObject> allCards;
    public List<GameObject> listPlayer;

    private int currentGroupID = 1;

    void Start()
    {
        CardGroupID();
        AssignCardIDs();
        InstantiateCardsRandomly();
    }

    public void CardGroupID()
    {
        currentGroupID = 1;
    }

    public void AssignCardIDs()
    {
        int cardsPerGroup = 4;

        for (int i = 0; i < allCards.Count; i++)
        {
            GameObject card = allCards[i];
            CardObject cardObject = card.GetComponent<CardObject>();

            if (cardObject != null)
            {
                cardObject.idCard = currentGroupID;

                if ((i + 1) % cardsPerGroup == 0)
                {
                    currentGroupID++;
                }
            }
        }
    }

    public void InstantiateCardsRandomly()
    {
        List<GameObject> shuffledCards = new List<GameObject>(allCards);

        // Shuffle the cards randomly
        for (int i = 0; i < shuffledCards.Count - 1; i++)
        {
            int randomIndex = Random.Range(i, shuffledCards.Count);
            GameObject temp = shuffledCards[i];
            shuffledCards[i] = shuffledCards[randomIndex];
            shuffledCards[randomIndex] = temp;
        }

        // Instantiate the shuffled cards
        int cardsPerGroup = 4;

        for (int i = 0; i < shuffledCards.Count; i++)
        {
            GameObject cardPrefab = shuffledCards[i];
            GameObject card = Instantiate(cardPrefab);

            CardObject cardObject = card.GetComponent<CardObject>();

            if (cardObject != null)
            {
                // No need to assign idCard here as it's already assigned in AssignCardIDs
            }

            // Parent the card to the current player (you can modify this logic)
            if (listPlayer.Count > 0)
            {
                Transform playerTransform = listPlayer[i / cardsPerGroup].transform;
                card.transform.SetParent(playerTransform);
            }
        }
    }


    void Update()
    {

    }
}
