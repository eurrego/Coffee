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
    /// Lógica de interacción para frmInsumo.xaml
    /// </summary>
    public partial class frmInsumo : UserControl
    {
        #region Singleton

        private static frmInsumo instance;

        public static frmInsumo GetInstance()
        {
            if (instance == null)
            {
                instance = new frmInsumo();
            }

            return instance;
        }
        #endregion

        bool validacion = false;

        public frmInsumo()
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

            tblInsumo.Height = height - 285;
            tblInsumosIhhabilitados.Height = height - 285;
        }

        //mostrar
        public void Mostrar()
        {
            tblInsumo.ItemsSource = MInsumo.GetInstance().ConsultarInsumo();
            cmbTipoInsumo.ItemsSource = MInsumo.GetInstance().ConsultarTipoInsumo();
            cmbTipoInsumo.SelectedIndex = 0;

            if (tblInsumo.Items.Count != 0)
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
            lblActivos.Text = (MInsumo.GetInstance().ConsultarInactivos().Count).ToString();
        }

        //mostrar inhabilitados
        public void MostrarInhabilitado()
        {
            tblInsumosIhhabilitados.ItemsSource = MInsumo.GetInstance().ConsultarInactivos();

            if (tblInsumosIhhabilitados.Items.Count != 0)
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
            tblInsumo.ItemsSource = MInsumo.GetInstance().ConsultarParametroInsumo(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreInhabilitado()
        {
            tblInsumosIhhabilitados.ItemsSource = MInsumo.GetInstance().ConsultarParametroInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }

        private void CantidadRegistros()
        {
            lblRegistros.Text = tblInsumo.Items.Count.ToString();
        }

        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblInsumosIhhabilitados.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {
            if (cmbTipoInsumo.SelectedIndex == 0 || txtNombre.Text == string.Empty || txtMarca.Text == string.Empty || txtUnidadMedida.Text == string.Empty || txtDescripcion.Text == string.Empty)
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
            cmbTipoInsumo.SelectedIndex = 0;
            txtNombre.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtUnidadMedida.Text = string.Empty;
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
                    if (IsValid(txtNombre) && IsValid(txtDescripcion) && IsValid(txtMarca) && IsValid(txtUnidadMedida) && IsValid(cmbTipoInsumo))
                    {
                        rpta = MInsumo.GetInstance().GestionInsumo(Convert.ToByte(cmbTipoInsumo.SelectedValue), txtNombre.Text, txtDescripcion.Text, txtMarca.Text, txtUnidadMedida.Text, 0, 1);
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

                        frmCompra.GetInstance().Mostrar();
                    }
                }
            }
            else if (IsValid(txtNombre) && IsValid(txtDescripcion) && IsValid(txtMarca) && IsValid(txtUnidadMedida) && IsValid(cmbTipoInsumo))
            {
                rpta = MInsumo.GetInstance().GestionInsumo(Convert.ToByte(cmbTipoInsumo.SelectedValue), txtNombre.Text, txtDescripcion.Text, txtMarca.Text, txtUnidadMedida.Text, Convert.ToInt32(txtId.Text), 2);
                mensajeInformacion(rpta);
                Limpiar();
                tabBuscar.IsEnabled = true;
                tabNuevo.Header = "NUEVO";
                tabBuscar.Focus();
                tblInsumo.IsEnabled = true;

                if (pnlResultados.IsVisible)
                {
                    limpiarPantalla();
                }
                else
                {
                    Mostrar();
                }

                frmCompra.GetInstance().Mostrar();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            tabNuevo.Header = "NUEVO";
            tblInsumo.IsEnabled = true;
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
            tblInsumo.IsEnabled = false;

            Insumo item = tblInsumo.SelectedItem as Insumo;

            var itemDescripcion = item.Descripcion.ToLower();

            txtNombre.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item.NombreInsumo.ToLower());
            txtDescripcion.Text = itemDescripcion.First().ToString().ToUpper() + itemDescripcion.Substring(1);
            txtMarca.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item.Marca.ToLower());
            txtUnidadMedida.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item.UnidadMedida.ToLower());
            cmbTipoInsumo.SelectedValue = item.idTipoInsumo;
            txtId.Text = item.idInsumo.ToString();

            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Insumo item = tblInsumo.SelectedItem as Insumo;

            string nombre = item.NombreInsumo;
            string descripcion = item.Descripcion;
            int id = item.idInsumo;
            string marca = item.Marca;
            string unidadMedida = item.UnidadMedida;
            decimal cantidad = item.CantidadExistente;
            byte idTipoInsumo = Convert.ToByte(item.idTipoInsumo);


            if (cantidad > 0)
            {
                mensajeError("El registro no puede Inhabilitarse ya que aún tiene existencias disponibles");
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

                    rpta = MInsumo.GetInstance().GestionInsumo(idTipoInsumo, nombre, descripcion, marca, unidadMedida, id, 3).ToString();
                    mensajeInformacion(rpta);

                    if (pnlResultados.IsVisible)
                    {
                        limpiarPantalla();
                    }
                    else
                    {
                        Mostrar();
                    }

                    frmCompra.GetInstance().Mostrar();
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

            Insumo item = tblInsumosIhhabilitados.SelectedItem as Insumo;

            string nombre = item.NombreInsumo;
            string descripcion = item.Descripcion;
            int id = item.idInsumo;
            string marca = item.Marca;
            string unidadMedida = item.UnidadMedida;
            byte idTipoInsumo = Convert.ToByte(item.idTipoInsumo);

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Habilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MInsumo.GetInstance().GestionInsumo(idTipoInsumo, nombre, descripcion, marca, unidadMedida, id, 4).ToString();
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

                frmCompra.GetInstance().Mostrar();
            }
        }

        public ICommand textBoxButtonCmd => new RelayCommand(ExecuteSearch);

        private void ExecuteSearch(object o)
        {
            if (txtBuscarNombre.Text != string.Empty)
            {

                if (tblInsumo.IsVisible)
                {
                    tblInsumo.Height = tblInsumo.Height - 61;

                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblInsumosIhhabilitados.IsVisible)
                {
                    tblInsumosIhhabilitados.Height = tblInsumosIhhabilitados.Height - 61;

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

        public ICommand ComboBoxButtonCmd => new RelayCommand(ExecuteAdd);

        private void ExecuteAdd(object o)
        {
            frmAdministracionCostos.GetInstance().tabTipoInsumos.Focus();
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
            if (tblInsumo.IsVisible)
            {
                tblInsumo.Height = tblInsumo.Height + 61;
            }
            else if (tblInsumosIhhabilitados.IsVisible)
            {
                tblInsumosIhhabilitados.Height = tblInsumosIhhabilitados.Height + 61;
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
