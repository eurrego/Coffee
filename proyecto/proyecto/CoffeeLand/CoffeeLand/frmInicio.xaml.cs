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
    /// Lógica de interacción para frmInicio.xaml
    /// </summary>
    public partial class frmInicio : UserControl
    {
        public frmInicio()
        {
            InitializeComponent();
            mostrar();
        }

        private void mostrar()
        {
            var itemLotes = MInicio.GetInstance().ConsultarLote();
            var itemArboles = MInicio.GetInstance().ConsultarCantidadArboles();
            var itemEmpleados = MInicio.GetInstance().ConsultarEmpleados();
            var itemProductos = MInicio.GetInstance().ConsultarEmpleados();

            lblArboles.Text = string.Format("{0:0,0}", itemArboles);
            lblEmpleados.Text = string.Format("{0:0}", itemEmpleados.Count);
            lblLotes.Text = string.Format("{0:0}", itemLotes.Count);
            lblProductos.Text = string.Format("{0:0}", itemProductos.Count);

        }
    }
}
