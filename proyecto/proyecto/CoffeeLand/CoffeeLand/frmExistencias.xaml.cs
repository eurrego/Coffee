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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using CoffeeLand.Validator;

namespace CoffeeLand
{
    /// <summary>
    /// Interaction logic for frmExistencias.xaml
    /// </summary>
    public partial class frmExistencias : UserControl
    {
        #region Singleton

        private static frmExistencias instance;

        public static frmExistencias GetInstance()
        {
            if (instance == null)
            {
                instance = new frmExistencias();
            }

            return instance;
        }
        #endregion

        public frmExistencias()
        {
            InitializeComponent();
            instance = this;
            DataContext = this;
            Mostrar();
            tamanioPantalla();
        }

        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 1.75;
            pnlContainer.Width = anchoContainer;

            tblInsumo.Height = height - 270;
        }

        //mostrar
        public void Mostrar()
        {
            tblInsumo.ItemsSource = MInsumo.GetInstance().ConsultarInsumo();

            if (tblInsumo.Items.Count != 0)
            {
                pnlHabilitados.Visibility = Visibility.Visible;
                pnlSinRegistros.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblSinRegistros.Text = "disponibles.";
                pnlSinRegistros.Visibility = Visibility.Visible;
                pnlHabilitados.Visibility = Visibility.Collapsed;
            }

            CantidadRegistros();
        }

        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblInsumo.ItemsSource = MInsumo.GetInstance().ConsultarParametroInsumo(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        private void CantidadRegistros()
        {
            lblRegistros.Text = tblInsumo.Items.Count.ToString();
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        public void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        public ICommand textBoxButtonCmd => new RelayCommand(ExecuteSearch);

        private void ExecuteSearch(object o)
        {
            if (txtBuscarNombre.Text != string.Empty)
            {
                if (tblInsumo.IsVisible)
                {
                    btnHabilitados.IsEnabled = false;

                    BuscarNombre();
                    lblBusqueda.Text = txtBuscarNombre.Text.ToUpper();
                    pnlResultados.Visibility = Visibility.Visible;
                    txtBuscarNombre.Text = string.Empty;
                }
                else
                {
                    mensajeInformacion("No tiene registros dispobibles para realizar una búsqueda");
                    txtBuscarNombre.Text = string.Empty;
                }
            }
            else
            {
                mensajeError("Debe ingresar una palabra a buscar");
            }
        }


        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
        }

        private void limpiarPantalla()
        {
            btnHabilitados.IsEnabled = true;
            tabBuscar.Focus();
            pnlResultados.Visibility = Visibility.Collapsed;
            lblBusqueda.Text = string.Empty;
            Mostrar();
        }


    }
}
