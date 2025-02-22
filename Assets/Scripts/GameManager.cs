using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MAX_NUMBER_OF_PLAYERS = 4;
    private const int MIN_NUMBER_OF_PLAYERS = 2;
    private const int INITIAL_HAND_SIZE = 7;
    private const int HUMAN_PLAYER_INDEX = 1;
    private const float CARD_STACK_DISPLACEMENT = 0.015f;

    private CardDeck deck;
    public List<Card> playedCards = new List<Card>();
    private Vector3[] handStartPositions = new Vector3[MAX_NUMBER_OF_PLAYERS];
    private Vector3[] handEndPositions = new Vector3[MAX_NUMBER_OF_PLAYERS];
    private Vector3[] handRotations = new Vector3[MAX_NUMBER_OF_PLAYERS];
    private int numberOfPlayers = 2;
    private int currentPlayer = HUMAN_PLAYER_INDEX;
    private bool normalPlayDirection = true;

    [SerializeField] public CardDeck deckPrefab;
    [SerializeField] public Player playerPrefab;
    [SerializeField] public Player opponentPrefab;

    public Player[] players;
    public Card lastPlayedCard;

    void Start()
    {
        DefineHandPositions();
        SetNumberOfPlayers(4);
        SetupDeck();
        ShowHands();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            players[HUMAN_PLAYER_INDEX].DrawCard(DrawCardForPlayer());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            players[0].DrawCard(DrawCardForPlayer());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            players[2].DrawCard(DrawCardForPlayer());
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            players[3].DrawCard(DrawCardForPlayer());
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayCard(players[HUMAN_PLAYER_INDEX].hand[0]);
        }
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

    void DefineHandPositions()
    {
        // Preset positions for players to spawn hands at
        handStartPositions[0] = new Vector3(-10f, 5f, 20f);
        handStartPositions[1] = new Vector3(-10f, 5f, -20f);
        handStartPositions[2] = new Vector3(20f, 5f, -10f);
        handStartPositions[3] = new Vector3(-20f, 5f, -10f);

        handEndPositions[0] = new Vector3(10f, 5f, 20f);
        handEndPositions[1] = new Vector3(10f, 5f, -20f);
        handEndPositions[2] = new Vector3(20f, 5f, 10f);
        handEndPositions[3] = new Vector3(-20f, 5f, 10f);

        handRotations[0] = new Vector3(90f, 0f, 0f);
        handRotations[1] = new Vector3(90f, 180f, 0f);
        handRotations[2] = new Vector3(90f, 90f, 0f);
        handRotations[3] = new Vector3(90f, -90f, 0f);
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
            players[i].handStartPosition = handStartPositions[i];
            players[i].handEndPosition = handEndPositions[i];
            players[i].handRotation = handRotations[i];
            players[i].direction = i;
            players[i].playerNumber = i+1;
        }
    }

    public void SetupDeck()
    {
        deck = Instantiate(deckPrefab);
        deck.transform.parent = this.gameObject.transform;
        deck.name = "Card Deck";
        deck.GenerateDeck();
        MoveDeck();
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
        playedCards[0].MoveCard(Vector3.zero, Vector3.zero);
    }

    void MoveDeck()
    {
        for(int i = 0; i < deck.deck.Count; i++)
        {
            deck.deck[i].transform.Translate(new Vector3(4f, CARD_STACK_DISPLACEMENT*i, 0f));
            deck.deck[i].transform.Rotate(new Vector3(0f, 0f, 180f));
        }
    }

    void ShowHands()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            players[i].ShowHandWithDelay();
        }
    }

    void PlayCard(Card playedCard)
    {
        playedCards.Add(playedCard);
        playedCard.rotatedToHand = false;

        Vector3 playedCardPosition = new Vector3(0f, CARD_STACK_DISPLACEMENT*(playedCards.Count-1), 0f);

        playedCard.MoveCard(playedCardPosition, Vector3.zero);
        PlayedCardAction(playedCard);
    }

    void PlayedCardAction(Card playedCard)
    {
        int playedCardNumber = playedCard.number;

        switch(playedCardNumber)
        {
            case 1:
                // Ace - Switch Direction of Play
                if(normalPlayDirection)
                {
                    normalPlayDirection = false;
                }
                else
                {
                    normalPlayDirection = true;
                }
                
                lastPlayedCard = playedCard;
                break;
            case 2:
                // Two - Next Player Picks Up 2 Cards and Misses Turn
                PickupCards(2, players[GetNextPlayerIndex()]);
                SetNextPlayerIndex();

                lastPlayedCard = playedCard;
                break;
            case 8:
                // Eight - Next Player Misses Turn
                SetNextPlayerIndex();

                lastPlayedCard = playedCard;
                break;
            case 11:
                // Jack - Change To Chosen Suit
                lastPlayedCard = playedCard;
                break;
            default:
                // Regular Cards
                lastPlayedCard = playedCard;
                break;
        }

        SetNextPlayerIndex();
    }

    int GetNextPlayerIndex()
    {
        if(normalPlayDirection)
        {
            if(currentPlayer < players.Length-1)
            {
                return currentPlayer+1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if(currentPlayer > 0)
            {
                return currentPlayer-1;
            }
            else
            {
                return players.Length-1;
            }
        }
    }

    void SetNextPlayerIndex()
    {
        if(normalPlayDirection)
        {
            if(currentPlayer < players.Length-1)
            {
                currentPlayer++;
            }
            else
            {
                currentPlayer = 0;
            }
        }
        else
        {
            if(currentPlayer > 0)
            {
                currentPlayer--;
            }
            else
            {
                currentPlayer = players.Length-1;
            }
        }
    }

    void PickupCards(int numberOfCards, Player targetPlayer)
    {
        for(int i = 0; i < numberOfCards; i++)
        {
            targetPlayer.AddCardToHand(DrawCardForPlayer());
        }
    }

    public Card DrawCardForPlayer()
    {
        return deck.DrawCard();
    }
}
