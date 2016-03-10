﻿using System;
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

                        int cant = 0;

                        if (cantidad != string.Empty)
                        {
                            cant = Convert.ToInt32(cantidad);
                        }

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
                            else
                            {
                                if (cantidad.Equals("0"))
                                {
                                    result = "La cantidad no puede ser cero";
                                }
                                else if (cant > 2000000000)
                                {
                                    result = "La cantidad debe ser menor a 2000000000";
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
