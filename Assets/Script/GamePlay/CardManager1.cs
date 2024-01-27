using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager1 : MonoBehaviour
{
    //tes
    //tis
    //tus
    public List<GameObject> allCards;
    public List<GameObject> listPlayer;
    public GameObject restOfCard;

    private int currentGroupID = 1;

    public List<GameObject> cardPanels = new List<GameObject>();

    void Start()
    {
        CardGroupID();
        AssignCardIDs();
        InstantiateCardsRandomly();

        // Get all GameObjects with the "Player" tag
        /*GameObject[] allPanelObjects = Resources.FindObjectsOfTypeAll<GameObject>()
        .Where(obj => obj.CompareTag("panel") && !UnityEditor.EditorUtility.IsPersistent(obj))
        .ToArray();

        // Clear the list before populating it again
        cardPanels.Clear();

        foreach (GameObject panelObject in allPanelObjects)
        {
            // Add the GameObject directly to the list
            cardPanels.Add(panelObject);
        }
*/
        GameObject[] allPanelObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        // Clear the list before populating it again
        cardPanels.Clear();

        foreach (GameObject panelObject in allPanelObjects)
        {
            if (panelObject.CompareTag("panel") && panelObject.scene.rootCount != 0)
            {
                cardPanels.Add(panelObject);
            }
        }

        // Check if all components are inactive
        if (CheckAllComponentsInactive())
        {
            // Proceed with your action...
            Debug.Log("All components are inactive. Proceeding with the action...");
        }
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
            CardObject1 cardObject = card.GetComponent<CardObject1>();

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

        int cardsPerGroup = 4;

        for (int i = 0; i < listPlayer.Count; i++)
        {
            // Instantiate the shuffled cards for each player
            for (int j = 0; j < cardsPerGroup; j++)
            {
                int cardIndex = i * cardsPerGroup + j;

                if (cardIndex < shuffledCards.Count)
                {
                    GameObject cardPrefab = shuffledCards[cardIndex];
                    GameObject card = Instantiate(cardPrefab, listPlayer[i].transform); // Instantiate with the intended parent
                    CardObject1 cardObject = card.GetComponent<CardObject1>();

                    if (cardObject != null)
                    {
                        // No need to assign idCard here as it's already assigned in AssignCardIDs
                    }
                }
            }
        }

        // Remaining cards are assigned to restOfCard
        for (int i = listPlayer.Count * cardsPerGroup; i < shuffledCards.Count; i++)
        {
            GameObject remainingCardPrefab = shuffledCards[i];
            GameObject remainingCard = Instantiate(remainingCardPrefab, restOfCard.transform); // Instantiate with the intended parent
                                                                                               // You may want to set properties or perform additional setup here
        }
    }

    void Update()
    {

    }


    public bool CheckAllComponentsInactive()
    {
        // Check if all components are inactive
        foreach (GameObject panelObject in cardPanels)
        {
            if (panelObject.gameObject.activeSelf)
            {
                // If at least one component is active, return false
                return false;
            }
        }
        // If no active components found, return true
        return true;
    }
}
