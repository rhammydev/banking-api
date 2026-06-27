namespace SimpleBankingAPI.Repository.Interface;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string toEmail, string customerName, string accountNumber, decimal balance);
    Task SendAccountUpdateEmailAsync(string toEmail, string customerName, string accountNumber, Dictionary<string, string> updatedFields);
    
    Task SendAccountDeletionEmailAsync(string toEmail, string customerName, string accountNumber);
}