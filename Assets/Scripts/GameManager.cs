using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MAX_NUMBER_OF_PLAYERS = 8;
    private const int MIN_NUMBER_OF_PLAYERS = 2;

    private CardDeck deck;
    public Player[] players;
    private int numberOfPlayers = 2;

    void Start()
    {
        deck = new CardDeck();
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
    }
}
