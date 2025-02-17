using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public Card cardPrefab;

    public List<Card> deck = new List<Card>();
    public List<Material> cardMaterials = new List<Material>();

    private Dictionary<string, int> Suits = new Dictionary<string, int>()
    {
        { "Clubs", 0 },
        { "Diamonds", 1 },
        { "Hearts", 2 },
        { "Spades", 3 }
    };

    public void GenerateDeck()
    {
        for(int i = 1; i < 53; i++)
        {
            deck.Add(Instantiate(cardPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity));
            Card currentCard = deck[i-1];

            if(i < 14)
            {
                currentCard.transform.parent = this.gameObject.transform;
                currentCard.number = i;
                currentCard.suit = Suits["Clubs"];
                currentCard.name = currentCard.ReadCard();
                currentCard.transform.GetChild(0).GetComponent<MeshRenderer>().material = cardMaterials[i-1];
            }
            else if(i < 27)
            {
                currentCard.transform.parent = this.gameObject.transform;
                currentCard.number = i-13;
                currentCard.suit = Suits["Diamonds"];
                currentCard.name = currentCard.ReadCard();
                currentCard.transform.GetChild(0).GetComponent<MeshRenderer>().material = cardMaterials[i-1];
            }
            else if(i < 40)
            {
                currentCard.transform.parent = this.gameObject.transform;
                currentCard.number = i-26;
                currentCard.suit = Suits["Hearts"];
                currentCard.name = currentCard.ReadCard();
                currentCard.transform.GetChild(0).GetComponent<MeshRenderer>().material = cardMaterials[i-1];
            }
            else
            {
                currentCard.transform.parent = this.gameObject.transform;
                currentCard.number = i-39;
                currentCard.suit = Suits["Spades"];
                currentCard.name = currentCard.ReadCard();
                currentCard.transform.GetChild(0).GetComponent<MeshRenderer>().material = cardMaterials[i-1];
            }
        }

        ShuffleDeck();
    }

    public void ShuffleDeck()
    {
        // Modern Fisher-Yates Shuffle altered to move every card at least once
        // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm

        for (int i = 51; i > 0; i--)
        {
            int cardIndex = Random.Range(0, i-1);

            Card randomCard = deck[cardIndex];

            deck[cardIndex] = deck[i];
            deck[i] = randomCard;
        }
    }

    public Card DrawCard()
    {
        int topCardIndex = deck.Count-1;
        Card nextCard = deck[topCardIndex];
        deck.RemoveAt(topCardIndex);

        return nextCard;
    }
}
