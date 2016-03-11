using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeLand.Validator
{
    public class UsuarioValidator : IDataErrorInfo
    {
        private string usuario = string.Empty;

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private string contrasena = string.Empty;

        public string Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }

        private string confirmarContrasena = string.Empty;

        public string ConfirmarContrasena
        {
            get { return confirmarContrasena; }
            set { confirmarContrasena = value; }
        }

        private string respuesta = string.Empty;

        public string Respuesta
        {
            get { return respuesta; }
            set { respuesta = value; }
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
                    case "Usuario":
                        if (string.IsNullOrEmpty(usuario))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else if (usuario.Length > 15)
                        {

                            result = "El nombre de usuario debe ser menor que 15 caracteres.";

                        }
                        break;

                    case "Contrasena":
                        if (string.IsNullOrEmpty(contrasena))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (contrasena.Length > 40)
                            {
                                result = "La contraseña debe ser menor que 40 caracteres.";
                            }
                        }
                        break;
                    case "ConfirmarContrasena":
                        if (string.IsNullOrEmpty(confirmarContrasena))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (confirmarContrasena != contrasena)
                            {
                                result = "La contraseñas no coinciden";
                            }
                            else if (confirmarContrasena.Length > 40)
                            {
                                result = "La contraseña debe ser menor que 40 caracteres.";
                            }
                        }
                        break;
                    case "Respuesta":
                        if (string.IsNullOrEmpty(respuesta))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (respuesta.Length > 30)
                            {
                                result = "La respuesta debe ser menor que 30 caracteres.";
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
