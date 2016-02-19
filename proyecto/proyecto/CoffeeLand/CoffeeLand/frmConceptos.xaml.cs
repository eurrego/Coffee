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
using MahApps.Metro.Controls.Dialogs;
using Modelo;
using MahApps.Metro.Controls;
using System.Globalization;
using CoffeeLand.Validator;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmConceptosxaml.xaml
    /// </summary>
    public partial class frmConceptos : UserControl
    {
        #region Singleton

        private static frmConceptos instance;

        public static frmConceptos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmConceptos();
            }

            return instance;
        }

        #endregion

        bool validacion;

        public frmConceptos()
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

            tblConceptos.Height = height - 285;
            tblConceptosIhhabilitados.Height = height - 285;
        }

        //mostrar
        public void Mostrar()
        {
            tblConceptos.ItemsSource = MConcepto.GetInstance().ConsultarConcepto();

            if (tblConceptos.Items.Count != 0)
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
            CantidadRegistrosConcepto();
            lblActivos.Text = (MConcepto.GetInstance().ConsultarInactivos().Count).ToString();
        }

        //mostrar
        public void MostrarInhabilitado()
        {
            tblConceptosIhhabilitados.ItemsSource = MConcepto.GetInstance().ConsultarInactivos();

            if (tblConceptosIhhabilitados.Items.Count != 0)
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
            CantidadRegistrosConcepto();
            CantidadRegistrosInhabilitados();
        }

        //Método para Buscar por nombre
        private void BuscarNombreConcepto()
        {
            tblConceptos.ItemsSource = MConcepto.GetInstance().ConsultarParametroConcepto(txtBuscarNombre.Text);
            CantidadRegistrosConcepto();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreConceptoInhabilitado()
        {
            tblConceptosIhhabilitados.ItemsSource = MConcepto.GetInstance().ConsultarParametroConceptoInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }


        private void CantidadRegistrosConcepto()
        {
            lblRegistros.Text = tblConceptos.Items.Count.ToString();
        }


        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblConceptosIhhabilitados.Items.Count.ToString();
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
        private void LimpiarConcepto()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        // mensaje de Error
        public void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("ERROR", mensaje);
        }

        public void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("INFORMACIÓN", mensaje);
        }


        private void btnGuardarConcepto_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    if (IsValid(txtNombre) && IsValid(txtDescripcion))
                    {
                        rpta = MConcepto.GetInstance().GestionConcepto(txtNombre.Text, txtDescripcion.Text, 0, 1).ToString();
                        mensajeInformacion(rpta);
                        LimpiarConcepto();
                        tabBuscar.Focus();
                        frmGastos.GetInstance().Mostrar();

                        if (pnlResultados.IsVisible)
                        {
                            limpiarPantalla();
                        }
                        else
                        {
                            Mostrar();
                        }

                        frmGastos.GetInstance().Mostrar();
                    }
                }
            }
            else if (IsValid(txtNombre) && IsValid(txtDescripcion))
            {
                rpta = MConcepto.GetInstance().GestionConcepto(txtNombre.Text, txtDescripcion.Text, Convert.ToByte(txtId.Text), 2).ToString();
                mensajeInformacion(rpta);
                LimpiarConcepto();
                tabBuscar.IsEnabled = true;
                tabNuevo.Header = "NUEVO";
                tabBuscar.Focus();
                tblConceptos.IsEnabled = true;

                if (pnlResultados.IsVisible)
                {
                    limpiarPantalla();
                }
                else
                {
                    Mostrar();
                }

                frmGastos.GetInstance().Mostrar();
            }
        }

        private void btnCancelarConcepto_Click(object sender, RoutedEventArgs e)
        {
            LimpiarConcepto();
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            tabNuevo.Header = "NUEVO";
            tblConceptos.IsEnabled = true;
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }

        private void btnModificarConcepto_Click(object sender, RoutedEventArgs e)
        {
            tblConceptos.IsEnabled = false;

            Concepto item = tblConceptos.SelectedItem as Concepto;

            var itemDescripcion = item.Descripcion.ToLower();

            txtNombre.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item.NombreConcepto.ToLower());
            txtDescripcion.Text = itemDescripcion.First().ToString().ToUpper() + itemDescripcion.Substring(1);
            txtId.Text = item.idConcepto.ToString();

            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }

        private async void btnInhabilitarConcepto_Click(object sender, RoutedEventArgs e)
        {
            Concepto item = tblConceptos.SelectedItem as Concepto;

            string nombre = item.NombreConcepto;
            string descripcion = item.Descripcion;
            byte id = Convert.ToByte(item.idConcepto);

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                string rpta = "";

                rpta = MConcepto.GetInstance().GestionConcepto(nombre, descripcion, id, 3).ToString();
                mensajeInformacion(rpta);

                if (pnlResultados.IsVisible)
                {
                    limpiarPantalla();
                }
                else
                {
                    Mostrar();
                }

                frmGastos.GetInstance().Mostrar();
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

            Concepto item = tblConceptosIhhabilitados.SelectedItem as Concepto;

            string nombre = item.NombreConcepto;
            string descripcion = item.Descripcion;
            byte id = Convert.ToByte(item.idConcepto);

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Habilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MConcepto.GetInstance().GestionConcepto(nombre, descripcion, id, 4).ToString();
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
                if (tblConceptos.IsVisible)
                {
                    tblConceptos.Height = tblConceptos.Height - 61;

                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombreConcepto();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblConceptosIhhabilitados.IsVisible)
                {
                    tblConceptosIhhabilitados.Height = tblConceptosIhhabilitados.Height - 61;

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
            tblConceptos.Height = tblConceptos.Height + 61;
            tblConceptosIhhabilitados.Height = tblConceptosIhhabilitados.Height + 61;

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
