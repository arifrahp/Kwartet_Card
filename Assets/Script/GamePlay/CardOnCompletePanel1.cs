using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardOnCompletePanel1 : MonoBehaviour
{
    public Image thisImage;
    public TMP_Text tittleText;
    public bool isAlreadyShowed = false;
    public CardManager1 cardManager;
    public float timeAppear;
    void Start()
    {
        cardManager = FindAnyObjectByType<CardManager1>();
        thisImage = GetComponent<Image>();
        thisImage.gameObject.SetActive(false);
    }

    public void ShowNotification()
    {
        if(!isAlreadyShowed)
        {
            thisImage.gameObject.SetActive(true);
            Invoke("NotificationPanelDeactivate", timeAppear);
            isAlreadyShowed = true;
        }
    }

    public void NotificationPanelDeactivate()
    {
        thisImage.gameObject.SetActive(false);
    }

    public void CloseAllPanel()
    {
        foreach (GameObject panel in cardManager.cardPanels)
        {
            panel.SetActive(false);
        }
    }
}
