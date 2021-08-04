using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Alchemy_Black_Desert.Converts
{
    public class DisplayAuction : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parametr, CultureInfo culture)
        {
            var displayCost = string.Empty;
            if (value != null)
            {
                int recipeCost = (int)value;
                culture = new CultureInfo("ru-RU");
                displayCost = string.Format(recipeCost.ToString("#,#", culture));
            }
            return displayCost;
        }
        public object ConvertBack(object value, Type TargetType, object parametr, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
