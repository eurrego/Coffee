using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class PrestamosValidator : IDataErrorInfo
    {

        private string valor = string.Empty;

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        private string valorAbono = string.Empty;

        public string ValorAbono
        {
            get { return valorAbono; }
            set { valorAbono = value; }
        }

        private string descripcion = string.Empty;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
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
                            if (descripcion.Length > 45)
                            {
                                result = "La descripción debe ser menor que 45 caracteres.";
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
                    case "ValorAbono":
                        if (string.IsNullOrEmpty(valor))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(valorAbono))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (valorAbono.Equals("0"))
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
