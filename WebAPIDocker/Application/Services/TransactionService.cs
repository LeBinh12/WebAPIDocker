using sepending.Application.DTOs;
using sepending.Domain.Entities;
using sepending.Infrastructure.Repositories;
using sepending.Share;

namespace sepending.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> CreateTransaction(int userId, CreateTransactionDto dto)
    {
        var entity = new Transaction
        {
            user_id = userId,
            category_id = dto.CategoryId,
            amount = dto.Amount,
            type = dto.Type,
            transaction_date = dto.TransactionDate,
            note = dto.Note
        };
        await _repository.Add(entity);
        
        return await Result<int>.SuccessAsync(entity.id,"Thêm dữ liệu thành công");
    }

    public async Task<Result<IEnumerable<TransactionDto>>> GetTransactions(int userId, DateTime? from = null, DateTime? to = null)
    {
        var list = await _repository.GetByUser(userId);

        var transaction = list.Select(x => new TransactionDto
        {
            Id = x.id,
            CategoryId = x.category_id,
            Amount = x.amount,
            Type = x.type,
            TransactionDate = x.transaction_date,
            Note = x.note
        });
        
        return await Result<IEnumerable<TransactionDto>>.SuccessAsync(transaction, "Lấy dữ liệu thành công");
    }
    
    public async Task<Result<decimal>> GetTotalByPeriod(int userId, string type, DateTime from, DateTime to, int? categoryId = null)
    {
        var total = await _repository.SumAmount(userId, type, from, to, categoryId);
        return await Result<decimal>.SuccessAsync(total, "Tính tổng thành công");
    }

    public async Task<Result<decimal>> GetCurrentBalance(int userId)
    {
        var balance = await _repository.GetBalance(userId);
        return await Result<decimal>.SuccessAsync(balance, "Lấy số dư hiện có thành công");
    }
}