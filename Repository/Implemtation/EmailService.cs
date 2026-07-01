
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SimpleBankingAPI.Model;
using SimpleBankingAPI.Repository.Interface;
using SimpleBankingAPI.Utilities;

namespace SimpleBankingAPI.Repository.Implemtation;

public class EmailService : IEmailService
{
    private readonly SmtpMail _smtpMail;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<SmtpMail> smtpMail, ILogger<EmailService> logger)
    {
        _logger = logger;
        _smtpMail = smtpMail.Value;
    }

    private async Task SendMimeMessageAsync(MimeMessage message)
    {
        
        _logger.LogInformation($"Sending email to {message.To}...");

        using var client = new SmtpClient();

        try
        {
            _logger.LogInformation($"Connecting to Smtp server {_smtpMail.Server}:{_smtpMail.Port}...");

            await client.ConnectAsync(_smtpMail.Server, _smtpMail.Port, false);
            _logger.LogInformation("Connected to Smtp server...");

            await client.AuthenticateAsync(_smtpMail.Username, _smtpMail.Password);
            _logger.LogInformation("Authenticated to Smtp server...");

            await client.SendAsync(message);
            _logger.LogInformation($"Email sent to {message.To} successfully.");
            
            await client.DisconnectAsync(true);
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occured during sending email to {message.To}.");
        }
    }

    private MimeMessage CreateBaseMessage(string toEmail, string subject)
    {
        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress(_smtpMail.SenderName, _smtpMail.SenderEmail));
        message.To.Add(new MailboxAddress(toEmail, toEmail));
        message.Subject = subject;
        
        return message;
    }
    
    public async Task SendWelcomeEmailAsync(string toEmail, string customerName, string accountNumber, decimal balance)
    {
        var subject = $"Welcome to Prestige Banking, {customerName}!";
        var htmlbody = MailUtils.GetWelcomeEmailHtml(customerName, accountNumber, balance);
        
        var message = CreateBaseMessage(toEmail, subject);
        message.Body = new TextPart(TextFormat.Html) { Text = htmlbody };
        
        await SendMimeMessageAsync(message);
    }

    public async Task SendAccountUpdateEmailAsync(string toEmail, string customerName, string accountNumber, Dictionary<string, string> updatedFields)
    {
        var subject = $"Prestige Banking: Account Update Confirmation - {accountNumber}";
        var htmlbody = MailUtils.GetAccountUpdatedEmailHtml(customerName, accountNumber, updatedFields);
        
        var message = CreateBaseMessage(toEmail, subject);
        message.Body = new TextPart(TextFormat.Html) { Text = htmlbody };
        
        await SendMimeMessageAsync(message);
    }

    public async Task SendAccountDeletionEmailAsync(string toEmail, string customerName, string accountNumber)
    {
        var subject = $"Prestige Banking: Account Closure Confirmation - {accountNumber}";
        var htmlbody = MailUtils.GetAccountDeletedEmailHtml(customerName, accountNumber);
        var message = CreateBaseMessage(toEmail, subject);
        message.Body = new TextPart(TextFormat.Html) { Text = htmlbody };
        
        await SendMimeMessageAsync(message);
    }
    
    public async Task SendAccountReactivationEmailAsync(string toEmail, string customerName, string accountNumber)
    {
        var subject = $"Prestige Banking: Account Activation Confirmation - {accountNumber}";
        var htmlbody = MailUtils.GetAccountReactivationEmailHtml(customerName, accountNumber);
        var message = CreateBaseMessage(toEmail, subject);
        message.Body = new TextPart(TextFormat.Html) { Text = htmlbody };
        
        await SendMimeMessageAsync(message);
    }

    public async Task SendAccountDepositEmailAsync(string toEmail, string customerName, string accountNumber, decimal amount,
        decimal balance)
    {
        var subject = $"Prestige Banking: Deposit of ₦{amount:N0} Confirmed - {accountNumber}";
        var htmlbody = MailUtils.GetDepositEmailHtml(customerName, accountNumber, amount, balance);
        var message = CreateBaseMessage(toEmail, subject);
        message.Body = new TextPart(TextFormat.Html) { Text = htmlbody };
        
        await SendMimeMessageAsync(message);
    }

    public async Task SendTransferDebitEmailAsync(string toEmail, string customerName, string senderAccountNumber,
        string recipientAccountNumber, decimal amount, decimal senderNewBalance)
    {
        var subject = $"Prestige Banking: Debit Alert - ₦{amount:N0} Sent from {senderAccountNumber}";
        var htmlbody = MailUtils.GetTransferDebitEmailHtml(customerName, senderAccountNumber, recipientAccountNumber, amount, senderNewBalance);
        var message = CreateBaseMessage(toEmail, subject);
        message.Body = new TextPart(TextFormat.Html) { Text = htmlbody };
        
        await SendMimeMessageAsync(message);
    }
    
    public async Task SendTransferCreditEmailAsync(string toEmail, string customerName, string senderAccountNumber,
        string recipientAccountNumber, decimal amount, decimal recipentNewBalance)
    {
        var subject = $"Prestige Banking: Credit Alert - ₦{amount:N0} Received in {recipientAccountNumber}";
        var htmlbody = MailUtils.GetTransferCreditEmailHtml(customerName, senderAccountNumber, recipientAccountNumber, amount, recipentNewBalance);
        var message = CreateBaseMessage(toEmail, subject);
        message.Body = new TextPart(TextFormat.Html) { Text = htmlbody };
        
        await SendMimeMessageAsync(message);
    }

    public async Task SendWithdrawalEmailAsync(string toEmail, string customerName, string accountNumber, decimal amount,
        decimal balance)
    {
        var subject = $"Prestige Banking: Withdrawal of ₦{amount:N0} Confirmed - {accountNumber}";
        var htmlbody = MailUtils.GetWithdrawalEmailHtml(customerName, accountNumber, amount, balance);
        var message = CreateBaseMessage(toEmail, subject);
        message.Body = new TextPart(TextFormat.Html) { Text = htmlbody };
        
        await SendMimeMessageAsync(message);
    }
}