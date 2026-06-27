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

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }
}