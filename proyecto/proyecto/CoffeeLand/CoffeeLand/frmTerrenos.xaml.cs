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


        public frmTerrenos()
        {
            InitializeComponent();
            mostrar();
            tamanioPantalla();
        }

        public void mostrar()
        {
            tblLotes.ItemsSource = MTerrenos.GetInstance().ConsultarLote();
            tblLabores.ItemsSource = MTerrenos.GetInstance().ConsultarLabor();
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

            tblLotes.Height = height - 285;
            tblLabores.Height = height - 285;

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

        private void tblLotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = tblLotes.SelectedItem as MTerrenos;

            if (item != null)
            {
                var tolower = item.NombreLote.ToLower();
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
            lblLote.Text = "Seleccione un";
            lblInicioLote.Visibility = Visibility.Visible;
            lblTotalArboles.Text = "0";
            lblTotalCuadras.Text = "0";

            tblLotes.SelectedItem = null;
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (lblLote.Text.Equals("Seleccione un"))
            {
                mensajeError("Debe seleccionar un lote");
            }
            else
            {
                tabLabor.Focus();
            }
        }
    }
}
