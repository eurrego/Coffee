using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmLabores.xaml
    /// </summary>
    public partial class frmLabores : UserControl
    {
        bool validacion = false;

        public frmLabores()
        {
            InitializeComponent();
            Mostrar();
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        // Define el estilo de las celdas 
        private void CantidadRegistros()
        {
            lblRegistros.Text = tblLabores.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (txtNombre.Text == string.Empty || txtDescripcion.Text == string.Empty || validarGroupBox() == false)
            {
                mensajeError("Debe Ingresar todos los Campos");
                validacion = false;
            }
            else
            {
                validacion = true;
            }

            return validacion;
        }


        // validar group box 
        private bool validarGroupBox()
        {
            bool pase1 = false;
            bool pase2 = false;

            if (rbtnArbolesSi.IsChecked == true || rbtnArbolesNo.IsChecked == true)
            {
                pase1 = true;
            }

            if (rbtnInsumoSi.IsChecked == true || rbtnInsumoNo.IsChecked == true)
            {
                pase2 = true;
            }

            if (pase1 == true && pase2 == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // limpiar Controles
        private void Limpiar()
        {

            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;
            rbtnArbolesNo.IsChecked = true;
            rbtnInsumoNo.IsChecked = true;
        }

        //mostrar
        private void Mostrar()
        {
            tblLabores.ItemsSource = MLabores.GetInstance().consultarLabor();
            CantidadRegistros();
            lblActivos.Text = (MLabores.GetInstance().ConsultarInactivos().Count).ToString();
        }


        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblLabores.ItemsSource = MLabores.GetInstance().buscarLabor(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (IsValid(txtNombre) && IsValid(txtDescripcion))
                {
                    rpta = MLabores.GetInstance().GestionLabor(txtNombre.Text, Convert.ToBoolean(rbtnArbolesSi.IsChecked), Convert.ToBoolean(rbtnInsumoSi.IsChecked), txtDescripcion.Text, 0, 1);
                    mensajeError(rpta);
                    Limpiar();
                    tabBuscar.Focus();
                }
            }
            else
            {
                if (IsValid(txtNombre) && IsValid(txtDescripcion))
                {
                    rpta = MLabores.GetInstance().GestionLabor(txtNombre.Text, Convert.ToBoolean(rbtnArbolesSi.IsChecked), Convert.ToBoolean(rbtnInsumoSi.IsChecked), txtDescripcion.Text, Convert.ToInt32(txtId.Text), 2);
                    mensajeError(rpta);
                    Limpiar();
                    tabBuscar.IsEnabled = true;
                    tabNuevo.Header = "NUEVO";
                    tabBuscar.Focus();
                    tblLabores.IsEnabled = true;
                }
            }
            Mostrar();
        }



        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Labor item = tblLabores.SelectedItem as Labor;

            txtId.Text = item.idLabor.ToString();
            txtNombre.Text = item.NombreLabor;
            txtDescripcion.Text = item.Descripcion;


            if (item.ModificaArboles == true)
            {
                rbtnArbolesSi.IsChecked = true;
                rbtnArbolesNo.IsChecked = false;
            }
            else
            {
                rbtnArbolesSi.IsChecked = false;
                rbtnArbolesNo.IsChecked = true;
            }

            if (item.RequiereInsumo == true)
            {
                rbtnInsumoSi.IsChecked = true;
                rbtnInsumoNo.IsChecked = false;
            }
            else
            {
                rbtnInsumoSi.IsChecked = false;
                rbtnInsumoNo.IsChecked = true;
            }

            tblLabores.IsEnabled = false;
            tabBuscar.IsEnabled = false;
            tabNuevo.Header = "EDITAR";
            tabNuevo.Focus();
        }



        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            tabNuevo.Header = "NUEVO";
            rbtnArbolesNo.IsChecked = true;
            rbtnInsumoNo.IsChecked = true;
            tblLabores.IsEnabled = true;
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Labor item = tblLabores.SelectedItem as Labor;

            int id = item.idLabor;
            string nombre = item.NombreLabor;
            string descripcion = item.Descripcion;
            bool insumo = item.RequiereInsumo;
            bool modifica = item.ModificaArboles;


            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",

            };

            MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                string rpta = "";

                rpta = MLabores.GetInstance().GestionLabor(nombre, insumo, modifica, descripcion, id, 3).ToString();
                mensajeError(rpta);
                Mostrar();
            }
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }

    }
}
