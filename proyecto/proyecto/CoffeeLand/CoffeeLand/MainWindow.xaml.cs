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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Modelo;
using CoffeeLand.Validator;
using System.Collections;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Singleton

        private static MainWindow instance;

        public static MainWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new MainWindow();
            }

            return instance;
        }

        #endregion


        bool validacion = false;
        frmAdministracionCostos misCostos = new frmAdministracionCostos();
        frmAdministracionEmpleados misEmpleados = new frmAdministracionEmpleados();
        frmAdministracionVentas misVentas = new frmAdministracionVentas();
        frmGestionTerrenos misTerrenos = new frmGestionTerrenos();
        frmReportes misResportes = new frmReportes();
        frmInicio miInicio = new frmInicio();

        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            DataContext = this;
            tamanioPAntalla();
            MainContainer.Content = miInicio;
            lblTitulo.Text = "Inicio";
        }

        private void tamanioPAntalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            frmUsuario.Width = width;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 1.75;
            pnlContainer.Width = anchoContainer;

            tblUsuarios.Height = height - 285;
            tblUsuariosIhhabilitados.Height = height - 285;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsOpen = true;
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Inicio";
            MainContainer.Content = miInicio;
            Menu.IsOpen = false;
        }

        private void btnGestionTerrenos_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Gestión de Terrenos";
            MainContainer.Content = misTerrenos;
            Menu.IsOpen = false;
        }

        private void btnAdministracionCostos_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Administración de Costos";
            misCostos.tabRegistroCompras.Focus();
            MainContainer.Content = misCostos;
            Menu.IsOpen = false;
        }

        private void btnAdministracionVentas_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Administración de Ventas";
            MainContainer.Content = misVentas;
            Menu.IsOpen = false;
        }

        private void btnAdministracionEmpleados_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Administración de Empleados";
            MainContainer.Content = misEmpleados;
            Menu.IsOpen = false;
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Reportes";
            MainContainer.Content = misResportes;
            Menu.IsOpen = false;
        }

        private void btnUsuario_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void btnPerfil_Click(object sender, RoutedEventArgs e)
        {
            if (MUsuario.GetInstance().rol == "Administrador")
            {
                frmUsuario.IsOpen = true;
                frmUsuario.IsPinned = true;
                btnMenu.Visibility = Visibility.Collapsed;
                lblTitulo.Visibility = Visibility.Collapsed;
                MostrarInhabilitado();
                Mostrar();
            }
            else
            {
                Usuario.IsOpen = true;
                Usuario.IsPinned = true;
                btnMenu.Visibility = Visibility.Collapsed;
                lblTitulo.Visibility = Visibility.Collapsed;
                cargarCmb();
                mostrarEmpleado();
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Usuario_ClosingFinished(object sender, RoutedEventArgs e)
        {
            btnMenu.Visibility = Visibility.Visible;
            lblTitulo.Visibility = Visibility.Visible;
            Usuario.IsPinned = false;
        }

        private void frmUsuario_ClosingFinished(object sender, RoutedEventArgs e)
        {
            btnMenu.Visibility = Visibility.Visible;
            lblTitulo.Visibility = Visibility.Visible;
            frmUsuario.IsPinned = false;
        }

        private void cargarCmb()
        {
            List<string> datosPreguntaSeguridad = new List<string>();
            datosPreguntaSeguridad.Add("Seleccione una pregunta de seguridad");
            datosPreguntaSeguridad.Add("¿Cuál es la ciudad de nacimiento de su mamá?");
            datosPreguntaSeguridad.Add("¿Cuál es la fecha de expedición de la cédula?");
            datosPreguntaSeguridad.Add("¿Cuál es el nombre de su primer mascota?");
            datosPreguntaSeguridad.Add("¿Cuál es la marca de su primer auto?");
            datosPreguntaSeguridad.Add("¿Cuál es el nombre de su primer jefe?");
            cmbPregunta.ItemsSource = datosPreguntaSeguridad;
            cmbPreguntaEmpleado.ItemsSource = datosPreguntaSeguridad;

            List<string> dataRol = new List<string>();
            dataRol.Add("Seleccione un Rol");
            dataRol.Add("Administrador");
            dataRol.Add("Empleado");
            cmbRol.ItemsSource = dataRol;
        }

        private void mostrarEmpleado()
        {
            var items = MUsuario.GetInstance().ConsultarUsuarioParametro(lblUser.Text);

            string Nickname = string.Empty;
            string Respuesta = string.Empty;
            string Password = string.Empty;
            string confirmarPassword = string.Empty;
            string id = string.Empty;
            string PreguntaSeguridad = string.Empty;

            foreach (var item in items)
            {
                Type v = item.GetType();
                Nickname = v.GetProperty("Nickname").GetValue(item).ToString();
                Respuesta = v.GetProperty("Respuesta").GetValue(item).ToString();
                Password = DesEncriptar(v.GetProperty("Contrasena").GetValue(item).ToString());
                confirmarPassword = DesEncriptar(v.GetProperty("Contrasena").GetValue(item).ToString());
                id = v.GetProperty("idUsuario").GetValue(item).ToString();
                PreguntaSeguridad = v.GetProperty("PreguntaSeguridad").GetValue(item).ToString();
            }

            txtUsuarioEmpleado.Text = Nickname;
            txtRespuestaEmpleado.Text = Respuesta;

            txtContrasenaEmpleado.Password = Password;
            txtConfirmarContrasenaEmpleado.Password = confirmarPassword;

            txtIdEmpleado.Text = id;
            cmbPreguntaEmpleado.SelectedItem = PreguntaSeguridad;
        }


        //mostrar
        private void Mostrar()
        {
            tblUsuarios.ItemsSource = MUsuario.GetInstance().ConsultarUsuario();
            cargarCmb();

            if (tblUsuarios.Items.Count != 0)
            {
                pnlHabilitados.Visibility = Visibility.Visible;
                pnlInhabilitados.Visibility = Visibility.Collapsed;
                pnlSinRegistros.Visibility = Visibility.Collapsed;
                lblPosicion.Text = "HABILITADOS";
                lblPosicion.Foreground = Brushes.Green;
            }
            else
            {
                pnlHabilitados.Visibility = Visibility.Collapsed;
                pnlInhabilitados.Visibility = Visibility.Collapsed;
                pnlSinRegistros.Visibility = Visibility.Collapsed;
                lblPosicion.Text = "";
            }

            pnlRegistrosInhabilitados.Background = Brushes.LightGray;
            pnlRegistrosHabilitados.Background = Brushes.Silver;
            CantidadRegistros();
            CantidadRegistrosInhabilitados();
        }

        //mostrar inhabilitados
        public void MostrarInhabilitado()
        {
            tblUsuariosIhhabilitados.ItemsSource = MUsuario.GetInstance().ConsultarInactivos();

            if (tblUsuariosIhhabilitados.Items.Count != 0)
            {
                pnlInhabilitados.Visibility = Visibility.Visible;
                pnlHabilitados.Visibility = Visibility.Collapsed;
                pnlSinRegistros.Visibility = Visibility.Collapsed;
                lblPosicion.Text = "INHABILITADOS";
                lblPosicion.Foreground = Brushes.Crimson;
            }
            else
            {
                lblSinRegistros.Text = "Inhabilitados";
                pnlSinRegistros.Visibility = Visibility.Visible;
                pnlHabilitados.Visibility = Visibility.Collapsed;
                pnlInhabilitados.Visibility = Visibility.Collapsed;
                lblPosicion.Text = "";
            }

            pnlRegistrosHabilitados.Background = Brushes.LightGray;
            pnlRegistrosInhabilitados.Background = Brushes.Silver;
            CantidadRegistros();
            CantidadRegistrosInhabilitados();
        }

        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblUsuarios.ItemsSource = MUsuario.GetInstance().ConsultarParametro(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreInhabilitado()
        {
            tblUsuariosIhhabilitados.ItemsSource = MUsuario.GetInstance().ConsultarParametroInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }

        private void CantidadRegistros()
        {
            lblRegistros.Text = tblUsuarios.Items.Count.ToString();
        }

        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblUsuariosIhhabilitados.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbRol.SelectedIndex == 0 || cmbPregunta.SelectedIndex == 0 || txtUsuario.Text == string.Empty || txtRespuesta.Text == string.Empty || txtContrasena.Password == string.Empty || txtConfirmarContrasena.Password == string.Empty)
            {
                mensajeError("Debe Ingresar todos los Campos");
                validacion = false;
            }
            else
            {
                validacion = true;
            }

            return validacion;
        }

        private bool validarCamposEmpleado()
        {

            if (cmbPreguntaEmpleado.SelectedIndex == 0 || txtUsuarioEmpleado.Text == string.Empty || txtRespuestaEmpleado.Text == string.Empty || txtContrasenaEmpleado.Password == string.Empty || txtConfirmarContrasenaEmpleado.Password == string.Empty)
            {
                mensajeError("Debe Ingresar todos los Campos");
                validacion = false;
            }
            else
            {
                validacion = true;
            }

            return validacion;
        }


        // limpiar Controles
        private void Limpiar()
        {
            cmbPregunta.SelectedIndex = 0;
            cmbRol.SelectedIndex = 0;
            txtUsuario.Text = string.Empty;
            txtRespuesta.Text = string.Empty;
            txtContrasena.Password = string.Empty;
            txtConfirmarContrasena.Password = string.Empty;
            txtId.Text = string.Empty;
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        public void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            tabNuevo.Header = "NUEVO";
            tblUsuarios.IsEnabled = true;
        }

        public string Encriptar(string cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.Unicode.GetBytes(cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    if (IsValid(txtUsuario) && IsValid(txtRespuesta) && IsValid(txtContrasena) && IsValid(txtConfirmarContrasena))
                    {
                        if (txtContrasena.Password == txtConfirmarContrasena.Password)
                        {
                            rpta = MUsuario.GetInstance().GestionUsuario(0, txtUsuario.Text, Convert.ToString(cmbRol.SelectedItem), Encriptar(txtContrasena.Password.ToString()), Convert.ToString(cmbPregunta.SelectedItem), txtRespuesta.Text, 1);
                            mensajeInformacion(rpta);
                            Limpiar();
                            tabBuscar.Focus();
                            tblUsuarios.IsEnabled = true;

                            if (pnlResultados.IsVisible)
                            {
                                limpiarPantalla();
                            }
                            else
                            {
                                Mostrar();
                            }
                        }
                        else
                        {
                            mensajeError("Las contraseñas no coinciden");
                        }
                    }
                }
            }
            else
            {
                if (txtContrasena.Password == txtConfirmarContrasena.Password)
                {

                    if (verificarRol())
                    {
                        rpta = MUsuario.GetInstance().GestionUsuario(Convert.ToInt32(txtId.Text), txtUsuario.Text, Convert.ToString(cmbRol.SelectedItem), Encriptar(txtContrasena.Password.ToString()), Convert.ToString(cmbPregunta.SelectedItem), txtRespuesta.Text, 2);
                        mensajeInformacion(rpta);

                        if (lblIdUser.Text == txtId.Text)
                        {
                            MUsuario.GetInstance().rol = Convert.ToString(cmbRol.SelectedItem);

                            if (MUsuario.GetInstance().rol == "Empleado")
                            {
                                frmUsuario.IsOpen = false;
                                btnMenu.Visibility = Visibility.Visible;
                                lblTitulo.Visibility = Visibility.Visible;
                                frmUsuario.IsPinned = false;
                            }
                        }

                        Limpiar();
                        tabBuscar.IsEnabled = true;
                        tabNuevo.Header = "NUEVO";
                        tabBuscar.Focus();
                        tblUsuarios.IsEnabled = true;

                        if (pnlResultados.IsVisible)
                        {
                            limpiarPantalla();
                        }
                        else
                        {
                            Mostrar();
                        }
                    }
                    else
                    {
                        mensajeError("No se puede cambiar el rol, siempre debe existir un usuario con rol de administrador");
                        Limpiar();
                        tabBuscar.IsEnabled = true;
                        tabBuscar.Focus();
                        tabNuevo.Header = "NUEVO";
                        tblUsuarios.IsEnabled = true;
                    }
                }
                else
                {
                    mensajeError("Las contraseñas no coinciden");
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            tblUsuarios.IsEnabled = false;

            Usuario item = tblUsuarios.SelectedItem as Usuario;

            txtUsuario.Text = item.Nickname;
            txtRespuesta.Text = item.Respuesta;

            txtContrasena.Password = DesEncriptar(item.Contrasena);
            txtConfirmarContrasena.Password = DesEncriptar(item.Contrasena);

            txtId.Text = item.idUsuario.ToString();
            cmbPregunta.SelectedItem = item.PreguntaSeguridad;
            cmbRol.SelectedItem = item.Rol;

            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }

        private bool verificarRol()
        {
            Usuario items = tblUsuarios.SelectedItem as Usuario;
            byte id = Convert.ToByte(items.idUsuario);
            string rol = string.Empty;

            if (items.Rol == "Administrador")
            {
                int cantidad = 0;

                foreach (var item in tblUsuarios.Items)
                {
                    Type v = item.GetType();
                    rol = v.GetProperty("Rol").GetValue(item).ToString();

                    if (rol == "Administrador")
                    {
                        cantidad++;
                    }
                }

                if (cantidad > 1)
                {
                    return true;
                }
                else if (cantidad == 1 && cmbRol.SelectedItem.Equals("Administrador"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }

        public string DesEncriptar(string cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {

            Usuario items = tblUsuarios.SelectedItem as Usuario;
            byte id = Convert.ToByte(items.idUsuario);

            if (items.Rol == "Administrador")
            {
                int cantidad = 0;

                foreach (var item in tblUsuarios.Items)
                {
                    Type v = item.GetType();
                    var rol = v.GetProperty("Rol").GetValue(item).ToString();

                    if (rol == "Administrador")
                    {
                        cantidad++;
                    }
                }

                if ((cantidad - 1) >= 1)
                {
                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Aceptar",
                        NegativeButtonText = "Cancelar",

                    };

                    MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                    if (result != MessageDialogResult.Negative)
                    {
                        string rpta = "";

                        rpta = MUsuario.GetInstance().GestionUsuario(id, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 3);
                        mensajeInformacion(rpta);

                        if (pnlResultados.IsVisible)
                        {
                            limpiarPantalla();
                        }
                        else
                        {
                            MostrarInhabilitado();
                            Mostrar();
                        }

                    }
                }
                else
                {
                    mensajeError("El usuario no se puede inhabilitar, siempre debe existir un usuario administrador activo");
                }

            }
            else
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Aceptar",
                    NegativeButtonText = "Cancelar",

                };

                MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result != MessageDialogResult.Negative)
                {
                    string rpta = "";

                    rpta = MUsuario.GetInstance().GestionUsuario(id, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 3);
                    mensajeInformacion(rpta);

                    if (pnlResultados.IsVisible)
                    {
                        limpiarPantalla();
                    }
                    else
                    {
                        MostrarInhabilitado();
                        Mostrar();
                    }

                }
            }
        }

        private void btnHabilitados_Click(object sender, RoutedEventArgs e)
        {
            Mostrar();
            pnlRegistrosInhabilitados.Background = Brushes.LightGray;
            pnlRegistrosHabilitados.Background = Brushes.Silver;
        }

        private void btnInhabilitados_Click(object sender, RoutedEventArgs e)
        {
            MostrarInhabilitado();
            pnlRegistrosHabilitados.Background = Brushes.LightGray;
            pnlRegistrosInhabilitados.Background = Brushes.Silver;
        }

        private async void btnHabilitar_Click(object sender, RoutedEventArgs e)
        {
            Usuario item = tblUsuariosIhhabilitados.SelectedItem as Usuario;

            byte id = Convert.ToByte(item.idUsuario);

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",

            };

            MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Habilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                string rpta = "";

                rpta = MUsuario.GetInstance().GestionUsuario(id, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 4);
                mensajeInformacion(rpta);

                if (pnlResultados.IsVisible)
                {
                    limpiarPantalla();
                }
                else
                {
                    MostrarInhabilitado();
                    Mostrar();
                }
            }

        }

        public ICommand textBoxButtonCmd => new RelayCommand(ExecuteSearch);

        private void ExecuteSearch(object o)
        {
            if (txtBuscarNombre.Text != string.Empty)
            {
                if (tblUsuarios.IsVisible)
                {
                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblUsuariosIhhabilitados.IsVisible)
                {
                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombreInhabilitado();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else
                {
                    mensajeInformacion("No tiene registros dispobibles para realizar una búsqueda");
                    txtBuscarNombre.Text = string.Empty;
                }
            }
            else
            {
                mensajeError("Debe ingresar una palabra a buscar");
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            tabNuevo.Focus();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            tabBuscar.Focus();
        }

        private void limpiarPantalla()
        {
            btnHabilitados.IsEnabled = true;
            btnInhabilitados.IsEnabled = true;

            tabBuscar.Focus();
            pnlResultados.Visibility = Visibility.Collapsed;
            lblBusqueda.Text = string.Empty;
            MostrarInhabilitado();
            Mostrar();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
        }

        private void btnGuardarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (validarCamposEmpleado())
            {
                if (IsValid(txtUsuarioEmpleado) && IsValid(txtRespuestaEmpleado) && IsValid(txtContrasenaEmpleado) && IsValid(txtConfirmarContrasenaEmpleado))
                {
                    if (txtContrasenaEmpleado.Password == txtConfirmarContrasenaEmpleado.Password)
                    {

                        string rpta = "";
                        rpta = MUsuario.GetInstance().GestionUsuario(Convert.ToInt32(txtIdEmpleado.Text), txtUsuarioEmpleado.Text, "Empleado", Encriptar(txtContrasenaEmpleado.Password.ToString()), Convert.ToString(cmbPreguntaEmpleado.SelectedItem), txtRespuestaEmpleado.Text, 2);
                        lblUser.Text = txtUsuarioEmpleado.Text;
                        mensajeInformacion(rpta);
                        mostrarEmpleado();
                    }
                    else
                    {
                        mensajeError("Las contraseñas no coinciden");
                    }
                }
            }

        }

        private void btnCancelarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            Usuario.IsOpen = false;
            btnMenu.Visibility = Visibility.Visible;
            lblTitulo.Visibility = Visibility.Visible;
            Usuario.IsPinned = false;
        }

        private void cmbRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbRol.SelectedIndex > 0 )
            {
                
            }
        }
    }
}
