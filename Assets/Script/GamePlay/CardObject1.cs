using Palmmedia.ReportGenerator.Core.Reporting.Builders;
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

    private bool isHovering;
    private void Start()
    {
        //cardTouchButton = GetComponentInChildren<Button>();
        playManager = FindAnyObjectByType<PlayManager1>();
        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Bot 1");
        player3 = GameObject.Find("Bot 2");
        player4 = GameObject.Find("Bot 3");
        restOfCards = GameObject.Find("RestOfCard");
        questionPanel.SetActive(false);

        cardHover = GetComponentInChildren<CardHover1>();
        cardImage = GetComponentInChildren<Image>();
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
                cardImage.color = Color.black;
            }
            if (!player.isBot)
            {
                cardImage.color = Color.white;
            }
        }
        if (this.gameObject.transform.parent == restOfCards.transform)
        {
            cardImage.color = Color.black;
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
                playManager.player1.SetChildCardInteractable();
                playManager.player1HaveGuessCard = true;
                break;

            case PlayManager1.State.Player2Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player2.SetChildCardInteractable();
                playManager.player2HaveGuessCard = true;
                break;

            case PlayManager1.State.Player3Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player3.SetChildCardInteractable();
                playManager.player3HaveGuessCard = true;
                break;

            case PlayManager1.State.Player4Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player4.SetChildCardInteractable();
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
                playManager.player1.SetChildCardInteractable();
                playManager.player1HaveGuessCard = true;
                break;

            case PlayManager1.State.Player2Turn:
                this.transform.SetParent(player2.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player2.SetChildCardInteractable();
                playManager.player2HaveGuessCard = true;
                break;

            case PlayManager1.State.Player3Turn:
                this.transform.SetParent(player3.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player3.SetChildCardInteractable();
                playManager.player3HaveGuessCard = true;
                break;

            case PlayManager1.State.Player4Turn:
                this.transform.SetParent(player4.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player4.SetChildCardInteractable();
                playManager.player4HaveGuessCard = true;
                break;
        }
    }

    public void CardOnTouch()
    {
        PlayManager1.State currentState = playManager.state;

        CardObject1[] allCards = FindObjectsOfType<CardObject1>();

        switch (currentState)
        {
            case PlayManager1.State.Player1Turn:

                if (playManager.player1HaveCheckCard && !playManager.player1HaveGuessCard)
                {
                    foreach (CardObject1 card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }

                    if(idCard == playManager.player1.cardIDHolder)
                    {
                        CorrectAnswer();
                    }
                    else if(idCard != playManager.player1.cardIDHolder)
                    {
                        WrongAnser();
                    }

                    playManager.player1HaveChooseCard = true;
                }
                
                if (!playManager.player1HaveCheckCard)
                {
                    player.cardIDHolder = idCard;
                    playManager.player2.SetCardButtonInteractable(idCard);
                    playManager.player3.SetCardButtonInteractable(idCard);
                    playManager.player4.SetCardButtonInteractable(idCard);
                    player.SetChildCardNotInteractable();

                    if (playManager.player2.CheckNoInteractableCards()
                        && playManager.player3.CheckNoInteractableCards()
                        && playManager.player4.CheckNoInteractableCards())
                    {
                        if (restOfCard.cards.Count > 0)
                        {
                            restOfCard.CardGoesToPlayer();
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

                if (playManager.player2HaveCheckCard && !playManager.player2HaveGuessCard)
                {
                    foreach (CardObject1 card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }

                    if (idCard == playManager.player2.cardIDHolder)
                    {
                        CorrectAnswer();
                    }
                    else if (idCard != playManager.player2.cardIDHolder)
                    {
                        WrongAnser();
                    }

                    playManager.player2HaveChooseCard = true;
                }

                if (!playManager.player2HaveCheckCard)
                {
                    player.cardIDHolder = idCard;
                    foreach (CardObject1 card in allCards)
                    playManager.player1.SetCardButtonInteractable(idCard);
                    playManager.player3.SetCardButtonInteractable(idCard);
                    playManager.player4.SetCardButtonInteractable(idCard);
                    player.SetChildCardNotInteractable();

                    if (playManager.player1.CheckNoInteractableCards()
                        && playManager.player3.CheckNoInteractableCards()
                        && playManager.player4.CheckNoInteractableCards())
                    {
                        if (restOfCard.cards.Count > 0)
                        {
                            restOfCard.CardGoesToPlayer();
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

                if (playManager.player3HaveCheckCard && !playManager.player3HaveGuessCard)
                {
                    foreach (CardObject1 card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }

                    if (idCard == playManager.player3.cardIDHolder)
                    {
                        CorrectAnswer();
                    }
                    else if (idCard != playManager.player3.cardIDHolder)
                    {
                        WrongAnser();
                    }

                    playManager.player3HaveChooseCard = true;
                }

                if (!playManager.player3HaveCheckCard)
                {
                    player.cardIDHolder = idCard;
                    playManager.player1.SetCardButtonInteractable(idCard);
                    playManager.player2.SetCardButtonInteractable(idCard);
                    playManager.player4.SetCardButtonInteractable(idCard);
                    player.SetChildCardNotInteractable();

                    if (playManager.player1.CheckNoInteractableCards()
                        && playManager.player2.CheckNoInteractableCards()
                        && playManager.player4.CheckNoInteractableCards())
                    {
                        if (restOfCard.cards.Count > 0)
                        {
                            restOfCard.CardGoesToPlayer();
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

                if (playManager.player4HaveCheckCard && !playManager.player4HaveGuessCard)
                {
                    foreach (CardObject1 card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }

                    if (idCard == playManager.player4.cardIDHolder)
                    {
                        CorrectAnswer();
                    }
                    else if (idCard != playManager.player4.cardIDHolder)
                    {
                        WrongAnser();
                    }

                    playManager.player4HaveChooseCard = true;
                }

                if (!playManager.player4HaveCheckCard)
                {
                    player.cardIDHolder = idCard;
                    playManager.player1.SetCardButtonInteractable(idCard);
                    playManager.player2.SetCardButtonInteractable(idCard);
                    playManager.player3.SetCardButtonInteractable(idCard);
                    player.SetChildCardNotInteractable();

                    if (playManager.player1.CheckNoInteractableCards()
                        && playManager.player2.CheckNoInteractableCards()
                        && playManager.player3.CheckNoInteractableCards())
                    {
                        if (restOfCard.cards.Count > 0)
                        {
                            restOfCard.CardGoesToPlayer();
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
