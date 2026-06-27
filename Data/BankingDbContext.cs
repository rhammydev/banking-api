using Microsoft.EntityFrameworkCore;
using SimpleBankingAPI.Model;

namespace SimpleBankingAPI.Data;

public class BankingDbContext : DbContext
{
    public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Account>  Accounts { get; set; }
    public DbSet<Transaction>  Transactions { get; set; }
}