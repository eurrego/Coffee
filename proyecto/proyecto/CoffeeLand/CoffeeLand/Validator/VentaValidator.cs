using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class VentaValidator : IDataErrorInfo
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

        private string valorBeneficio = string.Empty;

        public string ValorBeneficio
        {
            get { return valorBeneficio; }
            set { valorBeneficio = value; }
        }

        private string valorCarga;

        public string ValorCarga
        {
            get { return valorCarga; }
            set { valorCarga = value; }
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
                Regex Cargas = new Regex("^[0-9 ,]*$");
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
                            if (!Cargas.IsMatch(cantidad))
                            {
                                result = "El campo solo acepta números y comas.";
                            }
                            else if (cantidad.Equals("0"))
                            {
                                result = "La cantidad no puede ser cero";
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
                    case "ValorBeneficio":
                        if (string.IsNullOrEmpty(valorBeneficio))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(valorBeneficio))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else
                            {
                                if (valorBeneficio.Equals("0"))
                                {
                                    result = "El valor no puede ser cero";
                                }
                                else
                                {
                                    if (valorBeneficio.Length > 7)
                                    {
                                        result = "El valor debe ser menor de ocho números ";
                                    }
                                    else
                                    {
                                        long val = Convert.ToInt64(valorBeneficio);

                                        if (val > 9999999)
                                        {
                                            result = "El valor del beneficio debe ser menor de 10.000.000 ";
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "ValorCarga":
                        if (string.IsNullOrEmpty(valorCarga))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(valorCarga))
                            {
                                result = "El campo solo acepta números.";
                            }
                            else
                            {
                                if (valorCarga.Equals("0"))
                                {
                                    result = "El valor no puede ser cero";
                                }
                                else
                                {
                                    if (valorCarga.Length > 7)
                                    {
                                        result = "El valor debe ser menor de ocho números ";
                                    }
                                    else
                                    {
                                        long val = Convert.ToInt64(valorCarga);

                                        if (val > 9999999)
                                        {
                                            result = "El valor del insumo debe ser menor de 10.000.000 ";
                                        }
                                    }
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
