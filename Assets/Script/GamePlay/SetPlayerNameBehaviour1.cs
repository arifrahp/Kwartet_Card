using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerNameBehaviour1 : MonoBehaviour
{

    public GameObject thisGameObject;
    public GameObject playerObject;
    public GameObject bot1Object;
    public GameObject bot2Object;
    public GameObject bot3Object;

    public bool isShow = false;

    public TMP_Text player1InputField;
    public TMP_Text player2InputField;
    public TMP_Text player3InputField;
    public TMP_Text player4InputField;

    private void Start()
    {
        isShow = false;
        thisGameObject.SetActive(true);
    }

    private void Update()
    {
        if (isShow)
        {
            thisGameObject.SetActive(false);
        }
    }

    public void ConfirmButtonPressed()
    {
        string player1Name = player1InputField.text;
        string player2Name = player2InputField.text;
        string player3Name = player3InputField.text;
        string player4Name = player4InputField.text;

        if (!string.IsNullOrEmpty(player1Name))
        {
            playerObject.name = player1Name;
        }
        else if(string.IsNullOrEmpty(player1Name))
        {
            player1Name = "Player 1";
        }

        if (!string.IsNullOrEmpty(player2Name))
        {
            bot1Object.name = player2Name;
        }
        else if (string.IsNullOrEmpty(player2Name))
        {
            bot2Object.name = "Bot 1";
        }


        if (!string.IsNullOrEmpty(player3Name))
        {
            bot2Object.name = player3Name;
        }
        else if (string.IsNullOrEmpty(player3Name))
        {
            bot2Object.name = "Bot 2";
        }

        if (!string.IsNullOrEmpty(player4Name))
        {
            bot3Object.name = player4Name;
        }
        else if (string.IsNullOrEmpty(player4Name))
        {
            bot3Object.name = "Bot 3";
        }

        thisGameObject.SetActive(false);
        isShow = true;
    }
}
