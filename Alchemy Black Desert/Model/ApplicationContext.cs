using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace Alchemy_Black_Desert.Model
{
    // Убрать типы крафтов
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

    public class Reagent
    {
        public int ReagentId { get; set; }
        public int ReagentTypeId { get; set; }
        public ReagentType ReagentType { get; set; }
        public string Name { get; set; }
        public int PriceOrdinary { get; set; }
        public int PriceRare { get; set; }
        public int ImperialPriceOrdinary { get; set; }
        public int ImperialPriceRare { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        /*public Reagent(string name, ReagentType reagentType, int priceOrdinary)
        {
            Name = name;
            ReagentType = reagentType;
            PriceOrdinary = priceOrdinary;
        }*/
    }

    public class Recipe
    {
        public int RecipeId { get; set; }
        public int? PotionId { get; set; }
        public Reagent Potion { get; set; }

        // Добавить цену изготовления, а также выгоду изготовления
        // public int CreatePrice { get; set; }
        // public int AuctionPrice { get; set; }
        // public int ImperialPrice { get; set; }

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

        // Основные сумму имперской алхимии, сделать таблицу отдельную! и у Реагента указывать количество для крафта, а выводить стоимость пака
        const double imperialOne = 130000; // Сундук ученика
        const double imperialTwo = 130000; // Сундук профессионалла
        const double imperialThree = 300000; // Сундук эксперта
        const double imperialFour = 400000; // Сундук мастера
        const double imperialFive = 550000; // Сундук грандмастера
        const double imperialSix = 800000; // Сундук специалиста

        public DbSet<TypeCraft> TypeCrafts { get; set; }
        public DbSet<Craft> Crafts { get; set; }
        public DbSet<ReagentType> ReagentTypes { get; set; }
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

            modelBuilder.Entity<Craft>().HasData(
                new Craft[]
                {
                    new Craft { CraftId = 1, TypeId = 1, CountCraft = 900, CountOrdinary = 1452},
                    new Craft { CraftId = 2, TypeId = 2, CountCraft = 1800, CountOrdinary = 5044, CountRare = 448},
                });

            modelBuilder.Entity<ReagentType>().HasData(
                 new ReagentType[]
                 {
                    new ReagentType { ReagentTypeId = 1, Name = "Реагент 1 уровня"},
                    new ReagentType { ReagentTypeId = 2, Name = "Реагент 2 уровня"},
                    new ReagentType { ReagentTypeId = 3, Name = "Реагент 3 уровня"}
                });

            modelBuilder.Entity<Reagent>().HasData(
                new Reagent[]
                {
                    new Reagent { ReagentId = 1, ReagentTypeId = 1, Name = "Азалия", PriceOrdinary = 251},
                    new Reagent { ReagentId = 2, ReagentTypeId = 1, Name = "Сорняк", PriceOrdinary = 1000},
                    new Reagent { ReagentId = 3, ReagentTypeId = 1, Name = "Очищенная вода", PriceOrdinary = 4300},
                    new Reagent { ReagentId = 4, ReagentTypeId = 1, Name = "Сахар", PriceOrdinary = 100},
                    new Reagent { ReagentId = 5, ReagentTypeId = 2, Name = "Порошковый реагент", PriceOrdinary = 1380 },

                    new Reagent { ReagentId = 6, ReagentTypeId = 2, Name = "Кровь тирана", PriceOrdinary = 31000 },
                    new Reagent { ReagentId = 7, ReagentTypeId = 1, Name = "Пыль тьмы", PriceOrdinary = 2340 },
                    new Reagent { ReagentId = 8, ReagentTypeId = 1, Name = "Сок клена", PriceOrdinary = 4110 },
                    new Reagent { ReagentId = 9, ReagentTypeId = 1, Name = "Лотос", PriceOrdinary = 525 },
                    new Reagent { ReagentId = 10, ReagentTypeId = 3, Name = "Зелье заклинаний", PriceOrdinary = 25000, PriceRare = 76000, ImperialPriceOrdinary = (int)(imperialFour/15), ImperialPriceRare = (int)(imperialFour/5)},

                    new Reagent { ReagentId = 11, ReagentTypeId = 1, Name = "Кровь огра", PriceOrdinary = 13000 },
                    new Reagent { ReagentId = 12, ReagentTypeId = 1, Name = "Кровь тролля", PriceOrdinary = 12300 },
                    new Reagent { ReagentId = 13, ReagentTypeId = 1, Name = "Ветвь монаха", PriceOrdinary = 5400 },
                    new Reagent { ReagentId = 14, ReagentTypeId = 1, Name = "Порошок варваров", PriceOrdinary = 29200 },

                    new Reagent { ReagentId = 15, ReagentTypeId = 2, Name = "Кровь грешника", PriceOrdinary = 12600 },
                    new Reagent { ReagentId = 16, ReagentTypeId = 1, Name = "Порошок черного камня", PriceOrdinary = 2550 },
                    new Reagent { ReagentId = 17, ReagentTypeId = 1, Name = "Сок кипариса", PriceOrdinary = 2430 },
                    new Reagent { ReagentId = 18, ReagentTypeId = 1, Name = "Боровик", PriceOrdinary = 775 },
                    new Reagent { ReagentId = 20, ReagentTypeId = 3, Name = "Зелье охоты на Айн", PriceOrdinary = 15000, PriceRare = 75500, ImperialPriceOrdinary = (int)(imperialTwo/18), ImperialPriceRare = (int)(imperialTwo/6)},

                    new Reagent { ReagentId = 21, ReagentTypeId = 1, Name = "Синяк", PriceOrdinary = 2380 },
                    new Reagent { ReagentId = 22, ReagentTypeId = 1, Name = "Небесно-голубой цветок", PriceOrdinary = 4110 },
                    new Reagent { ReagentId = 23, ReagentTypeId = 1, Name = "Пыль веков", PriceOrdinary = 2040 },
                    new Reagent { ReagentId = 24, ReagentTypeId = 1, Name = "Сок мшистого дерева", PriceOrdinary = 2425 },
                    new Reagent { ReagentId = 25, ReagentTypeId = 3, Name = "Зелье духа Винни", PriceOrdinary = 20100, PriceRare = 98000, ImperialPriceOrdinary = (int)(imperialThree/15), ImperialPriceRare = (int)(imperialThree/5)},

                    new Reagent { ReagentId = 26, ReagentTypeId = 1, Name = "Сок ясеня", PriceOrdinary = 5750 },
                    new Reagent { ReagentId = 27, ReagentTypeId = 1, Name = "Головач", PriceOrdinary = 406 },
                    new Reagent { ReagentId = 28, ReagentTypeId = 3, Name = "Зелье ярости", PriceOrdinary = 29900, PriceRare = 87000, ImperialPriceOrdinary = (int)(imperialThree/15), ImperialPriceRare = (int)(imperialThree/5)},

                    new Reagent { ReagentId = 29, ReagentTypeId = 1, Name = "Сок ольхи", PriceOrdinary = 1000 },
                    new Reagent { ReagentId = 30, ReagentTypeId = 3, Name = "Зелье энергии", PriceOrdinary = 15400, PriceRare = 49000, ImperialPriceOrdinary = (int)(imperialFour/24), ImperialPriceRare = (int)(imperialFour/8)},

                    new Reagent { ReagentId = 31, ReagentTypeId = 1, Name = "Кровь волка", PriceOrdinary = 7950 },
                    new Reagent { ReagentId = 32, ReagentTypeId = 1, Name = "Волшебный листок", PriceOrdinary = 3710 },
                    new Reagent { ReagentId = 33, ReagentTypeId = 1, Name = "Пыль тьмы", PriceOrdinary = 2680 },
                    new Reagent { ReagentId = 34, ReagentTypeId = 2, Name = "Жидкий реагент", PriceOrdinary = 1390 },
                    new Reagent { ReagentId = 35, ReagentTypeId = 2, Name = "Кровь шута", PriceOrdinary = 14100 },
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
