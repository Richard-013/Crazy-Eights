using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int MAX_NUMBER_OF_PLAYERS = 8;
    private const int MIN_NUMBER_OF_PLAYERS = 2;

    public Player[] players;
    private int numberOfPlayers = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
