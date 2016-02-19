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
    /// Lógica de interacción para frmPrestamosEmpleados.xaml
    /// </summary>
    public partial class frmPrestamosEmpleados : UserControl
    {
        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmPrestamosEmpleados()
        {
            InitializeComponent();
            MostarCmb();
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
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

            return total;
        }

        //mostrar
        private void Mostrar()
        {
            tblPrestamos.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarDeudaEmpleado(cmbEmpleado.SelectedValue.ToString());
            deudaTotal();
        }

        private void MostarCmb()
        {
            cmbEmpleado.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarEmpleado();
        }

        private void cmbEmpleado_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbEmpleado.SelectedIndex == 0)
            {
                tabNuevo.Visibility = Visibility.Collapsed;
                tabAbono.Visibility = Visibility.Collapsed;
            }
            else
            {
                Mostrar();

                if (tblPrestamos.Items.Count == 0)
                {
                    Persona item = cmbEmpleado.SelectedItem as Persona;
                    lblEmpleado.Text = item.NombrePersona;
                    lblEmpleadoAbono.Text = item.NombrePersona;

                    tabNuevo.Visibility = Visibility.Visible;
                    tabAbono.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Persona item = cmbEmpleado.SelectedItem as Persona;
                    lblEmpleado.Text = item.NombrePersona;
                    lblEmpleadoAbono.Text = item.NombrePersona;

                    tabAbono.Visibility = Visibility.Visible;
                    tabNuevo.Visibility = Visibility.Visible;

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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            string rpta = "";

            if (validarCampos())
            {

                rpta = MPrestamosEmpleados.GetInstance().insercionDeudaEmpleado(cmbEmpleado.SelectedValue.ToString(), Convert.ToDecimal(txtValor.Text), Convert.ToDateTime(dtdFecha.SelectedDate), txtDescripcion.Text).ToString();
                tabBuscar.IsEnabled = true;
                tabBuscar.Focus();
                mensajeError(rpta);
                Limpiar();
            }
            Mostrar();

            if (tblPrestamos.Items.Count != 0)
            {
                tabAbono.Visibility = Visibility.Visible;
            }
        }


        private void btnConfirmarAbono_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtAbono.Text != string.Empty)
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
                        Mostrar();
                    }
                    mensajeError(rpta);

                    tabBuscar.Focus();
                    if (tblPrestamos.Items.Count == 0)
                    {
                        tabAbono.Visibility = Visibility.Collapsed;
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
    }
}
