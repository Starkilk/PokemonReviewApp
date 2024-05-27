using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация таблицы PokemonCategory
            modelBuilder.Entity<PokemonCategory>()
                .HasKey(pc => new { pc.PokemonId, pc.CategoryId }); // Определение составного первичного ключа
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon) // Устанавливаем отношение "один-ко-многим" с таблицей Pokemon
                .WithMany(pc => pc.PokemonCategories) // Один Pokemon имеет много PokemonCategory
                .HasForeignKey(p => p.PokemonId); // Установка внешнего ключа PokemonId
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(c => c.Category) // Устанавливаем отношение "один-ко-многим" с таблицей Category
                .WithMany(pc => pc.PokemonCategories) // Одна Category имеет много PokemonCategory
                .HasForeignKey(c => c.CategoryId); // Установка внешнего ключа CategoryId

            // Конфигурация таблицы PokemonOwner
            modelBuilder.Entity<PokemonOwner>()
                .HasKey(po => new { po.PokemonId, po.OwnerId }); // Определение составного первичного ключа
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon) // Устанавливаем отношение "один-ко-многим" с таблицей Pokemon
                .WithMany(po => po.PokemonOwners) // Один Pokemon имеет много PokemonOwner
                .HasForeignKey(p => p.PokemonId); // Установка внешнего ключа PokemonId
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(o => o.Owner) // Устанавливаем отношение "один-ко-многим" с таблицей Owner
                .WithMany(po => po.PokemonOwners) // Один Owner имеет много PokemonOwner
                .HasForeignKey(o => o.OwnerId); // Установка внешнего ключа OwnerId
        }

    }
}
