using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MAX_NUMBER_OF_PLAYERS = 8;
    private const int MIN_NUMBER_OF_PLAYERS = 2;
    private const int INITIAL_HAND_SIZE = 7;
    private const int HUMAN_PLAYER_INDEX = 0;

    private CardDeck deck;
    private int numberOfPlayers = 2;
    private int currentPlayer = 0;

    public Player[] players;
    public Card lastPlayedCard;

    void Start()
    {
        SetNumberOfPlayers(4);
        SetupDeck();
        ShowHands();
        CurrentPlayerTurn();
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

    void SetupPlayers()
    {
        players = new Player[numberOfPlayers];

        for(int i = 0; i < numberOfPlayers; i++)
        {
            players[i] = gameObject.AddComponent<Player>();
            players[i].game = this;
        }

        players[HUMAN_PLAYER_INDEX].SetAsPlayer(); // Set the first player in the list as the human player
    }

    public void SetupDeck()
    {
        deck = new CardDeck();
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
    }

    void ShowHands()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            Debug.Log("Player " + i + " Hand:");
            players[i].ShowHand();
        }
    }
    
    void CurrentPlayerTurn()
    {
        // If current player is not human, use opponent logic
        if(currentPlayer != HUMAN_PLAYER_INDEX)
        {
            players[currentPlayer].Turn(true);
        }
        else
        {
            players[currentPlayer].Turn(false);
        }
    }
}
