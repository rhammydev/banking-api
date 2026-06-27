using SimpleBankingAPI.DTOs.Request;
using SimpleBankingAPI.DTOs.Response;

namespace SimpleBankingAPI.Repository.Interface;

public interface IBankingService
{
    Task<ApiResponse<AccountResponse>> CreateAccountAsync(CreateAccountRequest createAccountRequest);
    Task<ApiResponse<AccountResponse>> GetAccountAsync(string accountNumber);
    Task<ApiResponse<AccountResponse>> UpdateAccountAsync(string accountNumber, UpdateAccountRequest updateAccountRequest);
    Task<ApiResponse<bool>> DeleteAccountAsync(string accountNumber);
    Task<ApiResponse<IEnumerable<AccountResponse>>> GetAllAccountAsync();
}