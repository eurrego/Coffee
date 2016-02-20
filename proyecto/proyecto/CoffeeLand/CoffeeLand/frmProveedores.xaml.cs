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
using Modelo;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Globalization;
using CoffeeLand.Validator;
using System.Collections;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmProveedores.xaml
    /// </summary>
    public partial class frmProveedores : UserControl
    {
        #region Singleton

        private static frmProveedores instance;

        public static frmProveedores GetInstance()
        {
            if (instance == null)
            {
                instance = new frmProveedores();
            }

            return instance;
        }

        #endregion

        bool validacion = false;

        public frmProveedores()
        {
            InitializeComponent();
            instance = this;
            DataContext = this;
            Mostrar();
            tamanioPantalla();
        }


        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 1.75;
            pnlContainer.Width = anchoContainer;

            tblProveedor.Height = height - 285;
            tblProveedoresIhhabilitados.Height = height - 285;
        }

        //mostrar
        public void Mostrar()
        {
            tblProveedor.ItemsSource = MProveedor.GetInstance().ConsultarProveedor();
            cargarCmbTipoDocumento();

            if (tblProveedor.Items.Count != 0)
            {
                pnlHabilitados.Visibility = Visibility.Visible;
                pnlSinRegistros.Visibility = Visibility.Collapsed;
                pnlInhabilitados.Visibility = Visibility.Collapsed;
                lblPosicion.Text = "HABILITADOS";
                lblPosicion.Foreground = Brushes.Green;
            }
            else
            {
                lblSinRegistros.Text = "registrados o habilitados.";
                pnlSinRegistros.Visibility = Visibility.Visible;
                pnlHabilitados.Visibility = Visibility.Collapsed;
                pnlInhabilitados.Visibility = Visibility.Collapsed;
                lblPosicion.Text = "";
            }

            pnlRegistrosInhabilitados.Background = Brushes.LightGray;
            pnlRegistrosHabilitados.Background = Brushes.Silver;
            CantidadRegistros();
            lblActivos.Text = (MProveedor.GetInstance().ConsultarInactivos().Count).ToString();
        }

        //mostrar inhabilitados
        public void MostrarInhabilitado()
        {
            tblProveedoresIhhabilitados.ItemsSource = MProveedor.GetInstance().ConsultarInactivos();

            if (tblProveedoresIhhabilitados.Items.Count != 0)
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
            tblProveedor.ItemsSource = MProveedor.GetInstance().ConsultarParametroProveedor(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreInhabilitado()
        {
            tblProveedoresIhhabilitados.ItemsSource = MProveedor.GetInstance().ConsultarParametroInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }

        // Define el estilo de las celdas 
        private void CantidadRegistros()
        {
            lblRegistros.Text = tblProveedor.Items.Count.ToString();
        }

        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblProveedoresIhhabilitados.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbTipoDocumento.SelectedIndex == 0 || txtNombre.Text == string.Empty || txtDocumento.Text == string.Empty || txtTelefono.Text == string.Empty || txtDireccion.Text == string.Empty)
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
            cmbTipoDocumento.SelectedIndex = 0;
            txtNombre.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    if (IsValid(txtNombre) && IsValid(txtDocumento) && IsValid(txtDireccion) && IsValid(txtTelefono) && IsValid(cmbTipoDocumento))
                    {
                        rpta = MProveedor.GetInstance().GestionProveedor(txtDocumento.Text, txtNombre.Text, txtTelefono.Text, txtDireccion.Text, Convert.ToString(cmbTipoDocumento.SelectedItem), 1);
                        mensajeInformacion(rpta);
                        Limpiar();
                        tabBuscar.Focus();
                        frmCompra.GetInstance().Mostrar();
                        frmEstadoCuenta.GetInstance().Mostrar();

                        if (pnlResultados.IsVisible)
                        {
                            limpiarPantalla();
                        }
                        else
                        {
                            Mostrar();
                        }
                    }
                }
            }
            else if (IsValid(txtNombre) && IsValid(txtDocumento) && IsValid(txtDireccion) && IsValid(txtTelefono) && IsValid(cmbTipoDocumento))
            {
                rpta = MProveedor.GetInstance().GestionProveedor(txtId.Text, txtNombre.Text, txtTelefono.Text, txtDireccion.Text, Convert.ToString(cmbTipoDocumento.SelectedItem), 2);
                mensajeInformacion(rpta);
                Limpiar();
                tabBuscar.IsEnabled = true;
                tabNuevo.Header = "NUEVO";
                tabBuscar.Focus();
                tblProveedor.IsEnabled = true;

                if (pnlResultados.IsVisible)
                {
                    limpiarPantalla();
                }
                else
                {
                    Mostrar();
                }
            } 
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            tabNuevo.Header = "NUEVO";
            tblProveedor.IsEnabled = true;
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            tblProveedor.IsEnabled = false;

            Proveedor item = tblProveedor.SelectedItem as Proveedor;

            txtNombre.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item.NombreProveedor.ToLower()); 
            txtDocumento.Text = item.Nit;
            txtTelefono.Text = item.Telefono;
            txtDireccion.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item.Direccion.ToLower());
            cmbTipoDocumento.SelectedItem = item.TipoDocumento;
            txtId.Text = item.Nit;

            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Proveedor item = tblProveedor.SelectedItem as Proveedor;

            string nombre = item.NombreProveedor;
            string id = item.Nit;
            string telefono = item.Telefono;
            string direccion = item.Direccion;
            string tipoDocumento = item.TipoDocumento;

            var deuda = MProveedor.GetInstance().consultarDeuda(id) as IEnumerable;

            decimal? total = 0;

            foreach (ComprasProveedor_Result1 valor in deuda)
            {
                total += valor.adeuda;
            }

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",

            };

            if (total <= 0)
            {
                MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result != MessageDialogResult.Negative)
                {
                    string rpta = "";

                    rpta = MProveedor.GetInstance().GestionProveedor(id, nombre, telefono, direccion, tipoDocumento, 3);
                    mensajeInformacion(rpta);
                    frmCompra.GetInstance().Mostrar();

                    if (pnlResultados.IsVisible)
                    {
                        limpiarPantalla();
                    }
                    else
                    {
                        Mostrar();
                    }
                }
            }
            else
            {
                mensajeError("El proveedor no puede inhabilitarse porque tiene cuentas por pagar pendientes.");
            }

        }

        private void btnInhabilitados_Click(object sender, RoutedEventArgs e)
        {
            MostrarInhabilitado();
            pnlRegistrosHabilitados.Background = Brushes.LightGray;
            pnlRegistrosInhabilitados.Background = Brushes.Silver;
        }

        private void btnHabilitados_Click(object sender, RoutedEventArgs e)
        {
            Mostrar();
            pnlRegistrosInhabilitados.Background = Brushes.LightGray;
            pnlRegistrosHabilitados.Background = Brushes.Silver;
        }

        private async void btnHabilitar_Click(object sender, RoutedEventArgs e)
        {

            Proveedor item = tblProveedoresIhhabilitados.SelectedItem as Proveedor;

            string nombre = item.NombreProveedor;
            string id = item.Nit;
            string telefono = item.Telefono;
            string direccion = item.Direccion;
            string tipoDocumento = item.TipoDocumento;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Habilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MProveedor.GetInstance().GestionProveedor(id, nombre, telefono, direccion, tipoDocumento, 4);
                mensajeInformacion(rpta);
                frmCompra.GetInstance().Mostrar();

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

                if (tblProveedor.IsVisible)
                {
                    tblProveedor.Height = tblProveedor.Height - 61;

                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblProveedoresIhhabilitados.IsVisible)
                {
                    tblProveedoresIhhabilitados.Height = tblProveedoresIhhabilitados.Height - 61;

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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            tabBuscar.Focus();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            tabNuevo.Focus();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            if (tblProveedor.IsVisible)
            {
                tblProveedor.Height = tblProveedor.Height + 61;
            }
            else if (tblProveedoresIhhabilitados.IsVisible)
            {
                tblProveedoresIhhabilitados.Height = tblProveedoresIhhabilitados.Height + 61;
            }

            limpiarPantalla();
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


        private void cargarCmbTipoDocumento()
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un Tipo de Documento...");
            data.Add("CC");
            data.Add("TI");
            data.Add("NIT");
            cmbTipoDocumento.ItemsSource = data;
        }        
    }
}
