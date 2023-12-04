using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayManager : MonoBehaviour
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

    public Dictionary<Player, int> playerScores = new Dictionary<Player, int>();
    public List<Player> allPlayers = new List<Player>();

    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;
    private BotBeheaviour botBeheaviour;

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
        state = State.Initialization;

        botBeheaviour = FindAnyObjectByType<BotBeheaviour>();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerObject in playerObjects)
        {
            Player playerComponent = playerObject.GetComponent<Player>();
            if (playerComponent != null)
            {
                allPlayers.Add(playerComponent);
            }
        }
    }

    void Update()
    {
        switch (state)
        {
            case State.Initialization:
                bool isAddUp;
                isAddUp = false;

                if(roundCount != 20)
                {
                    if (!isAddUp)
                    {
                        player1HaveCheckCard = false;
                        player1HaveChooseCard = false;
                        player1HaveGuessCard = false;

                        player2HaveCheckCard = false;
                        player2HaveChooseCard = false;
                        player2HaveGuessCard = false;

                        player3HaveCheckCard = false;
                        player3HaveChooseCard = false;
                        player3HaveGuessCard = false;

                        player4HaveCheckCard = false;
                        player4HaveChooseCard = false;
                        player4HaveGuessCard = false;

                        Player[] players = FindObjectsOfType<Player>();
                        foreach (Player player in players)
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
                if(roundCount == 20)
                {
                    state = State.GameOver;
                }

                break;

            case State.Player1Turn:
                if (!player1HaveCheckCard)
                    player1CheckCard.Invoke();

                /*if (player1.isBot)
                {
                    Invoke("BotGetInteractableButton", 6f);
                    botBeheaviour.ClickRandomButton();
                }*/

                if (player1HaveCheckCard && player1HaveChooseCard && player1HaveGuessCard)
                    state = State.Player2Turn;
                break;

            case State.Player2Turn:
                if (!player2HaveCheckCard)
                    player2CheckCard.Invoke();

                /*if (player2.isBot)
                {
                    Invoke("BotGetInteractableButton", 6f);
                    botBeheaviour.ClickRandomButton();
                }*/

                if (player2HaveCheckCard && player2HaveChooseCard && player2HaveGuessCard)
                    state = State.Player3Turn;
                break;

            case State.Player3Turn:
                if (!player3HaveCheckCard)
                    player3CheckCard.Invoke();

                /*if (player3.isBot)
                {
                    Invoke("BotGetInteractableButton", 6f);
                    botBeheaviour.ClickRandomButton();
                }*/

                if (player3HaveCheckCard && player3HaveChooseCard && player3HaveGuessCard)
                    state = State.Player4Turn;
                break;

            case State.Player4Turn:
                if (!player4HaveCheckCard)
                    player4CheckCard.Invoke();

                /*if (player4.isBot)
                {
                    Invoke("BotGetInteractableButton", 6f);
                    botBeheaviour.ClickRandomButton();
                }*/

                if (player4HaveCheckCard && player4HaveChooseCard && player4HaveGuessCard)
                    state = State.Initialization;
                break;

            case State.GameOver:
                GameIsOver();
                break;
        }
        Debug.Log(state);
    }

    void CalculatePlayerScores()
    {
        playerScores.Clear();

        foreach (Player player in allPlayers)
        {
            int score = player.CalculateScore();
            playerScores.Add(player, score);
        }
    }

    void OrderPlayerScores()
    {
        List<Player> orderedPlayers = allPlayers.OrderBy(player => -playerScores[player]).ToList();
        allPlayers = orderedPlayers;
    }

    void GameIsOver()
    {
        CalculatePlayerScores();
        OrderPlayerScores();

        // Sort players based on scores in descending order
        allPlayers.Sort((player1, player2) => playerScores[player2].CompareTo(playerScores[player1]));

        // Assign players to winner positions
        for (int i = 0; i < allPlayers.Count; i++)
        {
            Player player = allPlayers[i];
            GameObject winnerGameObject = GetWinnerGameObject(i + 1);
            player.transform.SetParent(winnerGameObject.transform);
        }
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

}
