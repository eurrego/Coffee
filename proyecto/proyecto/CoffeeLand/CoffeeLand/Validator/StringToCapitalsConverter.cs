using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace CoffeeLand.Validator
{
    public class StringToCapitalsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var text = ((string)value);
                var tolower = text.ToLower();
                if (tolower == "recoleccion")
                {

                    var recoleccion = ("recolección");
                    return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(recoleccion);

                }
                else
                {
                    return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                }
                
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
