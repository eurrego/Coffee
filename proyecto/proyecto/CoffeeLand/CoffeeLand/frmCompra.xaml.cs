using System;
using System.Collections.Generic;
using System.Data;
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
using System.IO;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmCompra.xaml
    /// </summary>
    public partial class frmCompra : UserControl
    {
        #region Singleton

        private static frmCompra instance;

        public static frmCompra GetInstance()
        {
            if (instance == null)
            {
                instance = new frmCompra();
            }

            return instance;
        }

        #endregion

        DataTable dtDetalleCompra = new DataTable();
        int init = 0;
        int index = -1;

        public frmCompra()
        {
            InitializeComponent();
            dtdFecha.DisplayDateEnd = DateTime.Now;
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

            var temp = anchoContainer - 185;

            columnNombre.Width = temp / 4;
            columnSubtotal.Width = temp / 4;
            columnPrecio.Width = temp / 4;
            columnCantidad.Width = temp / 4;

            pnlContainer.Width = anchoContainer;

            tblDetalleCompra.Height = height - 320;
        }

        public void Mostrar()
        {
            cmbProveedor.ItemsSource = MCompra.GetInstance().ConsultarProveedor();
            cmbTipoInsumo.ItemsSource = MCompra.GetInstance().ConsultarTipoInsumo();
            cmbProveedor.SelectedIndex = 0;
            cmbTipoInsumo.SelectedIndex = 0;
        }

        public void limpiarCampos()
        {
            cmbTipoInsumo.SelectedIndex = 0;
            cmbInsumo.SelectedIndex = 0;
            txtValor.Text = string.Empty;
            txtCantidad.Text = string.Empty;
        }

        public void limpiarCamposCompra()
        {
            cmbProveedor.SelectedIndex = 0;
            txtNumeroFactura.Text = string.Empty;
            dtdFecha.SelectedDate = null;
            lblTotal.Text = "0";
        }

        public void TotalCompra()
        {
            int total = 0;

            foreach (DataRow row in dtDetalleCompra.Rows)
            {
                total += int.Parse(row["Subtotal"].ToString());
            }

            lblTotal.Text = string.Format("{0:$0,0}", total);
        }

        private bool validarCampos(int campos)
        {
            bool validacion = false;

            if (campos == 1)
            {
                if (txtCantidad.Text == string.Empty || txtValor.Text == string.Empty || cmbInsumo.SelectedIndex == 0 || cmbTipoInsumo.SelectedIndex == 0)
                {
                    mensajeError("Debe Ingresar todos los Campos");
                    validacion = false;
                }
                else
                {
                    validacion = true;
                }
            }
            else if (campos == 2)
            {
                if (txtNumeroFactura.Text == string.Empty || dtdFecha.Text == string.Empty || cmbProveedor.SelectedIndex == 0)
                {
                    mensajeError("Debe Ingresar todos los Campos");
                    validacion = false;
                }
                else
                {
                    validacion = true;
                }
            }
            else if (campos == 3)
            {
                if (dtDetalleCompra.Rows.Count == 0)
                {
                    mensajeError("Debe agregar por lo menos un insumo");
                    validacion = false;
                }
                else
                {
                    validacion = true;
                }
            }

            return validacion;
        }

        private bool validarCamposProveedor()
        {
            bool validacion = false;

            if (txtNumeroFactura.Text == string.Empty || dtdFecha.Text == string.Empty || cmbProveedor.SelectedIndex == 0)
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

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        public void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos(1))
            {
                if (IsValid(txtValor) && IsValid(txtCantidad))
                {
                    if (init == 0)
                    {
                        dtDetalleCompra.Columns.Add("Nombre", typeof(string));
                        dtDetalleCompra.Columns.Add("Cantidad", typeof(string));
                        dtDetalleCompra.Columns.Add("Precio", typeof(string));
                        dtDetalleCompra.Columns.Add("Subtotal", typeof(string));
                        dtDetalleCompra.Columns.Add("idInsumo", typeof(string));
                        dtDetalleCompra.Columns.Add("idCompra", typeof(string));
                        dtDetalleCompra.Columns.Add("TipoInsumo", typeof(string));
                        init++;
                    }

                    Insumo item = cmbInsumo.SelectedItem as Insumo;

                    DataRow fila = dtDetalleCompra.NewRow();
                    fila["idInsumo"] = item.idInsumo.ToString();
                    fila["Nombre"] = item.NombreInsumo.ToString();
                    fila["Cantidad"] = txtCantidad.Text;
                    fila["Precio"] = txtValor.Text;
                    fila["Subtotal"] = (int.Parse(txtValor.Text) * int.Parse(txtCantidad.Text)).ToString();
                    fila["TipoInsumo"] = cmbTipoInsumo.Text;
                    


                    int RindexRow = 0;
                    int indexRow = dtDetalleCompra.Rows.Count + 1;
                    int DistintoPrecio = 0;

                    foreach (DataRow row in dtDetalleCompra.Rows)
                    {

                        if (row["idInsumo"].Equals(item.idInsumo.ToString()))
                        {

                            if (row["Precio"].Equals(txtValor.Text))
                            {

                                fila["Cantidad"] = (int.Parse(row["Cantidad"].ToString()) + int.Parse(txtCantidad.Text)).ToString();
                                fila["Subtotal"] = (int.Parse(txtValor.Text) * int.Parse(fila["Cantidad"].ToString())).ToString();

                                indexRow = RindexRow;
                            }

                            else
                            {
                                mensajeError("El insumo ya existe en la factura con un valor distinto al cual desea ingresar");

                                DistintoPrecio = 1;
                            }

                        }

                        RindexRow++;
                    }


                    if (indexRow != (dtDetalleCompra.Rows.Count + 1))
                    {
                        dtDetalleCompra.Rows[indexRow].Delete();
                    }



                    if (DistintoPrecio == 0)
                    {

                        dtDetalleCompra.Rows.Add(fila);

                        tblDetalleCompra.ItemsSource = dtDetalleCompra.DefaultView;
                        limpiarCampos();
                        TotalCompra();

                    }

                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlData.Visibility = Visibility.Visible;
                    pnlEstado.Visibility = Visibility.Visible;
                }
            }

            if (tblDetalleCompra.Items.Count == 0)
            {
                btnAtras.IsEnabled = true;
            }
            else
            {
                btnAtras.IsEnabled = false;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (validarCampos(2))
            {
                if (validarCampos(3))
                {
                    string compra = "";

                    try
                    {
                        compra = MCompra.GetInstance().RegistroCompra(cmbProveedor.SelectedValue.ToString(), Convert.ToDateTime(dtdFecha.SelectedDate), int.Parse(txtNumeroFactura.Text)).ToString();
                    }
                    catch (Exception ex)
                    {
                        string filePath = @"C:\Users\Snug\LogCoffeeLand.txt";

                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                               "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                        }

                        mensajeError("Ha ocurrido un error inesperado, consulte con el administrador del sistema");

                    }

                    if (!compra.Equals("0"))
                    {
                        foreach (DataRow dr in dtDetalleCompra.Rows)
                        {
                            dr["idCompra"] = compra;
                        }

                        try
                        {
                            dtDetalleCompra.Columns.Remove("TipoInsumo");
                            MCompra.GetInstance().RegistroDetalleCompra(dtDetalleCompra);

                            dtDetalleCompra.Clear();
                            limpiarCamposCompra();
                            limpiarCampos();
                            dtDetalleCompra.Columns.Add("TipoInsumo");
                            pnlEstado.Visibility = Visibility.Collapsed;
                            pnlData.Visibility = Visibility.Collapsed;
                            pnlInicio.Visibility = Visibility.Visible;
                            btnPaso1.IsChecked = true;
                            btnPaso2.IsChecked = false;
                            tabProveedor.Focus();

                            frmConsultaCompras.GetInstance().Mostrar();
                            frmEstadoCuenta.GetInstance().Mostrar();

                            mensajeInformacion("Registro exitoso");
                        }
                        catch (Exception ex)
                        {
                            string filePath = @"C:\Users\Snug\LogCoffeeLand.txt";

                            using (StreamWriter writer = new StreamWriter(filePath, true))
                            {
                                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                            }

                            mensajeError("Ha ocurrido un error inesperado, consulte con el administrador del sistema");

                        }

                    }
                    else
                    {
                        mensajeError("Este número de factura ya se encuentra asociado a este proveedor");
                        tabProveedor.Focus();
                        txtNumeroFactura.Focus();
                    }

                }
            }
        }

        private void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            index = tblDetalleCompra.SelectedIndex;
            dtDetalleCompra.Rows[index].Delete();
            index = -1;
            TotalCompra();

            if (tblDetalleCompra.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                pnlEstado.Visibility = Visibility.Collapsed;
                btnAtras.IsEnabled = true;
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            index = tblDetalleCompra.SelectedIndex;
            cmbTipoInsumo.Text = dtDetalleCompra.Rows[index].ItemArray[6].ToString();

            TipoInsumo item = cmbTipoInsumo.SelectedItem as TipoInsumo;
            cmbInsumo.ItemsSource = MCompra.GetInstance().ConsultarInsumo(item.idTipoInsumo);

            cmbInsumo.Text = dtDetalleCompra.Rows[index].ItemArray[0].ToString();
            txtCantidad.Text = dtDetalleCompra.Rows[index].ItemArray[1].ToString();
            txtValor.Text = dtDetalleCompra.Rows[index].ItemArray[2].ToString();
            dtDetalleCompra.Rows[index].Delete();
            TotalCompra();

            if (tblDetalleCompra.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                pnlEstado.Visibility = Visibility.Collapsed;
            }
        }

        private void cmbTipoInsumo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoInsumo.SelectedIndex == 0)
            {
                cmbInsumo.IsEnabled = false;
            }
            else
            {
                cmbInsumo.SelectedIndex = 0;
                TipoInsumo item = cmbTipoInsumo.SelectedItem as TipoInsumo;
                cmbInsumo.ItemsSource = MCompra.GetInstance().ConsultarInsumo(item.idTipoInsumo);
                cmbInsumo.SelectedIndex = 0;
                cmbInsumo.IsEnabled = true;
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (validarCamposProveedor())
            {
                if (IsValid(cmbProveedor) && IsValid(dtdFecha) && IsValid(txtNumeroFactura))
                {
                    if (MVentas.GetInstance().ValidarFactura(int.Parse(txtNumeroFactura.Text), cmbProveedor.SelectedValue.ToString()) == 0)
                    {

                        tabInsumo.Focus();
                        lblProveedor.Text = cmbProveedor.Text;
                        lblFactura.Text = txtNumeroFactura.Text;

                        btnPaso1.IsChecked = false;
                        btnPaso2.IsChecked = true;
                    }
                    else
                    { 
                        mensajeError("Este número de factura ya se encuentra asociado a este proveedor");
                    }
                }
            }

            if (tblDetalleCompra.Items.Count == 0)
            {
                btnAtras.IsEnabled = true;
            }
            else
            {
                btnAtras.IsEnabled = false;
            }
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            tabProveedor.Focus();
            btnPaso2.IsChecked = false;
            btnPaso1.IsChecked = true;
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dtDetalleCompra.Clear();
            limpiarCamposCompra();
            limpiarCampos();
            pnlEstado.Visibility = Visibility.Collapsed;
            pnlData.Visibility = Visibility.Collapsed;
            pnlInicio.Visibility = Visibility.Visible;
            btnPaso1.IsChecked = true;
            btnPaso2.IsChecked = false;
            btnAtras.IsEnabled = true;
            tabProveedor.Focus();
        }

        private void cmbInsumo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInsumo.SelectedIndex != -1)
            {


                var item = cmbInsumo.SelectedItem as Insumo;
                txtUnidad.Text = item.UnidadMedida.ToString();

            }

        }
    }
}
