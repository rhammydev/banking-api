namespace SimpleBankingAPI.Repository.Interface;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string toEmail, string customerName, string accountNumber, decimal balance);
    Task SendAccountUpdateEmailAsync(string toEmail, string customerName, string accountNumber, Dictionary<string, string> updatedFields);
    
    Task SendAccountDeletionEmailAsync(string toEmail, string customerName, string accountNumber);
    
    Task SendAccountDepositEmailAsync(string toEmail, string customerName, string accountNumber, decimal amount, decimal balance);
    
    Task SendTransferDebitEmailAsync(string toEmail, string customerName, string senderAccountNumber, string recipientAccountNumber, decimal amount, decimal senderNewBalance);
    Task SendTransferCreditEmailAsync(string toEmail, string customerName, string senderAccountNumber, string recipientAccountNumber, decimal amount, decimal recipientNewBalance);
    
    Task SendWithdrawalEmailAsync(string toEmail, string customerName, string accountNumber, decimal amount, decimal balance);
}