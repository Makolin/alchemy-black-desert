using System.Windows;
using Alchemy_Black_Desert.Model;

namespace Alchemy_Black_Desert.Pages
{
    public partial class MainTable : Window
    {
        public MainTable()
        {
            InitializeComponent();
            DataContext = new ReagentCollection();
        }
    }
}
