namespace SimpleBankingAPI.DTOs.Response;

public class TransactionResponse
{
    public string TransactionType { get; set; } 
    
    public string AccountNumber { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Naration { get; set; }
    
    public string Reference { get; set; }
    
    public DateTime CreatedAt { get; set; }
}