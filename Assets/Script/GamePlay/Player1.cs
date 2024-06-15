using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    public GameObject thisPlayer;
    public List<int> cards = new List<int>();

    private int previousChildCount = 0;
    public int score = 0;

    public bool isBot;
    public BotBeheaviour1 botBeheaviour;
    public int cardIDHolder;
    public List<CardObject1> cardsWithID = new List<CardObject1>();

    public List<int> throwCards = new List<int>();
    public List<int> nonThrowCards = new List<int>();

    private PlayManager1 playManager;


    void Start()
    {
        PopulateCardIDs();
        botBeheaviour = FindAnyObjectByType<BotBeheaviour1>();
        playManager = FindAnyObjectByType<PlayManager1>();
    }

    public void GetCardsByCardID()
    {
        cardsWithID.Clear();

        foreach (Transform card in transform)
        {
            CardObject1 cardObject = card.GetComponent<CardObject1>();

            if (cardObject != null && cardObject.idCard == cardIDHolder)
            {
                cardsWithID.Add(cardObject);
            }
        }
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
                    localPosition.x = i * 0.5f;
                    localPosition.y = 0f;
                    localPosition.z = 0f;

                    Quaternion localRotation = Quaternion.Euler(0f, 11f, 0f);

                    child.localPosition = localPosition;
                    child.localRotation = localRotation;

                    cardObject.GetRotationAndPosition();
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
            localPosition.x = i * 0.5f;
            localPosition.y = 0f;
            localPosition.z = 0f;

            Quaternion localRotation = Quaternion.Euler(0f, 11f, 0f);

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
            playerScore += cardCount / 4 * 100; // Score 100 for every 4 cards of the same ID
            playerScore += cardCount; // Score 1 for each card
        }

        // Store the calculated score in the 'score' variable
        score = playerScore;

        return playerScore;
    }


    void Update()
    {
        PopulateCardIDs();
        CalculateScore();
        NotificationOn4CardsShow();
    }

    public void SetOtherCardButtonsInteractable()
    {
        // Iterate through all players
        Player1[] players = FindObjectsOfType<Player1>();
        foreach (Player1 player in players)
        {
            // Skip the current player
            if (player == this)
                continue;

            // Iterate through all cards of each player
            foreach (Transform card in player.transform)
            {
                Button cardButton = card.GetComponentInChildren<Button>();
                CardObject1 cardObject = card.GetComponent<CardObject1>();

                // Check if this card has a button component and it's not the current player's card
                if (cardButton != null && player != this)
                {
                    // If the current player has 4 cards of the same ID, set buttons of those cards to be non-interactable
                    if (cards.Count(cardID => cardID == cardObject.idCard) >= 4)
                    {
                        cardButton.interactable = false;
                    }
                    else
                    {
                        // Set the interactable property of the button to false for cards of other players
                        cardButton.interactable = false;
                    }
                }
            }
        }

        // Now, set the interactable property to true for the current player's cards
        foreach (Transform card in transform)
        {
            Button cardButton = card.GetComponentInChildren<Button>();
            CardObject1 cardObject = card.GetComponent<CardObject1>();

            if (cardButton != null)
            {
                // If the current player has 4 cards of the same ID, set buttons of those cards to be non-interactable
                if (cards.Count(cardID => cardID == cardObject.idCard) >= 4)
                {
                    cardButton.interactable = false;
                }
                else
                {
                    cardButton.interactable = true;
                }
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
    
    public void SetChildCardInteractable()
    {
        foreach (Transform card in transform)
        {
            Button cardButton = card.GetComponentInChildren<Button>();

            if (cardButton != null)
            {
                cardButton.interactable = true;
            }
        }
    }

    public void SetCardButtonInteractable(int tempIDHolder)
    {
        bool anyCardMatchesID = false;

        foreach (Transform card in transform)
        {
            Button cardButton = card.GetComponentInChildren<Button>();
            CardObject1 cardObject = card.GetComponent<CardObject1>();

            if (cardObject != null)
            {
                if (cardObject.idCard == tempIDHolder)
                {
                    anyCardMatchesID = true;
                    break;  // No need to continue checking once a match is found
                }
            }
        }

        // Set interactable property based on whether any child card has the same ID
        foreach (Transform card in transform)
        {
            Button cardButton = card.GetComponentInChildren<Button>();
            cardButton.interactable = anyCardMatchesID;
        }
    }

    public void NotificationOn4CardsShow()
    {
        Dictionary<int, int> cardIDCounts = new Dictionary<int, int>();

        // Count the occurrences of each cardID
        foreach (int cardID in cards)
        {
            if (cardIDCounts.ContainsKey(cardID))
            {
                cardIDCounts[cardID]++;
            }
            else
            {
                cardIDCounts[cardID] = 1;
            }
        }

        foreach (Transform card in transform)
        {
            Button cardButton = card.GetComponentInChildren<Button>();
            CardObject1 cardObject = card.GetComponent<CardObject1>();

            if (cardButton != null)
            {
                // If the current player has 4 cards of the same ID, set buttons of those cards to be non-interactable
                if (cardIDCounts.TryGetValue(cardObject.idCard, out int count) && count >= 4)
                {
                    if (!isBot)
                    {
                        switch (cardObject.idCard)
                        {
                            case 1:
                                playManager.cardOnCompletePanel1.tittleText.text = "Kamu Telah Mengumpulkan Kartu Pangeran Sisisngamangaraja";
                                playManager.cardOnCompletePanel1.ShowNotification();
                                break;
                            case 2:
                                playManager.cardOnCompletePanel2.tittleText.text = "Kamu Telah Mengumpulkan Kartu Pangeran Sultan Ageng Tirtayasa";
                                playManager.cardOnCompletePanel2.ShowNotification();
                                break;
                            case 3:
                                playManager.cardOnCompletePanel3.tittleText.text = "Kamu Telah Mengumpulkan Kartu Pangeran Pangeran Diponegoro";
                                playManager.cardOnCompletePanel3.ShowNotification();
                                break;
                            case 4:
                                playManager.cardOnCompletePanel4.tittleText.text = "Kamu Telah Mengumpulkan Kartu Pangeran Pangeran Antasari";
                                playManager.cardOnCompletePanel4.ShowNotification();
                                break;
                            case 5:
                                playManager.cardOnCompletePanel5.tittleText.text = "Kamu Telah Mengumpulkan Kartu Pangeran Sultan Hassanuddin";
                                playManager.cardOnCompletePanel5.ShowNotification();
                                break;
                            case 6:
                                playManager.cardOnCompletePanel6.tittleText.text = "Kamu Telah Mengumpulkan Kartu Pangeran Pattimura";
                                playManager.cardOnCompletePanel6.ShowNotification();
                                break;
                        }
                    }
                    else if (isBot)
                    {
                        switch (cardObject.idCard)
                        {
                            case 1:
                                playManager.cardOnCompletePanel1.tittleText.text = thisPlayer.name + "Telah Mengumpulkan Kartu Pangeran Sisisngamangaraja";
                                playManager.cardOnCompletePanel1.ShowNotification();
                                break;
                            case 2:
                                playManager.cardOnCompletePanel2.tittleText.text = thisPlayer.name + "Telah Mengumpulkan Kartu Pangeran Sultan Ageng Tirtayasa";
                                playManager.cardOnCompletePanel2.ShowNotification();
                                break;
                            case 3:
                                playManager.cardOnCompletePanel3.tittleText.text = thisPlayer.name + "Telah Mengumpulkan Kartu Pangeran Pangeran Diponegoro";
                                playManager.cardOnCompletePanel3.ShowNotification();
                                break;
                            case 4:
                                playManager.cardOnCompletePanel4.tittleText.text = thisPlayer.name + "Telah Mengumpulkan Kartu Pangeran Pangeran Antasari";
                                playManager.cardOnCompletePanel4.ShowNotification();
                                break;
                            case 5:
                                playManager.cardOnCompletePanel5.tittleText.text = thisPlayer.name + "Telah Mengumpulkan Kartu Pangeran Sultan Hassanuddin";
                                playManager.cardOnCompletePanel5.ShowNotification();
                                break;
                            case 6:
                                playManager.cardOnCompletePanel6.tittleText.text = thisPlayer.name + "Telah Mengumpulkan Kartu Pangeran Pattimura";
                                playManager.cardOnCompletePanel6.ShowNotification();
                                break;
                        }
                    }
                }
            }
        }
    }


    public bool CheckCardIsZero()
    {
        if (transform.childCount > 0)
            return false;

        else
            return true;
    }

    public bool CheckNoInteractableCards()
    {
        foreach (Transform card in transform)
        {
            Button cardButton = card.GetComponentInChildren<Button>();

            if (cardButton != null && cardButton.interactable)
            {
                return false;
            }
        }

        return true;
    }
}
