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
using System.Windows.Media.Animation;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmLogin.xaml
    /// </summary>
    public partial class frmLogin : MetroWindow
    {

        private IEnumerable<Usuario> usuario { get; set; }

        public frmLogin()
        {
            InitializeComponent();
            mostrar();
        }

        private void mostrar()
        {
            List<string> datosPreguntaSeguridad = new List<string>();
            datosPreguntaSeguridad.Add("Seleccione una pregunta de seguridad");
            datosPreguntaSeguridad.Add("¿Cuál es la ciudad de nacimiento de su mamá?");
            datosPreguntaSeguridad.Add("¿Cuál es la fecha de expedición de la cédula?");
            datosPreguntaSeguridad.Add("¿Cuál es el nombre de su primer mascota?");
            datosPreguntaSeguridad.Add("¿Cuál es la marca de su primer auto?");
            datosPreguntaSeguridad.Add("¿Cuál es el nombre de su primer jefe?");
            cmbPreguntaSeguridad.ItemsSource = datosPreguntaSeguridad;
        }

        public void ShowStatus(double duration = 2.5)
        {
            BooleanAnimationUsingKeyFrames statusAnimation = new BooleanAnimationUsingKeyFrames();
            statusAnimation.KeyFrames.Add(new DiscreteBooleanKeyFrame() { KeyTime = TimeSpan.FromSeconds(0), Value = true });
            statusAnimation.KeyFrames.Add(new DiscreteBooleanKeyFrame() { KeyTime = TimeSpan.FromSeconds(duration + 0.5), Value = false }); //That 0.5 is for the Show animation
            message.BeginAnimation(Flyout.IsOpenProperty, statusAnimation);
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {

            if (txtUsuario.Text != string.Empty && txtPassword.Password.ToString() != string.Empty)
            {
                IEnumerable<Usuario> usua = MUsuario.GetInstance().InciarSesion(txtUsuario.Text) as IEnumerable<Usuario>;


                if (usua.Count() != 0)
                {
                    foreach (var item in usua)
                    {

                        if (item.Nickname != null)
                        {
                            if (item.Contrasena.Equals(Encriptar(txtPassword.Password.ToString())))
                            {
                                MUsuario.GetInstance().rol = item.Rol;
                                MainWindow.GetInstance().lblUser.Text = item.Nickname;
                                MainWindow.GetInstance().lblIdUser.Text = item.idUsuario.ToString();

                                DialogResult = true;
                            }
                            else
                            {
                                lblTitulo.Text = "Error";
                                lblMensaje.Text = "Usuario y/o Contraseña incorrecta";
                                lblMensaje.FontSize = 18;
                                ShowStatus();
                            }
                        }

                    }
                }
                else
                {
                    lblTitulo.Text = "Error";
                    lblMensaje.Text = "Usuario y/o Contraseña incorrecta";
                    lblMensaje.FontSize = 18;
                    ShowStatus();
                }
            }
            else
            {
                lblTitulo.Text = "Error";
                lblMensaje.Text = "Debe ingresar todos los campos";
                lblMensaje.FontSize = 18;
                ShowStatus();
            }
        }

        public string Encriptar(string cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.Unicode.GetBytes(cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        private void btnRecuperarContraseña_Click(object sender, RoutedEventArgs e)
        {

            if (txtUsuario.Text != string.Empty)
            {
                usuario = MUsuario.GetInstance().InciarSesion(txtUsuario.Text);


                if (usuario.Count() != 0)
                {
                    foreach (var item in usuario)
                    {
                        if (item.Nickname != string.Empty)
                        {
                            containerRecuperar.Visibility = Visibility.Collapsed;
                            pnlRecuperar.Visibility = Visibility.Visible;
                        }

                    }
                }
                else
                {
                    lblTitulo.Text = "Error";
                    lblMensaje.Text = "Usuario no registrado";
                    lblMensaje.FontSize = 18;
                    ShowStatus();
                }
            }
            else
            {
                lblTitulo.Text = "Error";
                lblMensaje.Text = "Debe ingresar el usuario";
                lblMensaje.FontSize = 18;
                ShowStatus();
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Close();
        }

        private void btnRecuperar_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in usuario)
            {
                if (item.PreguntaSeguridad.Equals(cmbPreguntaSeguridad.SelectedItem.ToString()))
                {
                    if (item.Respuesta.Equals(txtRepuestaSeguridad.Text))
                    {
                        pnlValidate.Visibility = Visibility.Collapsed;
                        pnlChangePassword.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lblTitulo.Text = "Error";
                        lblMensaje.Text = "Pregunta y/o Respuesta de seguridad incorrectas";
                        lblMensaje.FontSize = 15;
                        ShowStatus();
                    }
                }
                else
                {
                    lblTitulo.Text = "Error";
                    lblMensaje.Text = "Pregunta y/o Respuesta de seguridad incorrectas";
                    lblMensaje.FontSize = 15;
                    ShowStatus();
                }

            }
        }

        private void pnlPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard sb = Resources["ShowPnlLogin"] as Storyboard;
            sb.Begin(pnlLogin);
        }

        private void btnCambiar_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in usuario)
            {
                if (txtContrasena.Password.ToString().Equals(txtConfirmarContrasena.Password.ToString()))
                {
                    lblTitulo.Text = "Información";
                    lblMensaje.Text = MUsuario.GetInstance().GestionUsuario(item.idUsuario, string.Empty, string.Empty, Encriptar(txtContrasena.Password.ToString()), string.Empty, string.Empty, 5);
                    lblMensaje.FontSize = 15;
                    ShowStatus();

                    pnlRecuperar.Visibility = Visibility.Collapsed;
                    pnlValidate.Visibility = Visibility.Visible;
                    pnlChangePassword.Visibility = Visibility.Collapsed;
                    pnlLogin.Visibility = Visibility.Visible;
                    containerRecuperar.Visibility = Visibility.Visible;
                    limpiar();
                }
                else
                {
                    lblTitulo.Text = "Error";
                    lblMensaje.Text = "Las contraseñas no coinciden";
                    lblMensaje.FontSize = 18;
                    ShowStatus();
                }
            }
        }

        private void limpiar()
        {
            txtConfirmarContrasena.Password = string.Empty;
            txtContrasena.Password = string.Empty;
            txtRepuestaSeguridad.Text = string.Empty;
            cmbPreguntaSeguridad.SelectedIndex = 0;
        }

    }
}
