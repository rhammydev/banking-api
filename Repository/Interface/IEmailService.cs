namespace SimpleBankingAPI.Repository.Interface;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string toEmail, string customerName, string accountNumber, decimal balance);
}