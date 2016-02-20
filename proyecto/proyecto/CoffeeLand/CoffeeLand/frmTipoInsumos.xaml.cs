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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmTipoInsumos.xaml
    /// </summary>
    public partial class frmTipoInsumos : UserControl
    {
       

        #region Singleton

        private static frmTipoInsumos instance;

        public static frmTipoInsumos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmTipoInsumos();

            }

            return instance;
        }
        #endregion

        bool validacion;

        public frmTipoInsumos()
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

            tblTipoInsumo.Height = height - 285;
            tblTiposInsumosIhhabilitados.Height = height - 285;
        }


        //mostrar
        public void Mostrar()
        {
            tblTipoInsumo.ItemsSource = MTipoInsumo.GetInstance().ConsultarTipoInsumo();

            if (tblTipoInsumo.Items.Count != 0)
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
            lblActivos.Text = (MTipoInsumo.GetInstance().ConsultarInactivos().Count).ToString();
        }

        //mostrar inhabilitados
        public void MostrarInhabilitado()
        {
            tblTiposInsumosIhhabilitados.ItemsSource = MTipoInsumo.GetInstance().ConsultarInactivos();

            if (tblTiposInsumosIhhabilitados.Items.Count != 0)
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
            tblTipoInsumo.ItemsSource = MTipoInsumo.GetInstance().ConsultarParametroTipoInsumo(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreInhabilitado()
        {
            tblTiposInsumosIhhabilitados.ItemsSource = MTipoInsumo.GetInstance().ConsultarParametroInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }


        private void CantidadRegistros()
        {
            lblRegistros.Text = tblTipoInsumo.Items.Count.ToString();
        }

        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblTiposInsumosIhhabilitados.Items.Count.ToString();
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
                    if (IsValid(txtNombre) && IsValid(txtDescripcion))
                    {
                        rpta = MTipoInsumo.GetInstance().GestionTipoInsumo(txtNombre.Text, txtDescripcion.Text, 0, 1).ToString();
                        mensajeInformacion(rpta);
                        Limpiar();
                        tabBuscar.Focus();

                        if (pnlResultados.IsVisible)
                        {
                            limpiarPantalla();
                        }
                        else
                        {
                            Mostrar();
                        }

                        frmInsumo.GetInstance().Mostrar();
                        frmCompra.GetInstance().Mostrar();
                    }
                }
            }
            else if (IsValid(txtNombre) && IsValid(txtDescripcion))
            {
                rpta = MTipoInsumo.GetInstance().GestionTipoInsumo(txtNombre.Text, txtDescripcion.Text, Convert.ToByte(txtId.Text), 2).ToString();
                mensajeInformacion(rpta);
                Limpiar();
                tabBuscar.IsEnabled = true;
                tabNuevo.Header = "NUEVO";
                tabBuscar.Focus();
                tblTipoInsumo.IsEnabled = true;

                if (pnlResultados.IsVisible)
                {
                    limpiarPantalla();
                }
                else
                {
                    Mostrar();
                }

                frmInsumo.GetInstance().Mostrar();
                frmCompra.GetInstance().Mostrar();
            }
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            tabNuevo.Header = "NUEVO";
            tblTipoInsumo.IsEnabled = true;
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
            tblTipoInsumo.IsEnabled = false;

            TipoInsumo item = tblTipoInsumo.SelectedItem as TipoInsumo;

            var itemDescripcion = item.Descripcion.ToLower();

            txtNombre.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item.NombreTipoInsumo.ToLower());
            txtDescripcion.Text = itemDescripcion.First().ToString().ToUpper() + itemDescripcion.Substring(1);
            txtId.Text = item.idTipoInsumo.ToString();

            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            TipoInsumo item = tblTipoInsumo.SelectedItem as TipoInsumo;

            byte id = item.idTipoInsumo;
            string nombre = item.NombreTipoInsumo;
            string descripcion = item.Descripcion;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",

            };

            MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                string rpta = "";

                rpta = MTipoInsumo.GetInstance().GestionTipoInsumo(nombre, descripcion, id, 3).ToString();
                mensajeInformacion(rpta);

                if (pnlResultados.IsVisible)
                {
                    limpiarPantalla();
                }
                else
                {
                    Mostrar();
                }

                frmInsumo.GetInstance().Mostrar();
                frmCompra.GetInstance().Mostrar();
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

            TipoInsumo item = tblTiposInsumosIhhabilitados.SelectedItem as TipoInsumo;

            string nombre = item.NombreTipoInsumo;
            string descripcion = item.Descripcion;
            byte id = Convert.ToByte(item.idTipoInsumo);

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Habilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MTipoInsumo.GetInstance().GestionTipoInsumo(nombre, descripcion, id, 4).ToString();
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

                frmInsumo.GetInstance().Mostrar();
                frmCompra.GetInstance().Mostrar();
            }
        }

        public ICommand textBoxButtonCmd => new RelayCommand(ExecuteSearch);

        private void ExecuteSearch(object o)
        {
            if (txtBuscarNombre.Text != string.Empty)
            {

                if (tblTipoInsumo.IsVisible)
                {
                    tblTipoInsumo.Height = tblTipoInsumo.Height - 61;

                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblTiposInsumosIhhabilitados.IsVisible)
                {

                    tblTiposInsumosIhhabilitados.Height = tblTiposInsumosIhhabilitados.Height - 61;

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
            if (tblTipoInsumo.IsVisible)
            {
                tblTipoInsumo.Height = tblTipoInsumo.Height + 61;
            }
            else if (tblTiposInsumosIhhabilitados.IsVisible)
            {
                tblTiposInsumosIhhabilitados.Height = tblTiposInsumosIhhabilitados.Height + 61;
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
    }
}
