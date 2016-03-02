using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using CrystalDecisions.Shared;
using CoffeeLand.Reportes;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmReporteDeudaEmpleado.xaml
    /// </summary>
    public partial class frmReporteDeudaEmpleado : UserControl

    {
        ReporteDeudaEmpleado rptDoc = new ReporteDeudaEmpleado();

        public frmReporteDeudaEmpleado()
        {
            InitializeComponent();
            dtdFechaInicio.DisplayDateEnd = DateTime.Now;
            dtdFechaFin.DisplayDateEnd = DateTime.Now;
            tamanioPantalla();
        }

        private void tamanioPantalla()
        {
            var width = SystemParameters.WorkArea.Width;
            var height = SystemParameters.WorkArea.Height;

            Width = width;
            Height = height - 175;

            var anchoContainer = width / 1.5;
            pnlPrincipal.Width = anchoContainer;

            pnlPrincipal.Height = height - 220;
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

        private void btnExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = "C:\\Users\\Naits\\Desktop\\Informe Deudas Empleados.pdf";
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

        private void btnReporte_Click(object sender, RoutedEventArgs e)
        {
            if (!dtdFechaInicio.SelectedDate.Equals(null))
            {
                if (dtdFechaInicio.SelectedDate <= dtdFechaFin.SelectedDate)
                {
                    rptDoc.Load("C:\\Users\\Caty\\Documents\\GitHub\\CoffeeLand.Metro\\Proyecto\\CoffeeLand\\ReporteDeudaEmpleado.rpt");

                    rptDoc.SetParameterValue("@FECHA_INI", DateTime.Parse(dtdFechaInicio.SelectedDate.ToString()));
                    rptDoc.SetParameterValue("@FECHA_FIN", DateTime.Parse(dtdFechaFin.SelectedDate.ToString()));


                    crystalReportsViewer1.ViewerCore.ReportSource = rptDoc;

                }
                else
                {
                    mensajeError("La fecha inicial, no puede ser mayor que la fecha final.");
                    dtdFechaFin.SelectedDate = null;
                }
            }
            else
            {
                mensajeError("Por favor seleccione la fecha inicial.");
                dtdFechaFin.SelectedDate = null;
            }
        }
    }
}
