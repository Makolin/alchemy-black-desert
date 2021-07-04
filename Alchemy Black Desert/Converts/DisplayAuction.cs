using Alchemy_Black_Desert.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Alchemy_Black_Desert.Converts
{
    // Считаем выгоду на одну единицу товара при продаже на аукцион
    public class DisplayAuction : IValueConverter
    {
        // Добавить в настройки
        const double tax = 0.848;
        public object Convert(object value, Type TargetType, object parametr, CultureInfo culture)
        {
            var expenses = 0;
            var profit = 0;
            double priceAuction = 0;
            string textOut = string.Empty;
            if (value != null)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var currentRecipe = db.Recipes.Where(t => t.RecipeId == (int)value).FirstOrDefault();
                    var currentReagent = db.Reagents.Where(t => t.ReagentId == currentRecipe.PotionId).FirstOrDefault();

                    if (currentRecipe.OneId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == currentRecipe.OneId).FirstOrDefault();
                        expenses = currentRecipe.OneCount * Reagent.PriceOrdinary;
                    }
                    if (currentRecipe.TwoId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == currentRecipe.TwoId).FirstOrDefault();
                        expenses += currentRecipe.TwoCount * Reagent.PriceOrdinary;
                    }
                    if (currentRecipe.ThreeId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == currentRecipe.ThreeId).FirstOrDefault();
                        expenses += currentRecipe.ThreeCount * Reagent.PriceOrdinary;
                    }
                    if (currentRecipe.FourId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == currentRecipe.FourId).FirstOrDefault();
                        expenses += currentRecipe.FourCount * Reagent.PriceOrdinary;
                    }
                    if (currentRecipe.FiveId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == currentRecipe.FiveId).FirstOrDefault();
                        expenses += currentRecipe.FiveCount * Reagent.PriceOrdinary;
                    }

                    // Теперь получим прибыль за изготовление
                    Craft craft = null;
                    if (currentReagent.ReagentTypeId == 2)
                    {
                        craft = db.Crafts.Where(t => t.TypeId == 1).FirstOrDefault();
                        profit = currentReagent.PriceOrdinary * craft.CountOrdinary;
                    }
                    else if (currentReagent.ReagentTypeId == 3)
                    {
                        craft = db.Crafts.Where(t => t.TypeId == 2).FirstOrDefault();
                        profit = currentReagent.PriceOrdinary * craft.CountOrdinary;
                        profit += currentReagent.PriceRare * craft.CountRare;
                    }
                    expenses *= craft.CountCraft;
                    priceAuction = (profit * tax - expenses) / craft.CountCraft * 900;
                    priceAuction = Math.Round(priceAuction);

                    culture = new CultureInfo("ru-RU");
                    textOut = string.Format(priceAuction.ToString("#,#", culture));
                }
            }
            return textOut;
        }

        public object ConvertBack(object value, Type TargetType, object parametr, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
