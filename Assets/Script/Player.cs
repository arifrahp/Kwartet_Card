using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public List<int> cards = new List<int>();

    private int previousChildCount = 0;
    public int score = 0;

    public bool isBot;
    public BotBeheaviour botBeheaviour;

    void Start()
    {
        PopulateCardIDs();
        botBeheaviour = FindAnyObjectByType<BotBeheaviour>();
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
                CardObject cardObject = child.GetComponent<CardObject>();
                if (cardObject != null)
                {
                    int cardID = cardObject.idCard;
                    cards.Add(cardID);

                    Vector3 localPosition = Vector3.zero;
                    localPosition.x = i * 1f;

                    localPosition.y = 0f;
                    localPosition.z = 0f;

                    Quaternion localRotation = Quaternion.identity;

                    child.localPosition = localPosition;
                    child.localRotation = localRotation;
                }

                previousChildCount = currentChildCount;
            }
        }

    }

    public void ShuffleCardPositions()
    {
        int cardCount = transform.childCount;
        List<Transform> cardsTransforms = new List<Transform>();

        // Collect all card transforms
        for (int i = 0; i < cardCount; i++)
        {
            Transform child = transform.GetChild(i);
            cardsTransforms.Add(child);
        }

        // Shuffle the card transforms
        for (int i = 0; i < cardCount; i++)
        {
            int randomIndex = Random.Range(i, cardCount);
            Transform temp = cardsTransforms[i];
            cardsTransforms[i] = cardsTransforms[randomIndex];
            cardsTransforms[randomIndex] = temp;
        }

        // Apply the shuffled positions
        for (int i = 0; i < cardCount; i++)
        {
            Transform cardTransform = cardsTransforms[i];
            Vector3 localPosition = Vector3.zero;
            localPosition.x = i * 1f;
            localPosition.y = 0f;
            localPosition.z = 0f;

            Quaternion localRotation = Quaternion.identity;

            cardTransform.localPosition = localPosition;
            cardTransform.localRotation = localRotation;
        }
    }


    public int CalculateScore()
    {
        Dictionary<int, int> cardCounts = new Dictionary<int, int>();

        foreach (int cardID in cards)
        {
            if (cardCounts.ContainsKey(cardID))
            {
                cardCounts[cardID]++;
            }
            else
            {
                cardCounts[cardID] = 1;
            }
        }

        int playerScore = 0;

        foreach (int cardCount in cardCounts.Values)
        {
            playerScore += cardCount / 4;
        }

        // Store the calculated score in the 'score' variable
        score = playerScore;

        return playerScore;
    }

    void Update()
    {
        PopulateCardIDs();

        CalculateScore();
    }

    public void SetOtherCardButtonsInteractable()
    {
        // Iterate through all players
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            // Iterate through all cards of each player
            foreach (Transform card in player.transform)
            {
                Button cardButton = card.GetComponentInChildren<Button>();

                // Check if this card has a button component and it's not the current player's card
                if (cardButton != null && player != this)
                {
                    // Set the interactable property of the button to false for cards of other players
                    cardButton.interactable = false;
                }
            }
        }

        // Now, set the interactable property to true for the current player's cards
        foreach (Transform card in transform)
        {
            Button cardButton = card.GetComponentInChildren<Button>();

            if (cardButton != null)
            {
                cardButton.interactable = true;
            }
        }

        /*if (isBot)
        {
            Invoke("BotGetInteractableButton", 6f);
            botBeheaviour.ClickRandomButton();
        }*/
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

    public void BotGetInteractableButton()
    {
        botBeheaviour.GetInteractableButton();
    }

    public void BotPressedButton()
    {
        botBeheaviour.ClickRandomButton();
    }

    public bool CheckCardIsZero()
    {
        if (transform.childCount > 0)
            return false;

        else
            return true;
    }
}
