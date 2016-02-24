using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class TerrenosValidator : IDataErrorInfo
    {
        private string cantidad = string.Empty;

        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private string cantidadEmpleados = string.Empty;

        public string CantidadEmpleados
        {
            get { return cantidadEmpleados; }
            set { cantidadEmpleados = value; }
        }

        private string valor = string.Empty;

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
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
                        if (string.IsNullOrEmpty(cantidad))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(cantidad))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (cantidad.Equals("0"))
                            {
                                result = "La cantidad no puede ser cero";
                            }
                        }
                        break;
                    case "CantidadEmpleados":
                        if (string.IsNullOrEmpty(cantidad))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(cantidad))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (cantidad.Equals("0"))
                            {
                                result = "La cantidad no puede ser cero";
                            }
                        }
                        break;
                    case "Valor":
                        if (string.IsNullOrEmpty(valor))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(valor))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (valor.Equals("0"))
                            {
                                result = "El valor no puede ser cero";
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
