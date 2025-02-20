public class Opponent : Player
{
    public override void AddCardToHand(Card newCard)
    {
        hand.Add(newCard);
        newCard.faceHidden = true;
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

    protected override void DrawCard()
    {
        Card newCard = game.DrawCardForPlayer();
        AddCardToHand(newCard);
        newCard.HideCardFace();
        //newCard.MoveCard();
    }
}