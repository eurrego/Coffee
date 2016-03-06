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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Data;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmEstadoCuenta.xaml
    /// </summary>
    public partial class frmEstadoCuenta : UserControl
    {
        #region Singleton

        private static frmEstadoCuenta instance;

        public static frmEstadoCuenta GetInstance()
        {
            if (instance == null)
            {
                instance = new frmEstadoCuenta();
            }

            return instance;
        }

        #endregion

        int total;
        int idCompra;
        int init = 0;

        public frmEstadoCuenta()
        {
            InitializeComponent();
            instance = this;
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

            tblCompras.Height = height - 280;
            tblDetalleCompra.Height = height - 280;
        }

        public void Mostrar()
        {
            cmbProveedor.ItemsSource = MEstadoCuenta.GetInstance().ConsultarProveedor();
            cmbProveedor.SelectedIndex = 0;
        }

        private void cmbProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProveedor.SelectedIndex == 0)
            {
                tblCompras.ItemsSource = null;
                lblTotal.Text = "$0";
                lblSuperior.Text = "Seleccione un";
                lblInferior.Text = "Proveedor";
                pnlInicio.Visibility = Visibility.Visible;
                pnlData.Visibility = Visibility.Collapsed;
            }
            else
            {
                Proveedor item = cmbProveedor.SelectedItem as Proveedor;

                var data = MEstadoCuenta.GetInstance().ConsultaCompraProveedor(item.Nit) as IEnumerable;
                tblCompras.ItemsSource = data;
                TotalAdeuda(data);
                lblProveedor.Text = item.NombreProveedor;

                if (tblCompras.Items.Count == 0)
                {
                    lblSuperior.Text = "Este proveedor no tiene";
                    lblInferior.Text = "cuentas pendientes";
                    pnlInicio.Visibility = Visibility.Visible;
                    pnlData.Visibility = Visibility.Collapsed;
                }
                else
                {
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlData.Visibility = Visibility.Visible;
                }
            }
        }


        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        private void mensaje(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        public string quitarDecimales(string valor)
        {

            int dondeestalacoma = valor.IndexOf(',');

            string valorRecortado = valor.Substring(0, dondeestalacoma);

            return valorRecortado;

        }

        public void TotalAdeuda(IEnumerable data)
        {
            int total = 0;
            foreach (ComprasProveedor_Result1 valor in data)
            {
                total += int.Parse(quitarDecimales(valor.adeuda.ToString()));
            }
            lblTotal.Text = string.Format("{0:c}", total); 
        }

        private void btnAbonar_Click(object sender, RoutedEventArgs e)
        {
            tabAbono.Visibility = Visibility.Visible;
            tabAbono.Focus();
            tabBuscar.IsEnabled = false;
            txtAbono.Text = string.Empty;
            ComprasProveedor_Result1 item = tblCompras.SelectedItem as ComprasProveedor_Result1;
            idCompra = int.Parse(item.idCompra.ToString());
            total = int.Parse(quitarDecimales(item.adeuda.ToString()));
            tblCompras.IsEnabled = false;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtAbono.Text != string.Empty)
            {
                if (int.Parse(txtAbono.Text) <= total)
                {
                    string rpta = MEstadoCuenta.GetInstance().RegistroAbono(idCompra, decimal.Parse(txtAbono.Text), Convert.ToDateTime(dtdFechaAbono.SelectedDate), total);
                    Proveedor item = cmbProveedor.SelectedItem as Proveedor;

                    var data = MEstadoCuenta.GetInstance().ConsultaCompraProveedor(item.Nit) as IEnumerable;
                    tblCompras.ItemsSource = data;

                    tabBuscar.IsEnabled = true;
                    tabBuscar.Focus();
                    tabAbono.Visibility = Visibility.Collapsed;
                    mensaje(rpta);
                    tblCompras.IsEnabled = true;

                    TotalAdeuda(data);
                }
                else
                {
                    mensajeError("Debe ingresar un valor que no sea superior a lo que se adeuda");
                }
            }
            else
            {
                mensajeError("Debe ingresar un valor abonar");
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            tabAbono.Visibility = Visibility.Collapsed;
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            txtAbono.Text = string.Empty;
            tblCompras.IsEnabled = true;
        }

        private void btnDetalleCompra_Click(object sender, RoutedEventArgs e)
        {
            ComprasProveedor_Result1 item = tblCompras.SelectedItem as ComprasProveedor_Result1;

            lblFacturaDetalleCompra.Text = item.NumeroFactura.ToString();
            lblFechaDetalleCompra.Text = string.Format("{0:MMM d, yyyy}", item.Fecha);

            Proveedor itemProveedor = cmbProveedor.SelectedItem as Proveedor;

            lblProveedorDetalleCompra.Text = itemProveedor.NombreProveedor;

            lblTotalDetalleCompra.Text = string.Format("{0:c}", item.total);

            var detalle = MEstadoCuenta.GetInstance().ConsultaDetalleCompra(item.idCompra) as IEnumerable;

           

            if (detalle != null)
            {
                tblDetalleCompra.ItemsSource = detalle;

            }
            else
            {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("nombre");
                    dt.Columns.Add("cantidad");
                    dt.Columns.Add("valor");
                    dt.Columns.Add("subtotal");

                dt.Rows.Add("Beneficio","1", string.Format("{0:c}", item.total),string.Format("{0:c}", item.total));

                tblDetalleCompra.ItemsSource = dt.DefaultView;
            }

            
            tabDetalleCuenta.Focus();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            tabEstadoCuenta.Focus();
        }

        private void btnOcultar_Click(object sender, RoutedEventArgs e)
        {
            tblCompras.ItemsSource = null;
            lblTotal.Text = "$0";
            lblSuperior.Text = "Seleccione un";
            lblInferior.Text = "Proveedor";
            pnlInicio.Visibility = Visibility.Visible;
            pnlData.Visibility = Visibility.Collapsed;
            cmbProveedor.SelectedIndex = 0;
        }
    }
}
