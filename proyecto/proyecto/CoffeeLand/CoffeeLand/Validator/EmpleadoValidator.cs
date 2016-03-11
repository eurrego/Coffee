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
                return IsValid(name);
            }
        }

        private string IsValid(string propertyName)
        {
            Regex numeros = new Regex("^[0-9]*$");
            Regex letras = new Regex("^[a-zA-Z ñáéíóú]*$");

            switch (propertyName)
            {
                case "Nombre":
                    if (string.IsNullOrEmpty(nombre))
                    {
                        return  "El campo es obligatorio.";
                    }
                    else
                    {
                        if (!letras.IsMatch(nombre))
                        {
                            return  "El campo solo acepta letras.";
                        }
                        else
                        {
                            if (nombre.Length > 50)
                            {
                                return  "El nombre debe ser menor que 50 caracteres.";
                            }
                        }
                    }
                    break;
                case "Telefono":
                    if (string.IsNullOrEmpty(telefono))
                    {
                        return  "El campo es obligatorio.";
                    }
                    else
                    {
                        if (!numeros.IsMatch(telefono))
                        {
                            return  "El campo solo acepta números.";
                        }
                        else if (telefono.Length > 10)
                        {
                            return  "La teléfono debe ser menor que 10 caracteres.";
                        }
                    }
                    break;
                case "Documento":
                    if (string.IsNullOrEmpty(documento))
                    {
                        return  "El campo es obligatorio.";
                    }
                    else
                    {
                        if (!numeros.IsMatch(documento))
                        {
                            return  "El campo solo acepta letras.";
                        }
                        else
                        {
                            if (documento.Length > 15)
                            {
                                return  "El documento debe ser menor que 15 caracteres.";
                            }
                        }
                    }
                    break;
            }
            return null;
        }

      
    }
}
