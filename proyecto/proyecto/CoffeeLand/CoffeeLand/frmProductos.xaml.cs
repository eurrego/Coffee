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
    /// Lógica de interacción para frmProductos.xaml
    /// </summary>
    public partial class frmProductos : UserControl
    {
        bool validacion;

        public frmProductos()
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
            lblRegistros.Text = tblProductos.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (txtNombre.Text == string.Empty || txtDescripcion.Text == string.Empty)
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

        // limpiar Controles
        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        {
            tblProductos.ItemsSource = MProducto.GetInstance().ConsultarProducto();
            lblActivos.Text = (MProducto.GetInstance().ConsultarInactivos().Count).ToString();
            CantidadRegistros();
        }


        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblProductos.ItemsSource = MProducto.GetInstance().ConsultarParametroProducto(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (IsValid(txtNombre) && IsValid(txtDescripcion))
                {
                    rpta = MProducto.GetInstance().GestionProducto(txtNombre.Text, txtDescripcion.Text, 0, 1).ToString();
                    mensajeError(rpta);
                    Limpiar();
                    tabBuscar.Focus();
                }
            }
            else if (IsValid(txtNombre) && IsValid(txtDescripcion))
            {
                rpta = MProducto.GetInstance().GestionProducto(txtNombre.Text, txtDescripcion.Text, Convert.ToByte(txtId.Text), 2).ToString();
                mensajeError(rpta);
                Limpiar();
                tabBuscar.IsEnabled = true;
                tabNuevo.Header = "NUEVO";
                tabBuscar.Focus();
            }
            Mostrar();
        }

        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Producto item = tblProductos.SelectedItem as Producto;

            txtId.Text = item.idProducto.ToString();
            txtNombre.Text = item.NombreProducto;
            txtDescripcion.Text = item.Descripcion;

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
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }


        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Producto item = tblProductos.SelectedItem as Producto;

            byte id = item.idProducto;
            string nombre = item.NombreProducto;
            string descripcion = item.Descripcion;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
            };

            var result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.Equals(MessageDialogResult.Affirmative))
            {
                string rpta = "";

                rpta = MProducto.GetInstance().GestionProducto(nombre, descripcion, id, 3).ToString();
                mensajeError(rpta);
                Mostrar();
            }
        }
    }
}
