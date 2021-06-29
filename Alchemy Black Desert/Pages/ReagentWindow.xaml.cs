using Alchemy_Black_Desert.Model;
using System.Linq;
using System.Windows;

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

        private void InsertDataInTextBox(Recipe recipe)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var currentReagent = db.Reagents.Where(t => t.ReagentId == recipe.PotionId).FirstOrDefault();
                TextBoxPotion.Text = currentReagent.Name;
                TextBoxPriceAuctionOrdinary.Text = currentReagent.PriceOrdinary.ToString();
                TextBoxPriceAuctionRare.Text = currentReagent.PriceRare.ToString();
                TextBoxPriceImperialOrdinary.Text = currentReagent.ImperialPriceOrdinary.ToString();
                TextBoxPriceImperialRare.Text = currentReagent.ImperialPriceRare.ToString();

                var ReagentList = db.Reagents.OrderBy(t => t.Name).ToList();
                ComboBoxReagentOne.ItemsSource = ReagentList;
                ComboBoxReagentTwo.ItemsSource = ReagentList;
                ComboBoxReagentThree.ItemsSource = ReagentList;
                ComboBoxReagentFour.ItemsSource = ReagentList;
                ComboBoxReagentFive.ItemsSource = ReagentList;

                ComboBoxReagentOne.DisplayMemberPath = "Name";
                ComboBoxReagentTwo.DisplayMemberPath = "Name";
                ComboBoxReagentThree.DisplayMemberPath = "Name";
                ComboBoxReagentFour.DisplayMemberPath = "Name";
                ComboBoxReagentFive.DisplayMemberPath = "Name";

                if (recipe.OneId != 0)
                {
                    var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.OneId).FirstOrDefault();
                    ComboBoxReagentOne.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.OneId);
                    TextBoxReagentOneCount.Text = recipe.OneCount.ToString();
                    TextBoxReagentOnePrice.Text = Reagent.PriceOrdinary.ToString();
                }
                if (recipe.TwoId != 0)
                {
                    var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.TwoId).FirstOrDefault();
                    ComboBoxReagentTwo.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.TwoId);
                    TextBoxReagentTwoCount.Text = recipe.TwoCount.ToString();
                    TextBoxReagentTwoPrice.Text = Reagent.PriceOrdinary.ToString();
                }
                if (recipe.ThreeId != 0)
                {
                    var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.ThreeId).FirstOrDefault();
                    ComboBoxReagentThree.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.ThreeId);
                    TextBoxReagentThreeCount.Text = recipe.ThreeCount.ToString();
                    TextBoxReagentThreePrice.Text = Reagent.PriceOrdinary.ToString();
                }
                if (recipe.FourId != 0)
                {
                    var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.FourId).FirstOrDefault();
                    ComboBoxReagentFour.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.FourId);
                    TextBoxReagentFourCount.Text = recipe.FourCount.ToString();
                    TextBoxReagentFourPrice.Text = Reagent.PriceOrdinary.ToString();
                }
                if (recipe.FiveId != 0)
                {
                    var Reagent = db.Reagents.Where(t => t.ReagentId == recipe.FiveId).FirstOrDefault();
                    ComboBoxReagentFive.SelectedIndex = ReagentList.FindIndex(t => t.ReagentId == recipe.FiveId);
                    TextBoxReagentFiveCount.Text = recipe.FiveCount.ToString();
                    TextBoxReagentFivePrice.Text = Reagent.PriceOrdinary.ToString();
                }
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
