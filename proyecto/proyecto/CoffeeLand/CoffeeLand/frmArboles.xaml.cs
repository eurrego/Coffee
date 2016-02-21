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
using System.Collections;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmArboles.xaml
    /// </summary>
    public partial class frmArboles : UserControl
    {
        #region Singleton

        private static frmArboles instance;

        public static frmArboles GetInstance()
        {
            if (instance == null)
            {
                instance = new frmArboles();
            }

            return instance;
        }
        #endregion


        bool validacion = false;
        int lote = 0;

        public frmArboles()
        {
            InitializeComponent();
            //this.lote = lote;

            cmbTipoArbol.ItemsSource = MArbol.GetInstance().ConsultarTipoArbol();
            cmbLote.ItemsSource = MArbol.GetInstance().ConsultarLote();
            dtdFecha.DisplayDateEnd = DateTime.Now;
            //cmbLote.SelectedValue = lote;
            Mostrar();
            instance = this;
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }


        // Validación de campos
        private bool validarCampos()
        {

            if (cmbLote.SelectedIndex == 0 || txtCantidad.Text == string.Empty || dtdFecha.SelectedDate == null)
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
            cmbTipoArbol.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            dtdFecha.SelectedDate = null;
            txtId.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        {
            tblArboles.ItemsSource = MArbol.GetInstance().ConsultarArboles(Convert.ToInt32(cmbLote.SelectedValue)) as IEnumerable;
            cmbTipoArbol.SelectedValue = 0;
            dtdFecha.SelectedDate = DateTime.Now;
        }

        // define el total de arboles
        private void cantidadTotal()
        {
            int valor = 0;

            foreach (var item in tblArboles.Items)
            {
                Type v = item.GetType();
                var cantidad = v.GetProperty("Cantidad").GetValue(item).ToString();
                valor += Convert.ToInt32(cantidad);
            }

            lblRegistros.Text = string.Format("{0:0,0}", valor);
        }

        private void cmbLote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Lote item = cmbLote.SelectedItem as Lote;

            Mostrar();
            lblLote.Text = item.NombreLote;
            cantidadTotal();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (IsValid(txtCantidad) && IsValid(cmbTipoArbol))
            {
                if (txtId.Text == string.Empty)
                {
                    rpta = MArbol.GetInstance().gestionArboles(Convert.ToInt16(cmbLote.SelectedValue), Convert.ToByte(cmbTipoArbol.SelectedValue), Convert.ToInt32(txtCantidad.Text), Convert.ToDateTime(dtdFecha.SelectedDate), 0, 1).ToString();
                    mensajeError(rpta);
                    Limpiar();
                    tabBuscar.Focus();
                    tblArboles.IsEnabled = true;
                }
                else
                {
                    rpta = MArbol.GetInstance().gestionArboles(Convert.ToInt16(cmbLote.SelectedValue), Convert.ToByte(cmbTipoArbol.SelectedValue), Convert.ToInt32(txtCantidad.Text), Convert.ToDateTime(dtdFecha.SelectedDate), Convert.ToInt32(txtId.Text), 2).ToString();
                    mensajeError(rpta);
                    Limpiar();
                    tabBuscar.IsEnabled = true;
                    tabNuevo.Header = "NUEVO";
                    tabBuscar.Focus();
                    tblArboles.IsEnabled = true;

                    frmGestionTerrenos.GetInstance().tabDetalleArboles.Visibility = Visibility.Visible;
                    frmDetalleArboles.GetInstance().Mostrar();
                    frmGestionTerrenos.GetInstance().tabDetalleArboles.Focus();
                }
            }

            Mostrar();
            cantidadTotal();
        }

        private void btnDetalle_Click(object sender, RoutedEventArgs e)
        {
            object objeto = tblArboles.SelectedItem;
            Type t = objeto.GetType();
            string arbol = t.GetProperty("NombreTipoArbol").GetValue(objeto).ToString();
            int idArboles = Convert.ToInt32(t.GetProperty("idArboles").GetValue(objeto));
            int idTipoArbol = Convert.ToInt32(t.GetProperty("idTipoArbol").GetValue(objeto));


            frmDetalleArboles miDetalle = new frmDetalleArboles(Convert.ToInt16(cmbLote.SelectedValue), cmbLote.Text, arbol, idArboles, idTipoArbol );

            frmGestionTerrenos.GetInstance().tabDetalleArboles.Content = miDetalle;
            frmGestionTerrenos.GetInstance().tabDetalleArboles.Visibility = Visibility.Visible;
            frmGestionTerrenos.GetInstance().tabDetalleArboles.Focus();
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            tabBuscar.IsEnabled = true;
            tabBuscar.Focus();
            tabNuevo.Header = "NUEVO";
            tblArboles.IsEnabled = true;
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
