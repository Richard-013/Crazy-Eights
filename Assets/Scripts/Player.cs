using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected const float HAND_WIDTH = 20f;
    protected const float CARD_SEPARATION_DISPLACEMENT = 0.025f;

    public List<Card> hand = new List<Card>();
    protected Card lastPlayedCard;
    protected bool isOpponent = true;
    
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

            if(i == hand.Count-1)
            {
                hand[i].MoveCard(newPosition, handRotation);
            }
            else
            {
                hand[i].MoveCard(newPosition);
            }

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

    protected virtual void DrawCard()
    {
        Card newCard = game.DrawCardForPlayer();
        AddCardToHand(newCard);
        //newCard.MoveCard();
    }
}
