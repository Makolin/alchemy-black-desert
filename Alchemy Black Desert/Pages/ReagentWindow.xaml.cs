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

                // List<int> CountRecipe = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                List<TextBox> ComboBoxCount = new List<TextBox> { TextBoxReagentOneCount, TextBoxReagentTwoCount, TextBoxReagentThreeCount, TextBoxReagentFourCount, TextBoxReagentFiveCount };

                /*foreach (var comboBox in ComboBoxCount)
                {
                    comboBox.ItemsSource = CountRecipe;
                }*/

                if (recipe != null)
                {
                    var currentReagent = db.Reagents.Where(t => t.ReagentId == recipe.PotionId).FirstOrDefault();
                    TextBoxPriceAuctionOrdinary.Text = currentReagent.PriceOrdinary.ToString();
                    TextBoxPriceAuctionRare.Text = currentReagent.PriceRare.ToString();
                    TextBoxPriceImperialOrdinary.Text = currentReagent.ImperialPriceOrdinary.ToString();
                    TextBoxPriceImperialRare.Text = currentReagent.ImperialPriceRare.ToString();

                    ComboBoxPotion.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == currentReagent.ReagentId);
                    ComboBoxPotionType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == currentReagent.ReagentTypeId);

                    if (recipe.OneId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.OneId).FirstOrDefault();
                        ComboBoxReagentOne.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.OneId);
                        TextBoxReagentOneCount.Text = recipe.OneCount.ToString();
                        TextBoxReagentOnePrice.Text = Reagent.PriceOrdinary.ToString();
                        ComboBoxReagentOneType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == Reagent.ReagentTypeId);
                    }

                    if (recipe.TwoId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.TwoId).FirstOrDefault();
                        ComboBoxReagentTwo.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.TwoId);
                        TextBoxReagentTwoCount.Text = recipe.TwoCount.ToString();
                        TextBoxReagentTwoPrice.Text = Reagent.PriceOrdinary.ToString();
                        ComboBoxReagentTwoType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == Reagent.ReagentTypeId);
                    }

                    if (recipe.ThreeId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.ThreeId).FirstOrDefault();
                        ComboBoxReagentThree.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.ThreeId);
                        TextBoxReagentThreeCount.Text = recipe.ThreeCount.ToString();
                        TextBoxReagentThreePrice.Text = Reagent.PriceOrdinary.ToString();
                        ComboBoxReagentThreeType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == Reagent.ReagentTypeId);
                    }

                    if (recipe.FourId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.FourId).FirstOrDefault();
                        ComboBoxReagentFour.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.FourId);
                        TextBoxReagentFourCount.Text = recipe.FourCount.ToString();
                        TextBoxReagentFourPrice.Text = Reagent.PriceOrdinary.ToString();
                        ComboBoxReagentFourType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == Reagent.ReagentTypeId);
                    }

                    if (recipe.FiveId != 0)
                    {
                        var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.FiveId).FirstOrDefault();
                        ComboBoxReagentFive.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.FiveId);
                        TextBoxReagentFiveCount.Text = recipe.FiveCount.ToString();
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
                var reagent = new Reagent
                {
                    Name = ComboBoxPotion.Text,
                    ReagentTypeId = reagentType.ReagentTypeId,
                    PriceOrdinary = Convert.ToInt32(TextBoxPriceAuctionOrdinary.Text)
                };

                if (TextBoxPriceAuctionRare.Text != string.Empty)
                    reagent.PriceRare = Convert.ToInt32(TextBoxPriceAuctionRare.Text);

                if (TextBoxPriceImperialOrdinary.Text != string.Empty)
                    reagent.ImperialPriceOrdinary = Convert.ToInt32(TextBoxPriceImperialOrdinary.Text);

                if (TextBoxPriceImperialRare.Text != string.Empty)
                    reagent.ImperialPriceRare = Convert.ToInt32(TextBoxPriceImperialRare.Text);

                db.Reagents.Add(reagent);
                insertRecipe = new Recipe();
                insertRecipe.Potion = reagent;

                // Создаем список реагентов на основании нового списка
                CreatyNewRegent();

                ReagentCollection.Recipes.Add(insertRecipe);
                db.Entry(insertRecipe).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        private void CreatyNewRegent()
        {
            List<ComboBox> ListReagents = new List<ComboBox> { ComboBoxReagentOne, ComboBoxReagentTwo, ComboBoxReagentThree, ComboBoxReagentFour, ComboBoxReagentFive };
            List<ComboBox> ListReagentsTypes = new List<ComboBox> { ComboBoxReagentOneType, ComboBoxReagentTwoType, ComboBoxReagentThreeType, ComboBoxReagentFourType, ComboBoxReagentFiveType };
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
                        reagent = (Reagent)ListReagents[i].SelectedItem;
                    }

                    // Добавляем в рецепт весь список
                    if (reagent != null)
                    {
                        switch (i)
                        {
                            case 0:
                                insertRecipe.OneId = reagent.ReagentId;
                                break;
                            case 1:
                                insertRecipe.TwoId = reagent.ReagentId;
                                break;
                            case 2:
                                insertRecipe.ThreeId = reagent.ReagentId;
                                break;
                            case 3:
                                insertRecipe.FourId = reagent.ReagentId;
                                break;
                            case 4:
                                insertRecipe.FiveId = reagent.ReagentId;
                                break;
                        }
                    }
                }

                // Заполняем количество в текущем рецепте
                if (!string.IsNullOrEmpty(TextBoxReagentOneCount.Text))
                    insertRecipe.OneCount = Convert.ToInt32(TextBoxReagentOneCount.Text);

                if (!string.IsNullOrEmpty(TextBoxReagentTwoCount.Text))
                    insertRecipe.TwoCount = Convert.ToInt32(TextBoxReagentTwoCount.Text);

                if (!string.IsNullOrEmpty(TextBoxReagentThreeCount.Text))
                    insertRecipe.ThreeCount = Convert.ToInt32(TextBoxReagentThreeCount.Text);

                if (!string.IsNullOrEmpty(TextBoxReagentFourCount.Text))
                    insertRecipe.FourCount = Convert.ToInt32(TextBoxReagentFourCount.Text);

                if (!string.IsNullOrEmpty(TextBoxReagentFiveCount.Text))
                    insertRecipe.FiveCount = Convert.ToInt32(TextBoxReagentFiveCount.Text);

                // Заполняем стоимость, если менялась
                if (!string.IsNullOrEmpty(TextBoxReagentOnePrice.Text))
                {
                    reagent = db.Reagents.Where(t => t.ReagentId == insertRecipe.OneId).FirstOrDefault();
                    if (reagent != null)
                    {
                        reagent.PriceOrdinary = Convert.ToInt32(TextBoxReagentOnePrice.Text);
                        db.Entry(reagent).State = EntityState.Modified;
                    }
                }

                if (!string.IsNullOrEmpty(TextBoxReagentTwoPrice.Text))
                {
                    reagent = db.Reagents.Where(t => t.ReagentId == insertRecipe.TwoId).FirstOrDefault();
                    if (reagent != null)
                    {
                        reagent.PriceOrdinary = Convert.ToInt32(TextBoxReagentTwoPrice.Text);
                        db.Entry(reagent).State = EntityState.Modified;
                    }
                }

                if (!string.IsNullOrEmpty(TextBoxReagentThreePrice.Text))
                {
                    reagent = db.Reagents.Where(t => t.ReagentId == insertRecipe.ThreeId).FirstOrDefault();
                    if (reagent != null)
                    {
                        reagent.PriceOrdinary = Convert.ToInt32(TextBoxReagentThreePrice.Text);
                        db.Entry(reagent).State = EntityState.Modified;
                    }
                }

                if (!string.IsNullOrEmpty(TextBoxReagentFourPrice.Text))
                {
                    reagent = db.Reagents.Where(t => t.ReagentId == insertRecipe.FourId).FirstOrDefault();
                    if (reagent != null)
                    {
                        reagent.PriceOrdinary = Convert.ToInt32(TextBoxReagentFourPrice.Text);
                        db.Entry(reagent).State = EntityState.Modified;
                    }
                }

                if (!string.IsNullOrEmpty(TextBoxReagentFivePrice.Text))
                {
                    reagent = db.Reagents.Where(t => t.ReagentId == insertRecipe.FiveId).FirstOrDefault();
                    if (reagent != null)
                    {
                        reagent.PriceOrdinary = Convert.ToInt32(TextBoxReagentFivePrice.Text);
                        db.Entry(reagent).State = EntityState.Modified;
                    }
                }
            }
        }

        private void EditCurrentReagent()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                ReagentCollection.Recipes.Remove(insertRecipe);

                // Основные свойства реагента
                insertRecipe.Potion.Name = ComboBoxPotion.Text;

                if (!string.IsNullOrEmpty(TextBoxPriceAuctionOrdinary.Text))
                    insertRecipe.Potion.PriceOrdinary = Convert.ToInt32(TextBoxPriceAuctionOrdinary.Text);
                else
                    insertRecipe.Potion.PriceOrdinary = 0;

                if (!string.IsNullOrEmpty(TextBoxPriceAuctionRare.Text))
                    insertRecipe.Potion.PriceRare = Convert.ToInt32(TextBoxPriceAuctionRare.Text);
                else
                    insertRecipe.Potion.PriceRare = 0;

                if (!string.IsNullOrEmpty(TextBoxPriceImperialOrdinary.Text))
                    insertRecipe.Potion.ImperialPriceOrdinary = Convert.ToInt32(TextBoxPriceImperialOrdinary.Text);
                else
                    insertRecipe.Potion.ImperialPriceOrdinary = 0;

                if (!string.IsNullOrEmpty(TextBoxPriceImperialRare.Text))
                    insertRecipe.Potion.ImperialPriceRare = Convert.ToInt32(TextBoxPriceImperialRare.Text);
                else
                    insertRecipe.Potion.ImperialPriceRare = 0;

                // Создаем список реагентов на основании нового списка
                CreatyNewRegent();

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
                    ComboBoxCountCraft.Text = "1";
                    Calculate_Click(null, null);

                    if (insertRecipe == null) CreateNewReagent();
                    else EditCurrentReagent();
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
                        TextBoxReagentOneCount.Text = newCount.ToString();
                    }

                    if (insertRecipe.TwoId != 0)
                    {
                        var newCount = insertRecipe.TwoCount * calculateCount;
                        TextBoxReagentTwoCount.Text = newCount.ToString();
                    }

                    if (insertRecipe.ThreeId != 0)
                    {
                        var newCount = insertRecipe.ThreeCount * calculateCount;
                        TextBoxReagentThreeCount.Text = newCount.ToString();
                    }

                    if (insertRecipe.FourId != 0)
                    {
                        var newCount = insertRecipe.FourCount * calculateCount;
                        TextBoxReagentFourCount.Text = newCount.ToString();
                    }

                    if (insertRecipe.FiveId != 0)
                    {
                        var newCount = insertRecipe.FiveCount * calculateCount;
                        TextBoxReagentFiveCount.Text = newCount.ToString();
                    }
                }
            }
        }
        private void Timer_Click(object sender, RoutedEventArgs e)
        {

        }

        // При смене основного реагента необходимо менять его тип

        private void AllComboBoxChanged(int count)
        {
            List<ComboBox> ListReagents = new List<ComboBox> { ComboBoxReagentOne, ComboBoxReagentTwo, ComboBoxReagentThree, ComboBoxReagentFour, ComboBoxReagentFive };
            List<ComboBox> ListReagentsTypes = new List<ComboBox> { ComboBoxReagentOneType, ComboBoxReagentTwoType, ComboBoxReagentThreeType, ComboBoxReagentFourType, ComboBoxReagentFiveType };
            List<TextBox> ListReagentsPrice = new List<TextBox> { TextBoxReagentOnePrice, TextBoxReagentTwoPrice, TextBoxReagentThreePrice, TextBoxReagentFourPrice, TextBoxReagentFivePrice };

            using (ApplicationContext db = new ApplicationContext())
            {
                var TypeList = db.ReagentTypes.OrderBy(t => t.Name).ToList();
                var currentReagent = (Reagent)ListReagents[count].SelectedItem;
                if (currentReagent != null)
                {
                    ListReagentsTypes[count].SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == currentReagent.ReagentTypeId);
                    ListReagentsPrice[count].Text = currentReagent.PriceOrdinary.ToString();
                }
            }
        }

        private void ComboBoxPotion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;

            using (ApplicationContext db = new ApplicationContext())
            {
                var TypeList = db.ReagentTypes.OrderBy(t => t.Name).ToList();
                var currentReagent = (Reagent)ComboBoxPotion.SelectedItem;
                if (currentReagent != null)
                {
                    ComboBoxPotionType.SelectedIndex = TypeList.FindIndex(t => t.ReagentTypeId == currentReagent.ReagentTypeId);
                    TextBoxPriceAuctionOrdinary.Text = currentReagent.PriceOrdinary.ToString();
                    TextBoxPriceAuctionRare.Text = currentReagent.PriceRare.ToString();
                    TextBoxPriceImperialOrdinary.Text = currentReagent.ImperialPriceOrdinary.ToString();
                    TextBoxPriceImperialRare.Text = currentReagent.ImperialPriceRare.ToString();
                }
            }
        }

        private void ComboBoxReagentOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(0);
        }

        private void ComboBoxReagentTwo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(1);
        }

        private void ComboBoxReagentThree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(2);
        }
        private void ComboBoxReagentFour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(3);
        }

        private void ComboBoxReagentFive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            else AllComboBoxChanged(4);
        }
    }
}
