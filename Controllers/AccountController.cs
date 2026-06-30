using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleBankingAPI.DTOs.Request;
using SimpleBankingAPI.DTOs.Response;
using SimpleBankingAPI.Repository.Interface;

namespace SimpleBankingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IBankingService _bankingService;

    public AccountController(IBankingService bankingService)
    {
        _bankingService = bankingService;
    }

    [HttpPost("open-account")]
    public async Task<ActionResult<ApiResponse<AccountResponse>>> OpenAccountAsync(
        CreateAccountRequest createAccountRequest)
    {
        var result = await _bankingService.CreateAccountAsync(createAccountRequest);

        if (!result.IsSuccess) BadRequest(result);
        return Ok(result);
    }

    [HttpGet("{accountNumber}")]
    public async Task<ActionResult<ApiResponse<AccountResponse>>> GetAccountAsync(string accountNumber)
    {
        var result = await _bankingService.GetAccountAsync(accountNumber);
        if (!result.IsSuccess) BadRequest(result);
        return Ok(result);
        
    }

    [HttpPut("{accountNumber}")]
    public async Task<ActionResult<ApiResponse<AccountResponse>>> UpdateAccountAsync(string accountNumber,
        UpdateAccountRequest updateAccountRequest)
    {
        var result = await _bankingService.UpdateAccountAsync(accountNumber, updateAccountRequest);
        if (!result.IsSuccess) BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("delete-account/{accountNumber}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteAccountAsync(string accountNumber)
    {
        var result = await _bankingService.DeleteAccountAsync(accountNumber);
        if (!result.IsSuccess) BadRequest(result);
        return Ok(result);
    }

    [HttpGet("all-accounts")]
    public async Task<ActionResult<ApiResponse<AccountResponse>>> GetAllAccountsAsync()
    {
        var result = await _bankingService.GetAllAccountAsync();
        if (!result.IsSuccess) BadRequest(result);
        return Ok(result);
    }

    [HttpPost("deposit")]
    public async Task<ActionResult<ApiResponse<AccountResponse>>> DepositAsync(DepositRequest request)
    {
        var result = await _bankingService.DepositFundAsync(request);
        if (!result.IsSuccess) BadRequest(result);
        return Ok(result);
    }

    [HttpPost("withdraw")]
    public async Task<ActionResult<ApiResponse<AccountResponse>>> WithdrawAsync(WithdrawRequest request)
    {
        var result = await _bankingService.WithdrawFundAsync(request);
        if (!result.IsSuccess) BadRequest(result);
        return Ok(result);
    }

    [HttpPost("transfer-fund")]
    public async Task<ActionResult<ApiResponse<AccountResponse>>> TransferFundAsync(TransferRequest request)
    {
        var result = await _bankingService.TransferFundAsync(request);
        if (!result.IsSuccess) BadRequest(result);
        return Ok(result);
    }

    [HttpGet("balance/{accountNumber}")]
    public async Task<ActionResult<ApiResponse<BalanceResponse>>> GetBalanceAsync(string accountNumber)
    {
        var result = await _bankingService.GetBalanceAsync(accountNumber);
        if (!result.IsSuccess) BadRequest(result);
        return Ok(result);
    }
    
}