using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace Alchemy_Black_Desert.Model
{
    public class Option
    {
        public int OptionId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
    public class Craft
    {
        public int CraftId { get; set; }
        public string TypeName { get; set; }
        public int CountCraft { get; set; }
        public int CountOrdinary { get; set; }
        public int CountRare { get; set; }
        public List<Reagent> Reagents { get; set; } = new List<Reagent>();
    }

    public class ReagentType
    {
        public int ReagentTypeId { get; set; }
        public string Name { get; set; }
        public List<Reagent> Reagents { get; set; } = new List<Reagent>();
    }

    public class Reagent
    {
        public int ReagentId { get; set; }
        public int ReagentTypeId { get; set; }
        public ReagentType ReagentType { get; set; }
        public int? CraftTypeId { get; set; }
        public Craft Craft { get; set; }
        public string Name { get; set; }
        public int PriceOrdinary { get; set; }
        public int PriceRare { get; set; }
        public int? ImperialTypeId { get; set; }
        public ImperialType ImperialType { get; set; }
        public int ImperialCount { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }

    public class Recipe
    {
        public int RecipeId { get; set; }
        public int? PotionId { get; set; }
        public Reagent Potion { get; set; }
        public int CreatePrice { get; set; }

        public int AuctionProfitOrdinary { get; set; }
        public int AuctionProfitRare { get; set; }

        public int ImperialProfitOrdinary { get; set; }
        public int ImperialProfitRare { get; set; }

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

    public class ImperialType
    {
        public int ImperialTypeId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<Reagent> Reagents { get; set; } = new List<Reagent>();
    }


    // Создание контекста для базы данных
    class ApplicationContext : DbContext
    {
        private string connectionString;

        public DbSet<Option> Options { get; set; }
        public DbSet<Craft> Crafts { get; set; }
        public DbSet<ReagentType> ReagentTypes { get; set; }
        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<ImperialType> ImperialTypes { get; set; }

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
            modelBuilder.Entity<Option>().HasData(
                new Option[]
                {
                    new Option { OptionId = 1, Name = "Коммиссия аукциона", Value = 0.848M},
                    new Option { OptionId = 2, Name = "Сверхприбыль имперской торговли", Value = 1.141M},
                });

            modelBuilder.Entity<Craft>().HasData(
                new Craft[]
                {
                    new Craft { CraftId = 1, TypeName = "Реагент", CountCraft = 900, CountOrdinary = 1450},
                    new Craft { CraftId = 2, TypeName = "Зелье", CountCraft = 900, CountOrdinary = 2520, CountRare = 270},
                });

            modelBuilder.Entity<ReagentType>().HasData(
                 new ReagentType[]
                 {
                    new ReagentType { ReagentTypeId = 1, Name = "Реагент 1 уровня"},
                    new ReagentType { ReagentTypeId = 2, Name = "Реагент 2 уровня"},
                    new ReagentType { ReagentTypeId = 3, Name = "Реагент 3 уровня"},
                    new ReagentType { ReagentTypeId = 4, Name = "Реагент 4 уровня"},
                });

            modelBuilder.Entity<ImperialType>().HasData(
                new ImperialType[]
                {
                    new ImperialType { ImperialTypeId = 1, Name = "Ученика", Price = 130000},
                    new ImperialType { ImperialTypeId = 2, Name = "Профессионала", Price = 200000},
                    new ImperialType { ImperialTypeId = 3, Name = "Эксперта", Price = 300000},
                    new ImperialType { ImperialTypeId = 4, Name = "Мастера", Price = 400000},
                    new ImperialType { ImperialTypeId = 5, Name = "Грандмастера", Price = 550000},
                    new ImperialType { ImperialTypeId = 6, Name = "Специалиста", Price = 800000},
                });

            modelBuilder.Entity<Reagent>().HasData(
                new Reagent[]
                {
                    new Reagent { ReagentId = 1, ReagentTypeId = 1, CraftTypeId = 2, Name = "Азалия", PriceOrdinary = 251},
                    new Reagent { ReagentId = 2, ReagentTypeId = 1, CraftTypeId = 2, Name = "Сорняк", PriceOrdinary = 1000},
                    new Reagent { ReagentId = 3, ReagentTypeId = 1, CraftTypeId = 2, Name = "Очищенная вода", PriceOrdinary = 4300},
                    new Reagent { ReagentId = 4, ReagentTypeId = 1, CraftTypeId = 2, Name = "Сахар", PriceOrdinary = 100},
                    new Reagent { ReagentId = 5, ReagentTypeId = 2, CraftTypeId = 2, Name = "Порошковый реагент", PriceOrdinary = 1380 },

                    new Reagent { ReagentId = 6, ReagentTypeId = 2, CraftTypeId = 2, Name = "Кровь тирана", PriceOrdinary = 31000 },
                    new Reagent { ReagentId = 7, ReagentTypeId = 1, CraftTypeId = 2, Name = "Пыль тьмы", PriceOrdinary = 2340 },
                    new Reagent { ReagentId = 8, ReagentTypeId = 1, CraftTypeId = 2, Name = "Сок клена", PriceOrdinary = 4110 },
                    new Reagent { ReagentId = 9, ReagentTypeId = 1, CraftTypeId = 2, Name = "Лотос", PriceOrdinary = 525 },
                    new Reagent { ReagentId = 10, ReagentTypeId = 3, CraftTypeId = 2, Name = "Зелье заклинаний", PriceOrdinary = 25000, PriceRare = 76000, ImperialTypeId = 4, ImperialCount = 15},

                    new Reagent { ReagentId = 11, ReagentTypeId = 1, CraftTypeId = 2, Name = "Кровь огра", PriceOrdinary = 13000 },
                    new Reagent { ReagentId = 12, ReagentTypeId = 1, CraftTypeId = 2, Name = "Кровь тролля", PriceOrdinary = 12300 },
                    new Reagent { ReagentId = 13, ReagentTypeId = 1, CraftTypeId = 2, Name = "Ветвь монаха", PriceOrdinary = 5400 },
                    new Reagent { ReagentId = 14, ReagentTypeId = 1, CraftTypeId = 2, Name = "Порошок варваров", PriceOrdinary = 29200 },

                    new Reagent { ReagentId = 15, ReagentTypeId = 2, CraftTypeId = 2, Name = "Кровь грешника", PriceOrdinary = 12600 },
                    new Reagent { ReagentId = 16, ReagentTypeId = 1, CraftTypeId = 2, Name = "Порошок черного камня", PriceOrdinary = 2550 },
                    new Reagent { ReagentId = 17, ReagentTypeId = 1, CraftTypeId = 2, Name = "Сок кипариса", PriceOrdinary = 2430 },
                    new Reagent { ReagentId = 18, ReagentTypeId = 1, CraftTypeId = 2, Name = "Боровик", PriceOrdinary = 775 },
                    new Reagent { ReagentId = 20, ReagentTypeId = 3, CraftTypeId = 2, Name = "Зелье охоты на Айн", PriceOrdinary = 15000, PriceRare = 75500, ImperialTypeId = 2, ImperialCount = 18},

                    new Reagent { ReagentId = 21, ReagentTypeId = 1, CraftTypeId = 2, Name = "Синяк", PriceOrdinary = 2380 },
                    new Reagent { ReagentId = 22, ReagentTypeId = 1, CraftTypeId = 2, Name = "Небесно-голубой цветок", PriceOrdinary = 4110 },
                    new Reagent { ReagentId = 23, ReagentTypeId = 1, CraftTypeId = 2, Name = "Пыль веков", PriceOrdinary = 2040 },
                    new Reagent { ReagentId = 24, ReagentTypeId = 1, CraftTypeId = 2, Name = "Сок мшистого дерева", PriceOrdinary = 2425 },
                    new Reagent { ReagentId = 25, ReagentTypeId = 3, CraftTypeId = 2, Name = "Зелье духа Винни", PriceOrdinary = 20100, PriceRare = 98000, ImperialTypeId = 3, ImperialCount = 15},

                    new Reagent { ReagentId = 26, ReagentTypeId = 1, CraftTypeId = 2, Name = "Сок ясеня", PriceOrdinary = 5750 },
                    new Reagent { ReagentId = 27, ReagentTypeId = 1, CraftTypeId = 2, Name = "Головач", PriceOrdinary = 406 },
                    new Reagent { ReagentId = 28, ReagentTypeId = 3, CraftTypeId = 2, Name = "Зелье ярости", PriceOrdinary = 29900, PriceRare = 87000, ImperialTypeId = 3, ImperialCount = 15 },

                    new Reagent { ReagentId = 29, ReagentTypeId = 1, CraftTypeId = 2, Name = "Сок ольхи", PriceOrdinary = 1000 },
                    new Reagent { ReagentId = 30, ReagentTypeId = 3, CraftTypeId = 2, Name = "Зелье энергии", PriceOrdinary = 15400, PriceRare = 49000, ImperialTypeId = 4, ImperialCount = 24},

                    new Reagent { ReagentId = 31, ReagentTypeId = 1, CraftTypeId = 2, Name = "Кровь волка", PriceOrdinary = 7950 },
                    new Reagent { ReagentId = 32, ReagentTypeId = 1, CraftTypeId = 2, Name = "Волшебный листок", PriceOrdinary = 3710 },
                    new Reagent { ReagentId = 33, ReagentTypeId = 1, CraftTypeId = 2, Name = "Пыль тьмы", PriceOrdinary = 2680 },
                    new Reagent { ReagentId = 34, ReagentTypeId = 2, CraftTypeId = 2, Name = "Жидкий реагент", PriceOrdinary = 1390 },
                    new Reagent { ReagentId = 35, ReagentTypeId = 2, CraftTypeId = 2, Name = "Кровь шута", PriceOrdinary = 14100 },
                });

            // Первый рецепт для тестирования
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe[]
                {
                    new Recipe { RecipeId = 1, PotionId = 5, OneId = 1, OneCount = 1, TwoId = 2, TwoCount = 1, ThreeId = 3, ThreeCount = 1, FourId = 4, FourCount = 1},
                    new Recipe { RecipeId = 2, PotionId = 10, OneId = 6, OneCount = 1, TwoId = 7, TwoCount = 2, ThreeId = 8, ThreeCount = 3, FourId = 9, FourCount = 5},
                    new Recipe { RecipeId = 3, PotionId = 6, OneId = 5, OneCount = 1, TwoId = 11, TwoCount = 2, ThreeId = 13, ThreeCount = 1, FourId = 14, FourCount = 1},
                    new Recipe { RecipeId = 4, PotionId = 20, OneId = 15, OneCount = 1, TwoId = 16, TwoCount = 3, ThreeId = 17, ThreeCount = 4, FourId = 18, FourCount = 4},
                    new Recipe { RecipeId = 5, PotionId = 25, OneId = 21, OneCount = 2, TwoId = 22, TwoCount = 1, ThreeId = 24, ThreeCount = 4, FourId = 23, FourCount = 3, FiveId = 13, FiveCount = 5},
                    new Recipe { RecipeId = 6, PotionId = 28, OneId = 11, OneCount = 4, TwoId = 3, TwoCount = 3, ThreeId = 26, ThreeCount = 1, FourId = 27, FourCount = 4},
                    new Recipe { RecipeId = 7, PotionId = 30, OneId = 5, OneCount = 1, TwoId = 11, TwoCount = 4, ThreeId = 29, ThreeCount = 5, FourId = 27, FourCount = 2},
                    new Recipe { RecipeId = 8, PotionId = 35, OneId = 34, OneCount = 1, TwoId = 31, TwoCount = 2, ThreeId = 32, ThreeCount = 1, FourId = 33, FourCount = 1},
                });
        }
    }
}
