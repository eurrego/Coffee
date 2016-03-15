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
using System.Globalization;

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
        int idTipoArbol = 0;
        int idArboles = 0;

        public frmArboles()
        {
            InitializeComponent();
            instance = this;
            mostrarCmb();
            mostrarTipoArbol();
            tamanioPantalla();
            dtdFecha.DisplayDateEnd = DateTime.Now;
        }

        public void seleccionarLote(int lote)
        {
            cmbLote.SelectedValue = lote;
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

            tblArboles.Height = height - 280;
            tblMovimientosArboles.Height = height - 280;
        }

        public void mostrarTipoArbol()
        {
            cmbTipoArbol.ItemsSource = MArbol.GetInstance().ConsultarTipoArbol();
            cmbTipoArbol.SelectedIndex = 0;
        }

        public void mostrarCmb()
        {
            cmbEditarTipoArbol.ItemsSource = MArbol.GetInstance().ConsultarTipoArbol();
            cmbLote.ItemsSource = MArbol.GetInstance().ConsultarLote();
            cmbLote.SelectedIndex = 0;
        }

        private void Mostrar(int id)
        {
            tblArboles.ItemsSource = MArbol.GetInstance().ConsultarArboles(id) as IEnumerable;
            cantidadTotal();
            cmbTipoArbol.SelectedIndex = 0;
            cmbEditarTipoArbol.SelectedIndex = 0;
        }

        // limpiar Controles
        private void Limpiar()
        {
            cmbTipoArbol.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            dtdFecha.SelectedDate = null;
        }


        // limpiar Controles Editar
        private void LimpiarEditar()
        {
            cmbEditarTipoArbol.SelectedIndex = 0;
            txtEditarCantidad.Text = string.Empty;
            dtdEditarFecha.SelectedDate = null;
            txtEditarId.Text = string.Empty;
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

            lblTotal.Text = string.Format("{0:0,0}", valor);

            if (lblTotal.Text == "00")
            {
                lblTotal.Text = "0";
            }
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbTipoArbol.SelectedIndex == 0 || txtCantidad.Text == string.Empty || dtdFecha.SelectedDate == null)
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



        // Validación de campos
        private bool validarCamposEditar()
        {

            if (cmbEditarTipoArbol.SelectedIndex == 0 || txtEditarCantidad.Text == string.Empty || dtdEditarFecha.SelectedDate == null)
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

        private void cmbLote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLote.SelectedIndex == 0)
            {
                tblArboles.ItemsSource = null;
                lblTotal.Text = "0";
                lblSuperior.Text = "Seleccione un";
                lblInferior.Text = "Lote";
                pnlInicio.Visibility = Visibility.Visible;
                pnlData.Visibility = Visibility.Collapsed;
                tabNuevo.Visibility = Visibility.Collapsed;
                lblBuscarLote.Visibility = Visibility.Collapsed;
            }
            else
            {
                Lote item = cmbLote.SelectedItem as Lote;

                var id = item.idLote;
                var tolower = item.NombreLote.ToLower();
                lblLote.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblNuevoLote.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblBuscarLote.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
                lblBuscarLote.Visibility = Visibility.Visible;

                Mostrar(id);

                if (tblArboles.Items.Count == 0)
                {
                    lblSuperior.Text = "Este Lote no tiene";
                    lblInferior.Text = "árboles registrados";
                    pnlInicio.Visibility = Visibility.Visible;
                    pnlData.Visibility = Visibility.Collapsed;
                    tabNuevo.Visibility = Visibility.Visible;
                }
                else
                {
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlData.Visibility = Visibility.Visible;
                    tabNuevo.Visibility = Visibility.Visible;
                }
            }
        }


        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        private void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        // define el total de arboles
        private void cantidadMovTotal()
        {
            int valor = 0;

            foreach (var item in tblMovimientosArboles.Items)
            {
                Type v = item.GetType();
                var cantidad = v.GetProperty("Cantidad").GetValue(item).ToString();
                valor += Convert.ToInt32(cantidad);
            }

            lblTotalDetalle.Text = string.Format("{0:0,0}", valor);
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

            int idLote = Convert.ToInt32(cmbLote.SelectedValue);
            var items = MArbol.GetInstance().ConsultarLabor(idLote);

            if (items.Count == 0)
            {

                if (validarCampos())
                {
                    if (IsValid(txtCantidad) && IsValid(cmbTipoArbol))
                    {
                        rpta = MArbol.GetInstance().gestionArboles(Convert.ToInt16(cmbLote.SelectedValue), Convert.ToByte(cmbTipoArbol.SelectedValue), Convert.ToInt32(txtCantidad.Text), Convert.ToDateTime(dtdFecha.SelectedDate), 0, 1).ToString();
                        mensajeInformacion(rpta);
                        Mostrar(Convert.ToInt16(cmbLote.SelectedValue));
                        Limpiar();
                        tabBuscar.Focus();
                        tblArboles.IsEnabled = true;


                        if (tblArboles.Items.Count == 0)
                        {
                            lblSuperior.Text = "Este Lote no tiene";
                            lblInferior.Text = "árboles registrados";
                            pnlInicio.Visibility = Visibility.Visible;
                            pnlData.Visibility = Visibility.Collapsed;
                            tabNuevo.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            pnlInicio.Visibility = Visibility.Collapsed;
                            pnlData.Visibility = Visibility.Visible;
                            tabNuevo.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            else
            {
                mensajeError("Los árboles no pueden editarse porque ya existe una modificación de tipo de árbol asociada, debe hacerlo a través de una labor.");
            }
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            tabBuscar.Focus();
        }

        private void btnDetalle_Click(object sender, RoutedEventArgs e)
        {
            var objeto = tblArboles.SelectedItem;
            Type t = objeto.GetType();
            string arbol = t.GetProperty("NombreTipoArbol").GetValue(objeto).ToString();

            Lote item = cmbLote.SelectedItem as Lote;

            var tolower = item.NombreLote.ToLower();
            var tolowerArbol = arbol;

            lblDetalleLote.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);
            lblMovTipoArbol.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolowerArbol);
            lblEditarLote.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tolower);

            mostrarDetalle();
            tabDetalle.Focus();
        }

        private void mostrarDetalle()
        {
            var objeto = tblArboles.SelectedItem;
            Type t = objeto.GetType();
            idArboles = Convert.ToInt32(t.GetProperty("idArboles").GetValue(objeto));
            idTipoArbol = Convert.ToInt32(t.GetProperty("idTipoArbol").GetValue(objeto));

            tblMovimientosArboles.ItemsSource = MArbol.GetInstance().ConsultarMovimiento(idArboles);
            cantidadMovTotal();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            tabArboles.Focus();
        }

        private void btnOcultar_Click(object sender, RoutedEventArgs e)
        {
            tblArboles.ItemsSource = null;
            pnlInicio.Visibility = Visibility.Visible;
            pnlData.Visibility = Visibility.Collapsed;
            tabBuscar.Focus();
            cmbLote.SelectedIndex = 0;
            tblArboles.IsEnabled = true;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            int idLote = Convert.ToInt32(cmbLote.SelectedValue);
            var items = MArbol.GetInstance().ConsultarLabor(idLote);

            if (items.Count == 0)
            {
                MovimientosArboles item = tblMovimientosArboles.SelectedItem as MovimientosArboles;

                txtEditarCantidad.Text = item.Cantidad.ToString();
                dtdEditarFecha.SelectedDate = item.Fecha;
                cmbEditarTipoArbol.SelectedValue = idTipoArbol;
                txtEditarId.Text = item.idMovimientosArboles.ToString();

                tblMovimientosArboles.IsEnabled = false;
                btnAtras.IsEnabled = false;
                tabDetalleLote.IsEnabled = false;
                tabEditar.Visibility = Visibility.Visible;
                tabEditar.Focus();
            }
            else
            {
                mensajeError("Los árboles no pueden editarse porque ya existe una modificación de tipo de árbol asociada, debe hacerlo a través de una labor.");
            }
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {

            MovimientosArboles item = tblMovimientosArboles.SelectedItem as MovimientosArboles;

            int idArbol = item.idArboles;
            int cantidad = item.Cantidad;
            DateTime fecha = item.Fecha;
            int idMovimientoArbol = item.idMovimientosArboles;
            int idLote =Convert.ToInt32(cmbLote.SelectedValue);

            var items = MArbol.GetInstance().ConsultarLabor(idLote);

            if (items.Count == 0)
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Aceptar",
                    NegativeButtonText = "Cancelar",
                };

                MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Eliminar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result != MessageDialogResult.Negative)
                {
                    string rpta = "";

                    rpta = MArbol.GetInstance().gestionArboles(Convert.ToInt16(cmbLote.SelectedValue), Convert.ToByte(idTipoArbol), cantidad, fecha, idMovimientoArbol, 3).ToString();
                    mensajeInformacion(rpta);

                    Mostrar(Convert.ToInt16(cmbLote.SelectedValue));
                    tblMovimientosArboles.ItemsSource = MArbol.GetInstance().ConsultarMovimiento(idArbol);
                    cantidadMovTotal();

                    if (tblMovimientosArboles.Items.Count == 0)
                    {
                        if (tblArboles.Items.Count == 0)
                        {
                            lblSuperior.Text = "Este Lote no tiene";
                            lblInferior.Text = "árboles registrados";
                            pnlInicio.Visibility = Visibility.Visible;
                            pnlData.Visibility = Visibility.Collapsed;
                            tabNuevo.Visibility = Visibility.Visible;
                            tabArboles.Focus();
                        }
                        else
                        {
                            pnlInicio.Visibility = Visibility.Collapsed;
                            pnlData.Visibility = Visibility.Visible;
                            tabNuevo.Visibility = Visibility.Visible;
                            tabArboles.Focus();
                        }
                    }
                }
            }
            else
            {
                mensajeError("Los árboles no pueden editarse porque ya existe una modificación de tipo de árbol asociada, debe hacerlo a través de una labor.");
            } 
        }

        private void btnEditarCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEditar();
            tabDetalleLote.IsEnabled = true;
            tabEditar.Visibility = Visibility.Collapsed;
            tblMovimientosArboles.IsEnabled = true;
            btnAtras.IsEnabled = true;

            tabDetalleLote.Focus();
        }

        private void btnEditarGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";
            MovimientosArboles item = tblMovimientosArboles.SelectedItem as MovimientosArboles;
            int idLote = Convert.ToInt32(cmbLote.SelectedValue);

            if (validarCamposEditar())
            {
                if (IsValid(txtEditarCantidad) && IsValid(cmbEditarTipoArbol))
                {
                    var items = MArbol.GetInstance().ConsultarLabor(idLote);

                    if (items.Count == 0)
                    {
                        rpta = MArbol.GetInstance().gestionArboles(Convert.ToInt16(cmbLote.SelectedValue), Convert.ToByte(cmbEditarTipoArbol.SelectedValue), Convert.ToInt32(txtEditarCantidad.Text), Convert.ToDateTime(dtdEditarFecha.SelectedDate), Convert.ToInt32(txtEditarId.Text), 2).ToString();
                        mensajeInformacion(rpta);
                        Mostrar(Convert.ToInt16(cmbLote.SelectedValue));
                        LimpiarEditar();
                        tabDetalleLote.IsEnabled = true;
                        tabEditar.Visibility = Visibility.Collapsed;
                        tblMovimientosArboles.IsEnabled = true;
                        btnAtras.IsEnabled = true;

                        tabDetalleLote.Focus();

                        tblMovimientosArboles.ItemsSource = MArbol.GetInstance().ConsultarMovimiento(idArboles);
                        cantidadMovTotal();

                        if (tblArboles.Items.Count != 0)
                        {
                            pnlInicio.Visibility = Visibility.Collapsed;
                            pnlData.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            pnlData.Visibility = Visibility.Collapsed;
                            pnlInicio.Visibility = Visibility.Visible;
                        }

                        if (tblMovimientosArboles.Items.Count != 0)
                        {

                        }
                    }
                    else
                    {
                        mensajeError("Los árboles no pueden eliminarse porque ya existe una modificación de tipo de árbol asociada, debe hacerlo atravez de una labor.");
                    }

                }
            }
        }
    }
}
