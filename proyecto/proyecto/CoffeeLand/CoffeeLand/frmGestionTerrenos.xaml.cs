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

        private void tabArboles_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                string tab = (sender as TabItem).Header as string;
                frmArboles misArboles = new frmArboles();

                switch (tab)
                {
                    case "ARBOLES":
                        contentArboles.Content = misArboles;
                        break;
                    default:
                        break;
                }
            }
          

        }
    }
}
