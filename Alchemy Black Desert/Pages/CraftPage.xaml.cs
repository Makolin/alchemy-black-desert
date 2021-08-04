using Alchemy_Black_Desert.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Alchemy_Black_Desert.Pages
{
    public partial class CraftPage : Page
    {
        public CraftPage()
        {
            InitializeComponent();
            InsertDataToPage();
        }

        private void InsertDataToPage()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var craft = db.Crafts.Where(t => t.CraftId == 1).FirstOrDefault();
                TextBoxCountCraftTypeOne.Text = craft.CountCraft.ToString();
                TextBoxCountOrdinaryTypeOne.Text = craft.CountOrdinary.ToString();

                craft = db.Crafts.Where(t => t.CraftId == 2).FirstOrDefault();
                TextBoxCountCraftTypeTwo.Text = craft.CountCraft.ToString();
                TextBoxCountOrdinaryTypeTwo.Text = craft.CountOrdinary.ToString();
                TextBoxCountRareTypeTwo.Text = craft.CountRare.ToString();
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите сохранять изменения?",
                "Сохранение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var craft = db.Crafts.Where(t => t.CraftId == 1).FirstOrDefault();
                    craft.CountCraft = Convert.ToInt32(TextBoxCountCraftTypeOne.Text);
                    craft.CountOrdinary = Convert.ToInt32(TextBoxCountOrdinaryTypeOne.Text);

                    craft = db.Crafts.Where(t => t.CraftId == 2).FirstOrDefault();
                    craft.CountCraft = Convert.ToInt32(TextBoxCountCraftTypeTwo.Text);
                    craft.CountOrdinary = Convert.ToInt32(TextBoxCountOrdinaryTypeTwo.Text);
                    craft.CountRare = Convert.ToInt32(TextBoxCountRareTypeTwo.Text);
                    db.Entry(craft).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Данные сохранены в базу данных");
                }
            }
        }
    }
}
