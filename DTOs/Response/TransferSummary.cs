namespace SimpleBankingAPI.DTOs.Response;

public class TransferSummary
{
    public string TransactionReference { get; set; }
    
    public string SenderAccountNumber { get; set; }
    
    public string ReceiverAccountNumber { get; set; }
    
    public decimal Amount { get; set; }
    
    public decimal SenderNewBalance { get; set; }
    
    public DateTime CreatedAt { get; set; }
}