using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Alchemy_Black_Desert.Model
{
    // Таблица количества крафтов
    public class TypeCraft
    {
        public int TypeCraftId { get; set; }
        public string TypeCraftName { get; set; }
    }
    public class Craft
    {
        public int CraftId { get; set; }
        public int? TypeId { get; set; }
        public TypeCraft Type { get; set; }
        public int CountCraft { get; set; }
        public int CountOrdinary { get; set; }
        public int CountRare { get; set; }
    }


    // Таблица основных реагентов без рецепта

    public class Potion
    {
        public int PotionId { get; set; }

        public int? TypeId { get; set; }
        public TypeCraft Type { get; set; }

        public string Name { get; set; }
        public int PriceOrdinary { get; set; }
        public int PriceRare { get; set; }
        public int ImperialPriceOrdinary { get; set; }
        public int ImperialPriceRare { get; set; }

        // Указываем список реагентов, на основе которого будет готовиться зелье
        public int OneId { get; set; }
        public int OneCount { get; set; }

        public int TwoId { get; set; }
        public int TwoCount { get; set; }

        public int ThreeId { get; set; }
        public int ThreeCount { get; set; }

        public int FourId { get; set; }
        public int FourCount { get; set; }

        public int FiveId { get; set; }
        public int FiveCount { get; set; }
    }

    // Создание контекста для базы данных
    class ApplicationContext : DbContext
    {
        private string connectionString;
        public DbSet<TypeCraft> TypeCrafts { get; set; }
        public DbSet<Craft> Crafts { get; set; }
        public DbSet<Potion> Potions { get; set; }

        public ApplicationContext()
            : base()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("Resources//appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
            Database.EnsureCreated();
        }

        // Строка подключения к SQL серверу
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        // Первичные данные при создании базы данных
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeCraft>().HasData(
                new TypeCraft[]
                {
                    new TypeCraft { TypeCraftId = 1, TypeCraftName = "Reagent"},
                    new TypeCraft { TypeCraftId = 2, TypeCraftName = "Potion" }
                });

            modelBuilder.Entity<Craft>().HasData(
                new Craft[]
                {
                    new Craft { CraftId = 1, TypeId = 1, CountCraft = 500, CountOrdinary = 500},
                    new Craft { CraftId = 2, TypeId = 2, CountCraft = 500, CountOrdinary = 500, CountRare = 250},
                });

            /*modelBuilder.Entity<Potion>().HasData(
                new Potion[]
                {
                    new Potion { PotionId = 1, TypeId = 1, Name = "Азалия", PriceOrdinary = 390},
                    new Potion { PotionId = 2, TypeId = 1, Name = "Сорняк", PriceOrdinary = 390},
                    new Potion { PotionId = 3, TypeId = 1, Name = "Очищенная вода", PriceOrdinary = 390},
                    new Potion { PotionId = 4, TypeId = 1, Name = "Сахар", PriceOrdinary = 100},
                    new Potion { PotionId = 5, TypeId = 2, Name = "Порошковый реагент", PriceOrdinary = 500, ImperialPriceOrdinary = 490,
                        OneId = 1, OneCount = 1, TwoId = 2, TwoCount = 1, ThreeId = 3, ThreeCount = 1, FourId = 4, FourCount = 1}
                });*/
        }
    }
}
