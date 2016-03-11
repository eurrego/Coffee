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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmAdministracionVentas.xaml
    /// </summary>
    public partial class frmAdministracionVentas : UserControl
    {

        public frmAdministracionVentas()
        {
            InitializeComponent();
        }

        private void tabRegistroVentas_Selected(object sender, RoutedEventArgs e)
        {
            frmVentas miVenta = new frmVentas();
            contentRegistroVenta.Content = miVenta;
        }

        private void tabConsultarVentas_Selected(object sender, RoutedEventArgs e)
        {
            frmConsultarVentas miConsulta = new frmConsultarVentas();
            contentConsultarVenta.Content = miConsulta;
        }
    }
}
