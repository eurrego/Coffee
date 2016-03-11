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
using System.Globalization;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmPrestamosEmpleados.xaml
    /// </summary>
    public partial class frmPrestamosEmpleados : UserControl
    {
        #region Singleton

        private static frmPrestamosEmpleados instance;

        public static frmPrestamosEmpleados GetInstance()
        {
            if (instance == null)
            {
                instance = new frmPrestamosEmpleados();
            }

            return instance;
        }

        #endregion


        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmPrestamosEmpleados()
        {
            InitializeComponent();
            instance = this;
            Mostrar();
            tamanioPantalla();
            dtdFecha.DisplayDateEnd = DateTime.Now;
        }

        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 1.75;

            pnlContainer.Width = anchoContainer;
            pnlMovContainer.Width = anchoContainer;

            tblPrestamos.Height = height - 280;
            tblMovAbonos.Height = height - 280;
            tblMovDeudas.Height = height - 280;
        }

        public void Mostrar()
        {
            cmbEmpleado.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarEmpleado();
            cmbEmpleado.SelectedIndex = 0;
        }

        private void mostrarMovimiento()
        {
            tblMovDeudas.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarDetalleDeudasEmpleado(cmbEmpleado.SelectedValue.ToString());
            tblMovAbonos.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarDetalleAbonosEmpleado(cmbEmpleado.SelectedValue.ToString());
        }

        //mostrar
        private void MostrarDeudas()
        {
            tblPrestamos.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarDeudaEmpleado(cmbEmpleado.SelectedValue.ToString());
            deudaTotal();
        }


        private void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        // limpiar Controles
        private void Limpiar()
        {
            dtdFecha.SelectedDate = null;
            txtValor.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtAbono.Text = string.Empty;
            tblPrestamos.ItemsSource = null;
            lblTotal.Text = string.Empty;
        }

        // define la deuda total
        private decimal deudaTotal()
        {
            decimal total = 0;

            foreach (DeudaPersona item in tblPrestamos.ItemsSource)
            {
                total += item.Valor;
            }

            lblTotal.Text = string.Format("{0:c}", total);
            lblTotalAbonos.Text = string.Format("{0:c}", total);
            lblTotalDeuda.Text = string.Format("{0:c}", total); 
            return total;
        }

        private void cmbEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           var valor = cmbEmpleado.SelectedIndex; 

            if (cmbEmpleado.SelectedIndex <= 0)
            {
                tblPrestamos.ItemsSource = null;
                lblTotal.Text = "$0";
                lblSuperior.Text = "Seleccione un";
                lblInferior.Text = "Empleado";
                pnlInicio.Visibility = Visibility.Visible;
                pnlData.Visibility = Visibility.Collapsed;
                tabAbono.Visibility = Visibility.Collapsed;
                tabNuevo.Visibility = Visibility.Collapsed;
                btnAbono.Visibility = Visibility.Collapsed;
                btnNuevo.Visibility = Visibility.Collapsed;
                btnDetalle.Visibility = Visibility.Collapsed;
            }
            else
            {
                MostrarDeudas();
                mostrarMovimiento();

                Persona item = cmbEmpleado.SelectedItem as Persona;
 
                var tolower = item.NombrePersona.ToLower();
                lblEmpleado.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblTitleEmpleado.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblEmpleadoAbono.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblMovEmpleado.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);


                if (tblPrestamos.Items.Count == 0)
                {
                    lblSuperior.Text = "Este empleado no tiene";
                    lblInferior.Text = "deudas pendientes";
                    pnlInicio.Visibility = Visibility.Visible;
                    pnlData.Visibility = Visibility.Collapsed;
                    tabNuevo.Visibility = Visibility.Visible;
                    tabAbono.Visibility = Visibility.Collapsed;
                    btnAbono.Visibility = Visibility.Collapsed;
                    btnNuevo.Visibility = Visibility.Visible;
                    btnDetalle.Visibility = Visibility.Visible;
                }
                else
                {
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlData.Visibility = Visibility.Visible;
                    tabAbono.Visibility = Visibility.Visible;
                    tabNuevo.Visibility = Visibility.Visible;
                    btnAbono.Visibility = Visibility.Visible;
                    btnNuevo.Visibility = Visibility.Visible;
                    btnDetalle.Visibility = Visibility.Visible;
                }
            }
        }

        private bool validarCampos()
        {

            if (txtValor.Text == string.Empty || txtDescripcion.Text == string.Empty || dtdFecha.SelectedDate == null)
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

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            string rpta = "";

            if (validarCampos())
            {
                if (IsValid(txtValor) && IsValid(txtDescripcion))
                {
                    rpta = MPrestamosEmpleados.GetInstance().insercionDeudaEmpleado(cmbEmpleado.SelectedValue.ToString(), Convert.ToDecimal(txtValor.Text), Convert.ToDateTime(dtdFecha.SelectedDate), txtDescripcion.Text).ToString();
                    tabBuscar.IsEnabled = true;
                    tabBuscar.Focus();
                    mensajeInformacion(rpta);
                    Limpiar();
                }
            }
            MostrarDeudas();
            mostrarMovimiento();

            if (tblPrestamos.Items.Count != 0)
            {
                pnlData.Visibility = Visibility.Visible;
                pnlInicio.Visibility = Visibility.Collapsed;
                tabAbono.Visibility = Visibility.Visible;
                btnAbono.Visibility = Visibility.Visible;
            }
           
        }


        private void btnConfirmarAbono_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtAbono.Text != string.Empty)
            {
                if (IsValid(txtAbono))
                {
                    decimal valor = Convert.ToDecimal(txtAbono.Text);

                    if (Convert.ToInt32(deudaTotal()) < Convert.ToInt32(valor))
                    {
                        mensajeError("El valor del Abono no debe superar el total del Prestamo");
                        txtAbono.Text = string.Empty;
                    }
                    else
                    {
                        while (valor != 0)
                        {
                            DateTime newDate = DateTime.Now;
                            int idDeuda = 0;
                            decimal valorTotal = 0;

                            for (int i = 0; i < tblPrestamos.Items.Count; i++)
                            {
                                tblPrestamos.SelectedIndex = i;
                                DeudaPersona item = tblPrestamos.SelectedItem as DeudaPersona;

                                var fecha = item.Fecha;

                                TimeSpan ts = newDate - fecha;

                                if (ts.Days > 0)
                                {
                                    newDate = fecha;
                                    idDeuda = item.idDeudaPersona;
                                    valorTotal = item.Valor;
                                }
                                else if (ts.Days == 0)
                                {
                                    newDate = fecha;
                                    idDeuda = item.idDeudaPersona;
                                    valorTotal = item.Valor;
                                }
                            }

                            if (valor == valorTotal)
                            {
                                rpta = MPrestamosEmpleados.GetInstance().insercionAbonoDeuda(valor, DateTime.Now, idDeuda, 1);
                                valor = 0;
                            }
                            else if (valorTotal > valor)
                            {
                                rpta = MPrestamosEmpleados.GetInstance().insercionAbonoDeuda(valor, DateTime.Now, idDeuda, 2);
                                valor = 0;
                            }
                            else if (valorTotal < valor)
                            {
                                rpta = MPrestamosEmpleados.GetInstance().insercionAbonoDeuda(valorTotal, DateTime.Now, idDeuda, 3);
                                valor = valor - valorTotal;
                            }

                            Limpiar();
                            MostrarDeudas();
                            mostrarMovimiento();
                        }
                        mensajeInformacion(rpta);

                        tabBuscar.Focus();
                        if (tblPrestamos.Items.Count == 0)
                        {
                            lblSuperior.Text = "Este empleado no tiene";
                            lblInferior.Text = "deudas pendientes";
                            pnlInicio.Visibility = Visibility.Visible;
                            pnlData.Visibility = Visibility.Collapsed;
                            tabNuevo.Visibility = Visibility.Visible;
                            tabAbono.Visibility = Visibility.Collapsed;
                            btnAbono.Visibility = Visibility.Collapsed;
                            btnNuevo.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            else
            {
                mensajeError("Debe ingresar el valor del Abono");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtAbono.Text = string.Empty;
            tabBuscar.Focus();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            tabNuevo.Focus();
        }

        private void btnAbono_Click(object sender, RoutedEventArgs e)
        {
            tabAbono.Focus();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            tabBuscar.Focus();
            cmbEmpleado.SelectedIndex = 0;
        }

        private void btnOcultar_Click(object sender, RoutedEventArgs e)
        {
            tabBuscar.Focus();
            cmbEmpleado.SelectedIndex = 0;
        }

        private void btnDetalle_Click(object sender, RoutedEventArgs e)
        {

            if (tblMovDeudas.Items.Count == 0)
            {
                pnlMovInicio.Visibility = Visibility.Visible;
                pnlMovData.Visibility = Visibility.Collapsed;
            }
            else
            {
                pnlMovInicio.Visibility = Visibility.Collapsed;
                pnlMovData.Visibility = Visibility.Visible;
            }

            tabMovimiento.Focus();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            tabInicio.Focus();
        }
    }
}
