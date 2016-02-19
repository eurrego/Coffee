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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmdetalleArboles.xaml
    /// </summary>
    public partial class frmDetalleArboles : UserControl
    {
        #region Singleton

        private static frmDetalleArboles instance;

        public static frmDetalleArboles GetInstance()
        {
            if (instance == null)
            {
                instance = new frmDetalleArboles(0, string.Empty, string.Empty, 0 , 0);
            }

            return instance;
        }
        #endregion

        string lote;
        string arbol;
        int idArboles;
        int idTipoArboles;
        int idLote;

        public frmDetalleArboles(int _idLote, string _lote, string _arbol, int _idArboles, int _idTipoArboles)
        {
            InitializeComponent();
            lote = _lote;
            arbol = _arbol;
            idArboles = _idArboles;
            idTipoArboles = _idTipoArboles;
            idLote = _idLote;
            Mostrar();

            instance = this;
        }

        public void Mostrar()
        {
            tblMovimientosArboles.ItemsSource = MArbol.GetInstance().ConsultarMovimiento(idArboles);
            lblLoteConsultar.Text = lote;
            lblArbol.Text = arbol;
            cantidadTotal();
        }

        // define el total de arboles
        private void cantidadTotal()
        {
            int valor = 0;

            foreach (var item in tblMovimientosArboles.Items)
            {
                Type v = item.GetType();
                var cantidad = v.GetProperty("Cantidad").GetValue(item).ToString();
                valor += Convert.ToInt32(cantidad);
            }

            lblCantidad.Text = string.Format("{0:0,0}", valor);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            MovimientosArboles item = tblMovimientosArboles.SelectedItem as MovimientosArboles;

            frmArboles.GetInstance().tabNuevo.Header = "EDITAR";
            frmArboles.GetInstance().tabNuevo.Focus();
            frmGestionTerrenos.GetInstance().tabArboles.Focus();
            frmGestionTerrenos.GetInstance().tabDetalleArboles.Visibility = Visibility.Visible;

            frmArboles.GetInstance().txtCantidad.Text = item.Cantidad.ToString();
            frmArboles.GetInstance().dtdFecha.SelectedDate = item.Fecha;
            frmArboles.GetInstance().cmbTipoArbol.SelectedValue = idTipoArboles;
            frmArboles.GetInstance().txtId.Text = item.idMovimientosArboles.ToString();
            frmArboles.GetInstance().tblArboles.IsEnabled = false;

        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            MovimientosArboles item = tblMovimientosArboles.SelectedItem as MovimientosArboles;

            int idArbol = item.idArboles;
            int cantidad = item.Cantidad;
            DateTime fecha = item.Fecha;
            int tipoArbol = idTipoArboles;
            int idMovimientoArbol = item.idMovimientosArboles;


            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",

            };

            MessageDialogResult result = await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                string rpta = "";

                rpta = MArbol.GetInstance().gestionArboles(Convert.ToInt16(idLote), Convert.ToByte(tipoArbol), cantidad, fecha, idMovimientoArbol, 3).ToString();
                await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", rpta);
                Mostrar();
                tblMovimientosArboles.ItemsSource = MArbol.GetInstance().ConsultarMovimiento(idArbol);

            }

            frmGestionTerrenos.GetInstance().tabDetalleArboles.Visibility = Visibility.Visible;
        }
    }
}
