using Alchemy_Black_Desert.Model;
using System.Windows.Controls;

namespace Alchemy_Black_Desert.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new ReagentCollection();
        }

        private void FindString_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void ButtonFind_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void RecipeGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var editRecipe = (Recipe)RecipeGrid.SelectedItem;
            ReagentWindow reagentWindow = new ReagentWindow(editRecipe);
            reagentWindow.ShowDialog();
        }
    }
}
