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
        public frmAdministracionEmpleados()
        {
            InitializeComponent();
        }

        private void tabRegistroPrestamos_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            frmPrestamosEmpleados misPrestamos = new frmPrestamosEmpleados();
            contentPrestamos.Content = misPrestamos;
        }

        private void tabPagos_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            frmPagos misPagos = new frmPagos();
            contentPagos.Content = misPagos;
        }
    }
}
