using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class GastoValidator : IDataErrorInfo
    {

        private string descripcion = string.Empty;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
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

                    case "Descripcion":
                        if (string.IsNullOrEmpty(descripcion))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (descripcion.Length > 150)
                            {
                                result = "La descripción debe ser menor que 150 caracteres.";
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
                            else
                            {
                                if (valor.Equals("0"))
                                {
                                    result = "El valor no puede ser cero";
                                }
                                else if (valor.Length > 19)
                                {
                                    result = "El valor debe ser menor de 19 números";
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
