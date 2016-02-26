using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Modelo;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmLogin.xaml
    /// </summary>
    public partial class frmLogin : MetroWindow
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {

            if (txtUsuario.Text != string.Empty && txtPassword.Password.ToString() != string.Empty)
            {
                IEnumerable<Usuario> usua = MUsuario.GetInstance().InciarSesion(txtUsuario.Text) as IEnumerable<Usuario>;

                foreach (var item in usua)
                {

                    if (item.Nickname != null)
                    {
                        if (item.Contrasena.Equals(Encriptar(txtPassword.Password.ToString())))
                        {
                            MUsuario.GetInstance().rol = item.Rol;
                            DialogResult = true;
                        }
                        else
                        {
                            mensajeError("Usuario y/o Contraseña incorrecta");
                        }
                    }
                    else
                    {
                        mensajeError("Usuario y/o Contraseña incorrecta");
                    }
                }
            }
            else
            {
                mensajeError("Debe ingresar todos los campos");
            }
        }

        public string Encriptar(string cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        // mensaje de Error
        public void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        public void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        private void btnRecuperarContraseña_Click(object sender, RoutedEventArgs e)
        {

            if (txtUsuario.Text != string.Empty)
            {
                IEnumerable<Usuario> usu = MUsuario.GetInstance().InciarSesion(txtUsuario.Text);

                foreach (var item in usu)
                {
                    if (item.Nickname != string.Empty)
                    {
                        this.Close();
                        //frmRecuperarContrasena MiRecuperar = new frmRecuperarContrasena(usu);
                        //MiRecuperar.ShowDialog();

                    }
                    else
                    {
                        mensajeError("Este usuario no se encuentra registrado");
                    }

                }

            }
            else
            {
                mensajeError("Debe ingresar el usuario");
            }
        }
    }
}
