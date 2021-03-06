﻿using System;
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
using System.Collections;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.IO;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmPagos.xaml
    /// </summary>
    public partial class frmPagos : UserControl
    {

        #region Singleton
        private static frmPagos instance;

        public static frmPagos GetInstance()
        {
            if (instance == null)
            {
                instance = new frmPagos();
            }

            return instance;
        }
        #endregion

        int ingreso = 0;
        DataTable auxiliar = new DataTable();

        public frmPagos()
        {
            InitializeComponent();
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
            pnlMovContainer.Width = anchoContainer;

            tblPagos.Height = height - 285;
            tblDetallePago.Height = height - 285;
        }

        public void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        public void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }

        private void cmbTipoEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ingreso == 0)
            {
                if (cmbTipoEmpleado.SelectedIndex == 0)
                {
                    tblPagos.ItemsSource = null;
                    tblPagos.Items.Refresh();
                    pnlData.Visibility = Visibility.Collapsed;
                    pnlInicio.Visibility = Visibility.Visible;
                    pnlSinRegistros.Visibility = Visibility.Collapsed;
                    ingreso = 1;
                }
            }
            else
            {
                if (cmbTipoEmpleado.SelectedIndex == 0)
                {
                    tblPagos.ItemsSource = null;
                    tblPagos.Items.Refresh();
                    pnlData.Visibility = Visibility.Collapsed;
                    pnlInicio.Visibility = Visibility.Visible;
                    pnlSinRegistros.Visibility = Visibility.Collapsed;
                    btnPagar.Visibility = Visibility.Collapsed;
                }
            }


     
            if (cmbTipoEmpleado.SelectedIndex == 1)//consultar persona permanente
            {
                auxiliar.Reset();
                auxiliar.Columns.Add("DocumentoPersona");
                auxiliar.Columns.Add("Nombre");
                auxiliar.Columns.Add("Valor_a_pagar");
                auxiliar.Columns.Add("Valor_Deuda");

                tblPagos.ItemsSource = MPagos.GetInstance().ConsultarSalario(2) as IEnumerable;

                if (tblPagos.Items.Count != 0)
                {
                    pnlData.Visibility = Visibility.Visible;
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlSinRegistros.Visibility = Visibility.Collapsed;
                    lblTotal.Text = "$0";
                    lblTitleTipoPago.Text = "Empleado Permanente";
                    columnDetalle.Header = "Asignar Pago";
                    btnPagar.Visibility = Visibility.Visible;
                }
                else
                {
                    pnlData.Visibility = Visibility.Collapsed;
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlSinRegistros.Visibility = Visibility.Visible;
                    lblTotal.Text = "$0";
                    btnPagar.Visibility = Visibility.Collapsed;
                }

            }
            else if (cmbTipoEmpleado.SelectedIndex == 2)//consultar persona temporal
            {
                tblPagos.ItemsSource = MPagos.GetInstance().ConsultarSalario(1) as IEnumerable;

                if (tblPagos.Items.Count != 0)
                {
                    double valor = 0;

                    foreach (var item in tblPagos.Items)
                    {
                        Type v = item.GetType();
                        var cantidad = v.GetProperty("Valor_a_pagar").GetValue(item);
                        valor += Convert.ToDouble(cantidad);
                    }

                    lblTotal.Text = string.Format("{0:$0,0}", valor);
                    lblTitleTipoPago.Text = "Empleado Temporal";

                    pnlData.Visibility = Visibility.Visible;
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlSinRegistros.Visibility = Visibility.Collapsed;
                    columnDetalle.Header = "Detalle";
                    btnPagar.Visibility = Visibility.Visible;
                }
                else
                {
                    pnlData.Visibility = Visibility.Collapsed;
                    pnlInicio.Visibility = Visibility.Collapsed;
                    pnlSinRegistros.Visibility = Visibility.Visible;
                    lblTotal.Text = "$0";
                    btnPagar.Visibility = Visibility.Collapsed;
                }
            }
        }

        public static DataTable DataGridtoDataTable(DataGrid dg)
        {


            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);

            DataTable dt = new DataTable();
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
            }
            return dt;

        }

        private void btnDetallePago_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTipoEmpleado.SelectedIndex.Equals(2))//temporal
            {
                tabDetalle.Focus();
                PagosPersona_Result item = tblPagos.SelectedItem as PagosPersona_Result;
                tblDetallePago.ItemsSource = MPagos.GetInstance().DetalleSalario(int.Parse(item.DocumentoPersona)) as IEnumerable;

                lblNombreEmpleado.Text = item.Nombre.ToString();
                lblCedulaEmpleado.Text = item.DocumentoPersona.ToString();
            }
            else if (cmbTipoEmpleado.SelectedIndex.Equals(1))//permanente
            {
                tabAsignar.Visibility = Visibility.Visible;
                tabAsignar.Focus();
                txtPago.Text = string.Empty;
            }
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            tabInicio.Focus();
        }

        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            auxiliar = DataGridtoDataTable(tblPagos);
            string val = "0";

            if (cmbTipoEmpleado.SelectedIndex.Equals(1))//persona permanente
            {
                for (int i = 0; i < auxiliar.Rows.Count; i++)
                {
                    val = auxiliar.Rows[i].ItemArray[2].ToString();
                    if (val == "0")
                    {
                        mensajeError("Error Debe asignar un pago!");
                        break;
                    }
                }

                if (val != "0")
                {
                    
                    try
                    {
                        auxiliar.Columns.Remove("NOMBRE");
                        auxiliar.Columns.Remove("VALOR DEUDA");
                        auxiliar.Columns.Remove("ASIGNAR PAGO");
                        MPagos.GetInstance().insertarMultiplesSalarios(auxiliar, 1);
                        tblPagos.ItemsSource = null;
                        tblPagos.Items.Refresh();
                        cmbTipoEmpleado.SelectedIndex = 0;
                        lblTotal.Text = "$0";
                        mensajeInformacion("Registro de pago exitoso.");
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
            }
            else if (cmbTipoEmpleado.SelectedIndex.Equals(2))//persona temporal
            {

                try
                {
                    auxiliar.Columns.Remove("NOMBRE");
                    auxiliar.Columns.Remove("VALOR A PAGAR");
                    auxiliar.Columns.Remove("VALOR DEUDA");
                    auxiliar.Columns.Remove("DETALLE");

                    MPagos.GetInstance().insertarMultiplesSalarios(auxiliar, 2);

                    tblPagos.ItemsSource = null;
                    tblPagos.Items.Refresh();
                    cmbTipoEmpleado.SelectedIndex = 0;
                    lblTotal.Text = "$0";
                    mensajeInformacion("Registro de pago exitoso.");
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

        private void btnConfirmarPago_Click(object sender, RoutedEventArgs e)
        {
            PagosPersona_Result item = tblPagos.SelectedItem as PagosPersona_Result;

            item.Valor_a_pagar = decimal.Parse(txtPago.Text);
            tblPagos.Items.Refresh();

            tabAsignar.Visibility = Visibility.Collapsed;
            tabBuscar.Focus();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            tabAsignar.Visibility = Visibility.Collapsed;
            tabBuscar.Focus(); 
        }

        private void btnOcultar_Click(object sender, RoutedEventArgs e)
        {
            tabAsignar.Visibility = Visibility.Collapsed;
            tabBuscar.Focus();
            tabInicio.Focus();
            cmbTipoEmpleado.SelectedIndex = 0;
            lblTotal.Text = "$0";
        }
    }
}
