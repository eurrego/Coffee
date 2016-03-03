using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Format
{

    public class Converter
    {
        #region Singleton

        private static Converter instance;

        public static Converter GetInstance()
        {
            if (instance == null)
            {
                instance = new Converter();
            }

            return instance;
        }

        #endregion

        public string StringToCapitalsConverter(string value)
        {
            if (value != null)
            {
                var tolower = value.ToLower();
                return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
            }

            return value;
        }

        public string StringFirtsLetterToUpper(string value)
        {
            if (value != null)
            {
                var tolower = value.ToLower();
                return tolower.First().ToString().ToUpper() + tolower.Substring(1);
            }

            return value;
        }

    }
}
