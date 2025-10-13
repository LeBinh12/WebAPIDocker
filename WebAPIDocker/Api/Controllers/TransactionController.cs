using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sepending.Application.DTOs;
using sepending.Infrastructure.Repositories;
using sepending.Share;

namespace sepending.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpPost]
    [Route("create")]
    public async Task<Result<int>> CreateTransaction([FromBody] CreateTransactionDto dto, [FromQuery] int userId)
    {
        return await _transactionService.CreateTransaction(userId, dto);
    }
    
    [HttpGet]
    [Route("get-transactions")]
    public async Task<Result<IEnumerable<TransactionDto>>> GetTransactions([FromQuery] int userId, [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
    {
        return await _transactionService.GetTransactions(userId, from, to);
    }
    
    [HttpGet]
    [Route("get-total-by-period")]
    public async Task<Result<decimal>> GetTotalByPeriod([FromQuery] int userId, [FromQuery] string type, [FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        return await _transactionService.GetTotalByPeriod(userId, type, from, to);
    }

    [HttpGet]
    [Route("get-current-balance")]
    public async Task<Result<decimal>> GetCurrentBalance([FromQuery] int userId)
    {
        return await _transactionService.GetCurrentBalance(userId);
    }
}