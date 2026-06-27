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
        try
        {
            var account = await _dbContext.Accounts
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                _logger.LogError($"Account number {accountNumber} not found");
                return ApiResponse<AccountResponse>.FailureResponse("Account number not found");
            }
            _logger.LogInformation($"Successfully retrieved information for {account.AccountNumber}");
        
            var response = new AccountResponse()
            {
                CustomerName = account.FirstName + " " + account.LastName,
                AccountNumber = account.AccountNumber,
                Email = account.Email,
                Balance = account.Balance,
                CreatedAt = account.CreatedAt
            };
            return ApiResponse<AccountResponse>.SuccessResponse(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to retrieve account details");
            return ApiResponse<AccountResponse>.FailureResponse(
                "Failed to retrieve account details");
        }
    }

    public async Task<ApiResponse<AccountResponse>> UpdateAccountAsync(string accountNumber, UpdateAccountRequest updateAccountRequest)
    {
        try
        {
            var account = await _dbContext.Accounts
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                _logger.LogError($"Account number {accountNumber} not found");
                return ApiResponse<AccountResponse>.FailureResponse("Account number not found");
            }
        
            account.FirstName = updateAccountRequest.FirstName;
            account.LastName = updateAccountRequest.LastName;
        
            _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Successfully updated information for {account.AccountNumber}");

            var updatedFields = new Dictionary<string, string>
            {
                { "First Name", updateAccountRequest.FirstName },
                { "Last Name", updateAccountRequest.LastName },
            };
            
            await _emailService.SendAccountUpdateEmailAsync(account.Email, $"{account.FirstName} {account.LastName}", account.AccountNumber, updatedFields);
            
            var response = new AccountResponse()
            {
                CustomerName = account.FirstName + " " + account.LastName,
                AccountNumber = account.AccountNumber,
                Email = account.Email,
                Balance = account.Balance,
                CreatedAt = account.CreatedAt
            };
            
            return ApiResponse<AccountResponse>.SuccessResponse(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to update account details");
            return ApiResponse<AccountResponse>.FailureResponse(
                "Failed to update account details");
        }
    }

    public async Task<ApiResponse<bool>> DeleteAccountAsync(string accountNumber)
    {
        try
        {
            var account = await _dbContext.Accounts
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                _logger.LogError($"Account number {accountNumber} not found");
                return ApiResponse<bool>.FailureResponse("Account number not found");
            }
            
            account.isDeleted = true;
            
            _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();
            
            await _emailService.SendAccountDeletionEmailAsync(account.Email, $"{account.FirstName} {account.LastName}", account.AccountNumber);
            
            _logger.LogInformation($"Successfully deleted information for {account.AccountNumber}");
            return ApiResponse<bool>.SuccessResponse(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete account");
            return ApiResponse<bool>.FailureResponse("Failed to delete account");
        }
    }

    public async Task<ApiResponse<IEnumerable<AccountResponse>>> GetAllAccountAsync()
    {
        try
        {
            var accounts = await _dbContext.Accounts
                .Where(x => x.isDeleted == false)
                .Select(account => new AccountResponse()
                {
                    CustomerName = account.FirstName + " " + account.LastName,
                    AccountNumber = account.AccountNumber,
                    Email = account.Email,
                    Balance = account.Balance,
                    CreatedAt = account.CreatedAt
                }).ToListAsync();
            
            _logger.LogInformation($"Successfully retrieved {accounts.Count} accounts");
            
            return ApiResponse<IEnumerable<AccountResponse>>.SuccessResponse(accounts);
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to retrieve accounts");
            return ApiResponse<IEnumerable<AccountResponse>>.FailureResponse(
                "Failed to retrieve accounts");
        }
    }
}