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
    /// Lógica de interacción para FrmLotes.xaml
    /// </summary>
    public partial class frmLotes : UserControl
    {
        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmLotes()
        {
            InitializeComponent();
            Mostrar();
        }

        // Define el estilo de las celdas 
        private void CantidadRegistros()
        {
            lblRegistros.Text = tblLotes.Items.Count.ToString();
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (txtNombre.Text == string.Empty || txtCuadras.Text == 0.ToString() || txtDescripcion.Text == null)
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

        // limpiar Controles lote
        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtCuadras.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        {
            tblLotes.ItemsSource = MLote.GetInstance().ConsultarLotes();
            CantidadRegistros();
            lblActivos.Text = (MLote.GetInstance().ConsultarInactivos().Count).ToString();
        }

        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblLotes.ItemsSource = MLote.GetInstance().ConsultarParametroLote(txtBuscarNombre.Text);
            CantidadRegistros();
        }

        private void frmLotes1_Loaded(object sender, RoutedEventArgs e)
        {
            Mostrar();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (IsValid(txtNombre) && IsValid(txtDescripcion) && IsValid(txtCuadras))
                {
                    rpta = MLote.GetInstance().GestionLote(txtNombre.Text, txtDescripcion.Text, txtCuadras.Text, 0, 1).ToString();
                    mensajeError(rpta);
                    Limpiar();
                    tabBuscar.Focus();
                }
            }
            else if (IsValid(txtNombre) && IsValid(txtDescripcion) && IsValid(txtCuadras))
            {
                rpta = MLote.GetInstance().GestionLote(txtNombre.Text, txtDescripcion.Text, txtCuadras.Text, Convert.ToInt32(txtId.Text), 2).ToString();
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
            Lote item = tblLotes.SelectedItem as Lote;

            txtNombre.Text = item.NombreLote;
            txtDescripcion.Text = item.Observaciones;
            txtCuadras.Text = item.Cuadras;
            txtId.Text = item.idLote.ToString();

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

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Lote item = tblLotes.SelectedItem as Lote;

            string nombre = item.NombreLote;
            string descripcion = item.Observaciones;
            string cuadras = item.Cuadras;
            byte id = Convert.ToByte(item.idLote);

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",

            };

            MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                string rpta = "";

                rpta = MLote.GetInstance().GestionLote(nombre, descripcion, cuadras, id, 3).ToString();
                mensajeError(rpta);
                Mostrar();
            }
        }

        private void btnRegistrarArboles_Click(object sender, RoutedEventArgs e)
        {
       
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
