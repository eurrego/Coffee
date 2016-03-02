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

        private void tabRegistroVentas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            frmVentas miVenta = new frmVentas();
            contentRegistroVenta.Content = miVenta;

        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            tabRegistroVentas.Visibility = Visibility.Visible;
        }
    }
}
