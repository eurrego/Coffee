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
using System.Collections;
using System.Globalization;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Data;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmTerrenos.xaml
    /// </summary>
    public partial class frmTerrenos : UserControl
    {
        #region
        private static frmTerrenos instance;

        public static frmTerrenos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmTerrenos();
            }

            return instance;
        }

        #endregion

        public static DataTable dt;
        public static DataTable dt1;

        int init = 0;
        int init1 = 0;
        int opc = 0;
        int opc2 = 0;
        int index = -1;
        int cantidadArboles = 0;

        bool validacion = false;

        public frmTerrenos()
        {
            InitializeComponent();
            mostrar();
            tamanioPantalla();
            dtdFechaLabor.DisplayDateEnd = DateTime.Now;
        }

        public void mostrar()
        {
            tblLotes.ItemsSource = MTerrenos.GetInstance().ConsultarLote();
            tblLabores.ItemsSource = MTerrenos.GetInstance().ConsultarLabor();
            cmbInsumo.ItemsSource = MTerrenos.GetInstance().ConsultarInsumo();
            cmbEmpleado.ItemsSource = MTerrenos.GetInstance().ConsultarEmpleado();
            llenarCmbTipoPago();
        }

        private void llenarCmbTipoPago()
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un tipo de Pago");
            data.Add("Contrato");
            data.Add("Jornal");

            cmbTipoPago.ItemsSource = data;
        }

        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 2.25;
            pnlContainer.Width = anchoContainer;
            pnlContainerLabor.Width = anchoContainer;
            pnlContainerInsumos.Width = width / 2;
            pnlContainerEmpleados.Width = width / 2;
            pnlContainerArboles.Width = anchoContainer;

            tblLotes.Height = height - 285;
            columnLote.Width = anchoContainer - 70;
            tblLabores.Height = height - 285;
            columnLabor.Width = anchoContainer - 70;
            tblInsumos.Height = height - 285;
            tblProductividad.Height = height - 285;
            tblProductividad.Height = height - 285;
            tblArboles.Height = height - 285;

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

        private bool Validar(int opc)
        {

            switch (opc)
            {
                case 1:
                    if (cmbTipoPago.SelectedIndex == 0 || dtdFechaLabor.SelectedDate == null || lblLabores.Text.Equals("Seleccione un"))
                    {
                        mensajeError("Debe Ingresar todos los Campos");
                        validacion = false;
                    }
                    else
                    {
                        validacion = true;
                    }
                    break;
                case 2:
                    if (lblLote.Text.Equals("Seleccione un"))
                    {
                        mensajeError("Debe seleccionar un lote");
                        validacion = false;
                    }
                    else
                    {
                        validacion = true;
                    }
                    break;
                case 3:
                    if (cmbInsumo.SelectedIndex == 0 || txtCantidadInsumo.Text == string.Empty)
                    {
                        mensajeError("Debe Ingresar todos los Campos");
                        validacion = false;
                    }
                    else
                    {
                        validacion = true;
                    }

                    break;
                case 4:
                    if (cmbEmpleado.SelectedIndex == 0 || txtCantidadProductividad.Text == string.Empty || txtValorProductividad.Text == string.Empty)
                    {
                        mensajeError("Debe Ingresar todos los Campos");
                        validacion = false;
                    }
                    else
                    {
                        validacion = true;
                    }
                    break;


                default:
                    break;
            }

            return validacion;
        }


        public void limpiarCampos(int opc)
        {
            switch (opc)
            {
                case 1:
                    cmbTipoPago.SelectedIndex = 0;
                    dtdFechaLabor.SelectedDate = null;
                    lblLabores.Text = "Seleccione un";
                    lblInicioLabores.Visibility = Visibility.Visible;
                    tblLabores.SelectedItem = null;
                    break;
                case 2:
                    txtCantidadInsumo.Text = string.Empty;
                    cmbInsumo.SelectedIndex = 0;
                    break;
                case 3:
                    cmbEmpleado.SelectedIndex = 0;
                    txtCantidadProductividad.Text = string.Empty;
                    txtValorProductividad.Text = string.Empty;
                    break;

                default:
                    break;
            }

        }

        public void crearTabla(int opc2)
        {

            switch (opc2)
            {
                case 1:

                    if (init == 0)
                    {
                        dt = new DataTable();
                        dt.Columns.Add("idLabor_Lote");
                        dt.Columns.Add("IdInsumo");
                        dt.Columns.Add("Cantidad");
                        dt.Columns.Add("Precio");
                        dt.Columns.Add("NombreInsumo");
                        init++;
                    }
                    break;

                case 2:

                    if (init1 == 0)
                    {
                        dt1 = new DataTable();
                        dt1.Columns.Add("DocumentoPersona");
                        dt1.Columns.Add("idLabor_Lote");
                        dt1.Columns.Add("Cantidad");
                        dt1.Columns.Add("Valor");
                        dt1.Columns.Add("NombrePersona");

                        init1++;
                    }
                    break;
            }
        }

        private void tblLotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = tblLotes.SelectedItem as MTerrenos;

            if (item != null)
            {
                var tolower = item.NombreLote.ToLower();
                lblLote.FontFamily = new FontFamily("Arial Rounded MT Bold");
                lblLote.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblLoteLabores.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                LblLoteInsumo.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblLoteEmpleado.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblLoteArboles.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);

                lblInicioLote.Visibility = Visibility.Hidden;

                lblTotalCuadras.Text = string.Format("{0:0,0}", item.Cuadras);

                if (item.Cantidad == 0)
                {
                    lblTotalArboles.Text = "0";
                }
                else
                {
                    lblTotalArboles.Text = string.Format("{0:0,0}", item.Cantidad);
                }
            }
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            atras();
        }

        private void atras()
        {
            lblLote.FontFamily = new FontFamily("Segoe UI");
            lblLote.Text = "Seleccione un";
            lblInicioLote.Visibility = Visibility.Visible;
            lblTotalArboles.Text = "0";
            lblTotalCuadras.Text = "0";

            tblLotes.SelectedItem = null;
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (Validar(2))
            {
                tabLabor.Focus();
                btnLabores.IsChecked = true;
            }
        }


        private void btnSiguienteLabores_Click(object sender, RoutedEventArgs e)
        {
            if (Validar(1))
            {
                Labor item = tblLabores.SelectedItem as Labor;

                if (item.RequiereInsumo)
                {
                    tabInsumo.Focus();
                    btnInsumo.IsChecked = true;
                }
                else
                {
                    tabEmpleados.Focus();
                    btnEmpleado.IsChecked = true;
                }
            }
        }



        private void tblLabores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = tblLabores.SelectedItem as Labor;

            if (item != null)
            {
                var tolower = item.NombreLabor.ToLower();
                lblLabores.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblLaborInsumo.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblLaborempleado.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblLaborArboles.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);

                lblInicioLabores.Visibility = Visibility.Hidden;
            }
        }

        private void btnAtrasLabor_Click(object sender, RoutedEventArgs e)
        {
            tabLote.Focus();
            btnLabores.IsChecked = false;
        }

        private void cmbInsumo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Insumo insumo = cmbInsumo.SelectedItem as Insumo;

            if (cmbInsumo.SelectedIndex != 0)
            {
                var tolower = insumo.UnidadMedida.ToLower();
                txtUnidadMedida.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);

                var cantidad = 0;

                if (tblInsumos.Items.Count != 0)
                {
                    for (int i = 0; i < tblInsumos.Items.Count; i++)
                    {
                        int id = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
                        int idCmb = Convert.ToInt32(cmbInsumo.SelectedValue);

                        if (idCmb == id)
                        {
                            cantidad += Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                        }
                    }

                    lblStock.Text = Convert.ToString(Convert.ToInt32(insumo.CantidadExistente) - cantidad);
                }
                else
                {
                    lblStock.Text = insumo.CantidadExistente.ToString();
                }

                var precioPromedio = insumo.PrecioPromedio;
            }
            else
            {
                txtUnidadMedida.Text = string.Empty;
                lblStock.Text = string.Empty;
            }
        }

        private void btnAgregarInsumos_Click(object sender, RoutedEventArgs e)
        {
            Insumo insumo = cmbInsumo.SelectedItem as Insumo;


            if (Validar(3))
            {
                if (IsValid(txtCantidadInsumo))
                {
                    crearTabla(1);

                    if (int.Parse(lblStock.Text) >= int.Parse(txtCantidadInsumo.Text))
                    {
                        dt.Rows.Add(null, cmbInsumo.SelectedValue, txtCantidadInsumo.Text, insumo.PrecioPromedio, cmbInsumo.Text);

                        tblInsumos.ItemsSource = dt.DefaultView;

                        pnlInicio.Visibility = Visibility.Collapsed;
                        pnlData.Visibility = Visibility.Visible;
                        limpiarCampos(2);
                        btnAtrasInsumoLabor.IsEnabled = false;
                    }
                    else
                    {
                        mensajeError("La cantidad disponible del insumo es menor a la ingresada");
                    }

                }
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (tblInsumos.Items.Count != 0)
            {
                dt.Clear();
                limpiarCampos(2);
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                btnAtrasInsumoLabor.IsEnabled = true;
            }
            else
            {
                limpiarCampos(2);
                btnAtrasInsumoLabor.IsEnabled = true;
            }
        }

        private void btnModificarInsumo_Click(object sender, RoutedEventArgs e)
        {
            index = tblInsumos.SelectedIndex;
            cmbInsumo.Text = dt.Rows[index].ItemArray[4].ToString();
            txtCantidadInsumo.Text = dt.Rows[index].ItemArray[2].ToString();

            dt.Rows[index].Delete();

            var cantidad = 0;

            for (int i = 0; i < tblInsumos.Items.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
                int idCmb = Convert.ToInt32(cmbInsumo.SelectedValue);

                if (idCmb == id)
                {
                    cantidad += Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                }
            }

            Insumo insumo = cmbInsumo.SelectedItem as Insumo;
            lblStock.Text = Convert.ToString(Convert.ToInt32(insumo.CantidadExistente) - cantidad);


            if (tblInsumos.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
            }
        }

        private void btnInhabilitarInsumo_Click(object sender, RoutedEventArgs e)
        {
            index = tblInsumos.SelectedIndex;
            dt.Rows[index].Delete();
            index = -1;
            limpiarCampos(2);

            if (tblInsumos.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
            }
        }


        private void btnSiguienteInsumos_Click(object sender, RoutedEventArgs e)
        {
            if (tblInsumos.Items.Count == 0)
            {
                mensajeError("Debe agregar un Insumo");
            }
            else
            {
                tabEmpleados.Focus();
                btnEmpleado.IsChecked = true;
            }
        }

        private void btnAgregarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (Validar(4))
            {
                if (IsValid(txtCantidadProductividad) && IsValid(txtValorProductividad))
                {
                    crearTabla(2);
                    dt1.Rows.Add(cmbEmpleado.SelectedValue, null, int.Parse(txtCantidadProductividad.Text), int.Parse(txtValorProductividad.Text), cmbEmpleado.Text);

                    tblProductividad.ItemsSource = dt1.DefaultView;

                    pnlInicioEmpleados.Visibility = Visibility.Collapsed;
                    pnlDataEmpleados.Visibility = Visibility.Visible;
                    limpiarCampos(2);
                }
                limpiarCampos(3);
            }

        }

        private void btnInhabilitarProductividadEmpleado_Click(object sender, RoutedEventArgs e)
        {
            index = tblProductividad.SelectedIndex;
            dt1.Rows[index].Delete();
            index = -1;

        }
        private void btnModificarProductividadEmpleado_Click(object sender, RoutedEventArgs e)
        {
            CantidadProductividad();
            index = tblProductividad.SelectedIndex;
            cmbEmpleado.Text = dt1.Rows[index].ItemArray[4].ToString();
            txtCantidadProductividad.Text = dt1.Rows[index].ItemArray[2].ToString();
            txtValorProductividad.Text = dt1.Rows[index].ItemArray[3].ToString();

            dt1.Rows[index].Delete();
        }

        public void CantidadProductividad()
        {
            if (cmbTipoPago.SelectedItem.Equals("Jornal"))
            {
                txtCantidadProductividad.Text = "1";
                txtCantidadProductividad.IsEnabled = false;
            }
            else
            {
                txtCantidadProductividad.Text = string.Empty;
                txtCantidadProductividad.IsEnabled = true;
            }
        }

        private void cmbTipoPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CantidadProductividad();

            var tolower = cmbTipoPago.SelectedItem.ToString().ToLower();
            lblPagoEmpleado.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
            lblPagoempleadosArboles.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
        }

        private void btnAtrasInsumoLabor_Click(object sender, RoutedEventArgs e)
        {

            limpiarCampos(2);
            tabLabor.Focus();
        }

        private void btnAtrasEmpleadosInsumos_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos(3);
            Labor item = tblLabores.SelectedItem as Labor;

            if (item.RequiereInsumo)
            {
                tabInsumo.Focus();
            }
            else
            {
                tabLabor.Focus();
            }
        }

        private void btnCancelarEmpleados_Click(object sender, RoutedEventArgs e)
        {
            if (tblProductividad.Items.Count != 0)
            {
                dt1.Clear();
                limpiarCampos(3);
                pnlDataEmpleados.Visibility = Visibility.Collapsed;
                pnlInicioEmpleados.Visibility = Visibility.Visible;
            }
            else
            {
                limpiarCampos(3);
            }
        }

        private async void btnSiguienteEmpleados_Click(object sender, RoutedEventArgs e)
        {
            if (tblProductividad.Items.Count == 0)
            {
                mensajeError("Debe agregar un empleado");
            }
            else
            {
                Labor item = tblLabores.SelectedItem as Labor;

                if (item.ModificaArboles)
                {
                    MTerrenos itemTerrenos = tblLotes.SelectedItem as MTerrenos;

                    foreach (DataRow itemCantidad in dt1.Rows)
                    {
                        cantidadArboles += int.Parse(itemCantidad["Cantidad"].ToString());
                    }

                    txtArbolesModicacion.Text = (cantidadArboles).ToString();

                    cmbTipoArbolLote.ItemsSource = MTerrenos.GetInstance().LaborModificaArbol(Convert.ToInt32(itemTerrenos.Id)) as IEnumerable;
                    cmbTipoArbolModificar.ItemsSource = MTerrenos.GetInstance().ConsultarTipoArboles();
                    tblArboles.ItemsSource = MTerrenos.GetInstance().ConsultarCantidadTiposArboles(Convert.ToInt32(itemTerrenos.Id)) as IEnumerable;
                    btnArboles.IsChecked = true;
                    tabArboles.Focus();

                    if (tblArboles.Items.Count != 0)
                    {
                        pnlDataArboles.Visibility = Visibility.Visible;
                        pnlInicioArboles.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        pnlDataArboles.Visibility = Visibility.Collapsed;
                        pnlInicioArboles.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Aceptar",
                        NegativeButtonText = "Cancelar",

                    };

                    MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea guardar el registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                    if (result != MessageDialogResult.Negative)
                    {
                        try
                        {
                            GuardarDatos();
                            mensajeInformacion("Registro exitoso");
                            tabLote.Focus();
                            btnEmpleado.IsChecked = false;
                            btnLabores.IsChecked = false;
                            limpiarCampos(1);
                            limpiarCampos(2);
                            limpiarCampos(3);
                            atras();
                            tblLabores.SelectedItem = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void cmbTipoArbolLote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoArbolLote.SelectedIndex != 0)
            {
                MTerrenos item = tblLotes.SelectedItem as MTerrenos;
                var cantidad = MTerrenos.GetInstance().cantidadArbolesLote(int.Parse(cmbTipoArbolLote.SelectedValue.ToString()), Convert.ToInt32(item.Id));

                txtCantidadArbolesLotes.Text = cantidad.ToString();
            }
        }

        private async void btnSguientearboles_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",

            };

            MessageDialogResult result = await((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea guardar el registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                try
                {
                    GuardarDatos();
                    mensajeInformacion("Registro exitoso");
                    tabLote.Focus();
                    btnEmpleado.IsChecked = false;
                    btnLabores.IsChecked = false;
                    btnInsumo.IsChecked = false;
                    btnArboles.IsChecked = false;
                    limpiarCampos(1);
                    limpiarCampos(2);
                    limpiarCampos(3);
                    atras();
                    tblLabores.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public void GuardarDatos()
        {

            Labor itemLabor = tblLabores.SelectedItem as Labor;
            MTerrenos itemLotes = tblLotes.SelectedItem as MTerrenos;

            string Idlabor_lote = MTerrenos.GetInstance().asociarLaborLote(Convert.ToInt32(itemLabor.idLabor), Convert.ToInt32(itemLotes.Id), Convert.ToDateTime(dtdFechaLabor.SelectedDate)).ToString();

            foreach (DataRow itemEmpleado in dt1.Rows)
            {
                itemEmpleado["IdLabor_Lote"] = int.Parse(Idlabor_lote);
            }

            if (dt != null)
            {
                foreach (DataRow itemInsumo in dt.Rows)
                {
                    itemInsumo["IdLabor_Lote"] = int.Parse(Idlabor_lote);
                }

                dt.Columns.Remove("NombreInsumo");
                MTerrenos.GetInstance().asociarInsumoLaborLote(dt);
                dt.Clear();
                dt.Columns.Add("NombreInsumo");
            }


            dt1.Columns.Remove("NombrePersona");

            MTerrenos.GetInstance().salarioEmpleado(dt1);


            if (lblLabores.Text.Equals("Recoleccion"))
            {
                int cantidad = 0;
                foreach (DataRow itemCantidad in dt1.Rows)
                {
                    cantidad += int.Parse(itemCantidad["Cantidad"].ToString());
                }

                MTerrenos.GetInstance().registrarProduccion(Convert.ToInt32(itemLotes.Id), Convert.ToDateTime(dtdFechaLabor.ToString()), cantidad);
            }
            dt1.Columns.Add("NombrePersona");

        }



    }
}
