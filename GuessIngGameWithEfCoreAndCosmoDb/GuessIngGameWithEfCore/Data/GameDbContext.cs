using System;
using GuessIngGameWithEfCoreAndCosmoDb.Models;
using Microsoft.EntityFrameworkCore;
namespace GuessIngGameWithEfCoreAndCosmoDb.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<Guess> Guesses { get; set; }
        public DbSet<Contestant> Contestants { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contest>(c =>
           {
               c.ToContainer("gameContainer");
               c.HasPartitionKey(c => c.Id);
               c.OwnsMany(c => c.Prizes);
               c.OwnsMany(c => c.Contestants);
               c.OwnsMany(c => c.Guesses);
           });
        }
    }
}

