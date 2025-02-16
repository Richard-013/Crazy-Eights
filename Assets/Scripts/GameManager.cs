using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MAX_NUMBER_OF_PLAYERS = 8;
    private const int MIN_NUMBER_OF_PLAYERS = 2;
    private const int HUMAN_PLAYER_INDEX = 0;

    private CardDeck deck;
    public Player[] players;
    private int numberOfPlayers = 2;

    public Player[] players;
    void Start()
    {
        deck = new CardDeck();
        SetNumberOfPlayers(4);
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

    }
}
