using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CookBookApp.ValueConverters
{
    public class PreparationTimeStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value = $"Ready in {value.ToString()} minutes";
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            value = Regex.Match(value.ToString(), @"\d+").Value;
            return value;
        }
    }
}
