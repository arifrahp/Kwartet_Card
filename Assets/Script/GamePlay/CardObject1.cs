//using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardObject1 : MonoBehaviour
{
    public bool isThrow = false;
    public Image cardImage;
    public Sprite originImage;
    public Sprite closedImage;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject restOfCards;

    public GameObject questionPanel;
    public GameObject transitionPanel;
    public TMP_Text transitionText;

    public int idCard;
    public int cardNumber;
    public Button cardTouchButton;

    private PlayManager1 playManager;
    private Player1 player;
    private BotBeheaviour1 botBeheaviour;
    private CardHover1 cardHover;
    private RestOfCard1 restOfCard;

    public Vector3 originalPosition;
    public Quaternion originalRotation;

    public List<Button> cardButtons;

    public AudioSource cardSFX;

    private bool isHovering;
    private void Start()
    {
        //cardTouchButton = GetComponentInChildren<Button>();
        playManager = FindAnyObjectByType<PlayManager1>();
        cardImage = GetComponentInChildren<Image>();

        originImage = cardImage.sprite;

        string spritePath = "UsedInGame/belakang";
        closedImage = Resources.Load<Sprite>(spritePath);

        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Bot 1");
        player3 = GameObject.Find("Bot 2");
        player4 = GameObject.Find("Bot 3");
        restOfCards = GameObject.Find("RestOfCard");
        cardSFX = GameObject.Find("CardSFX").GetComponent<AudioSource>();
        questionPanel.SetActive(false);

        cardHover = GetComponentInChildren<CardHover1>();
        botBeheaviour = FindAnyObjectByType<BotBeheaviour1>();
        restOfCard = FindAnyObjectByType<RestOfCard1>();
    }


    public void GetRotationAndPosition()
    {
        originalPosition = this.transform.localPosition;
        originalRotation = this.transform.localRotation;
    }

    void Update()
    {
        player = GetComponentInParent<Player1>();
        if(this.gameObject.transform.parent != restOfCards.transform)
        {
            if (player.isBot)
            {
                cardImage.sprite = closedImage;
            }
            if (!player.isBot)
            {
                cardImage.sprite = originImage;
            }
        }
        if (this.gameObject.transform.parent == restOfCards.transform)
        {
            cardImage.sprite = closedImage;
            cardTouchButton.interactable = false;
        }

        if (isThrow)
        {
            cardImage.color = Color.white;
            cardTouchButton.interactable = false;
        }

        if (!cardHover.PointerOnHover() && !LeanTween.isTweening())
        {
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
        }

        PlayManager1.State currentState = playManager.state;

        CardObject1[] allCards = FindObjectsOfType<CardObject1>();

        switch (currentState)
        {
            case PlayManager1.State.Player1Turn:
                if (playManager.player1HaveCheckCard && playManager.player1HaveChooseCard && !playManager.player1HaveGuessCard)
                {
                    CompareAnswerWithCard(playManager.player1);
                }
                    break;
            case PlayManager1.State.Player2Turn:
                if (playManager.player2HaveCheckCard && playManager.player2HaveChooseCard && !playManager.player2HaveGuessCard)
                {
                    CompareAnswerWithCard(playManager.player2);
                }
                break;
            case PlayManager1.State.Player3Turn:
                if (playManager.player3HaveCheckCard && playManager.player3HaveChooseCard && !playManager.player3HaveGuessCard)
                {
                    CompareAnswerWithCard(playManager.player3);
                }
                break;
            case PlayManager1.State.Player4Turn:
                if (playManager.player4HaveCheckCard && playManager.player4HaveChooseCard && !playManager.player4HaveGuessCard)
                {
                    CompareAnswerWithCard(playManager.player4);
                }
                break;
        }

        GetAllButtonsInCard();
    }

    public void GetAllButtonsInCard()
    {
        Button[] allCardButtons = GetComponentsInChildren<Button>();

        cardButtons.Clear();

        if (allCardButtons != null)
        {
            foreach (Button buttonCard in allCardButtons)
            {
                cardButtons.Add(buttonCard);
            }
        }
    }

    public void OnPressedTest()
    {
        Debug.Log(idCard);
    }

    public IEnumerator DeactivateQuestionPanel()
    {
        yield return new WaitForSeconds(3f);
        transitionPanel.SetActive(false);
    }

    public void WrongAnser()
    {
        PlayManager1.State currentState = playManager.state;
        switch (currentState)
        {
            case PlayManager1.State.Player1Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player1HaveGuessCard = true;
                break;

            case PlayManager1.State.Player2Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player2HaveGuessCard = true;
                break;

            case PlayManager1.State.Player3Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player3HaveGuessCard = true;
                break;

            case PlayManager1.State.Player4Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player4HaveGuessCard = true;
                break;
        }
        if(restOfCard.cards.Count > 0)
        {
            restOfCard.CardGoesToPlayer();
        }
    }

    public void CorrectAnswer()
    {
        PlayManager1.State currentState = playManager.state;
        switch (currentState)
        {
            case PlayManager1.State.Player1Turn:
                this.transform.SetParent(player1.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player1HaveGuessCard = true;
                break;

            case PlayManager1.State.Player2Turn:
                this.transform.SetParent(player2.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player2HaveGuessCard = true;
                break;

            case PlayManager1.State.Player3Turn:
                this.transform.SetParent(player3.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player3HaveGuessCard = true;
                break;

            case PlayManager1.State.Player4Turn:
                this.transform.SetParent(player4.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player4HaveGuessCard = true;
                break;
        }
    }

    public void CardOnTouch()
    {
        if(!playManager.turnPlayerHolder.isBot)
        {
            cardSFX.Play();
        }

        PlayManager1.State currentState = playManager.state;

        CardObject1[] allCards = FindObjectsOfType<CardObject1>();

        switch (currentState)
        {
            case PlayManager1.State.Player1Turn:
                if (playManager.player1HaveCheckCard && !playManager.player1HaveChooseCard)
                {
                    foreach (CardObject1 card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }
                    questionPanel.SetActive(true);
                    playManager.player1HaveChooseCard = true;
                }

                if (!playManager.player1HaveCheckCard)
                {
                    player.cardIDHolder = idCard;
                    player.GetCardsByCardID();
                    foreach (CardObject1 card in allCards)
                    {
                        if (card.idCard == idCard)
                        {
                            card.cardTouchButton.interactable = true;
                        }
                        else
                        {
                            card.cardTouchButton.interactable = false;
                        }
                    }
                    player.SetChildCardNotInteractable();

                    if (playManager.player2.CheckNoInteractableCards()
                        && playManager.player3.CheckNoInteractableCards()
                        && playManager.player4.CheckNoInteractableCards())
                    {
                        if (restOfCard.cards.Count > 0)
                        {
                            restOfCard.CardGoesToPlayer();
                            playManager.cardNotifications[7].isAlreadyShowed = false;
                            playManager.cardNotifications[7].ShowNotification();
                        }

                        playManager.player1HaveChooseCard = true;
                        playManager.player1HaveGuessCard = true;
                    }

                    playManager.player1HaveCheckCard = true;
                }
                else
                    return;

                break;

            case PlayManager1.State.Player2Turn:
                if (playManager.player2HaveCheckCard && !playManager.player2HaveChooseCard)
                {
                    foreach (CardObject1 card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }
                    questionPanel.SetActive(true);
                    playManager.player2HaveChooseCard = true;
                }

                if (!playManager.player2HaveCheckCard)
                {
                    player.cardIDHolder = idCard;
                    player.GetCardsByCardID();
                    foreach (CardObject1 card in allCards)
                    {
                        if (card.idCard == idCard)
                        {
                            card.cardTouchButton.interactable = true;
                        }
                        else
                        {
                            card.cardTouchButton.interactable = false;
                        }
                    }
                    player.SetChildCardNotInteractable();

                    if (playManager.player1.CheckNoInteractableCards()
                        && playManager.player3.CheckNoInteractableCards()
                        && playManager.player4.CheckNoInteractableCards())
                    {
                        if (restOfCard.cards.Count > 0)
                        {
                            restOfCard.CardGoesToPlayer();
                            playManager.cardNotifications[7].isAlreadyShowed = false;
                            playManager.cardNotifications[7].ShowNotification();
                        }

                        playManager.player2HaveChooseCard = true;
                        playManager.player2HaveGuessCard = true;
                    }

                    playManager.player2HaveCheckCard = true;
                }
                else
                    return;

                break;

            case PlayManager1.State.Player3Turn:
                if (playManager.player3HaveCheckCard && !playManager.player3HaveChooseCard)
                {
                    foreach (CardObject1 card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }
                    questionPanel.SetActive(true);
                    playManager.player3HaveChooseCard = true;
                }

                if (!playManager.player3HaveCheckCard)
                {
                    player.cardIDHolder = idCard;
                    player.GetCardsByCardID();
                    foreach (CardObject1 card in allCards)
                    {
                        if (card.idCard == idCard)
                        {
                            card.cardTouchButton.interactable = true;
                        }
                        else
                        {
                            card.cardTouchButton.interactable = false;
                        }
                    }
                    player.SetChildCardNotInteractable();

                    if (playManager.player1.CheckNoInteractableCards()
                        && playManager.player2.CheckNoInteractableCards()
                        && playManager.player4.CheckNoInteractableCards())
                    {
                        if (restOfCard.cards.Count > 0)
                        {
                            restOfCard.CardGoesToPlayer();
                            playManager.cardNotifications[7].isAlreadyShowed = false;
                            playManager.cardNotifications[7].ShowNotification();
                        }

                        playManager.player3HaveChooseCard = true;
                        playManager.player3HaveGuessCard = true;
                    }

                    playManager.player3HaveCheckCard = true;
                }
                else
                    return;

                break;

            case PlayManager1.State.Player4Turn:
                if (playManager.player4HaveCheckCard && !playManager.player4HaveChooseCard)
                {
                    foreach (CardObject1 card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }
                    questionPanel.SetActive(true);
                    playManager.player4HaveChooseCard = true;
                }

                if (!playManager.player4HaveCheckCard)
                {
                    player.cardIDHolder = idCard;
                    player.GetCardsByCardID();
                    foreach (CardObject1 card in allCards)
                    {
                        if (card.idCard == idCard)
                        {
                            card.cardTouchButton.interactable = true;
                        }
                        else
                        {
                            card.cardTouchButton.interactable = false;
                        }
                    }
                    player.SetChildCardNotInteractable();

                    if (playManager.player1.CheckNoInteractableCards()
                        && playManager.player2.CheckNoInteractableCards()
                        && playManager.player3.CheckNoInteractableCards())
                    {
                        if (restOfCard.cards.Count > 0)
                        {
                            restOfCard.CardGoesToPlayer();
                            playManager.cardNotifications[7].isAlreadyShowed = false;
                            playManager.cardNotifications[7].ShowNotification();
                        }

                        playManager.player4HaveChooseCard = true;
                        playManager.player4HaveGuessCard = true;
                    }

                    playManager.player4HaveCheckCard = true;
                }
                else
                    return;

                break;
        }
    }
    
    public void DoNothing()
    {

    }

    public void CompareAnswerWithCard(Player1 chosenPlayer)
    {
        foreach (Button button in cardButtons)
        {
            bool foundMatch = false;  // Flag to check if a match is found for the current button

            foreach (CardObject1 cardObject in chosenPlayer.cardsWithID)
            {
                if (cardObject.cardNumber == cardButtons.IndexOf(button))
                {
                    button.interactable = false;
                    foundMatch = true;
                    break;  // Exit the loop since a match is found
                }
            }

            // If no match was found and the current button is at index 0, set it to not interactable
            if (!foundMatch && cardButtons.IndexOf(button) == 0)
            {
                button.interactable = false;
            }
            else if (!foundMatch)
            {
                button.interactable = true;
            }
        }
    }


    public void OnCardHoverEnter()
    {
        isHovering = true;
        LeanTween.cancel(this.gameObject);

        // Move the object to a new position and rotation in world space
        LeanTween.moveLocal(this.gameObject, originalPosition + new Vector3(0, 1.2f, -3), 0.3f);
        LeanTween.rotateLocal(this.gameObject, Vector3.zero, 0.3f);
    }

    public void OnCardHoverExit()
    {
        isHovering = false;
        LeanTween.cancel(this.gameObject);

        // Move the object back to its original position and rotation in world space
        LeanTween.moveLocal(this.gameObject, originalPosition, 0.1f);
        LeanTween.rotateLocal(this.gameObject, originalRotation.eulerAngles, 0.1f);
    }

}
