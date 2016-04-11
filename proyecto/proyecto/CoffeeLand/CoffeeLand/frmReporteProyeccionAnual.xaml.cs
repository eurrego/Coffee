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
    /// Lógica de interacción para frmReporteProyeccionAnual.xaml
    /// </summary>
    public partial class frmReporteProyeccionAnual : UserControl
    {

        ReporteProyeccionAnual rptDoc = new ReporteProyeccionAnual();
        public frmReporteProyeccionAnual()
        {
            InitializeComponent();
        }

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

           
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = path + "\\Informe Proyección Anual.pdf";
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

        private void btnReporte_Click(object sender, RoutedEventArgs e)
        {
          
                    crystalReportsViewer1.ViewerCore.ReportSource = rptDoc;
          
        }
    }
}
