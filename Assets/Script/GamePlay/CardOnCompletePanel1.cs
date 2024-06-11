using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardOnCompletePanel1 : MonoBehaviour
{
    public Image thisPanel;
    public bool isAlreadyShowed = false;
    public CardManager1 cardManager;
    void Start()
    {
        cardManager = FindAnyObjectByType<CardManager1>();

        thisPanel = GetComponent<Image>();
        thisPanel.gameObject.SetActive(false);
    }

    public void ShowNotification()
    {
        if(!isAlreadyShowed)
        {
            thisPanel.gameObject.SetActive(true);
            Invoke("NotificationPanelDeactivate", 13f);
            isAlreadyShowed = true;
        }
    }

    public void NotificationPanelDeactivate()
    {
        thisPanel.gameObject.SetActive(false);
    }

    public void CloseAllPanel()
    {
        foreach (GameObject panel in cardManager.cardPanels)
        {
            panel.SetActive(false);
        }
    }
}
