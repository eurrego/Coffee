using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class EmpleadoValidator : IDataErrorInfo
    {
        private string nombre = string.Empty;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string telefono = string.Empty;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        private string documento = string.Empty;

        public string Documento
        {
            get { return documento; }
            set { documento = value; }
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
                    case "Nombre":
                        if (string.IsNullOrEmpty(nombre))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!letras.IsMatch(nombre))
                            {
                                result = "El campo solo acepta letras.";
                            }
                            else
                            {
                                if (nombre.Length > 45)
                                {
                                    result = "El nombre debe ser menor que 45 caracteres.";
                                }
                            }
                        }
                        break;

                    case "Telefono":
                        if (string.IsNullOrEmpty(telefono))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(telefono))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (telefono.Length > 10)
                            {
                                result = "La teléfono debe ser menor que 10 caracteres.";
                            }
                        }
                        break;
                    case "Documento":
                        if (string.IsNullOrEmpty(documento))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(documento))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (documento.Length > 20)
                            {
                                result = "El documento debe ser menor que 20 caracteres.";
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
