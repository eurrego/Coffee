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


        frmLotes misLotes = new frmLotes();
        frmLabores misLabores = new frmLabores();
        frmTipoArboles miTipoArboles = new frmTipoArboles();
        frmArboles misArboles = new frmArboles(0);
        frmTerrenos misTerrenos = new frmTerrenos();

        public frmGestionTerrenos()
        {
            InitializeComponent();
            instance = this;
        }

        private void tabLotes_GotFocus(object sender, RoutedEventArgs e)
        {
            tabLotes.Content = misLotes;
        }

        private void tabLabores_GotFocus(object sender, RoutedEventArgs e)
        {
            tabLabores.Content = misLabores;
        }

        private void tabTipoArboles_GotFocus(object sender, RoutedEventArgs e)
        {
            tabTipoArboles.Content = miTipoArboles;
        }

        private void tabArboles_GotFocus(object sender, RoutedEventArgs e)
        {
            tabArboles.Content = misArboles;
        }

        private void tabDetalleArboles_LostFocus(object sender, RoutedEventArgs e)
        {
            tabDetalleArboles.Visibility = Visibility.Collapsed;
        }

        private void tabRegistroTerrenos_GotFocus(object sender, RoutedEventArgs e)
        {
            tabRegistroTerrenos.Content = misTerrenos;
        }
    }
}
