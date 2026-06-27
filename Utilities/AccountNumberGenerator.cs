namespace SimpleBankingAPI.Utilities;

public static class AccountNumberGenerator
{
    private static readonly Random _random = new Random();

    public static string GenerateAcct()
    {
        long Digits = _random.NextInt64(1000000000, 10000000000);
        return Digits.ToString();

    }
}