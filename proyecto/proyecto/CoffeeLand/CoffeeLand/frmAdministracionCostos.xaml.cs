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
using MahApps.Metro.Controls.Dialogs;


namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmAdministracionCostos.xaml
    /// </summary>
    public partial class frmAdministracionCostos : UserControl
    {
        #region Singleton

        private static frmAdministracionCostos instance;

        public static frmAdministracionCostos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmAdministracionCostos();
            }

            return instance;
        }

        #endregion

        public frmAdministracionCostos()
        {
            InitializeComponent();
            instance = this;
        }

        private void tabRegistroCompras_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            frmCompra miCompra = new frmCompra();
            contentRegistroCompra.Content = miCompra;
        }

        private void tabRegistroGastos_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            frmGastos miGasto = new frmGastos();
            contentRegistroGasto.Content = miGasto;

        }

        private void tabConsulta_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            frmEstadoCuenta misCuentas = new frmEstadoCuenta();
            contentCuentas.Content = misCuentas;
        }

        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            frmExistencias misExistencias = new frmExistencias();
            contentExistencias.Content = misExistencias;
        }
    }
}
