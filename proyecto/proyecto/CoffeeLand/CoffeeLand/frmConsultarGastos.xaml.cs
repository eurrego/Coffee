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
    /// Lógica de interacción para frmConsultarGastos.xaml
    /// </summary>
    public partial class frmConsultarGastos : UserControl
    {
        #region Singleton

        private static frmConsultarGastos instance;

        public static frmConsultarGastos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmConsultarGastos();
            }

            return instance;
        }
        #endregion

        public frmConsultarGastos()
        {
            InitializeComponent();
            Mostrar();
            instance = this;
            tamanioPantalla();
            rbtNinguno.IsChecked = true;
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

            columnNombreConcepto.Width = temp / 4;
            columnDescripcion.Width = temp / 4;
            columnValor.Width = temp / 4;
            columnFecha.Width = temp / 4;

            pnlContainer.Width = anchoContainer;
            tblGastos.Height = height - 270;
        }

        public void Mostrar()
        {

            tblGastos.ItemsSource = MGastos.GetInstance().ConsultaGastos() as IEnumerable;

            if (tblGastos.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                lblSuperior.Text = "No hay gastos";
                lblInferior.Text = "registrados.";
            }
            else
            {
                pnlInicio.Visibility = Visibility.Collapsed;
                pnlData.Visibility = Visibility.Visible;
                gboxFiltrado.Visibility = Visibility.Visible;
                cantidadTotal();
            }
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

        private void cantidadTotal()
        {
            decimal valor = 0;

            foreach (var item in tblGastos.Items)
            {
                Type v = item.GetType();
                var total = v.GetProperty("Valor").GetValue(item).ToString();

                valor += Convert.ToDecimal(total);

            }

            lblTotal.Text = string.Format("{0:c}", valor);
        }

        private void Registros()
        {
            if (tblGastos.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                lblSuperior.Text = "No hay Gastos que cumplan";
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
                    tblGastos.ItemsSource = MGastos.GetInstance().ConsultaGastosFecha(Convert.ToDateTime(dtdFecha.SelectedDate), DateTime.Now) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
                else if (dtdFecha.SelectedDate != null && dtdFechaFinal.SelectedDate != null)
                {
                    tblGastos.ItemsSource = MGastos.GetInstance().ConsultaGastosFecha(Convert.ToDateTime(dtdFecha.SelectedDate), Convert.ToDateTime(dtdFechaFinal.SelectedDate)) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
                else if (dtdFecha.SelectedDate == null && dtdFechaFinal.SelectedDate == null)
                {
                    tblGastos.ItemsSource = MGastos.GetInstance().ConsultaGastosFecha(Convert.ToDateTime("01/01/1970"), DateTime.Now) as IEnumerable;
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
                    tblGastos.ItemsSource = MGastos.GetInstance().ConsultaGastosFecha(Convert.ToDateTime("01/01/1970"), Convert.ToDateTime(dtdFechaFinal.SelectedDate)) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
                else
                {
                    tblGastos.ItemsSource = MGastos.GetInstance().ConsultaGastosFecha(Convert.ToDateTime(dtdFecha.SelectedDate), Convert.ToDateTime(dtdFechaFinal.SelectedDate)) as IEnumerable;
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
            tblGastos.ItemsSource = MGastos.GetInstance().ConsultaGastos() as IEnumerable;
            Registros();
            cantidadTotal();
        }

        private void rbtNinguno_Checked(object sender, RoutedEventArgs e)
        {
            rbtFecha.IsChecked = false;
            gboxFecha.Visibility = Visibility.Collapsed;
            dtdFecha.SelectedDate = null;
            dtdFechaFinal.SelectedDate = null;
            tblGastos.ItemsSource = MGastos.GetInstance().ConsultaGastos() as IEnumerable;
            Registros();
            cantidadTotal(); 
        }
    }
}
