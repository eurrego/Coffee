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
    /// Lógica de interacción para FrmLotes.xaml
    /// </summary>
    public partial class frmLotes : UserControl
    {
        #region Singleton

        private static frmLotes instance;

        public static frmLotes GetInstance()
        {
            if (instance == null)
            {
                instance = new frmLotes();
            }

            return instance;
        }
        #endregion



        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmLotes()
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

            tblLotes.Height = height - 285;
            tblLotesIhhabilitados.Height = height - 285;
        }

        //mostrar
        private void Mostrar()
        {
            tblLotes.ItemsSource = MLote.GetInstance().ConsultarLotes();

            if (tblLotes.Items.Count != 0)
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
            lblActivos.Text = (MLote.GetInstance().ConsultarInactivos().Count).ToString();
        }

        //mostrar
        public void MostrarInhabilitado()
        {
            tblLotesIhhabilitados.ItemsSource = MLote.GetInstance().ConsultarInactivos();

            if (tblLotesIhhabilitados.Items.Count != 0)
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
            tblLotes.ItemsSource = MLote.GetInstance().ConsultarParametroLote(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        //Método para Buscar por nombre concepto Inhabilitado
        private void BuscarNombreConceptoInhabilitado()
        {
            tblLotesIhhabilitados.ItemsSource = MLote.GetInstance().ConsultarParametroInhabilitado(txtBuscarNombre.Text);
            CantidadRegistrosInhabilitados();
        }

        private void CantidadRegistros()
        {
            lblRegistros.Text = tblLotes.Items.Count.ToString();
        }

        private void CantidadRegistrosInhabilitados()
        {
            lblActivos.Text = tblLotesIhhabilitados.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (txtNombre.Text == string.Empty || txtCuadras.Text == 0.ToString() || txtDescripcion.Text == null)
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

        // limpiar Controles lote
        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtCuadras.Text = string.Empty;
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
                    if (IsValid(txtNombre) && IsValid(txtDescripcion) && IsValid(txtCuadras))
                    {




                        rpta = MLote.GetInstance().GestionLote(txtNombre.Text, txtDescripcion.Text, txtCuadras.Text, 0, 1).ToString();
                        mensajeInformacion(rpta);
                        Limpiar();
                        tabBuscar.Focus();
                        tblLotes.IsEnabled = true;

                        if (pnlResultados.IsVisible)
                        {
                            limpiarPantalla();
                        }
                        else
                        {
                            Mostrar();
                        }
                        frmArboles.GetInstance().mostrarCmb();
                        frmTerrenos.GetInstance().mostrar();
                    }

                }
            }
            else if (validarCampos())
            {
                if (IsValid(txtNombre) && IsValid(txtDescripcion) && IsValid(txtCuadras))
                {
                    int tamañoLotes = 0;
                    IEnumerable<Lote> item = MLote.GetInstance().tamañoLotes(Convert.ToInt32(txtId.Text)) as IEnumerable<Lote>;

                    foreach (Lote item2 in item)
                    {
                        tamañoLotes += int.Parse(item2.Cuadras);
                    }
                   

                    if ( (tamañoLotes + int.Parse(txtCuadras.Text)) <= MFinca.GetInstance().TamañoFinca())
                    {
                        rpta = MLote.GetInstance().GestionLote(txtNombre.Text, txtDescripcion.Text, txtCuadras.Text, Convert.ToInt32(txtId.Text), 2).ToString();
                        mensajeInformacion(rpta);
                        Limpiar();
                        tabBuscar.IsEnabled = true;
                        tabNuevo.Header = "NUEVO";
                        tabBuscar.Focus();
                        tblLotes.IsEnabled = true;

                        if (pnlResultados.IsVisible)
                        {
                            limpiarPantalla();
                        }
                        else
                        {
                            Mostrar();
                        }
                        frmTerrenos.GetInstance().mostrar();
                    }
                    else
                    {
                        mensajeError("EL tamaño de los lote sno puede ser superior al de la finca");
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
            tblLotes.IsEnabled = true;
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
            tblLotes.IsEnabled = false;

            Lote item = tblLotes.SelectedItem as Lote;

            txtNombre.Text = item.NombreLote;
            txtDescripcion.Text = item.Observaciones;
            txtCuadras.Text = item.Cuadras;
            txtId.Text = item.idLote.ToString();

            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Lote item = tblLotes.SelectedItem as Lote;

            string nombre = item.NombreLote;
            string descripcion = item.Observaciones;
            string cuadras = item.Cuadras;
            byte id = Convert.ToByte(item.idLote);

            var items = MLote.GetInstance().ConsultarArboles(id);

            if (items.Count == 0)
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

                    rpta = MLote.GetInstance().GestionLote(nombre, descripcion, cuadras, id, 3).ToString();
                    mensajeInformacion(rpta);

                    if (pnlResultados.IsVisible)
                    {
                        limpiarPantalla();
                    }
                    else
                    {
                        Mostrar();
                    }
                    frmArboles.GetInstance().mostrarCmb();
                    frmTerrenos.GetInstance().mostrar();
                }
            }
            else
            {
                mensajeError("No se puede Inhabilitar el lote porque tiene árboles asociados, debe eliminarlos atravez de una labor");
            }


        }

        private void btnRegistrarArboles_Click(object sender, RoutedEventArgs e)
        {
            Lote item = tblLotes.SelectedItem as Lote;
            byte id = Convert.ToByte(item.idLote);

            frmArboles.GetInstance().seleccionarLote(id);
            frmGestionTerrenos.GetInstance().tabArboles.Focus();
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

            Lote item = tblLotesIhhabilitados.SelectedItem as Lote;

            string nombre = item.NombreLote;
            string descripcion = item.Observaciones;
            string cuadras = item.Cuadras;
            byte id = Convert.ToByte(item.idLote);

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Habilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MLote.GetInstance().GestionLote(nombre, descripcion, cuadras, id, 4).ToString();
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

                frmArboles.GetInstance().mostrarCmb();
                frmTerrenos.GetInstance().mostrar();
            }
        }

        public ICommand textBoxButtonCmd => new RelayCommand(ExecuteSearch);

        private void ExecuteSearch(object o)
        {
            if (txtBuscarNombre.Text != string.Empty)
            {
                if (tblLotes.IsVisible)
                {
                    btnHabilitados.IsEnabled = false;
                    btnInhabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else if (tblLotesIhhabilitados.IsVisible)
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
