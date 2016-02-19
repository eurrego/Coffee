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
        frmProductos misProductos = new frmProductos();

        public frmAdministracionVentas()
        {
            InitializeComponent();
        }

        private void tabRegistroVentas_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tabProductos_GotFocus(object sender, RoutedEventArgs e)
        {
            contentProductos.Content = misProductos;
        }
    }
}
