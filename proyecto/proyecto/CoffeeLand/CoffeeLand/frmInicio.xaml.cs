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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmInicio.xaml
    /// </summary>
    public partial class frmInicio : UserControl
    {
        #region Singleton

        private static frmInicio instance;

        public static frmInicio GetInstance()
        {
            if (instance == null)
            {
                instance = new frmInicio();
            }

            return instance;
        }

        #endregion


        bool validacion = false;

        public frmInicio()
        {
            InitializeComponent();
            instance = this;
            mostrar();
            tamanioPantalla();
        }

        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 100;

            var anchoContainer = width / 2.25;
            pnlInfo.Width = anchoContainer;

        }


        public void mostrar()
        {

            string finca = string.Empty;
            string telefono = string.Empty;

            cmbDepartamento.ItemsSource = MFinca.GetInstance().ConsultarDepartamento();

            var itemLotes = MInicio.GetInstance().ConsultarLote();
            var itemArboles = MInicio.GetInstance().ConsultarCantidadArboles();
            var itemEmpleados = MInicio.GetInstance().ConsultarEmpleados();
            var itemProductos = MInicio.GetInstance().ConsultarEmpleados();


            if (itemArboles == 0)
            {
                lblArboles.Text = "0";
            }
            else
            {
                lblArboles.Text = string.Format("{0:0,0}", itemArboles);
            }

            lblEmpleados.Text = string.Format("{0:0}", itemEmpleados.Count);
            lblLotes.Text = string.Format("{0:0}", itemLotes.Count);
            lblProductos.Text = string.Format("{0:0}", itemProductos.Count);

            infoFinca();

            var items = MFinca.GetInstance().ConsultarFinca();

            foreach (var item in items)
            {
                Type v = item.GetType();
                finca = v.GetProperty("NombreFinca").GetValue(item).ToString();
                telefono = v.GetProperty("Telefono").GetValue(item).ToString();
            }

            if (telefono == "123")
            {
                tabUpdateFinca.Visibility = Visibility.Visible;
                tabInfo.Visibility = Visibility.Collapsed;
            }
            else
            {
                tabInfo.Visibility = Visibility.Visible;
                tabUpdateFinca.Visibility = Visibility.Collapsed;
            }
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            tabFormulario.Visibility = Visibility.Visible;
            tabUpdateFinca.Visibility = Visibility.Collapsed;
        }

        // limpiar Controles
        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtPropietario.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtId.Text = string.Empty;
            txtVereda.Text = string.Empty;
            cmbDepartamento.SelectedItem = null;
            cmbMunicipio.SelectedItem = null;
        }


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {
                if (IsValid(txtNombre) && IsValid(txtPropietario) && IsValid(txtTelefono) && IsValid(txtVereda))
                {
                    int tamañoLotes = 0;
                    IEnumerable<Lote> item = MLote.GetInstance().tamañoLotesCompletos() as IEnumerable<Lote>;

                    foreach (Lote item2 in item)
                    {
                        tamañoLotes += int.Parse(item2.Cuadras);
                    }

                    if (tamañoLotes <= int.Parse(txtCuadras.Text))
                    {


                        MFinca.GetInstance().modificarFinca(txtNombre.Text, txtPropietario.Text, int.Parse(cmbMunicipio.SelectedValue.ToString()), txtVereda.Text, txtTelefono.Text, txtCuadras.Text);
                        mensajeInformacion("Registro Exitoso");
                        Limpiar();
                        infoFinca();
                        tabInfo.Visibility = Visibility.Visible;
                        tabUpdateFinca.Visibility = Visibility.Collapsed;
                        tabFormulario.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        mensajeError("El tamaño de la finca no puede ser menor que el de los lotes");
                    }
                }
            }
        }


        private void infoFinca()
        {
            var items = MFinca.GetInstance().ConsultarFinca();

            foreach (var item in items)
            {
                Type v = item.GetType();
                lblInfoFinca.Text = v.GetProperty("NombreFinca").GetValue(item).ToString();
                lblPropietario.Text = v.GetProperty("Propietario").GetValue(item).ToString();
                lblTelefono.Text = v.GetProperty("Telefono").GetValue(item).ToString();
                lblVereda.Text = v.GetProperty("Vereda").GetValue(item).ToString();
                lblCuadras.Text = v.GetProperty("Cuadras").GetValue(item).ToString();


                var municipio = MFinca.GetInstance().ConsultarMunicipioParametro(Convert.ToInt32(v.GetProperty("idMunicipio").GetValue(item))) as IEnumerable<Municipio>;


                foreach (var mun in municipio)
                {
                    lblMunicipio.Text = mun.NombreMunicipio;
                    int id = mun.idDepartamento;

                    var departamento = MFinca.GetInstance().ConsultarDepartamentoParametro(id) as IEnumerable<Departamento>;

                    foreach (var dep in departamento)
                    {
                        lblDepartamento.Text = dep.NombreDepartamento;
                    }

                }
            }
        }



        // Validación de campos
        private bool validarCampos()
        {

            if (txtCuadras.Text == string.Empty || txtNombre.Text == string.Empty || txtPropietario.Text == string.Empty || txtTelefono.Text == string.Empty || txtVereda.Text == string.Empty || cmbDepartamento.SelectedIndex == 0 || cmbMunicipio.SelectedIndex == 0)
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            string finca = string.Empty;
            string telefono = string.Empty;

            var items = MFinca.GetInstance().ConsultarFinca();

            foreach (var item in items)
            {
                Type v = item.GetType();
                finca = v.GetProperty("NombreFinca").GetValue(item).ToString();
                telefono = v.GetProperty("Telefono").GetValue(item).ToString();
            }

            if (telefono == "123")
            {
                tabUpdateFinca.Visibility = Visibility.Visible;
                tabInfo.Visibility = Visibility.Collapsed;
                tabFormulario.Visibility = Visibility.Collapsed;
            }
            else
            {
                tabInfo.Visibility = Visibility.Visible;
                tabUpdateFinca.Visibility = Visibility.Collapsed;
                tabFormulario.Visibility = Visibility.Collapsed;
            }
        }

        private void cmbDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDepartamento.SelectedIndex >= 1)
            {
                cmbMunicipio.ItemsSource = MFinca.GetInstance().ConsultarMunicipios(int.Parse(cmbDepartamento.SelectedValue.ToString()));
                cmbMunicipio.IsEnabled = true;
            }
            else
            {
                cmbMunicipio.IsEnabled = false;
            }
       
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = lblInfoFinca.Text;
            txtPropietario.Text = lblPropietario.Text;
            txtTelefono.Text = lblTelefono.Text;
            txtVereda.Text = lblVereda.Text;
            txtCuadras.Text = lblCuadras.Text;
            cmbDepartamento.Text = lblDepartamento.Text;
            cmbMunicipio.Text = lblMunicipio.Text;

            tabUpdateFinca.Visibility = Visibility.Collapsed;
            tabInfo.Visibility = Visibility.Collapsed;
            tabFormulario.Visibility = Visibility.Visible;
        }
    }
}

