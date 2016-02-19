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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmDetalleCuenta.xaml
    /// </summary>
    public partial class frmDetalleCuenta : UserControl
    {
        ComprasProveedor_Result1 item;
        string proveedor;

        public frmDetalleCuenta(ComprasProveedor_Result1 _item, string _proveedor)
        {
            InitializeComponent();
            item = _item;
            proveedor = _proveedor;
            Mostrar();
        }

        private void Mostrar()
        {
            lblFacturaDetalleCompra.Text = item.NumeroFactura.ToString();
            lblFechaDetalleCompra.Text = string.Format("{0:MMM d, yyyy}", item.Fecha);

            lblProveedorDetalleCompra.Text = proveedor;

            lblTotalDetalleCompra.Text = string.Format("{0:c}", item.total);
            tblDetalleCompra.ItemsSource = MEstadoCuenta.GetInstance().ConsultaDetalleCompra(item.idCompra) as IEnumerable;
        }
    }
}
