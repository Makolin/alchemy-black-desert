using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace Alchemy_Black_Desert.Model
{
    // Таблица количества крафтов
    public class TypeCraft
    {
        public int TypeCraftId { get; set; }
        public string TypeCraftName { get; set; }
        public List<Craft> Crafts { get; set; } = new List<Craft>();
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

    public class ReagentType
    {
        public int ReagentTypeId { get; set; }
        public string Name { get; set; }
        public List<Reagent> Reagents { get; set; } = new List<Reagent>();
    }

    // Таблица реагентов
    public class Reagent
    {
        public int ReagentId { get; set; }
        public int? TypeId { get; set; }
        public ReagentType Type { get; set; }
        public string Name { get; set; }
        public int PriceOrdinary { get; set; }
        public int PriceRare { get; set; }
        public int ImperialPriceOrdinary { get; set; }
        public int ImperialPriceRare { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }

    // Сделать списки для упрощения работы с рецептами
    /*public class RecipeReagent
    {
        public int RecipeReagentId { get; set; }
        public Reagent RecipeReagentName { get; set; }
        public int RecipeCount { get; set; }

    }*/

    // Рецепт изготовления любого из реагентов
    public class Recipe
    {
        public int RecipeId { get; set; }
        public int? PotionId { get; set; }
        public Reagent Potion { get; set; }

        public int OneId { get; set; }
        //public Reagent OneReagent { get; set; }
        public int OneCount { get; set; }

        public int TwoId { get; set; }
        //public Reagent TwoReagent { get; set; }
        public int TwoCount { get; set; }

        public int ThreeId { get; set; }
        //public Reagent ThreeReagent { get; set; }
        public int ThreeCount { get; set; }

        public int FourId { get; set; }
        //public Reagent FourReagent { get; set; }
        public int FourCount { get; set; }

        public int FiveId { get; set; }
        //public Reagent FiveReagent { get; set; }
        public int FiveCount { get; set; }
    }

    // Создание контекста для базы данных
    class ApplicationContext : DbContext
    {
        private string connectionString;
        // Добавить в переменные в отдельный файл
        const double prize = 1.14;

        // Основные сумму имперской алхимии
        const double imperialOne = 130000 * prize;
        const double imperialTwo = 300000 * prize;
        const double imperialThree = 400000 * prize;
        const double imperialFour = 550000 * prize;
        const double imperialFive = 800000 * prize;

        public DbSet<TypeCraft> TypeCrafts { get; set; }
        public DbSet<Craft> Crafts { get; set; }
        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

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

            // Будет пока что всего два вида крафта на алхимическом столе
            modelBuilder.Entity<Craft>().HasData(
                new Craft[]
                {
                    new Craft { CraftId = 1, TypeId = 1, CountCraft = 500, CountOrdinary = 500},
                    new Craft { CraftId = 2, TypeId = 2, CountCraft = 1800, CountOrdinary = 5044, CountRare = 448},
                });

            // Будет пока что всего два вида реагентов, собираемый и изготавливаемый
            modelBuilder.Entity<ReagentType>().HasData(
                 new ReagentType[]
                 {
                    new ReagentType { ReagentTypeId = 1, Name = "Reagent"},
                    new ReagentType { ReagentTypeId = 2, Name = "CraftReagent"},
                    new ReagentType { ReagentTypeId = 3, Name = "Potion"}
                });

            // Временный список для первого крафтового реагента
            modelBuilder.Entity<Reagent>().HasData(
                new Reagent[]
                {
                    new Reagent { ReagentId = 1, TypeId = 1, Name = "Азалия", PriceOrdinary = 390},
                    new Reagent { ReagentId = 2, TypeId = 1, Name = "Сорняк", PriceOrdinary = 390},
                    new Reagent { ReagentId = 3, TypeId = 1, Name = "Очищенная вода", PriceOrdinary = 390},
                    new Reagent { ReagentId = 4, TypeId = 1, Name = "Сахар", PriceOrdinary = 100},
                    new Reagent { ReagentId = 5, TypeId = 2, Name = "Порошковый реагент", PriceOrdinary = 100 },
                    new Reagent { ReagentId = 6, TypeId = 2, Name = "Кровь тирана", PriceOrdinary = 31000 },
                    new Reagent { ReagentId = 7, TypeId = 1, Name = "Пыль тьмы", PriceOrdinary = 2340 },
                    new Reagent { ReagentId = 8, TypeId = 1, Name = "Сок клена", PriceOrdinary = 4110 },
                    new Reagent { ReagentId = 9, TypeId = 1, Name = "Лотос", PriceOrdinary = 424 },
                    new Reagent { ReagentId = 10, TypeId = 3, Name = "Зелье заклинаний", PriceOrdinary = 25000, PriceRare = 76000, ImperialPriceOrdinary = (int)(imperialThree/15), ImperialPriceRare = (int)(imperialThree/5)},
                });

            // Первый рецепт для тестирования
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe[]
                {
                    new Recipe { RecipeId = 1, PotionId = 5, OneId = 1, OneCount = 1, TwoId = 2, TwoCount = 1, ThreeId = 3, ThreeCount = 1, FourId = 4, FourCount = 1},
                    new Recipe { RecipeId = 2, PotionId = 10, OneId = 6, OneCount = 1, TwoId = 7, TwoCount = 2, ThreeId = 8, ThreeCount = 3, FourId = 9, FourCount = 5}
                });
        }
    }
}
