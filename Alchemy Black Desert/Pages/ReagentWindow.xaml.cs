using Alchemy_Black_Desert.Calculations;
using Alchemy_Black_Desert.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Alchemy_Black_Desert.Pages
{
    public partial class ReagentWindow : Window
    {
        Recipe insertRecipe;
        public ReagentWindow(Recipe currentRecipe)
        {
            InitializeComponent();
            InsertDataInTextBox(currentRecipe);
        }

        // Заполнение полей 
        private void InsertDataInTextBox(Recipe recipe)
        {
            insertRecipe = recipe;
            using (ApplicationContext db = new ApplicationContext())
            {
                var ReagentList = db.Reagents.OrderBy(t => t.Name).ToList();
                // var ReagentListFilter = ReagentList.Where(t => t.ReagentTypeId != 1).ToList();
                List<ComboBox> ComboBoxList = new List<ComboBox> { ComboBoxPotion, ComboBoxReagentOne, ComboBoxReagentTwo, ComboBoxReagentThree, ComboBoxReagentFour, ComboBoxReagentFive };

                foreach (var comboBox in ComboBoxList)
                {
                    comboBox.ItemsSource = ReagentList;
                    comboBox.DisplayMemberPath = "Name";
                }

                var TypeList = db.ReagentTypes.OrderBy(t => t.Name).ToList();
                List<ComboBox> ComboBoxTypeList = new List<ComboBox> { ComboBoxPotionType, ComboBoxReagentOneType, ComboBoxReagentTwoType, ComboBoxReagentThreeType, ComboBoxReagentFourType, ComboBoxReagentFiveType };

                foreach (var comboBox in ComboBoxTypeList)
                {
                    comboBox.ItemsSource = TypeList;
                    comboBox.DisplayMemberPath = "Name";
                }

                List<int> CraftCountList = new List<int> { 1, 900, 1800, 2700, 3600, 4500, 5400, 6300 };
                ComboBoxCountCraft.ItemsSource = CraftCountList;
                ComboBoxCountCraft.SelectedItem = 1;

                List<int> CountRecipe = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                List<ComboBox> ComboBoxCount = new List<ComboBox> { ComboBoxReagentOneCount, ComboBoxReagentTwoCount, ComboBoxReagentThreeCount, ComboBoxReagentFourCount, ComboBoxReagentFiveCount };

                foreach (var comboBox in ComboBoxCount)
                {
                    comboBox.ItemsSource = CountRecipe;
                }

                var ImperialTypeList = db.ImperialTypes.OrderBy(t => t.ImperialTypeId).ToList();
                ComboBoxImperial.ItemsSource = ImperialTypeList;
                ComboBoxImperial.DisplayMemberPath = "Name";

                var CraftTypeList = db.Crafts.ToList();
                ComboBoxCraftType.ItemsSource = CraftTypeList;
                ComboBoxCraftType.DisplayMemberPath = "TypeName";

                if (recipe != null)
                {
                    var currentReagent = db.Reagents.Where(t => t.ReagentId == recipe.PotionId).FirstOrDefault();
                    TextBoxPriceAuctionOrdinary.Text = currentReagent.PriceOrdinary.ToString();
                    TextBoxPriceAuctionRare.Text = currentReagent.PriceRare.ToString();
                    TextBoxPriceImperialCount.Text = currentReagent.ImperialCount.ToString();

                    ComboBoxImperial.SelectedIndex = ImperialTypeList.FindIndex(t => t.ImperialTypeId == currentReagent.ImperialTypeId);
                    ComboBoxPotion.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == currentReagent.ReagentId);
                    ComboBoxPotionType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == currentReagent.ReagentTypeId);
                    ComboBoxCraftType.SelectedIndex = CraftTypeList.FindIndex(t => t.CraftId == currentReagent.CraftTypeId);

                    if (recipe.OneId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.OneId).FirstOrDefault();
                        ComboBoxReagentOne.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.OneId);
                        ComboBoxReagentOneCount.Text = recipe.OneCount.ToString();
                        TextBoxReagentOnePrice.Text = Reagent.PriceOrdinary.ToString();
                        ComboBoxReagentOneType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == Reagent.ReagentTypeId);
                    }

                    if (recipe.TwoId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.TwoId).FirstOrDefault();
                        ComboBoxReagentTwo.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.TwoId);
                        ComboBoxReagentTwoCount.Text = recipe.TwoCount.ToString();
                        TextBoxReagentTwoPrice.Text = Reagent.PriceOrdinary.ToString();
                        ComboBoxReagentTwoType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == Reagent.ReagentTypeId);
                    }

                    if (recipe.ThreeId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.ThreeId).FirstOrDefault();
                        ComboBoxReagentThree.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.ThreeId);
                        ComboBoxReagentThreeCount.Text = recipe.ThreeCount.ToString();
                        TextBoxReagentThreePrice.Text = Reagent.PriceOrdinary.ToString();
                        ComboBoxReagentThreeType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == Reagent.ReagentTypeId);
                    }

                    if (recipe.FourId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.FourId).FirstOrDefault();
                        ComboBoxReagentFour.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.FourId);
                        ComboBoxReagentFourCount.Text = recipe.FourCount.ToString();
                        TextBoxReagentFourPrice.Text = Reagent.PriceOrdinary.ToString();
                        ComboBoxReagentFourType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == Reagent.ReagentTypeId);
                    }

                    if (recipe.FiveId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.FiveId).FirstOrDefault();
                        ComboBoxReagentFive.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.FiveId);
                        ComboBoxReagentFiveCount.Text = recipe.FiveCount.ToString();
                        TextBoxReagentFivePrice.Text = Reagent.PriceOrdinary.ToString();
                        ComboBoxReagentFiveType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == Reagent.ReagentTypeId);
                    }
                }
            }
        }

        private void CreateNewReagent()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var reagentType = (ReagentType)ComboBoxPotionType.SelectedItem;
                var craftType = (Craft)ComboBoxCraftType.SelectedItem;
                Reagent reagent = null;

                if (ComboBoxPotion.SelectedIndex == -1 && !string.IsNullOrEmpty(ComboBoxPotion.Text))
                {
                    reagent = new Reagent { Name = ComboBoxPotion.Text };
                    db.Reagents.Add(reagent);
                    db.SaveChanges();
                }

                else
                {
                    reagent = (Reagent)ComboBoxPotion.SelectedItem;
                }

                reagent.ReagentTypeId = reagentType.ReagentTypeId;
                reagent.CraftTypeId = craftType.CraftId;
                reagent.PriceOrdinary = Convert.ToInt32(TextBoxPriceAuctionOrdinary.Text);

                if (!string.IsNullOrEmpty(TextBoxPriceAuctionRare.Text))
                    reagent.PriceRare = Convert.ToInt32(TextBoxPriceAuctionRare.Text);

                if (!string.IsNullOrEmpty(TextBoxPriceImperialCount.Text))
                    reagent.ImperialCount = Convert.ToInt32(TextBoxPriceImperialCount.Text);

                if (ComboBoxImperial.SelectedIndex == -1 && !string.IsNullOrEmpty(ComboBoxImperial.Text))
                {
                    var imperialType = (ImperialType)ComboBoxImperial.SelectedItem;
                    reagent.ImperialTypeId = imperialType.ImperialTypeId;
                }

                db.Entry(reagent).State = EntityState.Modified;
                insertRecipe = new Recipe();
                insertRecipe.Potion = reagent;

                CreatyNewRecipe();

                ReagentCollection.Recipes.Add(insertRecipe);
                db.Entry(insertRecipe).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        private void CreatyNewRecipe()
        {
            List<ComboBox> ListReagents = new List<ComboBox> { ComboBoxReagentOne, ComboBoxReagentTwo, ComboBoxReagentThree, ComboBoxReagentFour, ComboBoxReagentFive };
            List<ComboBox> ListReagentsTypes = new List<ComboBox> { ComboBoxReagentOneType, ComboBoxReagentTwoType, ComboBoxReagentThreeType, ComboBoxReagentFourType, ComboBoxReagentFiveType };
            List<ComboBox> ListReagentCounts = new List<ComboBox> { ComboBoxReagentOneCount, ComboBoxReagentTwoCount, ComboBoxReagentThreeCount, ComboBoxReagentFourCount, ComboBoxReagentFiveCount };
            List<TextBox> ListReagentsPrice = new List<TextBox> { TextBoxReagentOnePrice, TextBoxReagentTwoPrice, TextBoxReagentThreePrice, TextBoxReagentFourPrice, TextBoxReagentFivePrice };

            using (ApplicationContext db = new ApplicationContext())
            {
                Reagent reagent;
                ReagentType reagentType;

                for (var i = 0; i < 5; i++)
                {
                    if (ListReagents[i].SelectedIndex == -1 && !string.IsNullOrEmpty(ListReagents[i].Text))
                    {
                        reagentType = (ReagentType)ListReagentsTypes[i].SelectedItem;
                        // Добавить проверку, для иcключtния создания повторов с пробелами и т.д.
                        reagent = new Reagent
                        {
                            Name = ListReagents[i].Text,
                            ReagentTypeId = reagentType.ReagentTypeId,
                            PriceOrdinary = Convert.ToInt32(ListReagentsPrice[i].Text)
                        };
                        db.Reagents.Add(reagent);
                        db.SaveChanges();
                    }

                    else
                    {
                        reagentType = (ReagentType)ListReagentsTypes[i].SelectedItem;
                        reagent = (Reagent)ListReagents[i].SelectedItem;
                        if (reagentType != null && reagent != null)
                        {
                            reagent.ReagentTypeId = reagentType.ReagentTypeId;
                            db.Entry(reagent).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    // Добавляем в рецепт весь список
                    if (reagent != null)
                    {
                        int reagentCount = Convert.ToInt32(ListReagentCounts[i].Text);
                        int reagentPrice = Convert.ToInt32(ListReagentsPrice[i].Text);
                        switch (i)
                        {
                            case 0:
                                insertRecipe.OneId = reagent.ReagentId;
                                insertRecipe.OneCount = reagentCount;
                                reagent.PriceOrdinary = reagentPrice;
                                break;
                            case 1:
                                insertRecipe.TwoId = reagent.ReagentId;
                                insertRecipe.TwoCount = reagentCount;
                                reagent.PriceOrdinary = reagentPrice;
                                break;
                            case 2:
                                insertRecipe.ThreeId = reagent.ReagentId;
                                insertRecipe.ThreeCount = reagentCount;
                                reagent.PriceOrdinary = reagentPrice;
                                break;
                            case 3:
                                insertRecipe.FourId = reagent.ReagentId;
                                insertRecipe.FourCount = reagentCount;
                                reagent.PriceOrdinary = reagentPrice;
                                break;
                            case 4:
                                insertRecipe.FiveId = reagent.ReagentId;
                                insertRecipe.FiveCount = reagentCount;
                                reagent.PriceOrdinary = reagentPrice;
                                break;
                        }

                        db.Entry(reagent).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.Entry(insertRecipe).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void EditCurrentReagent()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                ReagentCollection.Recipes.Remove(insertRecipe);

                // Основные свойства реагента
                insertRecipe.Potion.Name = ComboBoxPotion.Text;

                var reagentType = (ReagentType)ComboBoxPotionType.SelectedItem;
                insertRecipe.Potion.ReagentTypeId = reagentType.ReagentTypeId;

                var craftType = (Craft)ComboBoxCraftType.SelectedItem;
                insertRecipe.Potion.CraftTypeId = craftType.CraftId;

                if (!string.IsNullOrEmpty(TextBoxPriceAuctionOrdinary.Text))
                    insertRecipe.Potion.PriceOrdinary = Convert.ToInt32(TextBoxPriceAuctionOrdinary.Text);
                else
                    insertRecipe.Potion.PriceOrdinary = 0;

                if (!string.IsNullOrEmpty(TextBoxPriceAuctionRare.Text))
                    insertRecipe.Potion.PriceRare = Convert.ToInt32(TextBoxPriceAuctionRare.Text);
                else
                    insertRecipe.Potion.PriceRare = 0;

                if (!string.IsNullOrEmpty(TextBoxPriceImperialCount.Text))
                    insertRecipe.Potion.ImperialCount = Convert.ToInt32(TextBoxPriceImperialCount.Text);
                else
                    insertRecipe.Potion.ImperialCount = 0;

                if (ComboBoxImperial.SelectedIndex == -1 && !string.IsNullOrEmpty(ComboBoxImperial.Text))
                {
                    var imperialType = (ImperialType)ComboBoxImperial.SelectedItem;
                    insertRecipe.Potion.ImperialTypeId = imperialType.ImperialTypeId;
                }

                // Создаем список реагентов на основании нового списка
                CreatyNewRecipe();

                ReagentCollection.Recipes.Add(insertRecipe);
                db.Entry(insertRecipe.Potion).State = EntityState.Modified;
                db.Entry(insertRecipe).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // Описать все необходимые проверки перед сохранением данного списка
        private bool CheckDataInsert()
        {
            var hasMistake = false;

            if (ComboBoxPotion.Text == string.Empty)
            {
                ComboBoxPotion.Background = Brushes.LightCoral;
                hasMistake = true;
            }

            if (ComboBoxPotionType.Text == string.Empty)
            {
                ComboBoxPotionType.Background = Brushes.LightCoral;
                hasMistake = true;
            }

            if (hasMistake) return false;
            else return true;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на ввод всех необходимых полей перед сохранением
            if (CheckDataInsert())
            {
                MessageBoxResult result = MessageBox.Show(
                    "Вы действительно хотите сохранять изменения?",
                    "Сохранение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Если количество крафтов не один, то будет сброс и деление всего рецепта
                    if (ComboBoxCountCraft.Text != "1")
                    {
                        ComboBoxCountCraft.Text = "1";
                        Calculate_Click(null, null);
                    }

                    if (insertRecipe == null) CreateNewReagent();
                    else EditCurrentReagent();
                    CalculatePrice.CalculateAllPrice();

                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите не сохранять изменения?",
                "Сохранение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.Yes) this.Close();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            int calculateCount = Convert.ToInt32(ComboBoxCountCraft.Text);
            using (ApplicationContext db = new ApplicationContext())
            {
                if (insertRecipe != null)
                {
                    if (insertRecipe.OneId != 0)
                    {
                        var newCount = insertRecipe.OneCount * calculateCount;
                        ComboBoxReagentOneCount.Text = newCount.ToString();
                    }

                    if (insertRecipe.TwoId != 0)
                    {
                        var newCount = insertRecipe.TwoCount * calculateCount;
                        ComboBoxReagentTwoCount.Text = newCount.ToString();
                    }

                    if (insertRecipe.ThreeId != 0)
                    {
                        var newCount = insertRecipe.ThreeCount * calculateCount;
                        ComboBoxReagentThreeCount.Text = newCount.ToString();
                    }

                    if (insertRecipe.FourId != 0)
                    {
                        var newCount = insertRecipe.FourCount * calculateCount;
                        ComboBoxReagentFourCount.Text = newCount.ToString();
                    }

                    if (insertRecipe.FiveId != 0)
                    {
                        var newCount = insertRecipe.FiveCount * calculateCount;
                        ComboBoxReagentFiveCount.Text = newCount.ToString();
                    }
                }
            }
        }

        // Таймер крафта, добавить визуальное отображение и звук после выполнения
        private void Timer_Click(object sender, RoutedEventArgs e)
        {

        }

        // При смене основного реагента необходимо менять его тип
        private void AllComboBoxChanged(int count)
        {
            List<ComboBox> ListReagents = new List<ComboBox> { ComboBoxPotion, ComboBoxReagentOne, ComboBoxReagentTwo, ComboBoxReagentThree, ComboBoxReagentFour, ComboBoxReagentFive };
            List<ComboBox> ListReagentsTypes = new List<ComboBox> { ComboBoxPotionType, ComboBoxReagentOneType, ComboBoxReagentTwoType, ComboBoxReagentThreeType, ComboBoxReagentFourType, ComboBoxReagentFiveType };
            List<TextBox> ListReagentsPrice = new List<TextBox> { TextBoxPriceAuctionOrdinary, TextBoxReagentOnePrice, TextBoxReagentTwoPrice, TextBoxReagentThreePrice, TextBoxReagentFourPrice, TextBoxReagentFivePrice };

            using (ApplicationContext db = new ApplicationContext())
            {
                var TypeList = db.ReagentTypes.OrderBy(t => t.Name).ToList();
                var currentReagent = (Reagent)ListReagents[count].SelectedItem;
                if (currentReagent != null)
                {
                    ListReagentsTypes[count].SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == currentReagent.ReagentTypeId);
                    ListReagentsPrice[count].Text = currentReagent.PriceOrdinary.ToString();

                    if (count == 0)
                    {
                        TextBoxPriceAuctionRare.Text = currentReagent.PriceRare.ToString();
                        TextBoxPriceImperialCount.Text = currentReagent.ImperialCount.ToString();

                        var ImperialTypeList = db.ImperialTypes.OrderBy(t => t.ImperialTypeId).ToList();
                        ComboBoxImperial.SelectedItem = ImperialTypeList.FindIndex(t => t.ImperialTypeId == currentReagent.ImperialTypeId);
                    }
                }
            }
        }

        private void ComboBoxPotion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(0);
        }

        private void ComboBoxReagentOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(1);
        }

        private void ComboBoxReagentTwo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(2);
        }

        private void ComboBoxReagentThree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(3);
        }
        private void ComboBoxReagentFour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(4);
        }

        private void ComboBoxReagentFive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(5);
        }
    }
}
