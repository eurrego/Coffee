using System;
using System.Windows;
using System.Windows.Controls;
using Modelo;
using CoffeeLand.Reportes;
using CrystalDecisions.Shared;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmPagoEmpleados.xaml
    /// </summary>
    public partial class frmPagoEmpleados : UserControl
    {
        ReportePagoEmpleadoPermanente rptDoc = new ReportePagoEmpleadoPermanente();
        ReportePagoEmpleadosTemp rptDoc1 = new ReportePagoEmpleadosTemp();
        bool validacion = false;

        public frmPagoEmpleados()
        {
            InitializeComponent();
            llenarCmbTipoEmpleado();
            dtdFechaInicio.DisplayDateEnd = DateTime.Now;
            dtdFechaFin.DisplayDateEnd = DateTime.Now;
            tamanioPantalla();
        }


        private void llenarCmbTipoEmpleado()
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un tipo de empleado");
            data.Add("Empleado Temporal");
            data.Add("Empleado Permanente");

            cmbTipoEmpleado.ItemsSource = data;
        }



        public void mensajeError(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", mensaje);
        }

        public void mensajeInformacion(string mensaje)
        {
            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Información", mensaje);
        }


        public void exportarReportePDFPermanente()
        {
            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                CrDiskFileDestinationOptions.DiskFileName = "C:\\Users\\Naits\\Desktop\\Informe Pago Empleado Permante.pdf";

                CrExportOptions = rptDoc.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                rptDoc.Export();
                mensajeInformacion("El informe fue exportado al escritorio");
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString().Equals("-2147215870"))
                {
                    mensajeError("Por favor seleccione las fechas para generar el informe.");
                }

            }

        }

        public void exportarReportePDFTemporal()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                CrDiskFileDestinationOptions.DiskFileName = path + "\\Informe Pago Empleado Temporal.pdf";

                CrExportOptions = rptDoc1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                rptDoc1.Export();
                mensajeInformacion("El informe fue exportado al escritorio");
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString().Equals("-2147215870"))
                {
                    mensajeError("Por favor seleccione las fechas para generar el informe.");
                }

            }
        }

        private bool Validar()
        {
            if (dtdFechaInicio.SelectedDate.Equals(null) || dtdFechaFin.SelectedDate.Equals(null) || cmbTipoEmpleado.SelectedIndex == 0)
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



        public void generarReporteEmpleadoPermanente()
        {

            if (Validar())
            {

               if (dtdFechaInicio.SelectedDate <= dtdFechaFin.SelectedDate)
                    {

                        rptDoc.SetParameterValue("@FECHA_INI", DateTime.Parse(dtdFechaInicio.SelectedDate.ToString()));
                        rptDoc.SetParameterValue("@FECHA_FIN", DateTime.Parse(dtdFechaFin.SelectedDate.ToString()));

                        //   rptDoc.SetDataSource(Mreporte.GetInstance().funcionreporteingresosLote(idlote, DateTime.Parse(dtdFechaInicio.SelectedDate.ToString()), DateTime.Parse(dtdFechaFin.SelectedDate.ToString())) as IEnumerable);

                        crystalReportsViewer2.ViewerCore.ReportSource = rptDoc;

                    }
                    else
                    {
                        mensajeError("La fecha inicial, no puede ser mayor que la fecha final.");
                    }
              
            }

        }

        public void generarReporteEmpleadoTemporal()
        {

            if (Validar())
            {

               
                    if (dtdFechaInicio.SelectedDate <= dtdFechaFin.SelectedDate)
                    {

                        rptDoc1.Load("C:\\Users\\Naits\\Documents\\GitHub\\coffee\\proyecto\\proyecto\\CoffeeLand\\CoffeeLand\\Reportes\\ReportePagoEmpleadoPermanente.rpt");

                        rptDoc1.SetParameterValue("@FECHA_INI", DateTime.Parse(dtdFechaInicio.SelectedDate.ToString()));
                        rptDoc1.SetParameterValue("@FECHA_FIN", DateTime.Parse(dtdFechaFin.SelectedDate.ToString()));

                        //   rptDoc.SetDataSource(Mreporte.GetInstance().funcionreporteingresosLote(idlote, DateTime.Parse(dtdFechaInicio.SelectedDate.ToString()), DateTime.Parse(dtdFechaFin.SelectedDate.ToString())) as IEnumerable);

                        crystalReportsViewer2.ViewerCore.ReportSource = rptDoc1;

                    }
                    else
                    {
                        mensajeError("La fecha inicial, no puede ser mayor que la fecha final.");
                    }
            }
  
        }

        private void btnExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTipoEmpleado.Text.Equals("Empleado Permanente"))
            {
                exportarReportePDFPermanente();
            }
            else
            {
                exportarReportePDFTemporal();
            }

        }

        private void btnReporte_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTipoEmpleado.Text.Equals("Empleado Permanente"))
            {
                generarReporteEmpleadoPermanente();
            }
            else
            {
                generarReporteEmpleadoTemporal();
            }

        }


        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 1.5;
            pnlPrincipal.Width = anchoContainer;
            crystalReportsViewer2.Width = anchoContainer - 20;

            pnlPrincipal.Height = height - 220;
        }



    }
}
