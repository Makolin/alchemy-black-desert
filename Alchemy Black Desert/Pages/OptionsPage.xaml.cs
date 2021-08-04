using Alchemy_Black_Desert.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Alchemy_Black_Desert.Pages
{
    public partial class OptionsPage : Page
    {
        public OptionsPage()
        {
            InitializeComponent();
            InsertDataToPage();
        }

        private void InsertDataToPage()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var auction = db.Options.Where(t => t.OptionId == 1).FirstOrDefault();
                var imperial = db.Options.Where(t => t.OptionId == 2).FirstOrDefault();

                TextBoxTax.Text = auction.Value.ToString();
                TextBoxPrize.Text = imperial.Value.ToString();
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
                    var auction = db.Options.Where(t => t.OptionId == 1).FirstOrDefault();
                    auction.Value = Convert.ToDecimal(TextBoxTax.Text);

                    auction = db.Options.Where(t => t.OptionId == 2).FirstOrDefault();
                    auction.Value = Convert.ToDecimal(TextBoxPrize.Text);

                    db.Entry(auction).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

        }
    }
}
