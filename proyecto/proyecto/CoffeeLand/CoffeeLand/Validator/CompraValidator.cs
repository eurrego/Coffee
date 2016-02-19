using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class CompraValidator : IDataErrorInfo
    {

        private string cantidad = string.Empty;

        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private string factura = string.Empty;

        public string Factura
        {
            get { return factura; }
            set { factura = value; }
        }

        private string valor = string.Empty;

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

       

        private DateTime fecha = DateTime.Now;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
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
                            else if (cantidad.Length > 10)
                            {
                                result = "La teléfono debe ser menor que 10 caracteres.";
                            }
                        }
                        break;
                    case "Factura":

                        if (!numeros.IsMatch(factura))
                        {
                            result = "El campo solo acepta números.";
                        }
                        else if (factura.Length > 10)
                        {
                            result = "La teléfono debe ser menor que 10 caracteres.";
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
                            else if (valor.Length > 10)
                            {
                                result = "La teléfono debe ser menor que 10 caracteres.";
                            }
                        }
                        break;
                    case "Fecha":
                        if (fecha == null)
                        {
                            result = "Debe seleccionar una fecha.";
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
