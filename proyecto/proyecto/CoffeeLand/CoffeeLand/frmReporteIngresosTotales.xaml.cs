using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using CrystalDecisions.Shared;
using MahApps.Metro.Controls;
using CoffeeLand.Reportes;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmReporteIngresosTotales.xaml
    /// </summary>
    public partial class frmReporteIngresosTotales : UserControl
    {
        ReporteIngresosTotales rptDoc = new ReporteIngresosTotales();
        bool validacion = false;

        public frmReporteIngresosTotales()
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
            crystalReportsViewer1.Width = anchoContainer - 20;

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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = path + "\\Informe Ingresos Totales.pdf";
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

        private bool Validar()
        {
            if (dtdFechaInicio.SelectedDate.Equals(null) || dtdFechaFin.SelectedDate.Equals(null) )
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

        private void btnReporte_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                if (dtdFechaInicio.SelectedDate <= dtdFechaFin.SelectedDate)
                {
                    rptDoc.SetParameterValue("@fecha_inicial", DateTime.Parse(dtdFechaInicio.SelectedDate.ToString()));
                    rptDoc.SetParameterValue("@fecha_fin", DateTime.Parse(dtdFechaFin.SelectedDate.ToString()));


                    crystalReportsViewer1.ViewerCore.ReportSource = rptDoc;

                }
                else
                {
                    mensajeError("La fecha inicial, no puede ser mayor que la fecha final.");
                }
            }
         
        }
    }
}
