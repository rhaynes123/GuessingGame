using System;
using GuessIngGameWithEfCore.Models;
using Microsoft.EntityFrameworkCore;
namespace GuessIngGameWithEfCore.Data
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
    }
}

