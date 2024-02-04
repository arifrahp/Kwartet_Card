using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayManager1 : MonoBehaviour
{
    [SerializeField] public State state;
    public UnityEvent player1CheckCard;
    public UnityEvent player2CheckCard;
    public UnityEvent player3CheckCard;
    public UnityEvent player4CheckCard;

    public int roundCount = 0;

    public bool player1HaveCheckCard = false;
    public bool player1HaveChooseCard = false;
    public bool player1HaveGuessCard = false;

    public bool player2HaveCheckCard = false;
    public bool player2HaveChooseCard = false;
    public bool player2HaveGuessCard = false;

    public bool player3HaveCheckCard = false;
    public bool player3HaveChooseCard = false;
    public bool player3HaveGuessCard = false;

    public bool player4HaveCheckCard = false;
    public bool player4HaveChooseCard = false;
    public bool player4HaveGuessCard = false;

    public GameObject winner1;
    public GameObject winner2;
    public GameObject winner3;
    public GameObject winner4;

    public Dictionary<Player1, int> playerScores = new Dictionary<Player1, int>();
    public List<Player1> allPlayers = new List<Player1>();

    public Player1 player1;
    public Player1 player2;
    public Player1 player3;
    public Player1 player4;

    public CardOnCompletePanel1 cardOnCompletePanel1;
    public CardOnCompletePanel1 cardOnCompletePanel2;
    public CardOnCompletePanel1 cardOnCompletePanel3;
    public CardOnCompletePanel1 cardOnCompletePanel4;
    public CardOnCompletePanel1 cardOnCompletePanel5;
    public CardOnCompletePanel1 cardOnCompletePanel6;
    public CardOnCompletePanel1 restOfCardsNotification;

    public GameObject holderCanvas;
    public GameObject gameOverCanvas;

    public TMP_Text winner1Text;
    public TMP_Text winner2Text;
    public TMP_Text winner3Text;
    public TMP_Text winner4Text;

    public bool player1IsDone = false;
    public bool player2IsDone = false;
    public bool player3IsDone = false;
    public bool player4IsDone = false;

    private BotBeheaviour1 botBeheaviour;
    private CardManager1 cardManager;

    public enum State
    {
        Initialization,
        Player1Turn,
        Player2Turn,
        Player3Turn,
        Player4Turn,
        GameOver
    }

    void Start()
    {
        gameOverCanvas.SetActive(false);
        holderCanvas.SetActive(false);
        state = State.Initialization;

        botBeheaviour = FindAnyObjectByType<BotBeheaviour1>();
        cardManager = FindAnyObjectByType<CardManager1>();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerObject in playerObjects)
        {
            Player1 playerComponent = playerObject.GetComponent<Player1>();
            if (playerComponent != null)
            {
                allPlayers.Add(playerComponent);
            }
        }
    }

    void Update()
    {
        HandleGameState();
    }

    void HandleGameState()
    {
        switch (state)
        {
            case State.Initialization:
                HandleInitializationState();
                break;

            case State.Player1Turn:
                HandlePlayer1TurnState();
                break;

            case State.Player2Turn:
                HandlePlayer2TurnState();
                break;

            case State.Player3Turn:
                HandlePlayer3TurnState();
                break;

            case State.Player4Turn:
                HandlePlayer4TurnState();
                break;

            case State.GameOver:
                GameIsOver();
                break;
        }
    }

    void HandleInitializationState()
    {
        bool isAddUp = false;

        if (!player1IsDone || !player2IsDone || !player3IsDone || !player4IsDone)
        {
            if (!isAddUp)
            {
                player1HaveCheckCard = false;
                player1HaveGuessCard = false;
                player1HaveChooseCard = false;

                player2HaveCheckCard = false;
                player2HaveGuessCard = false;
                player2HaveChooseCard = false;

                player3HaveCheckCard = false;
                player3HaveGuessCard = false;
                player3HaveChooseCard = false;

                player4HaveCheckCard = false;
                player4HaveGuessCard = false;
                player4HaveChooseCard = false;

                Player1[] players = FindObjectsOfType<Player1>();
                foreach (Player1 player in players)
                {
                    player.ShuffleCardPositions();
                }

                roundCount += 1;
                isAddUp = true;
            }
            if (isAddUp)
            {
                state = State.Player1Turn;
            }
        }
        if (player1IsDone && player2IsDone && player3IsDone && player4IsDone)
        {
            state = State.GameOver;
        }
    }

    void HandlePlayer1TurnState()
    {
        if (player1.CheckCardIsZero())
        {
            player1IsDone = true;
            state = State.Player2Turn;
        }

        else if (!player1.CheckCardIsZero())
        {
            if (!player1HaveCheckCard)
                player1CheckCard.Invoke();

            if (!player1HaveCheckCard && !player1HaveChooseCard && !player1HaveGuessCard && player1.CheckNoInteractableCards())
            {
                player1IsDone = true;
                state = State.Player2Turn;
            }
            

            if (player1.isBot)
            {
                holderCanvas.SetActive(true);
                if (!player1HaveCheckCard && !player1HaveChooseCard && !player1HaveGuessCard)
                {
                    if (cardManager.CheckAllComponentsInactive())
                    {
                        Debug.Log("masuk1");
                        Invoke("BotGetInteractableButton", 2f);
                        botBeheaviour.ClickRandomButton();
                    }
                }
                if (player1HaveCheckCard && !player1HaveChooseCard && !player1HaveGuessCard)
                {
                    Debug.Log("masuk2");
                    Invoke("BotGetInteractableButton", 2f);
                    botBeheaviour.ClickRandomButton();
                }
                if (player1HaveCheckCard && player1HaveChooseCard && !player1HaveGuessCard)
                {
                    Debug.Log("masuk3");
                    Invoke("BotGetInteractableButton", 2f);
                    botBeheaviour.ClickRandomButton();
                }
            }
            else if (!player1.isBot)
            {
                holderCanvas.SetActive(false);
            }

            if (player1HaveCheckCard && player1HaveChooseCard && player1HaveGuessCard)
                state = State.Player2Turn;
        }
    }

    void HandlePlayer2TurnState()
    {
        if (player2.CheckCardIsZero())
        {
            player2IsDone = true;
            state = State.Player3Turn;
        
        }

        else if (!player2.CheckCardIsZero())
        {
            if (!player2HaveCheckCard)
                player2CheckCard.Invoke();

            if (!player2HaveCheckCard && !player2HaveChooseCard && !player2HaveGuessCard && player2.CheckNoInteractableCards())
            {
                player2IsDone = true;
                state = State.Player3Turn;
            }

            if (player2.isBot)
            {
                holderCanvas.SetActive(true);
                if (!player2HaveCheckCard && !player2HaveChooseCard && !player2HaveGuessCard)
                {
                    if(cardManager.CheckAllComponentsInactive())
                    {
                        Debug.Log("masuk1");
                        Invoke("BotGetInteractableButton", 2f);
                        botBeheaviour.ClickRandomButton();
                    }
                }
                if (player2HaveCheckCard && !player2HaveChooseCard && !player2HaveGuessCard)
                {
                    Debug.Log("masuk2");
                    Invoke("BotGetInteractableButton", 2f);
                    botBeheaviour.ClickRandomButton();
                }
                if (player2HaveCheckCard && player2HaveChooseCard && !player2HaveGuessCard)
                {
                    Debug.Log("masuk3");
                    Invoke("BotGetInteractableButton", 2f);
                    botBeheaviour.ClickRandomButton();
                }
            }
            else if (!player2.isBot)
            {
                holderCanvas.SetActive(false);
            }
            StartCoroutine(WaitBotPerformAction(10f));

            if (player2HaveCheckCard && player2HaveChooseCard && player2HaveGuessCard)
                state = State.Player3Turn;
        }
    }

    void HandlePlayer3TurnState()
    {
        if (player3.CheckCardIsZero())
        {
            player3IsDone = true;
            state = State.Player4Turn;
        }

        else if (!player3.CheckCardIsZero())
        {
            if (!player3HaveCheckCard)
                player3CheckCard.Invoke();

            if (!player3HaveCheckCard && !player3HaveChooseCard && !player3HaveGuessCard && player3.CheckNoInteractableCards())
            {
                player3IsDone = true;
                state = State.Player4Turn;
            }

            if (player3.isBot)
            {
                if (!player3HaveCheckCard && !player3HaveChooseCard && !player3HaveGuessCard)
                {
                    holderCanvas.SetActive(true);
                    if (cardManager.CheckAllComponentsInactive())
                    {
                        Debug.Log("masuk1");
                        Invoke("BotGetInteractableButton", 2f);
                        botBeheaviour.ClickRandomButton();
                    }
                }
                if (player3HaveCheckCard && !player3HaveChooseCard && !player3HaveGuessCard)
                {
                    Debug.Log("masuk2");
                    Invoke("BotGetInteractableButton", 2f);
                    botBeheaviour.ClickRandomButton();
                }
                if (player3HaveCheckCard && player3HaveChooseCard && !player3HaveGuessCard)
                {
                    Debug.Log("masuk3");
                    Invoke("BotGetInteractableButton", 2f);
                    botBeheaviour.ClickRandomButton();
                }
            }
            else if (!player3.isBot)
            {
                holderCanvas.SetActive(false);
            }

            if (player3HaveCheckCard && player3HaveChooseCard && player3HaveGuessCard)
                state = State.Player4Turn;
        }
    }

    void HandlePlayer4TurnState()
    {
        if (player4.CheckCardIsZero())
        {
            player4IsDone = true;
            state = State.Initialization;
        }

        else if (!player4.CheckCardIsZero())
        {
            if (!player4HaveCheckCard)
                player4CheckCard.Invoke();

            if (!player4HaveCheckCard && !player4HaveChooseCard && !player4HaveGuessCard && player4.CheckNoInteractableCards())
            {
                player4IsDone = true;
                state = State.Initialization;
            }

            if (player4.isBot)
            {
                holderCanvas.SetActive(true);
                if (!player4HaveCheckCard && !player4HaveChooseCard && !player4HaveGuessCard)
                {
                    if (cardManager.CheckAllComponentsInactive())
                    {
                        Debug.Log("masuk1");
                        Invoke("BotGetInteractableButton", 2f);
                        botBeheaviour.ClickRandomButton();
                    }
                }
                if (player4HaveCheckCard && !player4HaveChooseCard && !player4HaveGuessCard)
                {
                    Debug.Log("masuk2");
                    Invoke("BotGetInteractableButton", 2f);
                    botBeheaviour.ClickRandomButton();
                }
                if (player4HaveCheckCard && player4HaveChooseCard && !player4HaveGuessCard)
                {
                    Debug.Log("masuk3");
                    Invoke("BotGetInteractableButton", 2f);
                    botBeheaviour.ClickRandomButton();
                }
            }
            else if (!player3.isBot)
            {
                holderCanvas.SetActive(false);
            }

            if (player4HaveCheckCard && player4HaveChooseCard && player4HaveGuessCard)
                state = State.Initialization;
        }
    }

    void GameIsOver()
    {
        CalculatePlayerScores();
        OrderPlayerScores();

        allPlayers.Sort((player1, player2) => playerScores[player2].CompareTo(playerScores[player1]));

        for (int i = 0; i < allPlayers.Count; i++)
        {
            Player1 player = allPlayers[i];
            GameObject winnerGameObject = GetWinnerGameObject(i + 1);
            player.transform.SetParent(winnerGameObject.transform);
        }

        PositionNaming();
        gameOverCanvas.SetActive(true);
    }

    GameObject GetWinnerGameObject(int position)
    {
        switch (position)
        {
            case 1:
                return winner1;
            case 2:
                return winner2;
            case 3:
                return winner3;
            case 4:
                return winner4;
            default:
                return null;
        }
    }

    public void BotGetInteractableButton()
    {
        botBeheaviour.GetInteractableButton();
    }

    private IEnumerator WaitBotPerformAction(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        botBeheaviour.GetInteractableButton();
    }

    void CalculatePlayerScores()
    {
        playerScores.Clear();

        foreach (Player1 player in allPlayers)
        {
            int score = player.CalculateScore();
            playerScores.Add(player, score);
        }
    }

    void OrderPlayerScores()
    {
        List<Player1> orderedPlayers = allPlayers.OrderBy(player => -playerScores[player]).ToList();
        allPlayers = orderedPlayers;
    }

    void PositionNaming()
    {
        winner1Text.text = allPlayers[0].transform.name;
        winner2Text.text = allPlayers[1].transform.name;
        winner3Text.text = allPlayers[2].transform.name;
        winner4Text.text = allPlayers[3].transform.name;
    }
}
