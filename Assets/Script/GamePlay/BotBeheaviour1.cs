using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotBeheaviour1 : MonoBehaviour
{
    public List<Button> interactableButtons = new List<Button>();
    private PlayManager1 playManager;
    private bool hasPerformedCheckCard = false;
    private bool hasPerformedChooseCard = false;
    private bool hasPerformedGuessCard = false;

    void Start()
    {
        playManager = FindObjectOfType<PlayManager1>();
    }

    public void GetInteractableButton()
    {
        interactableButtons.Clear();
        Button[] allButtons = FindObjectsOfType<Button>();

        // Filter and add only the interactable buttons to the list
        foreach (Button button in allButtons)
        {
            if (button.interactable && !button.CompareTag("menuButton"))
            {
                interactableButtons.Add(button);
            }
        }
    }

    public void ClickRandomButton()
    {
        // Check if there are any interactable buttons
        if (interactableButtons.Count > 0)
        {
            // Randomly select a button from the list
            int randomIndex = Random.Range(0, interactableButtons.Count);
            Button randomButton = interactableButtons[randomIndex];

            // Run the onClick function of the selected button
            RunOnClickFunction(randomButton);
        }
        else
        {
            Debug.LogWarning("No interactable buttons found.");
        }
    }

    public void GetAndClickButton()
    {
        GetInteractableButton();
        ClickRandomButton();
    }

    // Function to run the onClick function of a button
    void RunOnClickFunction(Button button)
    {
        // Check if the button has an onClick event assigned
        if (button.onClick != null && button.interactable)
        {
            // Invoke the onClick event, triggering the button's function
            button.onClick.Invoke();
        }
        else
        {
            Debug.LogWarning("Button has no onClick event assigned.");
        }
    }

    /*public void PlayBotTurn(Player botPlayer)
    {
        StartCoroutine(BotTurnRoutine(botPlayer));
    }

    IEnumerator BotTurnRoutine(Player botPlayer)
    {
        // Wait for a short duration (you can adjust this duration)
        yield return new WaitForSeconds(1f);

        // Check card (Perform only once)
        if (!hasPerformedCheckCard)
        {
            botPlayer.SetOtherCardButtonsInteractable();
            hasPerformedCheckCard = true;
        }
        yield return new WaitForSeconds(2f);

        // Choose card (Perform only once)
        if (!hasPerformedChooseCard)
        {
            botPlayer.BotGetInteractableButton();
            hasPerformedChooseCard = true;
        }
        yield return new WaitForSeconds(2f);

        // Guess card (Perform only once)
        if (!hasPerformedGuessCard)
        {
            botPlayer.BotPressedButton();
            hasPerformedGuessCard = true;
        }
        yield return new WaitForSeconds(2f);

        // Clear interactable state for the next turn
        botPlayer.SetChildCardNotInteractable();

        // Reset the flags for the next turn
        hasPerformedCheckCard = false;
        hasPerformedChooseCard = false;
        hasPerformedGuessCard = false;
    }*/
}
