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
    /// Lógica de interacción para frmTipoArboles.xaml
    /// </summary>
    public partial class frmTipoArboles : UserControl
    {
        #region Singleton

        private static frmTipoArboles instance;

        public static frmTipoArboles GetInstance()
        {
            if (instance == null)
            {
                instance = new frmTipoArboles();
            }

            return instance;
        }
        #endregion


        bool validacion;

        public frmTipoArboles()
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

            tblTipoArbol.Height = height - 285;
            tblTipoArbolesIhhabilitados.Height = height - 285;
        }

        //mostrar
        private void Mostrar()
        {
            tblTipoArbol.ItemsSource = MTipoArbol.GetInstance().consultarTipoArbol();

            if (tblTipoArbol.Items.Count != 0)
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
            lblActivos.Text = (MTipoArbol.GetInstance().ConsultarInactivos().Count).ToString();
        }

        //mostrar
        public void MostrarInhabilitado()
        {
            tblTipoArbolesIhhabilitados.ItemsSource = MTipoArbol.GetInstance().ConsultarInactivos();

            if (tblTipoArbolesIhhabilitados.Items.Count != 0)
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
            tblTipoArbol.ItemsSource = MTipoArbol.GetInstance().buscarTipoArbol(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreConceptoInhabilitado()
        {
            tblTipoArbolesIhhabilitados.ItemsSource = MTipoArbol.GetInstance().ConsultarParametroInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }

        private void CantidadRegistros()
        {
            lblRegistros.Text = tblTipoArbol.Items.Count.ToString();
        }

        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblTipoArbolesIhhabilitados.Items.Count.ToString();
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
                        rpta = MTipoArbol.GetInstance().registrarTipoArbol(txtNombre.Text, txtDescripcion.Text, 0, 1).ToString();
                        mensajeInformacion(rpta);
                        Limpiar();
                        tabBuscar.Focus();
                        tblTipoArbol.IsEnabled = true;

                        if (pnlResultados.IsVisible)
                        {
                            limpiarPantalla();
                        }
                        else
                        {
                            Mostrar();
                        }

                        frmArboles.GetInstance().mostrarTipoArbol();
                    }
                }
            }
            else if (IsValid(txtNombre) && IsValid(txtDescripcion))
            {
                rpta = MTipoArbol.GetInstance().registrarTipoArbol(txtNombre.Text, txtDescripcion.Text, Convert.ToInt32(txtId.Text), 2).ToString();
                mensajeInformacion(rpta);
                Limpiar();
                tabBuscar.IsEnabled = true;
                tabNuevo.Header = "NUEVO";
                tabBuscar.Focus();
                tblTipoArbol.IsEnabled = true;

                if (pnlResultados.IsVisible)
                {
                    limpiarPantalla();
                }
                else
                {
                    Mostrar();
                }

                frmArboles.GetInstance().mostrarTipoArbol();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            tabNuevo.Header = "NUEVO";
            tblTipoArbol.IsEnabled = true;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            tblTipoArbol.IsEnabled = false;

            TipoArbol item = tblTipoArbol.SelectedItem as TipoArbol;

            txtId.Text = item.idTipoArbol.ToString();
            txtNombre.Text = item.NombreTipoArbol;
            txtDescripcion.Text = item.Descripcion;

            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {

            TipoArbol item = tblTipoArbol.SelectedItem as TipoArbol;

            byte id = item.idTipoArbol;
            string nombre = item.NombreTipoArbol;
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

                rpta = MTipoArbol.GetInstance().registrarTipoArbol(nombre, descripcion, id, 3).ToString();
                mensajeInformacion(rpta);

                if (pnlResultados.IsVisible)
                {
                    limpiarPantalla();
                }
                else
                {
                    Mostrar();
                }

                frmArboles.GetInstance().mostrarTipoArbol();
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

            TipoArbol item = tblTipoArbolesIhhabilitados.SelectedItem as TipoArbol;

            byte id = item.idTipoArbol;
            string nombre = item.NombreTipoArbol;
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

                rpta = MTipoArbol.GetInstance().registrarTipoArbol(nombre, descripcion, id, 4).ToString();
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
                frmArboles.GetInstance().mostrarTipoArbol();
            }
        }

        public ICommand textBoxButtonCmd => new RelayCommand(ExecuteSearch);

        private void ExecuteSearch(object o)
        {
            if (txtBuscarNombre.Text != string.Empty)
            {
                if (tblTipoArbol.IsVisible)
                {
                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblTipoArbolesIhhabilitados.IsVisible)
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
