using Alchemy_Black_Desert.Model;
using Alchemy_Black_Desert.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alchemy_Black_Desert.Calculations
{
    public class CalculatePrice
    {
        // Обновляем все цены, в будущем обновлять только по ссылке
        public static void CalculateAllPrice()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var recipes = db.Recipes.ToList();
                foreach (var recipe in recipes)
                {
                    int recipeCost = 0;

                    if (recipe.OneId != 0)
                    {
                        var reagent = db.Reagents.Where(t => t.ReagentId == recipe.OneId).FirstOrDefault();
                        recipeCost = recipe.OneCount * reagent.PriceOrdinary;
                    }
                    if (recipe.TwoId != 0)
                    {
                        var reagent = db.Reagents.Where(t => t.ReagentId == recipe.TwoId).FirstOrDefault();
                        recipeCost += recipe.TwoCount * reagent.PriceOrdinary;
                    }
                    if (recipe.ThreeId != 0)
                    {
                        var reagent = db.Reagents.Where(t => t.ReagentId == recipe.ThreeId).FirstOrDefault();
                        recipeCost += recipe.ThreeCount * reagent.PriceOrdinary;
                    }
                    if (recipe.FourId != 0)
                    {
                        var reagent = db.Reagents.Where(t => t.ReagentId == recipe.FourId).FirstOrDefault();
                        recipeCost += recipe.FourCount * reagent.PriceOrdinary;
                    }
                    if (recipe.FiveId != 0)
                    {
                        var reagent = db.Reagents.Where(t => t.ReagentId == recipe.FiveId).FirstOrDefault();
                        recipeCost += recipe.FiveCount * reagent.PriceOrdinary;
                    }

                    // Добавление расчетов выгоды для продажи на аукционе
                    var potion = db.Reagents.Where(t => t.ReagentId == recipe.PotionId).FirstOrDefault();
                    var craft = db.Crafts.Where(t => t.CraftId == potion.CraftTypeId).FirstOrDefault();
                    decimal sumCost = recipeCost * craft.CountCraft;

                    var auctionValue = db.Options.Where(t => t.OptionId == 1).FirstOrDefault();
                    var imperialValue = db.Options.Where(t => t.OptionId == 2).FirstOrDefault();
                    decimal tax = auctionValue.Value;
                    decimal prize = imperialValue.Value;

                    if (potion.PriceRare != 0)
                    {
                        // Аукцион
                        decimal auctionOrdinary = potion.PriceOrdinary * craft.CountOrdinary * tax;
                        decimal auctionRare = potion.PriceRare * craft.CountRare * tax;
                        decimal sumAuctionProfit = auctionOrdinary + auctionRare;

                        decimal ordinaryPercent = auctionOrdinary / sumAuctionProfit;
                        decimal rarePercent = auctionRare / sumAuctionProfit;

                        var profitAuctionOrdinary = (auctionOrdinary - (sumCost * ordinaryPercent)) / craft.CountOrdinary;
                        var profitAuctionRare = (auctionRare - (sumCost * rarePercent)) / craft.CountRare;

                        // Имперская алхимия
                        var imperial = db.ImperialTypes.Where(t => t.ImperialTypeId == potion.ImperialTypeId).FirstOrDefault();
                        decimal imperialOrdinary = imperial.Price / potion.ImperialCount * craft.CountOrdinary * prize;
                        decimal imperialRare = imperial.Price / (potion.ImperialCount / 3) * craft.CountRare * prize;
                        decimal sumImperialProfit = imperialOrdinary + imperialRare;

                        ordinaryPercent = imperialOrdinary / sumImperialProfit;
                        rarePercent = imperialRare / sumImperialProfit;

                        var profitImperialOrdinary = (imperialOrdinary - (sumCost * ordinaryPercent)) / craft.CountOrdinary;
                        var profitImperialRare = (imperialRare - (sumCost * rarePercent)) / craft.CountRare;

                        recipe.CreatePrice = recipeCost;
                        recipe.AuctionProfitOrdinary = (int)profitAuctionOrdinary;
                        recipe.AuctionProfitRare = (int)profitAuctionRare;
                        recipe.ImperialProfitOrdinary = (int)profitImperialOrdinary;
                        recipe.ImperialProfitRare = (int)profitImperialRare;
                    }
                    else
                    {
                        decimal auctionOrdinary = potion.PriceOrdinary * craft.CountOrdinary * tax;
                        var profitAuctionOrdinary = (auctionOrdinary - sumCost) / craft.CountOrdinary;
                        recipe.CreatePrice = recipeCost;
                        recipe.AuctionProfitOrdinary = (int)profitAuctionOrdinary;

                        if (potion.ImperialCount != 0)
                        {
                            var imperial = db.ImperialTypes.Where(t => t.ImperialTypeId == potion.ImperialTypeId).FirstOrDefault();
                            decimal imperialOrdinary = imperial.Price / potion.ImperialCount * craft.CountOrdinary * prize;
                            var profitImperialOrdinary = (imperialOrdinary - sumCost) / craft.CountOrdinary;
                            recipe.ImperialProfitOrdinary = (int)profitImperialOrdinary;
                        }
                    }

                    db.Entry(recipe).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
