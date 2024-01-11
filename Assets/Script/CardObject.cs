using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardObject : MonoBehaviour
{
    public Image cardImage;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject questionPanel;
    public GameObject transitionPanel;
    public TMP_Text transitionText;

    public int idCard;
    public int cardNumber;
    public Button cardTouchButton;

    private PlayManager playManager;
    private Player player;
    public BotBeheaviour botBeheaviour;
    public CardHover cardHover;

    public Vector3 originalPosition;
    public Quaternion originalRotation;

    private bool isHovering;
    private void Start()
    {
        //cardTouchButton = GetComponentInChildren<Button>();
        playManager = FindAnyObjectByType<PlayManager>();
        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Bot 1");
        player3 = GameObject.Find("Bot 2");
        player4 = GameObject.Find("Bot 3");
        questionPanel.SetActive(false);

        cardHover = GetComponentInChildren<CardHover>();
        cardImage = GetComponentInChildren<Image>();
        botBeheaviour = FindAnyObjectByType<BotBeheaviour>();
    }


    public void GetRotationAndPosition()
    {
        originalPosition = this.transform.localPosition;
        originalRotation = this.transform.localRotation;
    }

    void Update()
    {
        player = GetComponentInParent<Player>();
        if (player.isBot)
        {
            cardImage.color = Color.black;
        }
        if (!player.isBot)
        {
            cardImage.color = Color.white;
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
        PlayManager.State currentState = playManager.state;
        switch (currentState)
        {
            case PlayManager.State.Player1Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player1HaveGuessCard = true;
                break;

            case PlayManager.State.Player2Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player2HaveGuessCard = true;
                break;

            case PlayManager.State.Player3Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player3HaveGuessCard = true;
                break;

            case PlayManager.State.Player4Turn:
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Salah";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player4HaveGuessCard = true;
                break;
        }
    }

    public void CorrectAnswer()
    {
        PlayManager.State currentState = playManager.state;
        switch (currentState)
        {
            case PlayManager.State.Player1Turn:
                this.transform.SetParent(player1.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player1HaveGuessCard = true;
                break;

            case PlayManager.State.Player2Turn:
                this.transform.SetParent(player2.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player2HaveGuessCard = true;
                break;

            case PlayManager.State.Player3Turn:
                this.transform.SetParent(player3.transform);
                questionPanel.SetActive(false);
                transitionPanel.SetActive(true);
                transitionText.text = "Benar";
                StartCoroutine(DeactivateQuestionPanel());
                playManager.player3HaveGuessCard = true;
                break;

            case PlayManager.State.Player4Turn:
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
        PlayManager.State currentState = playManager.state;

        CardObject[] allCards = FindObjectsOfType<CardObject>();

        switch (currentState)
        {
            case PlayManager.State.Player1Turn:
                if (playManager.player1HaveCheckCard && !playManager.player1HaveChooseCard)
                {
                    foreach (CardObject card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }
                    questionPanel.SetActive(true);
                    playManager.player1HaveChooseCard = true;
                }
                
                if (!playManager.player1HaveCheckCard)
                {
                    foreach (CardObject card in allCards)
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
                    playManager.player1HaveCheckCard = true;
                }
                else
                    return;

                break;

            case PlayManager.State.Player2Turn:
                if (playManager.player2HaveCheckCard && !playManager.player2HaveChooseCard)
                {
                    foreach (CardObject card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }
                    questionPanel.SetActive(true);
                    playManager.player2HaveChooseCard = true;
                }

                if (!playManager.player2HaveCheckCard)
                {
                    foreach (CardObject card in allCards)
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
                    /*if (player.isBot)
                    {
                        Invoke("BotGetInteractableButton", 6f);
                        botBeheaviour.ClickRandomButton();
                    }*/
                    playManager.player2HaveCheckCard = true;
                }
                else
                    return;

                break;
            
            case PlayManager.State.Player3Turn:
                if (playManager.player3HaveCheckCard && !playManager.player3HaveChooseCard)
                {
                    foreach (CardObject card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }
                    questionPanel.SetActive(true);
                    playManager.player3HaveChooseCard = true;
                }

                if (!playManager.player3HaveCheckCard)
                {
                    foreach (CardObject card in allCards)
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
                    playManager.player3HaveCheckCard = true;
                }
                else
                    return;

                break;
    
            case PlayManager.State.Player4Turn:
                if (playManager.player4HaveCheckCard && !playManager.player4HaveChooseCard)
                {
                    foreach (CardObject card in allCards)
                    {
                        card.cardTouchButton.interactable = false;
                    }
                    questionPanel.SetActive(true);
                    playManager.player4HaveChooseCard = true;
                }

                if (!playManager.player4HaveCheckCard)
                {
                    foreach (CardObject card in allCards)
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
        LeanTween.moveLocal(this.gameObject, originalPosition + new Vector3(0, 0.5f, -1), 0.3f);
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
