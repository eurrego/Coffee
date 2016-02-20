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
    /// Lógica de interacción para frmEmpleados.xaml
    /// </summary>
    public partial class frmEmpleados : UserControl
    {

        #region Singleton

        private static frmEmpleados instance;

        public static frmEmpleados GetInstance()
        {
            if (instance == null)
            {
                instance = new frmEmpleados();
            }

            return instance;
        }

        #endregion

        bool validacion = false;

        public frmEmpleados()
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

            tblEmpleado.Height = height - 285;
            tblEmpleadosInhabilitados.Height = height - 285;
        }

        //mostrar
        private void Mostrar()
        {
            tblEmpleado.ItemsSource = MPersona.GetInstance().ConsultarPersona();

            List<string> dataTipoDocumento = new List<string>();
            dataTipoDocumento.Add("Seleccione un Tipo De Documento");
            dataTipoDocumento.Add("CC");
            dataTipoDocumento.Add("TI");
            cmbTipoDocumento.ItemsSource = dataTipoDocumento;

            List<string> dataTipoContrato = new List<string>();
            dataTipoContrato.Add("Seleccione un Tipo De Contrato");
            dataTipoContrato.Add("Permanente");
            dataTipoContrato.Add("Temporal");
            cmbTipoContrato.ItemsSource = dataTipoContrato;
            CantidadRegistros();


            if (tblEmpleado.Items.Count != 0)
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
            lblActivos.Text = (MPersona.GetInstance().ConsultarInactivos().Count).ToString();

        }

        //mostrar inhabilitados
        public void MostrarInhabilitado()
        {
            tblEmpleadosInhabilitados.ItemsSource = MPersona.GetInstance().ConsultarInactivos();

            if (tblEmpleadosInhabilitados.Items.Count != 0)
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

        private void cmbGenero_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un Género...");
            data.Add("M");
            data.Add("F");
            cmbGenero.ItemsSource = data;
        }

        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblEmpleado.ItemsSource = MPersona.GetInstance().ConsultarParametroPersona(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreInhabilitado()
        {
            tblEmpleadosInhabilitados.ItemsSource = MPersona.GetInstance().ConsultarParametroInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }


        private void CantidadRegistros()
        {
            lblRegistros.Text = tblEmpleado.Items.Count.ToString();
        }

        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblEmpleadosInhabilitados.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbTipoDocumento.SelectedIndex == 0 || cmbTipoContrato.SelectedIndex == 0 || cmbGenero.SelectedIndex == 0 || txtNombre.Text == string.Empty || txtDocumento.Text == string.Empty || txtTelefono.Text == string.Empty || dtdFechaNacimiento.SelectedDate == null)
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
            cmbGenero.SelectedIndex = 0;
            cmbTipoContrato.SelectedIndex = 0;
            cmbTipoDocumento.SelectedIndex = 0;
            dtdFechaNacimiento.SelectedDate = null;
            txtNombre.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtTelefono.Text = string.Empty;
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
                    if (IsValid(txtNombre) && IsValid(txtTelefono) && IsValid(txtDocumento))
                    {
                        rpta = MPersona.GetInstance().GestionPersona(txtNombre.Text, Convert.ToString(cmbGenero.SelectedItem), txtTelefono.Text, Convert.ToDateTime(dtdFechaNacimiento.SelectedDate), Convert.ToInt32(txtDocumento.Text), 1, Convert.ToString(cmbTipoDocumento.SelectedItem), Convert.ToString(cmbTipoContrato.SelectedItem));
                        mensajeInformacion(rpta);
                        Limpiar();
                        tabBuscar.Focus();
                        tblEmpleado.IsEnabled = true;

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
            else
            {
                rpta = MPersona.GetInstance().GestionPersona(txtNombre.Text, Convert.ToString(cmbGenero.SelectedItem), txtTelefono.Text, Convert.ToDateTime(dtdFechaNacimiento.SelectedDate), Convert.ToInt32(txtId.Text), 2, Convert.ToString(cmbTipoDocumento.SelectedItem), Convert.ToString(cmbTipoContrato.SelectedItem));
                mensajeInformacion(rpta);
                Limpiar();
                tabBuscar.IsEnabled = true;
                tabNuevo.Header = "NUEVO";
                tabBuscar.Focus();
                tblEmpleado.IsEnabled = true;

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
            tblEmpleado.IsEnabled = true;
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
            tblEmpleado.IsEnabled = false;

            Persona item = tblEmpleado.SelectedItem as Persona;

            txtId.Text = item.DocumentoPersona;
            txtNombre.Text = item.NombrePersona;
            txtDocumento.Text = item.DocumentoPersona;
            txtTelefono.Text = item.Telefono;
            cmbGenero.SelectedItem = item.Genero;
            cmbTipoContrato.SelectedValue = item.TipoContratoPersona;
            cmbTipoDocumento.SelectedValue = item.TipoDocumento;
            dtdFechaNacimiento.SelectedDate = item.FechaNacimineto;

            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Persona item = tblEmpleado.SelectedItem as Persona;

            int id = Convert.ToInt32(item.DocumentoPersona);
            string nombre = item.NombrePersona;
            string telefono = item.Telefono;
            string genero = item.Genero;
            string tipoContrato = item.TipoContratoPersona;
            string tipoDocumento = item.TipoDocumento;
            DateTime fecha = item.FechaNacimineto;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",

            };

            MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                string rpta = "";

                rpta = MPersona.GetInstance().GestionPersona(nombre, genero, telefono, fecha, id, 3, tipoDocumento, tipoContrato).ToString();
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

            Persona item = tblEmpleadosInhabilitados.SelectedItem as Persona;

            int id = Convert.ToInt32(item.DocumentoPersona);
            string nombre = item.NombrePersona;
            string telefono = item.Telefono;
            string genero = item.Genero;
            string tipoContrato = item.TipoContratoPersona;
            string tipoDocumento = item.TipoDocumento;
            DateTime fecha = item.FechaNacimineto;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Habilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MPersona.GetInstance().GestionPersona(nombre, genero, telefono, fecha, id, 4, tipoDocumento, tipoContrato).ToString();
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

                if (tblEmpleado.IsVisible)
                {
                    tblEmpleado.Height = tblEmpleado.Height - 61;

                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblEmpleadosInhabilitados.IsVisible)
                {
                    tblEmpleadosInhabilitados.Height = tblEmpleadosInhabilitados.Height - 61;

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
            if (tblEmpleado.IsVisible)
            {
                tblEmpleado.Height = tblEmpleado.Height + 61;
            }
            else if (tblEmpleadosInhabilitados.IsVisible)
            {
                tblEmpleadosInhabilitados.Height = tblEmpleadosInhabilitados.Height + 61;
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
