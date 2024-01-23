using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestOfCard1 : MonoBehaviour
{
    public List<int> cards = new List<int>();
    private int previousChildCount = 0;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private PlayManager1 playManager;
    void Start()
    {
        playManager = FindAnyObjectByType<PlayManager1>();
        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Bot 1");
        player3 = GameObject.Find("Bot 2");
        player4 = GameObject.Find("Bot 3");
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

                    Quaternion localRotation = Quaternion.Euler(0f, 11f, 0f);

                    child.localPosition = localPosition;
                    child.localRotation = localRotation;

                    cardObject.GetRotationAndPosition();
                }

                previousChildCount = currentChildCount;
            }
        }

    }

    public void CardGoesToPlayer()
    {
        PlayManager1.State currentState = playManager.state;
        Transform playerTransform = null;

        switch (currentState)
        {
            case PlayManager1.State.Player1Turn:
                playerTransform = player1.transform;
                break;

            case PlayManager1.State.Player2Turn:
                playerTransform = player2.transform;
                break;

            case PlayManager1.State.Player3Turn:
                playerTransform = player3.transform;
                break;

            case PlayManager1.State.Player4Turn:
                playerTransform = player4.transform;
                break;
        }

        if (playerTransform != null && cards.Count > 0)
        {
            int cardIndex = 0; // You might want to adjust this based on your game logic
            Transform cardToMove = transform.GetChild(cardIndex);

            // Set the parent to the player's transform
            cardToMove.SetParent(playerTransform);

            // Optionally, reset the local position and rotation
            cardToMove.localPosition = Vector3.zero;
            cardToMove.localRotation = Quaternion.identity;

            // Remove the card from the cards list
            cards.RemoveAt(cardIndex);

            // Optionally, update the previousChildCount to trigger PopulateCardIDs in the next update
            previousChildCount = transform.childCount;
        }
    }
}
