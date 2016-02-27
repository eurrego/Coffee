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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Singleton

        private static MainWindow instance;

        public static MainWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new MainWindow();
            }

            return instance;
        }

        #endregion


        frmAdministracionCostos misCostos = new frmAdministracionCostos();
        frmAdministracionEmpleados misEmpleados = new frmAdministracionEmpleados();
        frmAdministracionVentas misVentas = new frmAdministracionVentas();
        frmGestionTerrenos misTerrenos = new frmGestionTerrenos();
        frmReportes misResportes = new frmReportes();
        frmInicio miInicio = new frmInicio();

        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            tamanioPAntalla();
            MainContainer.Content = miInicio;
            lblTitulo.Text = "Inicio";
            
        }

        private void tamanioPAntalla()
        {
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsOpen = true;
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Inicio";
            MainContainer.Content = miInicio;
            Menu.IsOpen = false;
        }

        private void btnGestionTerrenos_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Gestión de Terrenos";
            MainContainer.Content = misTerrenos;
            Menu.IsOpen = false;
        }

        private void btnAdministracionCostos_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Administración de Costos";
            misCostos.tabRegistroCompras.Focus();
            MainContainer.Content = misCostos;
            Menu.IsOpen = false;
        }

        private void btnAdministracionVentas_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Administración de Ventas";
            MainContainer.Content = misVentas;
            Menu.IsOpen = false;
        }

        private void btnAdministracionEmpleados_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Administración de Empleados";
            MainContainer.Content = misEmpleados;
            Menu.IsOpen = false;
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            lblTitulo.Text = "Reportes";
            MainContainer.Content = misResportes;
            Menu.IsOpen = false;
        }
    }
}
