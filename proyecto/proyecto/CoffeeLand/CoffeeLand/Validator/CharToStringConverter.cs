using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoffeeLand.Validator
{
    public class CharToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string cadena = string.Empty;

            if (value != null)
            {

                if (value.Equals("D"))
                {
                    cadena = "Debe";
                }
                else if (value.Equals("P"))
                {
                    cadena = "Pagado";
                }
            }

            return cadena;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
