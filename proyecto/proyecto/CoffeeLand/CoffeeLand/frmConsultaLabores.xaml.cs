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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections;

namespace CoffeeLand
{
    /// <summary>
    /// Interaction logic for frmConsultaLabores.xaml
    /// </summary>
    public partial class frmConsultaLabores : UserControl
    {
        #region Singleton

        private static frmConsultaLabores instance;

        public static frmConsultaLabores GetInstance()
        {
            if (instance == null)
            {
                instance = new frmConsultaLabores();
            }

            return instance;
        }

        #endregion

        public frmConsultaLabores()
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

            var anchoContainer = width / 2;
            var temp = anchoContainer - 185;

            pnlContainer.Width = anchoContainer;
            tblLabores.Height = height - 270;
        }

        public void Mostrar()
        {
            cmbLote.ItemsSource = MTerrenos.GetInstance().ConsultarLoteCmb();
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
            lblTotal.Text = tblLabores.Items.Count.ToString();
        }

        private void Registros()
        {
            if (tblLabores.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicioPrincipal.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                lblSuperior.Text = "No hay registros que cumplan";
                lblInferior.Text = "con este Filtrado.";
            }
            else
            {
                pnlInicio.Visibility = Visibility.Collapsed;
                pnlInicioPrincipal.Visibility = Visibility.Collapsed;
                pnlData.Visibility = Visibility.Visible;
                gboxFiltrado.Visibility = Visibility.Visible;
            }
        }

        private void dtdFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Lote item = cmbLote.SelectedItem as Lote;
            var id = item.idLote;

            if (dtdFecha.SelectedDate > dtdFechaFinal.SelectedDate)
            {
                mensajeError("La fecha final no debe ser inferior a la fecha inicial");
                dtdFecha.SelectedDate = null;
            }
            else
            {
                if (dtdFecha.SelectedDate != null && dtdFechaFinal.SelectedDate == null)
                {
                    tblLabores.ItemsSource = MTerrenos.GetInstance().ConsultaLaboresFecha(Convert.ToDateTime(dtdFecha.SelectedDate), DateTime.Now, id) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
                else if (dtdFecha.SelectedDate != null && dtdFechaFinal.SelectedDate != null)
                {
                    tblLabores.ItemsSource = MTerrenos.GetInstance().ConsultaLaboresFecha(Convert.ToDateTime(dtdFecha.SelectedDate), Convert.ToDateTime(dtdFechaFinal.SelectedDate), id) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
                else if (dtdFecha.SelectedDate == null && dtdFechaFinal.SelectedDate == null)
                {
                    tblLabores.ItemsSource = MTerrenos.GetInstance().ConsultaLaboresFecha(Convert.ToDateTime("01/01/1970"), DateTime.Now, id) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
            }
        }

        private void dtdFechaFinal_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Lote item = cmbLote.SelectedItem as Lote;
            var id = item.idLote;

            if (dtdFechaFinal.SelectedDate < dtdFecha.SelectedDate)
            {
                mensajeError("La fecha final no debe ser inferior la fecha inicial");
                dtdFechaFinal.SelectedDate = null;
            }
            else
            {
                if (dtdFecha.SelectedDate == null && dtdFechaFinal.SelectedDate != null)
                {
                    tblLabores.ItemsSource = MTerrenos.GetInstance().ConsultaLaboresFecha(Convert.ToDateTime("01/01/1970"), Convert.ToDateTime(dtdFechaFinal.SelectedDate), id) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
                else
                {
                    tblLabores.ItemsSource = MTerrenos.GetInstance().ConsultaLaboresFecha(Convert.ToDateTime(dtdFecha.SelectedDate), Convert.ToDateTime(dtdFechaFinal.SelectedDate), id) as IEnumerable;
                    Registros();
                    cantidadTotal();
                }
            }
        }

        private void rbtFecha_Checked(object sender, RoutedEventArgs e)
        {
            Lote item = cmbLote.SelectedItem as Lote;
            var id = item.idLote;

            dtdFecha.SelectedDate = null;
            dtdFechaFinal.SelectedDate = null;
            gboxFecha.Visibility = Visibility.Visible;
            tblLabores.ItemsSource = MTerrenos.GetInstance().consultarLabores(id) as IEnumerable;
            Registros();
            cantidadTotal();
        }

        private void rbtNinguno_Checked(object sender, RoutedEventArgs e)
        {
            Lote item = cmbLote.SelectedItem as Lote;
            var id = item.idLote;

            rbtFecha.IsChecked = false;
            gboxFecha.Visibility = Visibility.Collapsed;
            dtdFecha.SelectedDate = null;
            dtdFechaFinal.SelectedDate = null;
            tblLabores.ItemsSource = MTerrenos.GetInstance().consultarLabores(id) as IEnumerable;
            Registros();
            cantidadTotal();
        }

        private void cmbLote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLote.SelectedIndex == 0)
            {
                tblLabores.ItemsSource = null;
                lblTotal.Text = "0";
                pnlInicioPrincipal.Visibility = Visibility.Visible;
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Collapsed;
                gboxFiltrado.Visibility = Visibility.Collapsed;
                gboxFecha.Visibility = Visibility.Collapsed;

            }
            else
            {
                Lote item = cmbLote.SelectedItem as Lote;
                var id = item.idLote;
                tblLabores.ItemsSource = MTerrenos.GetInstance().consultarLabores(id) as IEnumerable;

                if (tblLabores.Items.Count == 0)
                {
                    pnlData.Visibility = Visibility.Collapsed;
                    pnlInicioPrincipal.Visibility = Visibility.Collapsed;
                    pnlInicio.Visibility = Visibility.Visible;
                    lblSuperior.Text = "No hay labores";
                    lblInferior.Text = "registradas";
                }
                else
                {
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlInicioPrincipal.Visibility = Visibility.Collapsed;
                    pnlData.Visibility = Visibility.Visible;
                    gboxFiltrado.Visibility = Visibility.Visible;
                    rbtNinguno.IsChecked = true;
                    cantidadTotal();
                }
            }
        }
    }
}
