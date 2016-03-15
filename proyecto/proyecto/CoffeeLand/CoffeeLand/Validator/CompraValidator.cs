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
                Regex letrasNumeros = new Regex("^[a-zA-Z ñáéíóú 0-9]*$");

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
                            else
                            {
                                if (cantidad.Equals("0"))
                                {
                                    result = "La cantidad no puede ser cero";
                                }
                                else
                                {
                                    if (cantidad.Length > 4)
                                    {
                                        result = "La cantidad debe ser menor de cinco números ";
                                    }
                                    else
                                    {
                                        long cant = Convert.ToInt64(cantidad);

                                        if (cant > 9999)
                                        {
                                            result = "La cantidad debe ser menor de 9999 unidades ";
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "Factura":
                        if (string.IsNullOrEmpty(factura))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!letrasNumeros.IsMatch(factura))
                            {
                                result = "El campo solo acepta números y letras";
                            }
                            else
                            {
                                if (!numeros.IsMatch(factura) && letras.IsMatch(factura))
                                {
                                    result = "La factura debe tener al menos un digito númerico";
                                }
                                else
                                {
                                    if (factura.Length > 20)
                                    {
                                        result = "La factura debe ser menor que 21 caracteres.";
                                    }
                                }
                                   
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
                                else
                                {
                                    if (valor.Length > 7)
                                    {
                                        result = "El valor debe ser menor de ocho números ";
                                    }
                                    else
                                    {
                                        long val = Convert.ToInt64(valor);

                                        if (val > 9999999)
                                        {
                                            result = "El valor del insumo debe ser menor de 10.000.000 ";
                                        }
                                    }
                                }
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
