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
using System.IO;

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

        private static DataTable dtInsumo;
        private static DataTable dtPersona;
        private static DataTable dtArboles;
        private static DataTable dtMovimientoArboles;

        int initInsumo = 0;
        int initPersona = 0;
        int initArboles = 0;
        int initMovimientoArboles = 0;
        int index = -1;
        int cantidadArboles = 0;

        consultarMovimientosArboles_Result1 item;

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
            pnlContainerModificarArboles.Width = anchoContainer;

            tblLotes.Height = height - 285;
            columnLote.Width = anchoContainer - 70;
            tblLabores.Height = height - 285;
            columnLabor.Width = anchoContainer - 70;
            tblInsumos.Height = height - 285;
            tblProductividad.Height = height - 285;
            tblProductividad.Height = height - 285;
            tblArboles.Height = height - 285;
            tblMovimientoArboles.Height = height - 285;

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
                    if (cmbTipoPago.SelectedIndex == 0 || dtdFechaLabor.SelectedDate == null || lblLabores.Text.Equals("Seleccione una"))
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
                    lblLabores.Text = "Seleccione una";
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

                case 4:
                    cmbTipoArbolLote.SelectedIndex = 0;
                    cmbTipoArbolModificar.SelectedIndex = 0;
                    txtArbolesModicacion.Text = "0";
                    txtArbolesModicacionFinal.Text = "0";

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

                    if (initInsumo == 0)
                    {
                        dtInsumo = new DataTable();
                        dtInsumo.Columns.Add("idLabor_Lote");
                        dtInsumo.Columns.Add("IdInsumo");
                        dtInsumo.Columns.Add("Cantidad");
                        dtInsumo.Columns.Add("Precio");
                        dtInsumo.Columns.Add("NombreInsumo");
                        initInsumo++;
                    }
                    break;

                case 2:

                    if (initPersona == 0)
                    {
                        dtPersona = new DataTable();
                        dtPersona.Columns.Add("DocumentoPersona");
                        dtPersona.Columns.Add("idLabor_Lote");
                        dtPersona.Columns.Add("Cantidad");
                        dtPersona.Columns.Add("Valor");
                        dtPersona.Columns.Add("NombrePersona");

                        initPersona++;
                    }
                    break;

                case 3:

                    if (initArboles == 0)
                    {
                        dtArboles = new DataTable();
                        dtArboles.Columns.Add("idMovimientosArboles");
                        dtArboles.Columns.Add("NombreTipoArbol");
                        dtArboles.Columns.Add("Cantidad");
                        dtArboles.Columns.Add("Fecha");
                        initArboles++;
                    }
                    break;
                case 4:
                    if (initMovimientoArboles == 0)
                    {
                        dtMovimientoArboles = new DataTable();
                        dtMovimientoArboles.Columns.Add("idMovimiento");
                        dtMovimientoArboles.Columns.Add("idArbol");
                        dtMovimientoArboles.Columns.Add("NombreArbol");
                        dtMovimientoArboles.Columns.Add("Cantidad");
                        dtMovimientoArboles.Columns.Add("Fecha", typeof(DateTime));
                        initMovimientoArboles++;
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

                if (item.ModificaArboles && cmbTipoPago.SelectedItem.Equals("Contrato") || item.NombreLabor.Equals("Recolección") && cmbTipoPago.SelectedItem.Equals("Contrato"))
                {
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
                else if (!item.ModificaArboles && !item.NombreLabor.Equals("Recolección"))
                {
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
                else if (item.NombreLabor.Equals("Recolección"))
                {
                    mensajeError("Esta labor indica la producción por lote, por lo tanto el tipo de pago solo puede ser por contrato");

                }
                else
                {
                    mensajeError("La labor modifica el tipo de árbol, por lo tanto el tipo de pago solo puede ser por contrato");
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
                        int id = Convert.ToInt32(dtInsumo.Rows[i].ItemArray[1]);
                        int idCmb = Convert.ToInt32(cmbInsumo.SelectedValue);

                        if (idCmb == id)
                        {
                            cantidad += Convert.ToInt32(dtInsumo.Rows[i].ItemArray[2]);
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
                        int indexrow = -1;
                        long CantidadInsumo = 0;

                        for (int i = 0; i < dtInsumo.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(dtInsumo.Rows[i].ItemArray[1]) == Convert.ToInt64(insumo.idInsumo))
                            {
                                CantidadInsumo += Convert.ToInt64(dtInsumo.Rows[i].ItemArray[2]) + Convert.ToInt64(txtCantidadInsumo.Text);
                                indexrow = i;
                                break;
                            }

                        }

                        if (indexrow != -1)
                        {
                            dtInsumo.Rows.Add(null, cmbInsumo.SelectedValue, CantidadInsumo, insumo.PrecioPromedio, cmbInsumo.Text);
                            dtInsumo.Rows[indexrow].Delete();
                        }
                        else
                        {
                            dtInsumo.Rows.Add(null, cmbInsumo.SelectedValue, txtCantidadInsumo.Text, insumo.PrecioPromedio, cmbInsumo.Text);
                        }


                        tblInsumos.ItemsSource = dtInsumo.DefaultView;

                        pnlInicio.Visibility = Visibility.Collapsed;
                        pnlData.Visibility = Visibility.Visible;
                        limpiarCampos(2);

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
                dtInsumo.Clear();
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
            cmbInsumo.Text = dtInsumo.Rows[index].ItemArray[4].ToString();
            txtCantidadInsumo.Text = dtInsumo.Rows[index].ItemArray[2].ToString();

            dtInsumo.Rows[index].Delete();

            var cantidad = 0;

            for (int i = 0; i < tblInsumos.Items.Count; i++)
            {
                int id = Convert.ToInt32(dtInsumo.Rows[i].ItemArray[1]);
                int idCmb = Convert.ToInt32(cmbInsumo.SelectedValue);

                if (idCmb == id)
                {
                    cantidad += Convert.ToInt32(dtInsumo.Rows[i].ItemArray[2]);
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
            dtInsumo.Rows[index].Delete();
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
                limpiarCampos(2);

            }
        }

        private void btnAgregarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (Validar(4))
            {
                if (IsValid(txtCantidadProductividad) && IsValid(txtValorProductividad))
                {
                    crearTabla(2);

                    int indexrow = -1;
                    long ProductividadEmpleado = 0;
                    int ValorDiferenteEmpleado = 0;

                    for (int i = 0; i < dtPersona.Rows.Count; i++)
                    {

                        if (Convert.ToInt64(dtPersona.Rows[i].ItemArray[0]) == Convert.ToInt64(cmbEmpleado.SelectedValue))
                        {

                            if (Convert.ToInt64(dtPersona.Rows[i].ItemArray[3]) == Convert.ToInt64(txtValorProductividad.Text))
                            {
                                ProductividadEmpleado += Convert.ToInt64(dtPersona.Rows[i].ItemArray[2]) + Convert.ToInt64(txtCantidadProductividad.Text);
                                indexrow = i;
                                break;
                            }
                            else
                            {
                                mensajeError("No puede agregar el mismo empleado con diferentes valores");
                                ValorDiferenteEmpleado++;
                            }
                        }

                    }

                    if (indexrow != -1)
                    {
                        dtPersona.Rows[indexrow].Delete();
                        dtPersona.Rows.Add(cmbEmpleado.SelectedValue, null, ProductividadEmpleado, Convert.ToInt64(txtValorProductividad.Text), cmbEmpleado.Text);
                        limpiarCampos(3);
                    }
                    else
                    {
                        if (ValorDiferenteEmpleado == 0)
                        {
                            dtPersona.Rows.Add(cmbEmpleado.SelectedValue, null, Convert.ToInt64(txtCantidadProductividad.Text), Convert.ToInt64(txtValorProductividad.Text), cmbEmpleado.Text);
                            limpiarCampos(3);
                        }

                    }

                    tblProductividad.ItemsSource = dtPersona.DefaultView;

                    pnlInicioEmpleados.Visibility = Visibility.Collapsed;
                    pnlDataEmpleados.Visibility = Visibility.Visible;



                    if (cmbTipoPago.SelectedItem.Equals("Jornal"))
                    {
                        txtCantidadProductividad.Text = "1";
                    }

                }
            }
        }

        private void btnInhabilitarProductividadEmpleado_Click(object sender, RoutedEventArgs e)
        {
            index = tblProductividad.SelectedIndex;
            dtPersona.Rows[index].Delete();
            index = -1;


            if (dtPersona.Rows.Count == 0)
            {
                pnlDataEmpleados.Visibility = Visibility.Collapsed;
                pnlInicioEmpleados.Visibility = Visibility.Visible;
            }


        }
        private void btnModificarProductividadEmpleado_Click(object sender, RoutedEventArgs e)
        {
            CantidadProductividad();
            index = tblProductividad.SelectedIndex;
            cmbEmpleado.Text = dtPersona.Rows[index].ItemArray[4].ToString();
            txtCantidadProductividad.Text = dtPersona.Rows[index].ItemArray[2].ToString();
            txtValorProductividad.Text = dtPersona.Rows[index].ItemArray[3].ToString();

            dtPersona.Rows[index].Delete();
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

            if (tblInsumos.Items.Count == 0)
            {
                limpiarCampos(2);
                tabLabor.Focus();
                btnInsumo.IsChecked = false;

            }
            else
            {
                mensajeError("No debe tener insumos agregados");
            }


        }

        private void btnAtrasEmpleadosInsumos_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos(3);

            if (cmbTipoPago.SelectedItem.Equals("Jornal"))
            {
                txtCantidadProductividad.Text = "1";
            }

            Labor item = tblLabores.SelectedItem as Labor;


            if (tblProductividad.Items.Count == 0)
            {
                if (item.RequiereInsumo)
                {
                    tabInsumo.Focus();
                }
                else
                {
                    tabLabor.Focus();
                }
                btnEmpleado.IsChecked = false;

            }
            else
            {
                mensajeError("No debe tener empleados agregados");
            }

        }

        private void btnCancelarEmpleados_Click(object sender, RoutedEventArgs e)
        {
            if (tblProductividad.Items.Count != 0)
            {
                dtPersona.Clear();

                limpiarCampos(3);

                if (cmbTipoPago.SelectedItem.Equals("Jornal"))
                {
                    txtCantidadProductividad.Text = "1";
                }

                pnlDataEmpleados.Visibility = Visibility.Collapsed;
                pnlInicioEmpleados.Visibility = Visibility.Visible;
            }
            else
            {

                limpiarCampos(3);

                if (cmbTipoPago.SelectedItem.Equals("Jornal"))
                {
                    txtCantidadProductividad.Text = "1";
                }
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
                    cantidadArboles = 0;
                    foreach (DataRow itemCantidad in dtPersona.Rows)
                    {
                        cantidadArboles += int.Parse(itemCantidad["Cantidad"].ToString());
                    }

                    if (item.NombreLabor.Equals("Siembra"))
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
                                var itemlote = tblLotes.SelectedItem as MTerrenos;
                                GuardarDatos();
                                int almacigo = MTerrenos.GetInstance().ConsultarAlmacigo();
                                MTerrenos.GetInstance().MovimientoArboles(short.Parse(itemlote.Id.ToString()), Convert.ToByte(almacigo), cantidadArboles, Convert.ToDateTime(dtdFechaLabor.SelectedDate));
                                MTerrenos.GetInstance().ActualizarCantidadArboles();
                                mensajeInformacion("Registro exitoso");

                                volverInicio();
                            }
                            catch (Exception ex)
                            {
                                string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                                string filePath = @"" + path + "\\LogCo.txt";

                                using (StreamWriter writer = new StreamWriter(filePath, true))
                                {
                                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                                }

                                mensajeError( "Ha ocurrido un error inesperado, consulte con el administrador del sistema");
                            }
                        }

                    }
                    else
                    {
                        txtArbolesModicacion.IsEnabled = false;
                        txtArbolesModicacion.Text = cantidadArboles.ToString();
                        txtArbolesModicacionFinal.Text = cantidadArboles.ToString();

                        cmbTipoArbolLote.ItemsSource = MTerrenos.GetInstance().LaborModificaArbol(Convert.ToInt32(itemTerrenos.Id)) as IEnumerable;
                        cmbTipoArbolModificar.ItemsSource = MTerrenos.GetInstance().ConsultarTipoArboles();

                        btnArboles.IsChecked = true;
                        tabArboles.Focus();

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
                            volverInicio();
                        }
                        catch (Exception ex)
                        {
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                            string filePath = @"" + path + "\\LogCo.txt";

                            using (StreamWriter writer = new StreamWriter(filePath, true))
                            {
                                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                            }

                            mensajeError( "Ha ocurrido un error inesperado, consulte con el administrador del sistema");
                        }
                    }
                }
            }
        }

        private void cmbTipoArbolLote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Insumo insumo = cmbInsumo.SelectedItem as Insumo;

            if (cmbTipoArbolLote.SelectedIndex != 0)
            {

                tblArboles.ItemsSource = MTerrenos.GetInstance().ConsultarTiposArboles(Convert.ToInt32(cmbTipoArbolLote.SelectedValue)) as IEnumerable;
                if (tblArboles.Items.Count != 0)
                {
                    pnlDataArboles.Visibility = Visibility.Visible;
                    pnlInicioArboles.Visibility = Visibility.Collapsed;
                    pnlArbolesSinRegistros.Visibility = Visibility.Collapsed;
                    btnSiguienteArboles.IsEnabled = true;

                    crearTabla(4);
                    if (dtMovimientoArboles.Rows.Count != 0)
                    {
                        foreach (consultarMovimientosArboles_Result1 item in tblArboles.Items)
                        {

                            for (int i = 0; i < dtMovimientoArboles.Rows.Count; i++)
                            {
                                if (item.idMovimientosArboles == int.Parse(dtMovimientoArboles.Rows[i].ItemArray[0].ToString()))
                                {
                                    item.Cantidad = item.Cantidad - Convert.ToInt32(dtMovimientoArboles.Rows[i].ItemArray[3]);

                                }

                            }

                        }


                    }


                }


            }
            else
            {
                pnlDataArboles.Visibility = Visibility.Collapsed;
                pnlInicioArboles.Visibility = Visibility.Visible;
                pnlArbolesSinRegistros.Visibility = Visibility.Collapsed;
                btnSiguienteArboles.IsEnabled = false;
            }

        }

        private async void btnSguientearboles_Click(object sender, RoutedEventArgs e)
        {
            var item = tblLotes.SelectedItem as MTerrenos;

            int CantidadArbolesAModificar = txtArbolesModicacion.Text == null ? 0 : int.Parse(txtArbolesModicacion.Text);
            if (CantidadArbolesAModificar == 0)
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
                        Labor itemLabor = tblLabores.SelectedItem as Labor;
                        if (itemLabor.NombreLabor.Equals("Eliminación"))
                        {
                            MTerrenos.GetInstance().InsertarMovimientoArbloes(dtMovimientoArboles);
                            MTerrenos.GetInstance().ActualizarCantidadArboles();
                            GuardarDatos();
                            mensajeInformacion("Registro exitoso");
                            dtMovimientoArboles.Clear();
                            volverInicio();
                        }
                        else
                        {
                            int cantidadTotalMovimiento = 0;

                            for (int i = 0; i < dtMovimientoArboles.Rows.Count; i++)
                            {
                                cantidadTotalMovimiento += Convert.ToInt32(dtMovimientoArboles.Rows[i].ItemArray[3]);
                            }

                            MTerrenos.GetInstance().InsertarMovimientoArbloes(dtMovimientoArboles);
                            MTerrenos.GetInstance().MovimientoArboles(short.Parse(item.Id.ToString()), byte.Parse(cmbTipoArbolModificar.SelectedValue.ToString()), cantidadTotalMovimiento, Convert.ToDateTime(dtdFechaLabor.SelectedDate));
                            MTerrenos.GetInstance().ActualizarCantidadArboles();
                            GuardarDatos();
                            mensajeInformacion("Registro exitoso");
                            dtMovimientoArboles.Clear();
                            volverInicio();

                        }


                    }
                    catch (Exception ex)
                    {
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        string filePath = @"" + path + "\\LogCo.txt";

                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                               "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                        }

                        mensajeError( "Ha ocurrido un error inesperado, consulte con el administrador del sistema");
                    }
                }

            }
            else
            {
                mensajeError("No puede puede guardar hasta que los árboles a modificar esten en cero");
            }
        }



        public void volverInicio()
        {
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
            pnlInicio.Visibility = Visibility.Visible;
            pnlData.Visibility = Visibility.Collapsed;
            pnlInicioEmpleados.Visibility = Visibility.Visible;
            pnlDataEmpleados.Visibility = Visibility.Collapsed;
        }


        public void GuardarDatos()
        {

            Labor itemLabor = tblLabores.SelectedItem as Labor;
            MTerrenos itemLotes = tblLotes.SelectedItem as MTerrenos;

            string Idlabor_lote = MTerrenos.GetInstance().asociarLaborLote(Convert.ToInt32(itemLabor.idLabor), Convert.ToInt32(itemLotes.Id), Convert.ToDateTime(dtdFechaLabor.SelectedDate)).ToString();

            foreach (DataRow itemEmpleado in dtPersona.Rows)
            {
                itemEmpleado["IdLabor_Lote"] = int.Parse(Idlabor_lote);
            }

            if (dtInsumo != null)
            {
                foreach (DataRow itemInsumo in dtInsumo.Rows)
                {
                    itemInsumo["IdLabor_Lote"] = int.Parse(Idlabor_lote);
                }

                dtInsumo.Columns.Remove("NombreInsumo");
                MTerrenos.GetInstance().asociarInsumoLaborLote(dtInsumo);
                dtInsumo.Clear();
                dtInsumo.Columns.Add("NombreInsumo");
            }


            dtPersona.Columns.Remove("NombrePersona");

            MTerrenos.GetInstance().salarioEmpleado(dtPersona);


            if (lblLabores.Text.Equals("Recolección"))
            {
                int cantidad = 0;
                foreach (DataRow itemCantidad in dtPersona.Rows)
                {
                    cantidad += int.Parse(itemCantidad["Cantidad"].ToString());
                }

                MTerrenos.GetInstance().registrarProduccion(Convert.ToInt32(itemLotes.Id), Convert.ToDateTime(dtdFechaLabor.ToString()), cantidad);
            }
            dtPersona.Columns.Add("NombrePersona");
            dtPersona.Clear();
            mostrar();
        }

        private void btnAtrasArboles_Click(object sender, RoutedEventArgs e)
        {

            if (tblMovimientoArboles.Items.Count == 0)
            {
                limpiarCampos(4);
                tabEmpleados.Focus();
                btnArboles.IsChecked = false;
            }
            else
            {
                mensajeError("No pueden haber registros agregados en la tabla de movimiento de los árboles ");
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Labor itemLabor = tblLabores.SelectedItem as Labor;
            item = tblArboles.SelectedItem as consultarMovimientosArboles_Result1;
            if (cmbTipoArbolModificar.Text != cmbTipoArbolLote.Text || itemLabor.NombreLabor.Equals("Eliminación"))
            {

                if (cmbTipoArbolModificar.SelectedIndex != 0 && txtCantidadArbolesAModificar.Text != "" || itemLabor.NombreLabor.Equals("Eliminación"))
                {

                    int CantidadArbolesAModificar = txtCantidadArbolesAModificar.Text == "" ? 0 : int.Parse(txtCantidadArbolesAModificar.Text);


                    if (CantidadArbolesAModificar <= cantidadArboles)
                    {

                        if (CantidadArbolesAModificar <= item.Cantidad)
                        {
                            int cantidadTotalArboles = 0;
                            int totalArboles = 0;
                            int indexRow = -1;
                            for (int i = 0; i < dtMovimientoArboles.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dtMovimientoArboles.Rows[i].ItemArray[0]) == Convert.ToInt32(item.idMovimientosArboles))
                                {
                                    cantidadTotalArboles += Convert.ToInt32(dtMovimientoArboles.Rows[i].ItemArray[3]);
                                    indexRow = i;
                                    break;
                                }
                            }

                            if (indexRow != -1)
                            {
                                dtMovimientoArboles.Rows[indexRow].Delete();
                            }

                            totalArboles = item.Cantidad - CantidadArbolesAModificar;
                            item.Cantidad = item.Cantidad - CantidadArbolesAModificar;
                             

                            tblArboles.Items.Refresh();

                            dtMovimientoArboles.Rows.Add(item.idMovimientosArboles, cmbTipoArbolModificar.SelectedValue.ToString(), item.NombreTipoArbol, (CantidadArbolesAModificar + cantidadTotalArboles), Convert.ToDateTime(dtdFechaLabor.SelectedDate));

                            pnlDataModificarArboles.Visibility = Visibility.Visible;
                            pnlInicioModificarArboles.Visibility = Visibility.Collapsed;
                            cantidadArboles = cantidadArboles - CantidadArbolesAModificar;
                            txtArbolesModicacion.Text = cantidadArboles.ToString();
                            txtArbolesModicacionFinal.Text = cantidadArboles.ToString();
                            lblArbolesExistentes.Text = totalArboles.ToString();
                            tblMovimientoArboles.ItemsSource = dtMovimientoArboles.DefaultView;
                            tabNuevoArboles.Focus();
                            txtCantidadArbolesAModificar.Text = string.Empty;
                            tblArboles.IsEnabled = true;
                            


                        }
                        else
                        {
                            mensajeError("La cantidad de árboles que ingreso no puede ser superior a la cantidad que ha seleccionado");
                        }

                    }
                    else
                    {
                        mensajeError("La cantidad de árboles que ingreso no puede ser mayor a la cantidad a modificar");
                    }

                }

                else
                {
                    mensajeError("Debe ingresar todos los campos");
                }
            }
            else
            {
                mensajeError("Debe seleccionar un tipo de árbol diferente, ya que no puede pasar al mismo tipo");
            }
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            item = tblArboles.SelectedItem as consultarMovimientosArboles_Result1;

            if (tblMovimientoArboles.Items.Count != 0)
            {
                cmbTipoArbolModificar.IsEnabled = false;
                pnlDataModificarArboles.Visibility = Visibility.Visible;
                pnlInicioModificarArboles.Visibility = Visibility.Collapsed;
            }
            else
            {
                cmbTipoArbolModificar.IsEnabled = true;
                pnlDataModificarArboles.Visibility = Visibility.Collapsed;
                pnlInicioModificarArboles.Visibility = Visibility.Visible;
            }

            Labor itemLabor = tblLabores.SelectedItem as Labor;
            if (itemLabor.NombreLabor.Equals("Eliminación"))
            {
                cmbTipoArbolModificar.Visibility = Visibility.Collapsed;
            }
            else
            {
                cmbTipoArbolModificar.Visibility = Visibility.Visible;
            }

            tblArboles.IsEnabled = false;

            lblArbolSeleccionado.Text = item.NombreTipoArbol;
            lblArbolesExistentes.Text = item.Cantidad.ToString();
            tabSeleccionar.Focus();
        }

        private void btnatras_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtArbolesModicacionFinal.Text.Equals("0"))
            {
                mensajeError("Los árboles a modificar han llegado a cero, si quiere volver atras debe eliminar un registro");
            }
            else
            {
                //txtCantidadArbolesAModificar.Text = string.Empty;
                tblArboles.IsEnabled = true;
                tabArboles.Focus();
            }
        }

        private void btnEliminarAlborModificar_Click(object sender, RoutedEventArgs e)
        {
            index = tblMovimientoArboles.SelectedIndex;

            cantidadArboles += Convert.ToInt32(dtMovimientoArboles.Rows[index].ItemArray[3]);
            txtArbolesModicacion.Text = cantidadArboles.ToString();
            txtArbolesModicacionFinal.Text = cantidadArboles.ToString(); 

            if (dtMovimientoArboles.Rows.Count != 0)
            {
                foreach (consultarMovimientosArboles_Result1 item in tblArboles.Items)
                {
                    if (Convert.ToInt32(item.idMovimientosArboles) == int.Parse(dtMovimientoArboles.Rows[index].ItemArray[0].ToString()))
                    {
                        item.Cantidad = item.Cantidad + Convert.ToInt32(dtMovimientoArboles.Rows[index].ItemArray[3]);

                    }

                }

                tblArboles.Items.Refresh();
                dtMovimientoArboles.Rows[index].Delete();
            }

            if (tblMovimientoArboles.Items.Count == 0)
            {
                tblArboles.IsEnabled = true;
                tabNuevoArboles.Focus();
                tblArboles.ItemsSource = MTerrenos.GetInstance().ConsultarTiposArboles(Convert.ToInt32(cmbTipoArbolLote.SelectedValue)) as IEnumerable;
                txtCantidadArbolesAModificar.Text = string.Empty;
                pnlInicioModificarArboles.Visibility = Visibility.Visible;
                pnlDataModificarArboles.Visibility = Visibility.Collapsed;
            }

        }

        private void btnAdelanteArboles_Click(object sender, RoutedEventArgs e)
        {
            if (tblMovimientoArboles.Items.Count != 0)
            {
                tabSeleccionar.Focus();
            }
            else
            {
                mensajeError("Debe seleccionar el árbol a modificar");
            }
        }
    }
}
