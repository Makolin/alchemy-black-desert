using Alchemy_Black_Desert.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Alchemy_Black_Desert.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new ReagentCollection();
        }

        private void FindString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var findString = FindString.Text;
                DataContext = new ReagentCollection(findString);
            }
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            var findString = FindString.Text;
            DataContext = new ReagentCollection(findString);
        }

        // Редактирование и просмотр рецепта в базе данных
        private void RecipeGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editRecipe = (Recipe)RecipeGrid.SelectedItem;
            ReagentWindow reagentWindow = new ReagentWindow(editRecipe);
            reagentWindow.ShowDialog();
        }

        // Создание рецепта в базе данных
        private void NewRecipe_Click(object sender, RoutedEventArgs e)
        {
            ReagentWindow reagentWindow = new ReagentWindow(null);
            reagentWindow.ShowDialog();
        }

        // Удаление выбранного рецепта из базы данных
        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            var deleteRecipe = (Recipe)RecipeGrid.SelectedItem;
            if (deleteRecipe != null)
            {
                MessageBoxResult result = MessageBox.Show(
                     $"Вы действительно хотите удалить рецепт с именем \"{deleteRecipe.Potion.Name}\" из базы данных?",
                     "Удалить",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        ReagentCollection.Recipes.Remove(deleteRecipe);
                        db.Recipes.Remove(deleteRecipe);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
