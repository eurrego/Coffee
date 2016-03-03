using System;
using System.Collections.Generic;
using System.Data;
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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmPagos.xaml
    /// </summary>
    public partial class frmPagos : UserControl
    {
        DataTable auxiliar = new DataTable();

        public frmPagos()
        {
            InitializeComponent();
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

            tblPagos.Height = height - 285;
        }

        private void cmbTipoEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoEmpleado.SelectedIndex == 0)
            {
                tblPagos.ItemsSource = null;
                tblPagos.Items.Refresh();
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                pnlSinRegistros.Visibility = Visibility.Collapsed;
                lblTotal.Text = "$0";

                //btnAbonos.IsEnabled = false;
                //btnGuardar.IsEnabled = false;
            }
            else if (cmbTipoEmpleado.SelectedIndex == 1)//consultar persona permanente
            {
                auxiliar.Reset();
                auxiliar.Columns.Add("DocumentoPersona");
                auxiliar.Columns.Add("Nombre");
                auxiliar.Columns.Add("Valor_a_pagar");
                auxiliar.Columns.Add("Valor_Deuda");

                tblPagos.ItemsSource = MPagos.GetInstance().ConsultarSalario(2) as IEnumerable;

                if (tblPagos.Items.Count != 0)
                {
                    pnlData.Visibility = Visibility.Visible;
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlSinRegistros.Visibility = Visibility.Collapsed;
                    lblTotal.Text = "$0";
                }
                else
                {
                    pnlData.Visibility = Visibility.Collapsed;
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlSinRegistros.Visibility = Visibility.Visible;
                    lblTotal.Text = "$0";
                }

            }
            else if (cmbTipoEmpleado.SelectedIndex == 2)//consultar persona temporal
            {
                tblPagos.ItemsSource = MPagos.GetInstance().ConsultarSalario(1) as IEnumerable;

                if (tblPagos.Items.Count != 0)
                {
                    double valor = 0;

                    foreach (var item in tblPagos.Items)
                    {
                        Type v = item.GetType();
                        var cantidad = v.GetProperty("Valor_a_pagar").GetValue(item);
                        valor += Convert.ToDouble(cantidad);
                    }

                    lblTotal.Text = string.Format("{0:$0,0}", valor);

                    pnlData.Visibility = Visibility.Visible;
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlSinRegistros.Visibility = Visibility.Collapsed;
                }
                else
                {
                    pnlData.Visibility = Visibility.Collapsed;
                    pnlSinRegistros.Visibility = Visibility.Visible;
                    lblTotal.Text = "$0";
                }
            }
        }
    }
}
