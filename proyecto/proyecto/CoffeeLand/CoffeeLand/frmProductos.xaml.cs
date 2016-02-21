using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using CoffeeLand.Validator;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmProductos.xaml
    /// </summary>
    public partial class frmProductos : UserControl
    {
        #region Singleton

        private static frmProductos instance;

        public static frmProductos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmProductos();
            }

            return instance;
        }
        #endregion


        bool validacion;

        public frmProductos()
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

            tblProductos.Height = height - 285;
            tblProductosIhhabilitados.Height = height - 285;
        }

        //mostrar
        private void Mostrar()
        {
            tblProductos.ItemsSource = MProducto.GetInstance().ConsultarProducto();

            if (tblProductos.Items.Count != 0)
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
            lblActivos.Text = (MProducto.GetInstance().ConsultarInactivos().Count).ToString();
            CantidadRegistros();
        }

        //mostrar
        public void MostrarInhabilitado()
        {
            tblProductosIhhabilitados.ItemsSource = MProducto.GetInstance().ConsultarInactivos();

            if (tblProductosIhhabilitados.Items.Count != 0)
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
            tblProductos.ItemsSource = MProducto.GetInstance().ConsultarParametroProducto(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreConceptoInhabilitado()
        {
            tblProductosIhhabilitados.ItemsSource = MProducto.GetInstance().ConsultarParametroInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }

    
        private void CantidadRegistros()
        {
            lblRegistros.Text = tblProductos.Items.Count.ToString();
        }

        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblProductosIhhabilitados.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (txtNombre.Text == string.Empty || txtDescripcion.Text == string.Empty)
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
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    if (IsValid(txtNombre) && IsValid(txtDescripcion))
                    {
                        rpta = MProducto.GetInstance().GestionProducto(txtNombre.Text, txtDescripcion.Text, 0, 1).ToString();
                        mensajeInformacion(rpta);
                        Limpiar();
                        tabBuscar.Focus();
                        tblProductos.IsEnabled = true;

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
            else if (IsValid(txtNombre) && IsValid(txtDescripcion))
            {
                rpta = MProducto.GetInstance().GestionProducto(txtNombre.Text, txtDescripcion.Text, Convert.ToByte(txtId.Text), 2).ToString();
                mensajeInformacion(rpta);
                Limpiar();
                tabBuscar.IsEnabled = true;
                tabNuevo.Header = "NUEVO";
                tabBuscar.Focus();
                tblProductos.IsEnabled = true;

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
            tblProductos.IsEnabled = true;
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
            tblProductos.IsEnabled = false;

            Producto item = tblProductos.SelectedItem as Producto;

            txtId.Text = item.idProducto.ToString();
            txtNombre.Text = item.NombreProducto;
            txtDescripcion.Text = item.Descripcion;

            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Producto item = tblProductos.SelectedItem as Producto;

            byte id = item.idProducto;
            string nombre = item.NombreProducto;
            string descripcion = item.Descripcion;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MProducto.GetInstance().GestionProducto(nombre, descripcion, id, 3).ToString();
                mensajeInformacion(rpta);

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

            Producto item = tblProductosIhhabilitados.SelectedItem as Producto;

            byte id = item.idProducto;
            string nombre = item.NombreProducto;
            string descripcion = item.Descripcion;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Habilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MProducto.GetInstance().GestionProducto(nombre, descripcion, id, 4).ToString();
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

                frmGastos.GetInstance().Mostrar();
            }
        }

        public ICommand textBoxButtonCmd => new RelayCommand(ExecuteSearch);

        private void ExecuteSearch(object o)
        {
            if (txtBuscarNombre.Text != string.Empty)
            {
                if (tblProductos.IsVisible)
                {

                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblProductosIhhabilitados.IsVisible)
                {
                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombreConceptoInhabilitado();
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
    }
}
