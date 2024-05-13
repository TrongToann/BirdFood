
using BirdFood.Application.Data;
using BirdFood.Domain.Abstraction;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Entities;
using BirdFood.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BirdFood.Persistence
{
    public class BirdFoodDBContext : DbContext, IApplicationDBContext, IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories;
        public BirdFoodDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BirdFoodDBContext).Assembly);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            //New Logic Here
            var result = await base.SaveChangesAsync();
            return result;
        }
        public virtual async Task<int> SaveChangesAsync(string Username)
        {
            foreach (var entry in base.ChangeTracker.Entries<EntityAuditBase<Guid>>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.ModifiedDate = DateTime.UtcNow;
                entry.Entity.ModifiedBy = Username;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = Username;
                }
            }
            var result = await base.SaveChangesAsync();
            return result;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : EntityBase<TKey>
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IGenericRepository<TEntity, TKey>)_repositories[typeof(TEntity)];
            }
            var repository = new GenericRepository<TEntity, TKey>(this);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<TokenUsed> TokenUsed { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Combo> Combo { get; set; }
        public DbSet<FoodCombo> FoodCombo { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderFoods> OrderFoods { get; set; }
        public DbSet<OrderShipping> OrderShipping { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartFood> CartFoods { get; set; }

    }
}
