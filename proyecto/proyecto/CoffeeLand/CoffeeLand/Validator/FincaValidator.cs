using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class FincaValidator : IDataErrorInfo
    {
        private string nombre = string.Empty;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string propietario = string.Empty;

        public string Propietario
        {
            get { return propietario; }
            set { propietario = value; }
        }

        private string vereda = string.Empty;

        public string Vereda
        {
            get { return vereda; }
            set { vereda = value; }
        }

        private string telefono = string.Empty;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        private string cuadras = string.Empty;

        public string Cuadras
        {
            get { return cuadras; }
            set { cuadras = value; }
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
                    case "Propietario":
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
                                    result = "El propietario debe ser menor que 45 caracteres.";
                                }
                            }
                        }
                        break;
                    case "Vereda":
                        if (string.IsNullOrEmpty(vereda))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!letras.IsMatch(vereda))
                            {
                                result = "El campo solo acepta letras.";
                            }
                            else
                            {
                                if (vereda.Length > 45)
                                {
                                    result = "La vereda debe ser menor que 45 caracteres.";
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
                    case "Cuadras":
                        if (string.IsNullOrEmpty(cuadras))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(cuadras))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else if (cuadras.Length > 20)
                            {
                                result = "El cuadras debe ser menor que 20 caracteres.";
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
