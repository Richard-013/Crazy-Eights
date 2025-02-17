using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MAX_NUMBER_OF_PLAYERS = 4;
    private const int MIN_NUMBER_OF_PLAYERS = 2;
    private const int INITIAL_HAND_SIZE = 7;
    private const int HUMAN_PLAYER_INDEX = 0;

    private CardDeck deck;
    public List<Card> playedCards = new List<Card>();
    private Vector3[] playerPositions = new Vector3[MAX_NUMBER_OF_PLAYERS];
    private int numberOfPlayers = 2;
    private int currentPlayer = 0;

    [SerializeField] public CardDeck deckPrefab;
    [SerializeField] public Player playerPrefab;
    [SerializeField] public Player opponentPrefab;

    public Player[] players;
    public Card lastPlayedCard;

    void Start()
    {
        DefinePlayerPositions();
        SetNumberOfPlayers(4);
        SetupDeck();
        ShowHands();
    }

    public void SetNumberOfPlayers(int numPlayers)
    {
        if(numPlayers < MIN_NUMBER_OF_PLAYERS)
        {
            numberOfPlayers = MIN_NUMBER_OF_PLAYERS;
        }
        else if(numPlayers > MAX_NUMBER_OF_PLAYERS)
        {
            numberOfPlayers = MAX_NUMBER_OF_PLAYERS;
        }
        else
        {
            numberOfPlayers = numPlayers;
        }

        SetupPlayers();
    }

    void DefinePlayerPositions()
    {
        // Preset positions for players to spawn hands at
        playerPositions[0] = new Vector3(0f, 5f, 20f);
        playerPositions[1] = new Vector3(0f, 5f, -20f);
        playerPositions[2] = new Vector3(20f, 5f, 0f);
        playerPositions[3] = new Vector3(-20f, 5f, 0f);
    }

    void SetupPlayers()
    {
        players = new Player[numberOfPlayers];

        for(int i = 0; i < numberOfPlayers; i++)
        {
            if(i == HUMAN_PLAYER_INDEX)
            {
                players[i] = Instantiate(playerPrefab);
                players[i].name = "Player";
                players[i].SetAsPlayer();
            }
            else
            {
                players[i] = Instantiate(opponentPrefab);
                players[i].name = "Opponent " + i;
            }

            players[i].transform.parent = this.gameObject.transform;
            players[i].game = this;
            players[i].position = playerPositions[i];
            players[i].direction = i;
        }
    }

    public void SetupDeck()
    {
        deck = Instantiate(deckPrefab);
        deck.transform.parent = this.gameObject.transform;
        deck.name = "Card Deck";
        deck.GenerateDeck();
        GiveInitialHands();
        DrawFirstCard();
    }

    void GiveInitialHands()
    {
        // Distribute the initial hand of cards
        // One card for each player then loop

        for(int i = 0; i < INITIAL_HAND_SIZE; i++)
        {
            for(int j = 0; j < numberOfPlayers; j++)
            {
                players[j].AddCardToHand(deck.DrawCard());
            }
        }
    }
    
    void DrawFirstCard()
    {
        lastPlayedCard = deck.DrawCard();
        playedCards.Add(lastPlayedCard);
        Debug.Log(lastPlayedCard.ReadCard());
    }

    void ShowHands()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            Debug.Log("Player " + i + " Hand:");
            players[i].ShowHand();
        }
    }

    void PlayedCardAction(Card playedCard)
    {
        int playedCardNumber = playedCard.number;

        if(playedCardNumber == 1)
        {
            // Ace
            lastPlayedCard = playedCard;
        }
        else if(playedCardNumber == 2)
        {
            // Two
            lastPlayedCard = playedCard;
        }
        else if(playedCardNumber == 8)
        {
            // Eight
            lastPlayedCard = playedCard;
        }
        else if(playedCardNumber == 11)
        {
            // Jack
            lastPlayedCard = playedCard;
        }
        else
        {
            // Regular Cards
            lastPlayedCard = playedCard;
        }
    }
}
