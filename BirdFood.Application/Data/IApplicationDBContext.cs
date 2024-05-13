using BirdFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirdFood.Application.Data
{
    public interface IApplicationDBContext
    {
        DbSet<Account> Account { get; set; }
        DbSet<Token> Token { get; set; }
        DbSet<TokenUsed> TokenUsed { get; set; }
        DbSet<Shop> Shop { get; set; }
        DbSet<Food> Food { get; set; }
        DbSet<Combo> Combo { get; set; }
        DbSet<FoodCombo> FoodCombo { get; set; }
    }
}
