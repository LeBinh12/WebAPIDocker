using Microsoft.EntityFrameworkCore;
using sepending.Domain.Entities;
using sepending.Infrastructure.Data;
using sepending.Share;

namespace sepending.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ExpenseDbContext _db ;

    public TransactionRepository(ExpenseDbContext db)
    {
        _db = db;
    }

    public async Task<Transaction?> GetById(int id, int userId)
    {
       return await _db.Transactions.FirstOrDefaultAsync(x => x.id == id && x.user_id == userId);
    }

    public async Task<IEnumerable<Transaction>> GetByUser(int userId, DateTime? from = null, DateTime? to = null)
    {
        var query = _db.Transactions.Where(x => x.user_id == userId);

        if (from.HasValue) query = query.Where(x => x.transaction_date >= from.Value);
        if (to.HasValue) query = query.Where(x => x.transaction_date <= to.Value);

        return await query.OrderByDescending(x => x.transaction_date).ToListAsync();
    }

    public async Task<decimal> GetBalance(int userId)
    {
        var income = await _db.Transactions.Where(x => x.user_id == userId && x.type == "INCOME")
            .SumAsync(x => (decimal?)x.amount ?? 0);
        
        var expense = await _db.Transactions.Where(x => x.user_id == userId && x.type == "EXPENSE")
            .SumAsync(x => (decimal?)x.amount ?? 0);
        
        return income - expense;
    }


    public async Task<decimal> SumAmount(int userId, string type, DateTime from, DateTime to, int? categoryId)
    {
        return await _db.Transactions
            .Where(x => x.user_id == userId 
                        && x.type == type 
                        && x.category_id == categoryId
                        && x.transaction_date >= from 
                        && x.transaction_date <= to)
            .SumAsync(x => x.amount);
    }


    public async Task<Transaction> Add(Transaction transaction)
    {
        try
        {
            await _db.Transactions.AddAsync(transaction);
            await _db.SaveChangesAsync();
            return transaction;
        } 
        catch (Exception ex)
        {
            Console.WriteLine("Save error: " + ex.InnerException?.Message);
            throw;
        }
    }

    public async Task Update(Transaction transaction)
    {
        _db.Transactions.Update(transaction);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(Transaction transaction)
    {
        _db.Transactions.Remove(transaction);
        await _db.SaveChangesAsync();
    }
}