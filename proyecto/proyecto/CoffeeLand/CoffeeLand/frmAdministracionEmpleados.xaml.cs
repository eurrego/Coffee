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
    /// Lógica de interacción para frmAdministracionEmpleados.xaml
    /// </summary>
    public partial class frmAdministracionEmpleados : UserControl
    {
        frmEmpleados misEmpleados = new frmEmpleados();
        frmPrestamosEmpleados misPrestamos = new frmPrestamosEmpleados();

        public frmAdministracionEmpleados()
        {
            InitializeComponent();
        }

        private void tabEmpleados_GotFocus(object sender, RoutedEventArgs e)
        {
            tabEmpleados.Content = misEmpleados;
        }

        private void tabRegistroPrestamos_GotFocus(object sender, RoutedEventArgs e)
        {
            tabRegistroPrestamos.Content = misPrestamos;
        }
    }
}
