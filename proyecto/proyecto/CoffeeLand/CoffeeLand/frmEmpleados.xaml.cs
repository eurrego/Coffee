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
    /// Lógica de interacción para frmEmpleados.xaml
    /// </summary>
    public partial class frmEmpleados : UserControl
    {
        bool validacion = false;

        public frmEmpleados()
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
            lblRegistros.Text = tblEmpleado.Items.Count.ToString();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbTipoDocumento.SelectedIndex == 0 || cmbTipoContrato.SelectedIndex == 0 || cmbGenero.SelectedIndex == 0 || txtNombre.Text == string.Empty || txtDocumento.Text == string.Empty || txtTelefono.Text == string.Empty || dtdFechaNacimiento.SelectedDate == null)
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
            cmbGenero.SelectedIndex = 0;
            cmbTipoContrato.SelectedIndex = 0;
            cmbTipoDocumento.SelectedIndex = 0;
            dtdFechaNacimiento.SelectedDate = null;
            txtNombre.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        {
            tblEmpleado.ItemsSource = MPersona.GetInstance().ConsultarPersona();
            lblActivos.Text = (MPersona.GetInstance().ConsultarInactivos().Count).ToString();

            List<string> dataTipoDocumento = new List<string>();
            dataTipoDocumento.Add("Seleccione un Tipo De Documento");
            dataTipoDocumento.Add("CC");
            dataTipoDocumento.Add("TI");
            cmbTipoDocumento.ItemsSource = dataTipoDocumento;

            List<string> dataTipoContrato = new List<string>();
            dataTipoContrato.Add("Seleccione un Tipo De Contrato");
            dataTipoContrato.Add("Permanente");
            dataTipoContrato.Add("Temporal");
            cmbTipoContrato.ItemsSource = dataTipoContrato;
            CantidadRegistros();
        }

        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblEmpleado.ItemsSource = MPersona.GetInstance().ConsultarParametroPersona(txtBuscarNombre.Text);
            CantidadRegistros();
        }


        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void cmbGenero_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un Género...");
            data.Add("M");
            data.Add("F");
            cmbGenero.ItemsSource = data;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    rpta = MPersona.GetInstance().GestionPersona(txtNombre.Text, Convert.ToString(cmbGenero.SelectedItem), txtTelefono.Text, Convert.ToDateTime(dtdFechaNacimiento.SelectedDate), Convert.ToInt32(txtDocumento.Text), 1, Convert.ToString(cmbTipoDocumento.SelectedItem), Convert.ToString(cmbTipoContrato.SelectedItem));
                    mensajeError(rpta);
                    Limpiar();
                    tabBuscar.Focus();
                }
            }
            else
            {
                rpta = MPersona.GetInstance().GestionPersona(txtNombre.Text, Convert.ToString(cmbGenero.SelectedItem), txtTelefono.Text, Convert.ToDateTime(dtdFechaNacimiento.SelectedDate), Convert.ToInt32(txtId.Text), 2, Convert.ToString(cmbTipoDocumento.SelectedItem), Convert.ToString(cmbTipoContrato.SelectedItem));
                mensajeError(rpta);
                Limpiar();
                tabBuscar.IsEnabled = true;
                tabNuevo.Header = "NUEVO";
                tabBuscar.Focus();
            }
            Mostrar();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Persona item = tblEmpleado.SelectedItem as Persona;

            txtId.Text = item.DocumentoPersona;
            txtNombre.Text = item.NombrePersona;
            txtDocumento.Text = item.DocumentoPersona;
            txtTelefono.Text = item.Telefono;
            cmbGenero.SelectedItem = item.Genero;
            cmbTipoContrato.SelectedValue = item.TipoContratoPersona;
            cmbTipoDocumento.SelectedValue = item.TipoDocumento;
            dtdFechaNacimiento.SelectedDate = item.FechaNacimineto;

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
            Persona item = tblEmpleado.SelectedItem as Persona;

            int id = Convert.ToInt32(item.DocumentoPersona);
            string nombre = item.NombrePersona;
            string telefono = item.Telefono;
            string genero = item.Genero;
            string tipoContrato = item.TipoContratoPersona;
            string tipoDocumento = item.TipoDocumento;
            DateTime fecha = item.FechaNacimineto;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",

            };

            MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                string rpta = "";

                rpta = MPersona.GetInstance().GestionPersona(nombre, genero, telefono, fecha, id, 3, tipoDocumento, tipoContrato).ToString();
                mensajeError(rpta);
                Mostrar();
            }
        }
    }
}
