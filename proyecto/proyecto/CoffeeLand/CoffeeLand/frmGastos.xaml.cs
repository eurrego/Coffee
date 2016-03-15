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

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using Modelo;
using System.Data;
using System.IO;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmGastos.xaml
    /// </summary>
    public partial class frmGastos : UserControl
    {
        #region Singleton

        private static frmGastos instance;

        public static frmGastos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmGastos();
            }

            return instance;
        }

        #endregion


        public static DataTable dt;
        int index = -1;

        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmGastos()
        {
            InitializeComponent();
            Mostrar();
            instance = this;
            tamanioPantalla();
        }

        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 1.75;
            pnlContainer.Width = anchoContainer;

            tblGastos.Height = height - 320;
        }

        public void Mostrar()
        {
            cmbConcepto.ItemsSource = MGastos.GetInstance().ConsultarConcepto();
            dtdFechaGasto.DisplayDateEnd = DateTime.Now;
            cmbConcepto.SelectedIndex = 0;
        }

        public void TotalGasto()
        {
            long total = 0;

            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToInt64(row["Valor"].ToString());
            }

            lblTotal.Text = string.Format("{0:$0,0}", total);
        }

        public void crearTabla()
        {
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("idConcepto");
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Fecha", typeof(DateTime));
                dt.Columns.Add("Valor");
                dt.Columns.Add("EstadoCuenta");
                dt.Columns.Add("Concepto");
            }
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        private void mensajeExitoso(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }


        // Validación de campos
        private bool validarCampos()
        {

            if ( txtDescripcionGasto.Text == string.Empty || txtValor.Text == string.Empty || cmbConcepto.SelectedIndex == 0 || dtdFechaGasto.SelectedDate == null)
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

        public void limpiarCampos()
        {
            cmbConcepto.SelectedIndex = 0;
            txtValor.Text = string.Empty;
            dtdFechaGasto.Text = null;
            txtDescripcionGasto.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Con este Procedimiento se envia una tabla completa a la 
                //base de datos, enviandole un DataTable como parametro 
                var db = new DBFincaEntities();
                dt.Columns.Remove("Concepto");
                db.SP_InsertMultiplesGastos(dt);
                dt.Clear();
                pnlEstado.Visibility = Visibility.Collapsed;
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                mensajeExitoso("Registro exitoso");

                dt.Columns.Add("Concepto");
                frmConsultarGastos.GetInstance().Mostrar();

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

                mensajeError("Ha ocurrido un error inesperado, consulte con el administrador del sistema");
            }

        }

        private void btnAgregarGasto_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {
                if (IsValid(txtValor) && IsValid(txtDescripcionGasto))
                {
                    crearTabla();

                    dt.Rows.Add(cmbConcepto.SelectedValue, txtDescripcionGasto.Text, Convert.ToDateTime(dtdFechaGasto.SelectedDate), txtValor.Text, "D", cmbConcepto.Text);
 
                    tblGastos.ItemsSource = dt.DefaultView;
                    limpiarCampos();
                    TotalGasto();
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlData.Visibility = Visibility.Visible;
                    pnlEstado.Visibility = Visibility.Visible;
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


        private void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            index = tblGastos.SelectedIndex;
            dt.Rows[index].Delete();
            index = -1;
            TotalGasto();

            if (tblGastos.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                pnlEstado.Visibility = Visibility.Collapsed;
            }
        }


        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            index = tblGastos.SelectedIndex;
            cmbConcepto.SelectedValue = dt.Rows[index].ItemArray[0];
            txtValor.Text = dt.Rows[index].ItemArray[3].ToString();
            dtdFechaGasto.SelectedDate = Convert.ToDateTime(dt.Rows[index].ItemArray[2]);
            txtDescripcionGasto.Text = dt.Rows[index].ItemArray[1].ToString();

            dt.Rows[index].Delete();
            TotalGasto();

            if (tblGastos.Items.Count == 0)
            {
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
                pnlEstado.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (tblGastos.Items.Count != 0)
            {
                dt.Clear();
                limpiarCampos();
                pnlEstado.Visibility = Visibility.Collapsed;
                pnlData.Visibility = Visibility.Collapsed;
                pnlInicio.Visibility = Visibility.Visible;
            }
            else
            {
                limpiarCampos();
            }
        }  
    }
}
