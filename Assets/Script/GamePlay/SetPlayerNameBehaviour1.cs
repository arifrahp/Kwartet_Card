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

    public TMP_Text playerNameCard;
    public TMP_Text bot1NameCard;
    public TMP_Text bot2NameCard;
    public TMP_Text bot3NameCard;

    public bool isShow = false;

    public TMP_Text player1InputField;
    public TMP_Text player2InputField;
    public TMP_Text player3InputField;
    public TMP_Text player4InputField;

    private const int MaxNameLength = 8;

    private PlayManager1 playManager;

    public Color notInTurnColor;
    public Color inTurnColor;
    private void Start()
    {
        playManager = FindAnyObjectByType<PlayManager1>();
        isShow = false;
        thisGameObject.SetActive(true);
    }

    private void Update()
    {
        if (isShow)
        {
            thisGameObject.SetActive(false);
        }

        /*Player1ChangeColorInTurn();
        Bot1ChangeColorInTurn();
        Bot2ChangeColorInTurn();
        Bot3ChangeColorInTurn();*/
    }

    public void ConfirmButtonPressed()
    {
        string player1Name = TruncateString(player1InputField.text, MaxNameLength);
        string player2Name = TruncateString(player2InputField.text, MaxNameLength);
        string player3Name = TruncateString(player3InputField.text, MaxNameLength);
        string player4Name = TruncateString(player4InputField.text, MaxNameLength);

        if (player1InputField.text.Length > 1)
        {
            playerObject.name = player1Name;
            playerNameCard.text = player1Name;
        }
        else if(player1InputField.text.Length < 2)
        {
            player1Name = "Player";
            playerNameCard.text = "Player";
        }

        if (player2InputField.text.Length > 1)
        {
            bot1Object.name = player2Name;
            bot1NameCard.text = player2Name;
        }
        else if (player2InputField.text.Length < 2)
        {
            bot2Object.name = "Bot 1";
            bot1NameCard.text = "Bot 1";
        }


        if (player3InputField.text.Length > 1)
        {
            bot2Object.name = player3Name;
            bot2NameCard.text = player3Name;
        }
        else if (player3InputField.text.Length < 2)
        {
            bot2Object.name = "Bot 2";
            bot2NameCard.text = "Bot 2";
        }

        if (player4InputField.text.Length > 1)
        {
            bot3Object.name = player4Name;
            bot3NameCard.text = player4Name;
        }
        else if (player4InputField.text.Length < 2)
        {
            bot3Object.name = "Bot 3";
            bot3NameCard.text = "Bot 3";
        }

        thisGameObject.SetActive(false);
        isShow = true;
    }

    private string TruncateString(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    /*public void Player1ChangeColorInTurn()
    {
        if(playManager.state == PlayManager1.State.Player1Turn)
        {
            LeanTween.textColor(playerNameCard.rectTransform, inTurnColor, 0f);
        }
        else
        {
            LeanTween.textColor(playerNameCard.rectTransform, notInTurnColor, 0f);
        }
    }

    public void Bot1ChangeColorInTurn()
    {
        if (playManager.state == PlayManager1.State.Player2Turn)
        {
            LeanTween.textColor(bot1NameCard.rectTransform, inTurnColor, 0f);
        }
        else
        {
            LeanTween.textColor(bot1NameCard.rectTransform, notInTurnColor, 0f);
        }
    }

    public void Bot2ChangeColorInTurn()
    {
        if (playManager.state == PlayManager1.State.Player3Turn)
        {
            LeanTween.textColor(bot2NameCard.rectTransform, inTurnColor, 0f);
        }
        else
        {
            LeanTween.textColor(bot2NameCard.rectTransform, notInTurnColor, 0f);
        }
    }

    public void Bot3ChangeColorInTurn()
    {
        if (playManager.state == PlayManager1.State.Player4Turn)
        {
            LeanTween.textColor(bot3NameCard.rectTransform, inTurnColor, 0f);
        }
        else
        {
            LeanTween.textColor(bot3NameCard.rectTransform, notInTurnColor, 0f);
        }
    }*/
}
