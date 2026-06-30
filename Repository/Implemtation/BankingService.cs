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

    public async Task<ApiResponse<TransferSummary>> TransferFundAsync(TransferRequest request)
    {
        try
        {
            if (request.SenderAccountNumber == request.ReceiverAccountNumber)
            {
                _logger.LogError($"Sender account number can not be the same as receiver account number {request.SenderAccountNumber}");
                return ApiResponse<TransferSummary>.FailureResponse("Sender account number can not be the same as receiver account number");
            }
            
            var senderAccount = await  _dbContext.Accounts
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.AccountNumber == request.SenderAccountNumber);
            
            var receiverAccount = await  _dbContext.Accounts
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.AccountNumber == request.ReceiverAccountNumber);

            if (senderAccount == null || receiverAccount == null)
            {
                _logger.LogError($"Sender account number or receiver account not found");
                return ApiResponse<TransferSummary>.FailureResponse("Sender account number or receiver account not found");
            }

            if (senderAccount.Balance < request.Amount)
            {
                _logger.LogError($"Insufficient funds for sender account {senderAccount.AccountNumber}");
                return ApiResponse<TransferSummary>.FailureResponse("Insufficient funds");
            }
            
            senderAccount.Balance -= request.Amount;
            receiverAccount.Balance += request.Amount;
            
            var reference = TransactionRefGenerator.GenerateRef("TRF");
            var debitTransaction = new Transaction()
            {
                AccountNumber = senderAccount.AccountNumber,
                Amount = request.Amount,
                Reference = $"D-{reference}",
                Description = $"Debit of {request.Amount} from {senderAccount.AccountNumber}",
                TransactionType = "Debit",
                Naration = request.Naration,
                Date = DateTime.Now
            };
            
            var creditTransaction = new Transaction()
            {
                AccountNumber = receiverAccount.AccountNumber,
                Amount = request.Amount,
                Reference = $"C-{reference}",
                Description = $"Credit of {request.Amount} to {receiverAccount.AccountNumber}",
                TransactionType = "Credit",
                Naration = request.Naration,
                Date = DateTime.Now
            };
            
            _dbContext.Transactions.AddRange(debitTransaction, creditTransaction);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Successfully transfer funds from {senderAccount.AccountNumber}  to {receiverAccount.AccountNumber}");

            await _emailService.SendTransferCreditEmailAsync(
                receiverAccount.Email,
                $"{receiverAccount.FirstName} {receiverAccount.LastName}",
                senderAccount.AccountNumber,
                receiverAccount.AccountNumber,
                request.Amount,
                receiverAccount.Balance);

            await _emailService.SendTransferDebitEmailAsync(
                senderAccount.Email,
                $"{senderAccount.FirstName} {senderAccount.LastName}",
                senderAccount.AccountNumber,
                receiverAccount.AccountNumber,
                request.Amount,
                senderAccount.Balance);
            
            var response = new TransferSummary()
            {
                TransactionReference = debitTransaction.Reference,
                SenderAccountNumber = senderAccount.AccountNumber,
                ReceiverAccountNumber = receiverAccount.AccountNumber,
                Amount = request.Amount,
                SenderNewBalance = senderAccount.Balance,
                CreatedAt = DateTime.Now
            };
            
            return ApiResponse<TransferSummary>.SuccessResponse(response, "Fund transfer processed successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ApiResponse<decimal>> DepositFundAsync(DepositRequest request)
    {
        try
        {
            var account = await  _dbContext.Accounts
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.AccountNumber == request.AccountNumber);
            
            if (account == null)
            {
                _logger.LogError($"Account with account number {request.AccountNumber} not found");
                return ApiResponse<decimal>.FailureResponse("Account not found");
            }
            
            account.Balance += request.Amount;
            var reference = TransactionRefGenerator.GenerateRef("DEP");

            var transaction = new Transaction()
            {
                AccountNumber = account.AccountNumber,
                Reference = reference,
                Amount = request.Amount,
                TransactionType = "Deposit",
                Naration = request.Naration,
                Date = DateTime.Now,
                Description = $"Deposit of  {request.Amount} to account {account.AccountNumber}",
            };
            
            
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
            
            await _emailService.SendAccountDepositEmailAsync(
                account.Email,
                $"{account.FirstName} {account.LastName}",
                account.AccountNumber,
                request.Amount,
                account.Balance);
            
            _logger.LogInformation($"Successfully deposited {request.Amount} to account {account.AccountNumber}");
            
            
            return ApiResponse<decimal>.SuccessResponse(request.Amount);
                
        }
        catch (Exception e)
        {
           _logger.LogError(e, "Failed to deposit account");
           return ApiResponse<decimal>.FailureResponse("Failed to deposit account");
        }
    }

    public async Task<ApiResponse<decimal>> WithdrawFundAsync(WithdrawRequest request)
    {
        try
        {
            var account = await  _dbContext.Accounts
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync(x => x.AccountNumber == request.AccountNumber);
            
            if (account == null)
            {
                _logger.LogError($"Withdrawal failed: Account with account number {request.AccountNumber} not found");
                return ApiResponse<decimal>.FailureResponse($"Account number {request.AccountNumber} not found");
            }

            if (account.Balance < request.Amount)
            {
                _logger.LogError($"Withdrawal failed: Insufficient balance for account {request.AccountNumber}");
                return ApiResponse<decimal>.FailureResponse($"Insufficient balance for account {request.AccountNumber}");
            }
            
            account.Balance -= request.Amount;
            
            var reference = TransactionRefGenerator.GenerateRef("WDR");
            var transaction = new Transaction()
            {
                AccountNumber = account.AccountNumber,
                Reference = reference,
                Amount = request.Amount,
                TransactionType = "Withdrawal",
                Description = $"Withdrawal of {request.Amount} to account {account.AccountNumber}",
                Naration = "",
                Date = DateTime.Now,
            };
            
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Successfully withdrawn {request.Amount} to account {account.AccountNumber}");
            
            await _emailService.SendWithdrawalEmailAsync(account.Email, $"{account.FirstName}  {account.LastName}", account.AccountNumber, transaction.Amount, account.Balance);
            
            return ApiResponse<decimal>.SuccessResponse(request.Amount);

        }
        catch (Exception e)
        {
           _logger.LogError(e, "Failed to withdraw account");
           return ApiResponse<decimal>.FailureResponse($"Failed to withdraw {request.Amount} from account {request.AccountNumber}");
        }
    }

    public async Task<ApiResponse<BalanceResponse>> GetBalanceAsync(string accountNumber)
    {
        var account = await _dbContext.Accounts
            .Where(x => x.isDeleted == false)
            .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
        
        if (account == null)
        {
            _logger.LogError($"Account with account number {accountNumber} not found");
            return ApiResponse<BalanceResponse>.FailureResponse("Account not found");
        }

        var response = new BalanceResponse()
        {
            AccountNumber = account.AccountNumber,
            CustomerName = $"{account.FirstName} {account.LastName}",
            Balance = account.Balance,
        };
        
        _logger.LogInformation($"Successfully retrieved balance for account {account.AccountNumber}");
        return ApiResponse<BalanceResponse>.SuccessResponse(response);
    }

    public async Task<ApiResponse<IEnumerable<TransactionResponse>>> GetTransactionsHistoryAsync(string accountNumber)
    {
        throw new NotImplementedException();
    }
}