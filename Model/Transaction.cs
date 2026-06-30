namespace SimpleBankingAPI.Model;

public class Transaction
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; }
    public string TransactionType { get; set; } // Deposit, Withdraw, TransferDebit, TransferCredit
    
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Naration { get; set; }
    public string Reference { get; set; }
    public DateTime Date { get; set; }
}