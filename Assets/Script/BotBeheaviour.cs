using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotBeheaviour : MonoBehaviour
{
    public List<Button> interactableButtons = new List<Button>();

    void Start()
    {
        
    }

    public void GetInteractableButton()
    {
        interactableButtons.Clear();
        Button[] allButtons = FindObjectsOfType<Button>();

        // Filter and add only the interactable buttons to the list
        foreach (Button button in allButtons)
        {
            if (button.interactable)
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
}
