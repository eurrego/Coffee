using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class ArbolesValidator : IDataErrorInfo
    {
        private string cantidad = string.Empty;

        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string name]
        {
            get
            {
                string result = null;

                Regex numeros = new Regex("^[0-9]*$");
                Regex letras = new Regex("^[a-zA-Z ñáéíóú]*$");

                switch (name)
                {

                    case "Cantidad":

                        double cant = 0;

                        if (string.IsNullOrEmpty(cantidad))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {

                            if (Cantidad.Length > 10)
                            {
                                result = "El campo no acepta mas de 10 números";
                            }
                            else
                            {
                                if (!numeros.IsMatch(cantidad))
                                {
                                    result = "El campo solo acepta números.";
                                }
                                else
                                {
                                    cant = Convert.ToInt64(cantidad);

                                    if (cantidad.Equals("0"))
                                    {
                                        result = "La cantidad no puede ser cero";
                                    }
                                    else if (cant > 1900000000)
                                    {
                                        result = "La cantidad debe ser menor a 1900000000";
                                    }

                                }
                            } 
                        }
                        break;

                    default:
                        break;
                }

                return result;
            }
        }
    }
}
