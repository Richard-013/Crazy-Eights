public class Opponent : Player
{
    public override Card Turn(Card recentCard)
    {
        lastPlayedCard = recentCard;
        return hand[0];
    }
}