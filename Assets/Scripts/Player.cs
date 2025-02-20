using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    public void ShowHand()
    {
        float horizontalDisplacementAmount = 20f/hand.Count;
        float horizontalDisplacement = horizontalDisplacementAmount;
        float cardSeparationDisplacement = 0.025f;

        for(int i = 0; i < hand.Count; i++)
        {
            Vector3 nextPosition;

            if(i == 0)
            {
                nextPosition = handStartPosition;
            }
            else
            {
                if(direction == 2 || direction == 3)
                {
                    if(direction == 2)
                    {
                        nextPosition = handStartPosition + new Vector3(cardSeparationDisplacement*i, 0f, horizontalDisplacement);
                    }
                    else
                    {
                        nextPosition = handStartPosition + new Vector3(-cardSeparationDisplacement*i, 0f, horizontalDisplacement);
                    }
                    
                }
                else
                {
                    if(direction == 0)
                    {
                        nextPosition = handStartPosition + new Vector3(horizontalDisplacement, 0f, cardSeparationDisplacement*i);
                    }
                    else
                    {
                        nextPosition = handStartPosition + new Vector3(horizontalDisplacement, 0f, -cardSeparationDisplacement*i);
                    }
                }

                horizontalDisplacement += horizontalDisplacementAmount;
            }

            float delay = 0.5f * i;

            hand[i].MoveCard(nextPosition, handRotation, delay);
        }
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
