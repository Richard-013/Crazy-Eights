using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected List<Card> hand = new List<Card>();
    protected Card lastPlayedCard;
    protected bool isOpponent = true;
    
    public Vector3 handStartPosition;
    public Vector3 handEndPosition;
    public int direction;
    public bool isTurn = false;
    
    public GameManager game;

    public void SetAsPlayer()
    {
        isOpponent = false;
    }

    public void AddCardToHand(Card newCard)
    {
        hand.Add(newCard);
    }

    public void ShowHand()
    {
        float horizontalDisplacementAmount = 20f/hand.Count;
        float horizontalDisplacement = horizontalDisplacementAmount;
        float cardSeparationDisplacement = 0.025f;

        for(int i = 0; i < hand.Count; i++)
        {
            //Debug.Log(hand[i].ReadCard());
            //Debug.Log(position);
            Vector3 nextPosition;
            Vector3 nextRotate;

            switch(direction)
            {
                case 0:
                    nextRotate = new Vector3(90f, 0f, 0f);
                    break;
                case 1:
                    nextRotate = new Vector3(90f, 180f, 0f);
                    break;
                case 2:
                    nextRotate = new Vector3(90f, 90f, 0f);
                    break;
                case 3:
                    nextRotate = new Vector3(90f, -90f, 0f);
                    break;
                default:
                    nextRotate = new Vector3(0f, 0f, 0f);
                    break;
            }

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

            hand[i].transform.Translate(nextPosition);
            hand[i].transform.Rotate(nextRotate);
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
}
