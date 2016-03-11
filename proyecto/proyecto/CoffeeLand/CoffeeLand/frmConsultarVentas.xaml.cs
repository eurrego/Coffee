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

namespace CoffeeLand
{
    /// <summary>
    /// Interaction logic for frmConsultarVentas.xaml
    /// </summary>
    public partial class frmConsultarVentas : UserControl
    {
        #region Singleton

        private static frmConsultarVentas instance;

        public static frmConsultarVentas GetInstance()
        {
            if (instance == null)
            {
                instance = new frmConsultarVentas();
            }

            return instance;
        }
        #endregion


        public frmConsultarVentas()
        {
            InitializeComponent();
            instance = this;
            Mostrar();
            tamanioPantalla();
            dtdFecha.DisplayDateEnd = DateTime.Now;
            dtdFechaFinal.DisplayDateEnd = DateTime.Now;
        }

        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 1.75;
            var temp = anchoContainer - 185;

            pnlContainer.Width = anchoContainer;
            tblVentas.Height = height - 270;
        }

        public void Mostrar()
        {

            tblVentas.ItemsSource = MVentas.GetInstance().ConsultarVentas() as IEnumerable;

            if (tblVentas.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                lblSuperior.Text = "No hay ventas";
                lblInferior.Text = "registradas.";
            }
            else
            {
                pnlInicio.Visibility = Visibility.Collapsed;
                pnlData.Visibility = Visibility.Visible;
                gboxFiltrado.Visibility = Visibility.Visible;
                rbtNinguno.IsChecked = true;
                cantidadTotal();
            }
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

        private void cantidadTotal()
        {
            decimal valor = 0;

            foreach (var item in tblVentas.Items)
            {
                Type v = item.GetType();
                var total = v.GetProperty("Total").GetValue(item).ToString();

                valor += Convert.ToDecimal(total);

            }

            lblTotal.Text = string.Format("{0:c}", valor);
        }

        private void Registros()
        {
            if (tblVentas.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                lblSuperior.Text = "No hay Ventas que cumplan";
                lblInferior.Text = "con este Filtrado.";
            }
            else
            {
                pnlInicio.Visibility = Visibility.Collapsed;
                pnlData.Visibility = Visibility.Visible;
                gboxFiltrado.Visibility = Visibility.Visible;
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
                    tblVentas.ItemsSource = MVentas.GetInstance().ConsultaVentasFecha(Convert.ToDateTime(dtdFecha.SelectedDate), DateTime.Now) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
                else if (dtdFecha.SelectedDate != null && dtdFechaFinal.SelectedDate != null)
                {
                    tblVentas.ItemsSource = MVentas.GetInstance().ConsultaVentasFecha(Convert.ToDateTime(dtdFecha.SelectedDate), Convert.ToDateTime(dtdFechaFinal.SelectedDate)) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
                else if (dtdFecha.SelectedDate == null && dtdFechaFinal.SelectedDate == null)
                {
                    tblVentas.ItemsSource = MVentas.GetInstance().ConsultaVentasFecha(Convert.ToDateTime("01/01/1970"), DateTime.Now) as IEnumerable;
                    Registros();
                    cantidadTotal();
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
                    tblVentas.ItemsSource = MVentas.GetInstance().ConsultaVentasFecha(Convert.ToDateTime("01/01/1970"), Convert.ToDateTime(dtdFechaFinal.SelectedDate)) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
                else
                {
                    tblVentas.ItemsSource = MVentas.GetInstance().ConsultaVentasFecha(Convert.ToDateTime(dtdFecha.SelectedDate), Convert.ToDateTime(dtdFechaFinal.SelectedDate)) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
            }
        }

        private void rbtFecha_Checked(object sender, RoutedEventArgs e)
        {
            dtdFecha.SelectedDate = null;
            dtdFechaFinal.SelectedDate = null;
            gboxFecha.Visibility = Visibility.Visible;
            tblVentas.ItemsSource = MVentas.GetInstance().ConsultarVentas() as IEnumerable;
            Registros();
            cantidadTotal();
        }

        private void rbtNinguno_Checked(object sender, RoutedEventArgs e)
        {
            rbtFecha.IsChecked = false;
            gboxFecha.Visibility = Visibility.Collapsed;
            dtdFecha.SelectedDate = null;
            dtdFechaFinal.SelectedDate = null;
            tblVentas.ItemsSource = MVentas.GetInstance().ConsultarVentas() as IEnumerable;
            Registros();
            cantidadTotal();
        }
    }
}
