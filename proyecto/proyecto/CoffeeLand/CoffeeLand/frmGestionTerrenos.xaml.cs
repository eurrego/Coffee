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
    /// Lógica de interacción para frmGestionTerrenos.xaml
    /// </summary>
    public partial class frmGestionTerrenos : UserControl
    {
        #region Singleton

        private static frmGestionTerrenos instance;

        public static frmGestionTerrenos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmGestionTerrenos();
            }

            return instance;
        }

        #endregion

        

        public frmGestionTerrenos()
        {
            InitializeComponent();
            instance = this;
            
        }

        private void tabRegistroTerrenos_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            frmTerrenos misTerrenos = new frmTerrenos();
            contentRegistroTerrenos.Content = misTerrenos;
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            tabRegistroTerrenos.Visibility = Visibility.Visible;
        }

        private void tabArboles_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;

            switch (tabItem)
            {
                case "ARBOLES":
                    frmArboles misArboles = new frmArboles();
                    contentArboles.Content = misArboles;
                    break;
            }
        }
    }
}
