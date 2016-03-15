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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmVentas.xaml
    /// </summary>
    public partial class frmVentas : UserControl
    {
        bool validacion = false;

        public frmVentas()
        {
            InitializeComponent();
            tamanioPantalla();
            mostrar();
            dtdFecha.DisplayDateEnd = DateTime.Now;
        }

        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 2.25;
            pnlContainer.Width = anchoContainer;

        }

        public void mostrar()
        {
            cmbProveedor.ItemsSource = MVentas.GetInstance().ConsultarProveedor();
            cmbProducto.ItemsSource = MVentas.GetInstance().ConsultarProducto();
            lblcargas.Text = MVentas.GetInstance().ConsultarProduccion().ToString();
        }

        // mensaje de Error
        public void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        public void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }


        public void limpiarCampos()
        {
            cmbProducto.SelectedIndex = 0;
            cmbProveedor.SelectedIndex = 0;
            txtCantidadCarga.Text = string.Empty;
            txtNumeroFactura.Text = string.Empty;
            txtValorBeneficio.Text = string.Empty;
            txtValorCarga.Text = string.Empty;
            dtdFecha.SelectedDate = null;
        }

        private bool validarCampos()
        {
            if (cmbProducto.SelectedIndex == 0 ||  txtValorCarga.Text == string.Empty ||  txtCantidadCarga.Text == string.Empty)
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

        private bool validarCamposBeneficio()
        {
            if (cmbProveedor.SelectedIndex == 0 ||  txtValorBeneficio.Text == string.Empty || txtNumeroFactura.Text == string.Empty )
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {
                if (decimal.Parse(txtCantidadCarga.Text.ToString()) > MVentas.GetInstance().ConsultarProduccion())
                {
                    mensajeError("La cantidad de cargas es superior a la existencia");
                }
                else
                {
                    decimal precioBeneficio = decimal.Parse(txtValorBeneficio.Text) * decimal.Parse(txtCantidadCarga.Text);
                    MVentas.GetInstance().GestionVenta(int.Parse(cmbProveedor.SelectedValue.ToString()), Convert.ToDateTime(dtdFecha.SelectedDate), int.Parse(txtNumeroFactura.Text), int.Parse(cmbProducto.SelectedValue.ToString()), decimal.Parse(txtValorCarga.Text), decimal.Parse(txtCantidadCarga.Text), precioBeneficio);
                    mensajeInformacion(MVentas.GetInstance().ventaProduccion(decimal.Parse(txtCantidadCarga.Text)));
                    limpiarCampos();
                    tabBeneficio.Focus();
                    lblcargas.Text = MVentas.GetInstance().ConsultarProduccion().ToString();
                }
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (validarCamposBeneficio())
            {
                if (MVentas.GetInstance().ValidarFactura(int.Parse(txtNumeroFactura.Text.ToString()),cmbProveedor.SelectedValue.ToString())==0)
                {
                    tabVenta.Focus();
                    btnPaso1.IsChecked = false;
                    btnPaso2.IsChecked = true;

                }
                else
                {
                    mensajeError("Este número de factura ya se encuentra asociado a este proveedor");
                }
          
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
            btnPaso1.IsChecked = true;
            btnPaso2.IsChecked = false;
            tabBeneficio.Focus();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            tabBeneficio.Focus();
            btnPaso2.IsChecked = false;
            btnPaso1.IsChecked = true;
        }
    }
}

