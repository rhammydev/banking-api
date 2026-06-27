namespace SimpleBankingAPI.DTOs.Request;

public class DepositRequest
{
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string Naration { get; set; }
}