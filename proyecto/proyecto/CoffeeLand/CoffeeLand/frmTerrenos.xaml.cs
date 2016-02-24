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
using System.Collections;
using System.Globalization;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Data;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmTerrenos.xaml
    /// </summary>
    public partial class frmTerrenos : UserControl
    {
        #region
        private static frmTerrenos instance;

        public static frmTerrenos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmTerrenos();
            }

            return instance;
        }

        #endregion

        public static DataTable dt;
        public static DataTable dt1;

        int init = 0;
        int init1 = 0;
        int opc = 0;
        int opc2 = 0;
        int index = -1;
        int cantidadArboles = 0;

        bool validacion = false;

        public frmTerrenos()
        {
            InitializeComponent();
            mostrar();
            tamanioPantalla();
            dtdFechaLabor.DisplayDateEnd = DateTime.Now;
        }

        public void mostrar()
        {
            tblLotes.ItemsSource = MTerrenos.GetInstance().ConsultarLote();
            tblLabores.ItemsSource = MTerrenos.GetInstance().ConsultarLabor();
            cmbInsumo.ItemsSource = MTerrenos.GetInstance().ConsultarInsumo();
            llenarCmbTipoPago();

        }

        private void llenarCmbTipoPago()
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un tipo de Pago");
            data.Add("Contrato");
            data.Add("Jornal");

            cmbTipoPago.ItemsSource = data;
        }

        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 2.25;
            pnlContainer.Width = anchoContainer;
            pnlContainerLabor.Width = anchoContainer;
            pnlContainerInsumos.Width = width / 1.75;

            tblLotes.Height = height - 285;
            columnLote.Width = anchoContainer - 70;
            tblLabores.Height = height - 285;
            columnLabor.Width = anchoContainer - 70;
            tblInsumos.Height = height - 285;

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

        private bool Validar(int opc )
        {

            switch (opc)
            {
                case 1:
                    if (cmbTipoPago.SelectedIndex == 0 || dtdFechaLabor.SelectedDate == null || lblLabores.Text.Equals("Seleccione un"))
                    {
                        mensajeError("Debe Ingresar todos los Campos");
                        validacion = false;
                    }
                    else
                    {
                        validacion = true;
                    }
                    break;
                case 2:
                    if (lblLote.Text.Equals("Seleccione un"))
                    {
                        mensajeError("Debe seleccionar un lote");
                        validacion = false;
                    }
                    else
                    {
                        validacion = true;
                    }
                    break;
                case 3:
                    if (cmbInsumo.SelectedIndex == 0 || txtCantidadInsumo.Text == string.Empty)
                    {
                        mensajeError("Debe Ingresar todos los Campos");
                        validacion = false;
                    }
                    else
                    {
                        validacion = true;
                    }

                    break;


                default:
                    break;
            }

            return validacion;
        }


        public void limpiarCampos(int opc)
        {
            switch (opc)
            {
                case 1:
                    cmbTipoPago.SelectedIndex = 0;
                    dtdFechaLabor.SelectedDate = null;
                    lblLabores.Text = "Seleccione un";
                    lblInicioLabores.Visibility = Visibility.Visible;
                    tblLabores.SelectedItem = null;
                    break;
                default:
                    break;
            }

        }

        public void crearTabla(int opc2)
        {

            switch (opc2)
            {
                case 1:

                    if (init == 0)
                    {
                        dt = new DataTable();
                        dt.Columns.Add("idLabor_Lote");
                        dt.Columns.Add("IdInsumo");
                        dt.Columns.Add("Cantidad");
                        dt.Columns.Add("Precio");
                        dt.Columns.Add("NombreInsumo");
                        init++;
                    }
                    break;

                case 2:

                    if (init1 == 0)
                    {
                        dt1 = new DataTable();
                        dt1.Columns.Add("DocumentoPersona");
                        dt1.Columns.Add("idLabor_Lote");
                        dt1.Columns.Add("Cantidad");
                        dt1.Columns.Add("Valor");
                        dt1.Columns.Add("NombrePersona");

                        init1++;
                    }
                    break;
            }
        }

        private void tblLotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = tblLotes.SelectedItem as MTerrenos;

            if (item != null)
            {
                var tolower = item.NombreLote.ToLower();
                lblLote.FontFamily = new FontFamily("Arial Rounded MT Bold");
                lblLote.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblLoteLabores.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);

                lblInicioLote.Visibility = Visibility.Hidden;

                lblTotalCuadras.Text = string.Format("{0:0,0}", item.Cuadras);

                if (item.Cantidad == 0)
                {
                    lblTotalArboles.Text = "0";
                }
                else
                {
                    lblTotalArboles.Text = string.Format("{0:0,0}", item.Cantidad);
                }
            }
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            lblLote.FontFamily = new FontFamily("Segoe UI");
            lblLote.Text = "Seleccione un";
            lblInicioLote.Visibility = Visibility.Visible;
            lblTotalArboles.Text = "0";
            lblTotalCuadras.Text = "0";

            tblLotes.SelectedItem = null;
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (Validar(2))
            {
                tabLabor.Focus();
                btnLabores.IsChecked = true;             
            }
        }

        
        private void btnSiguienteLabores_Click(object sender, RoutedEventArgs e)
        {
            if (Validar(1))
            {
                tabInsumo.Focus();
                btnInsumo.IsChecked = true;
            }
        }

        private void btnAtrasLabores_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos(1);
            tabLote.Focus();
            btnLabores.IsChecked = false;
        }

        private void tblLabores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = tblLabores.SelectedItem as Labor;

            if (item != null)
            {
                var tolower = item.NombreLabor.ToLower();
                lblLabores.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);

                lblInicioLabores.Visibility = Visibility.Hidden;
            }
        }

        private void btnAtrasLabor_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos(1);
        }

        private void cmbInsumo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Insumo insumo = cmbInsumo.SelectedItem as Insumo;

            if (cmbInsumo.SelectedIndex != 0)
            {
                var tolower = insumo.UnidadMedida.ToLower();
                txtUnidadMedida.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);

                lblStock.Text = insumo.CantidadExistente.ToString();
                var precioPromedio = insumo.PrecioPromedio;
            }
            else
            {
                txtUnidadMedida.Text = string.Empty;
                lblStock.Text = string.Empty;
            }
        }

        private void btnAgregarInsumos_Click(object sender, RoutedEventArgs e)
        {
            Insumo insumo = cmbInsumo.SelectedItem as Insumo;


            if (Validar(3))
            {
                crearTabla(1);

                if (insumo.CantidadExistente >= int.Parse(txtCantidadInsumo.Text))
                {
                    dt.Rows.Add(null, cmbInsumo.SelectedValue, txtCantidadInsumo.Text, insumo.PrecioPromedio, cmbInsumo.Text);

                    tblInsumos.ItemsSource = dt.DefaultView;
                }
                else
                {
                    mensajeError("La cantidad del insumo es menor a la ingresada");
                }


            }
            limpiarCampos(1);
        }
    }
}
