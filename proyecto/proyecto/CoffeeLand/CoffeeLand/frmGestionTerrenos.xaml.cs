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

    }
}
