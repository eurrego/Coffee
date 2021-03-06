﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class TipoInsumoValidator : IDataErrorInfo
    {
        private string nombre = string.Empty;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
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

                    default:
                        break;
                }

                return result;
            }
        }
    }
}
