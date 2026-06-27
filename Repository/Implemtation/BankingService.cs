using Microsoft.EntityFrameworkCore;
using SimpleBankingAPI.Data;
using SimpleBankingAPI.DTOs.Request;
using SimpleBankingAPI.DTOs.Response;
using SimpleBankingAPI.Model;
using SimpleBankingAPI.Repository.Interface;
using SimpleBankingAPI.Utilities;

namespace SimpleBankingAPI.Repository.Implemtation;

public class BankingService : IBankingService
{
    private readonly BankingDbContext _dbContext;
    private readonly ILogger<BankingService> _logger;
    private readonly IEmailService _emailService;

    public BankingService(BankingDbContext dbContext, ILogger<BankingService> logger, IEmailService emailService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _emailService = emailService;
    }
    
    public async Task<ApiResponse<AccountResponse>> CreateAccountAsync(CreateAccountRequest createAccountRequest)
    {
        try
        {
            var checkEmail = await _dbContext.Accounts.AnyAsync(x => x.Email == createAccountRequest.Email);
            if (checkEmail)
            {
                _logger.LogError($"Email already exist for {createAccountRequest.Email}");
                return ApiResponse<AccountResponse>.FailureResponse("Email already exists");
            }

            var newAccount = new Account()
            {
                FirstName = createAccountRequest.FirstName,
                LastName = createAccountRequest.LastName,
                AccountNumber = AccountNumberGenerator.GenerateAcct(),
                Email = createAccountRequest.Email,
                Balance = 0.00m,
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.Accounts.Add(newAccount);
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Successfully created account for a  new account number {newAccount.AccountNumber} for customer {newAccount.FirstName} {newAccount.LastName}");

            await _emailService.SendWelcomeEmailAsync(newAccount.Email, $"{newAccount.FirstName} {newAccount.LastName}",
                newAccount.AccountNumber, newAccount.Balance);

            var response = new AccountResponse()
            {
                CustomerName =  newAccount.FirstName + " " + newAccount.LastName,
                AccountNumber = newAccount.AccountNumber,
                Email = newAccount.Email,
                Balance = newAccount.Balance,
                CreatedAt = newAccount.CreatedAt
            };
            return ApiResponse<AccountResponse>.SuccessResponse(response);
        }
        catch (Exception e)
        {
           _logger.LogError(e, $"An error occured during creating account for {createAccountRequest.FirstName} {createAccountRequest.LastName}");
           return ApiResponse<AccountResponse>.FailureResponse("An error occured during account creation");
        }
    }

    public async Task<ApiResponse<AccountResponse>> GetAccountAsync(string accountNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<AccountResponse>> UpdateAccountAsync(string accountNumber, UpdateAccountRequest updateAccountRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<bool>> DeleteAccountAsync(string accountNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<IEnumerable<AccountResponse>>> GetAllAccountAsync()
    {
        throw new NotImplementedException();
    }
}