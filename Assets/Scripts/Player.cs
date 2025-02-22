using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected const float HAND_WIDTH = 20f;
    protected const float CARD_SEPARATION_DISPLACEMENT = 0.025f;

    public List<Card> hand = new List<Card>();

    protected List<Card> spades = new List<Card>();
    protected List<Card> diamonds = new List<Card>();
    protected List<Card> clubs = new List<Card>();
    protected List<Card> hearts = new List<Card>();

    protected Card lastPlayedCard;
    protected bool isOpponent = true;
    protected bool sortedByValue = true;
    
    public Vector3 handStartPosition;
    public Vector3 handEndPosition;

    public Vector3 handRotation;

    public int direction;
    public int playerNumber;
    public bool isTurn = false;
    
    public GameManager game;

    public void SetAsPlayer()
    {
        isOpponent = false;
    }

    public virtual void AddCardToHand(Card newCard)
    {
        hand.Add(newCard);
        newCard.faceHidden = false;

        switch(newCard.suit)
        {
            case 0:
                spades.Add(newCard);
                break;
            case 1:
                diamonds.Add(newCard);
                break;
            case 2:
                clubs.Add(newCard);
                break;
            case 3:
                hearts.Add(newCard);
                break;
            default:
                break;
        }

        if(sortedByValue)
        {
            SortByValue();
        }
        else
        {
            SortBySuit();
        }
        
    }

    public void ShowHandWithDelay()
    {
        float horizontalDisplacementAmount = HAND_WIDTH/hand.Count;
        float horizontalDisplacement = horizontalDisplacementAmount;

        Vector3 newPosition;

        for(int i = 0; i < hand.Count; i++)
        {
            newPosition = CalculateNewHandCardPosition(i, horizontalDisplacement);

            float delay = 0.5f * i;

            hand[i].MoveCard(newPosition, handRotation, delay);

            if(i != 0)
            {
                horizontalDisplacement += horizontalDisplacementAmount;
            }
        }
    }

    public void ShowHandNoDelay()
    {
        float horizontalDisplacementAmount = HAND_WIDTH/hand.Count;
        float horizontalDisplacement = horizontalDisplacementAmount;

        Vector3 newPosition;

        for(int i = 0; i < hand.Count; i++)
        {
            newPosition = CalculateNewHandCardPosition(i, horizontalDisplacement);

            hand[i].MoveCard(newPosition, handRotation);

            if(i != 0)
            {
                horizontalDisplacement += horizontalDisplacementAmount;
            }
        }
    }

    protected Vector3 CalculateNewHandCardPosition(int positionInHand, float cardDisplacement)
    {
        Vector3 nextPosition;

        if(positionInHand == 0)
        {
            return handStartPosition;
        }
        else
        {
            if(direction == 2 || direction == 3)
            {
                if(direction == 2)
                {
                    nextPosition = handStartPosition + new Vector3(CARD_SEPARATION_DISPLACEMENT*positionInHand, 0f, cardDisplacement);
                }
                else
                {
                    nextPosition = handStartPosition + new Vector3(-CARD_SEPARATION_DISPLACEMENT*positionInHand, 0f, cardDisplacement);
                }
                
            }
            else
            {
                if(direction == 0)
                {
                    nextPosition = handStartPosition + new Vector3(cardDisplacement, 0f, CARD_SEPARATION_DISPLACEMENT*positionInHand);
                }
                else
                {
                    nextPosition = handStartPosition + new Vector3(cardDisplacement, 0f, -CARD_SEPARATION_DISPLACEMENT*positionInHand);
                }
            }
        }

        return nextPosition;
    }

    public virtual Card Turn(Card recentCard)
    {
        lastPlayedCard = recentCard;
        return hand[0];
    }

    protected bool IsValidMove(Card chosenCard)
    {
        if(chosenCard.suit == lastPlayedCard.suit)
        {
            return true;
        }
        else if(chosenCard.number == lastPlayedCard.number)
        {
            return true;
        }

        return false;
    }

    protected int PlayCard(Card chosenCard)
    {
        if(IsValidMove(chosenCard))
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public virtual void DrawCard(Card newCard)
    {
        AddCardToHand(newCard);
        ShowHandNoDelay();
    }

    protected void SortByValue()
    {
        sortedByValue = true;
        Sort(hand);
    }

    protected void SortBySuit()
    {
        sortedByValue = false;
        hand = new List<Card>();

        for(int i = 0; i < 4; i++)
        {
            SortSuit(i);
        }

        for(int i = 0; i < 4; i++)
        {
            AddSuitToHand(i);
        }
    }

    protected void SortSuit(int suit)
    {
        switch(suit)
        {
            case 0:
                Sort(spades);
                break;
            case 1:
                Sort(diamonds);
                break;
            case 2:
                Sort(clubs);
                break;
            case 3:
                Sort(hearts);
                break;
            default:
                break;
        }
    }

    protected void AddSuitToHand(int suit)
    {
        List<Card> suitList;

        switch(suit)
        {
            case 0:
                suitList = spades;
                break;
            case 1:
                suitList = diamonds;
                break;
            case 2:
                suitList = clubs;
                break;
            case 3:
                suitList = hearts;
                break;
            default:
                suitList = clubs;
                break;
        }

        for(int i = 0; i < suitList.Count; i++)
        {
            hand.Add(suitList[i]);
        }
    }

    protected void Sort(List<Card> cardList)
    {
        for(int i = 1; i < cardList.Count; i++)
        {
            Card key = cardList[i];
            int comparisonIndex = i - 1;

            while(comparisonIndex >= 0 && cardList[comparisonIndex].number > key.number)
            {
                cardList[comparisonIndex+1] = cardList[comparisonIndex];
                comparisonIndex -= 1;
            }

            cardList[comparisonIndex+1] = key;
        }

        cardList.Reverse();

        // Keep Suits in same order when sorting by value
        if(sortedByValue)
        {
            for(int i = 0; i < cardList.Count-1; i++)
            {
                if(cardList[i].number == cardList[i+1].number)
                {
                    if(cardList[i].suit > cardList[i+1].suit)
                    {
                        Card tempCard = cardList[i+1];
                        cardList[i+1] = cardList[i];
                        cardList[i] = tempCard;
                    }
                }
            }
        }
    }
}
