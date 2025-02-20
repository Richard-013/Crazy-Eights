public class Opponent : Player
{
    public override void AddCardToHand(Card newCard)
    {
        hand.Add(newCard);
        newCard.HideCardFace();
    }

    public override Card Turn(Card recentCard)
    {
        lastPlayedCard = recentCard;

        Card playedCard = hand[0];
        playedCard.faceHidden = false;
        playedCard.ShowCardFace();

        return playedCard;
    }

    public override void DrawCard(Card newCard)
    {
        AddCardToHand(newCard);
        newCard.HideCardFace();
        ShowHandNoDelay();
    }
}