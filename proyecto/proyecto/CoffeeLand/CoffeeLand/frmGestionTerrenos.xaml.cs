﻿using System;
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

        private void tabArboles_Selected(object sender, RoutedEventArgs e)
        {
            frmArboles misArboles = new frmArboles();
            contentArboles.Content = misArboles;
        }

        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            frmTerrenos misTerrenos = new frmTerrenos();
            contentRegistroTerrenos.Content = misTerrenos;
        }

        private void tabProduccionTerrenos_Selected(object sender, RoutedEventArgs e)
        {
            frmConsultaProduccion miProduccion = new frmConsultaProduccion();
            contentproduccionTerrenos.Content = miProduccion;
        }

        private void tabRegistroTerrenos_Selected(object sender, RoutedEventArgs e)
        {
            frmTerrenos misTerrenos = new frmTerrenos();
            contentRegistroTerrenos.Content = misTerrenos;
        }

        private void tabLaboresTerrenos_Selected(object sender, RoutedEventArgs e)
        {
            frmConsultaLabores misLabores = new frmConsultaLabores();
            contentLaboresTerrenos.Content = misLabores;
        }
    }
}
