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
    /// Lógica de interacción para frmLabores.xaml
    /// </summary>
    public partial class frmLabores : UserControl
    {
        #region Singleton

        private static frmLabores instance;

        public static frmLabores GetInstance()
        {
            if (instance == null)
            {
                instance = new frmLabores();
            }

            return instance;
        }
        #endregion


        bool validacion = false;

        public frmLabores()
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

            tblLabores.Height = height - 285;
            tblLaboresIhhabilitados.Height = height - 285;
        }

        //mostrar
        private void Mostrar()
        {
            tblLabores.ItemsSource = MLabores.GetInstance().consultarLabor();

            if (tblLabores.Items.Count != 0)
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
            lblActivos.Text = (MLabores.GetInstance().ConsultarInactivos().Count).ToString();
        }

        //mostrar
        public void MostrarInhabilitado()
        {
            tblLaboresIhhabilitados.ItemsSource = MLabores.GetInstance().ConsultarInactivos();

            if (tblLaboresIhhabilitados.Items.Count != 0)
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
            tblLabores.ItemsSource = MLabores.GetInstance().buscarLabor(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreConceptoInhabilitado()
        {
            tblLaboresIhhabilitados.ItemsSource = MLabores.GetInstance().ConsultarParametroInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }


        private void CantidadRegistros()
        {
            lblRegistros.Text = tblLabores.Items.Count.ToString();
        }

        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblLaboresIhhabilitados.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (txtNombre.Text == string.Empty || txtDescripcion.Text == string.Empty || validarGroupBox() == false)
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
            rbtnArbolesNo.IsChecked = true;
            rbtnInsumoNo.IsChecked = true;
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
                        rpta = MLabores.GetInstance().GestionLabor(txtNombre.Text, Convert.ToBoolean(rbtnArbolesSi.IsChecked), Convert.ToBoolean(rbtnInsumoSi.IsChecked), txtDescripcion.Text, 0, 1);
                        mensajeInformacion(rpta);
                        Limpiar();
                        tabBuscar.Focus();
                        tblLabores.IsEnabled = true;

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
                if (IsValid(txtNombre) && IsValid(txtDescripcion))
                {
                    rpta = MLabores.GetInstance().GestionLabor(txtNombre.Text, Convert.ToBoolean(rbtnArbolesSi.IsChecked), Convert.ToBoolean(rbtnInsumoSi.IsChecked), txtDescripcion.Text, Convert.ToInt32(txtId.Text), 2);
                    mensajeInformacion(rpta);
                    Limpiar();
                    tabBuscar.IsEnabled = true;
                    tabNuevo.Header = "NUEVO";
                    tabBuscar.Focus();
                    tblLabores.IsEnabled = true;

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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            tabNuevo.Header = "NUEVO";
            rbtnArbolesNo.IsChecked = true;
            rbtnInsumoNo.IsChecked = true;
            tblLabores.IsEnabled = true;
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }



        // validar group box 
        private bool validarGroupBox()
        {
            bool pase1 = false;
            bool pase2 = false;

            if (rbtnArbolesSi.IsChecked == true || rbtnArbolesNo.IsChecked == true)
            {
                pase1 = true;
            }

            if (rbtnInsumoSi.IsChecked == true || rbtnInsumoNo.IsChecked == true)
            {
                pase2 = true;
            }

            if (pase1 == true && pase2 == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {       
            Labor item = tblLabores.SelectedItem as Labor;

            if (item.NombreLabor.Equals("Recoleccion"))
            {
                mensajeError("Esta labor no puede modificarse.");
            }
            else
            {
                tblLabores.IsEnabled = false;

                txtId.Text = item.idLabor.ToString();
                txtNombre.Text = item.NombreLabor;
                txtDescripcion.Text = item.Descripcion;


                if (item.ModificaArboles == true)
                {
                    rbtnArbolesSi.IsChecked = true;
                    rbtnArbolesNo.IsChecked = false;
                }
                else
                {
                    rbtnArbolesSi.IsChecked = false;
                    rbtnArbolesNo.IsChecked = true;
                }

                if (item.RequiereInsumo == true)
                {
                    rbtnInsumoSi.IsChecked = true;
                    rbtnInsumoNo.IsChecked = false;
                }
                else
                {
                    rbtnInsumoSi.IsChecked = false;
                    rbtnInsumoNo.IsChecked = true;
                }

                tblLabores.IsEnabled = false;
                tabBuscar.IsEnabled = false;
                tabNuevo.Header = "EDITAR";
                tabNuevo.Focus();
            }
        }


        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Labor item = tblLabores.SelectedItem as Labor;

            if (item.NombreLabor.Equals("Recoleccion"))
            {
                mensajeError("Esta labor no puede inhabilitarse.");
            }
            else
            {

                int id = item.idLabor;
                string nombre = item.NombreLabor;
                string descripcion = item.Descripcion;
                bool insumo = item.RequiereInsumo;
                bool modifica = item.ModificaArboles;


                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Aceptar",
                    NegativeButtonText = "Cancelar",

                };

                MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result != MessageDialogResult.Negative)
                {
                    string rpta = "";

                    rpta = MLabores.GetInstance().GestionLabor(nombre, insumo, modifica, descripcion, id, 3).ToString();
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

            Labor item = tblLaboresIhhabilitados.SelectedItem as Labor;

            int id = item.idLabor;
            string nombre = item.NombreLabor;
            string descripcion = item.Descripcion;
            bool insumo = item.RequiereInsumo;
            bool modifica = item.ModificaArboles;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Habilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MLabores.GetInstance().GestionLabor(nombre, insumo, modifica, descripcion, id, 4).ToString();
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
                if (tblLabores.IsVisible)
                {
                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblLaboresIhhabilitados.IsVisible)
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
