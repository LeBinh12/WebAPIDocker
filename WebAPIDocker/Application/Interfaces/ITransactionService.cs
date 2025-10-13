using sepending.Application.DTOs;
using sepending.Share;

namespace sepending.Infrastructure.Repositories;

public interface ITransactionService
{
    Task<Result<int>> CreateTransaction(int userId, CreateTransactionDto dto);
    Task<Result<IEnumerable<TransactionDto>>> GetTransactions(int userId, DateTime? from = null, DateTime? to = null);
    Task<Result<decimal>> GetTotalByPeriod(int userId, string type, DateTime from, DateTime to, int? categoryId = null);
    Task<Result<decimal>> GetCurrentBalance(int userId);

}