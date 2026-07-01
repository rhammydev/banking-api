using SimpleBankingAPI.DTOs.Request;
using SimpleBankingAPI.DTOs.Response;

namespace SimpleBankingAPI.Repository.Interface;

public interface IBankingService
{
    Task<ApiResponse<AccountResponse>> CreateAccountAsync(CreateAccountRequest createAccountRequest);
    Task<ApiResponse<AccountResponse>> GetAccountAsync(string accountNumber);
    Task<ApiResponse<AccountResponse>> UpdateAccountAsync(string accountNumber, UpdateAccountRequest updateAccountRequest);
    Task<ApiResponse<bool>> DeleteAccountAsync(string accountNumber);
    Task<ApiResponse<bool>> ReactivateAccountAsync(string accountNumber);
    Task<ApiResponse<IEnumerable<AccountResponse>>> GetAllAccountAsync();
    
    // bank operations
    Task<ApiResponse<TransferSummary>> TransferFundAsync(TransferRequest request);
    Task<ApiResponse<decimal>> DepositFundAsync(DepositRequest request);
    Task<ApiResponse<decimal>> WithdrawFundAsync(WithdrawRequest request);
    Task<ApiResponse<BalanceResponse>> GetBalanceAsync(string accountNumber);
    Task<ApiResponse<IEnumerable<TransactionResponse>>> GetTransactionsHistoryAsync(string accountNumber);
    
}