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
using System.Collections.ObjectModel;
using CoffeeLand.Validator;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmConsultaCompras.xaml
    /// </summary>
    public partial class frmConsultaCompras : UserControl
    {
        #region Singleton

        private static frmConsultaCompras instance;

        public static frmConsultaCompras GetInstance()
        {
            if (instance == null)
            {
                instance = new frmConsultaCompras();
            }

            return instance;
        }
        #endregion


        public frmConsultaCompras()
        {
            InitializeComponent();
            instance = this;
            Mostrar();
            dtdFecha.DisplayDateEnd = DateTime.Now;
            dtdFechaFinal.DisplayDateEnd = DateTime.Now;
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
            tblCompra.Height = height - 270;

        }

        public void Mostrar()
        {

            tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 4) as IEnumerable;

            if (tblCompra.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                lblSuperior.Text = "No hay compras";
                lblInferior.Text = "registradas.";
            }
            else
            {
                pnlInicio.Visibility = Visibility.Collapsed;
                pnlData.Visibility = Visibility.Visible;
                gboxFiltrado.Visibility = Visibility.Visible;
                rbtNinguno.IsChecked = true;
            }

            cantidadTotal();
        }

        private void cantidadTotal()
        {
            decimal valor = 0;

            foreach (ComprasProveedor_Result1 item in tblCompra.Items)
            {
                
                var total = item.total;

                if (item.EstadoCuenta.Equals("D"))
                {
                    valor += Convert.ToDecimal(total);
                }
            }

            lblTotal.Text = string.Format("{0:c}", valor);
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

        private void Registros()
        {
            if (tblCompra.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                lblSuperior.Text = "No hay compras que cumplan";
                lblInferior.Text = "con este Filtrado.";
            }
            else
            {
                pnlInicio.Visibility = Visibility.Collapsed;
                pnlData.Visibility = Visibility.Visible;
                gboxFiltrado.Visibility = Visibility.Visible;
            }
        }


        private void chkDebe_Checked(object sender, RoutedEventArgs e)
        {
            if (chkDebe.IsChecked == true && chkPagado.IsChecked == true)
            {

                tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 4) as IEnumerable;
                Registros();
            }
            else 
            {
                tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 3) as IEnumerable;
                Registros();
            }
        }

        private void chkPagado_Checked(object sender, RoutedEventArgs e)
        {
            if (chkDebe.IsChecked == true && chkPagado.IsChecked == true)
            {
                tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 4) as IEnumerable;
                Registros();
            }
            else
            {
                tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 2) as IEnumerable;
                Registros();
            }
        }

        private void chkPagado_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkDebe.IsChecked == true)
            {
                tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 3) as IEnumerable;
                Registros();
            }
            else
            {
                tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 4) as IEnumerable;
                Registros();
            }
        }

        private void chkDebe_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkPagado.IsChecked == true)
            {
                tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 2) as IEnumerable;
                Registros();
            }
            else
            {
                tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 4) as IEnumerable;
                Registros();
            }
        }

        private void dtdFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dtdFecha.SelectedDate > dtdFechaFinal.SelectedDate)
            {
                mensajeError("La fecha final no debe ser inferior a la fecha inicial");
                dtdFecha.SelectedDate = null;
            }
            else
            {
                if (dtdFecha.SelectedDate != null && dtdFechaFinal.SelectedDate == null)
                {
                    tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(Convert.ToDateTime(dtdFecha.SelectedDate), DateTime.Now ,5) as IEnumerable;
                    Registros();
                }
                else if (dtdFecha.SelectedDate != null && dtdFechaFinal.SelectedDate != null)
                {
                    tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(Convert.ToDateTime(dtdFecha.SelectedDate), Convert.ToDateTime(dtdFechaFinal.SelectedDate), 5) as IEnumerable;
                    Registros();
                }
                else if (dtdFecha.SelectedDate == null && dtdFechaFinal.SelectedDate == null)
                {
                    tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(Convert.ToDateTime("01/01/1970"), DateTime.Now, 5) as IEnumerable;
                    Registros();
                }
            }
        }

        private void dtdFechaFinal_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dtdFechaFinal.SelectedDate < dtdFecha.SelectedDate)
            {
                mensajeError("La fecha final no debe ser inferior la fecha inicial");
                dtdFechaFinal.SelectedDate = null;
            }
            else
            {
                if (dtdFecha.SelectedDate == null && dtdFechaFinal.SelectedDate != null)
                {
                    tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(Convert.ToDateTime("01/01/1970"), Convert.ToDateTime(dtdFechaFinal.SelectedDate), 5) as IEnumerable;
                    Registros();
                }
                else 
                {
                    tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(Convert.ToDateTime("01/01/1970"), DateTime.Now, 5) as IEnumerable;
                    Registros();
                }
            } 
        }

        private void rbtEstado_Checked(object sender, RoutedEventArgs e)
        {
            rbtFecha.IsChecked = false;
            gboxEstado.Visibility = Visibility.Visible;
            gboxFecha.Visibility = Visibility.Collapsed;
            tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 4) as IEnumerable;
            Registros();
        }

        private void rbtFecha_Checked(object sender, RoutedEventArgs e)
        {
            dtdFecha.SelectedDate = null;
            dtdFechaFinal.SelectedDate = null;
            rbtEstado.IsChecked = false;
            gboxEstado.Visibility = Visibility.Collapsed;
            gboxFecha.Visibility = Visibility.Visible;
            chkDebe.IsChecked = false;
            chkPagado.IsChecked = false;
            tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 4) as IEnumerable;
            Registros();
        }

        private void rbtNinguno_Checked(object sender, RoutedEventArgs e)
        {
            rbtFecha.IsChecked = false;
            rbtEstado.IsChecked = false;
            gboxEstado.Visibility = Visibility.Collapsed;
            gboxFecha.Visibility = Visibility.Collapsed;
            dtdFecha.SelectedDate = null;
            dtdFechaFinal.SelectedDate = null;
            chkDebe.IsChecked = false;
            chkPagado.IsChecked = false;
            tblCompra.ItemsSource = MCompra.GetInstance().ConsultaComprasEstadoCuenta(DateTime.Now, DateTime.Now, 4) as IEnumerable;
            Registros();
        }

    }
}
