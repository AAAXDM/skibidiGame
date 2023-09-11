
public class DoubleCoins : SpecialPower
{
    int coins = 2;
    protected override void Trigger() => stats.AddCoins(coins);
}
