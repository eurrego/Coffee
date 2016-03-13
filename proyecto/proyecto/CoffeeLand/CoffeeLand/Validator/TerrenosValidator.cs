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
        private string cantidadInsumo = string.Empty;

        public string CantidadInsumo
        {
            get { return cantidadInsumo; }
            set { cantidadInsumo = value; }
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

        private string cantidadArbolesAModificar = string.Empty;

        public string CantidadArbolesAModificar
        {
            get { return cantidadArbolesAModificar; }
            set { cantidadArbolesAModificar = value; }
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

                    case "CantidadInsumo":
                        if (string.IsNullOrEmpty(cantidadInsumo))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(cantidadInsumo))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (cantidadInsumo.Equals("0"))
                            {
                                result = "La cantidad no puede ser cero";
                            }
                        }
                        break;
                    case "CantidadEmpleados":
                        if (string.IsNullOrEmpty(cantidadEmpleados))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(cantidadEmpleados))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (cantidadEmpleados.Equals("0"))
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

                    case "CantidadArbolesAModificar":
                        if (string.IsNullOrEmpty(cantidadArbolesAModificar))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(cantidadArbolesAModificar))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (cantidadArbolesAModificar.Equals("0"))
                            {
                                result = "La cantidad no puede ser cero";
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
