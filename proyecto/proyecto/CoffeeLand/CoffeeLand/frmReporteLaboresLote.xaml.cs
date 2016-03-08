using System;
using System.Windows;
using System.Windows.Controls;
using Modelo;
using CoffeeLand.Reportes;
using CrystalDecisions.Shared;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmReporteLaboresLote.xaml
    /// </summary>
    public partial class frmReporteLaboresLote : UserControl
    {
        ReporteLaboresLote rptDoc = new ReporteLaboresLote();
        bool validacion = false;

        public frmReporteLaboresLote()
        {
            InitializeComponent();
            tamanioPantalla();
            cmbLotes.ItemsSource = MTerrenos.GetInstance().ConsultarLoteCmb();
            dtdFechaInicio.DisplayDateEnd = DateTime.Now;
            dtdFechaFin.DisplayDateEnd = DateTime.Now;
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

            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = path + "\\Informe Labores Lote.pdf";
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
            if (dtdFechaInicio.SelectedDate.Equals(null) || dtdFechaFin.SelectedDate.Equals(null) || cmbLotes.SelectedIndex == 0)
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
                int idlote = int.Parse(cmbLotes.SelectedValue.ToString());

                if (dtdFechaInicio.SelectedDate <= dtdFechaFin.SelectedDate)
                {
                    rptDoc.SetParameterValue("@idLote", idlote);
                    rptDoc.SetParameterValue("@fecha_ini", DateTime.Parse(dtdFechaInicio.SelectedDate.ToString()));
                    rptDoc.SetParameterValue("@fecha_fin", DateTime.Parse(dtdFechaFin.SelectedDate.ToString()));

                    //   rptDoc.SetDataSource(Mreporte.GetInstance().funcionreporteingresosLote(idlote, DateTime.Parse(dtdFechaInicio.SelectedDate.ToString()), DateTime.Parse(dtdFechaFin.SelectedDate.ToString())) as IEnumerable);

                    crystalReportsViewer2.ViewerCore.ReportSource = rptDoc;

                }
                else
                {
                    mensajeError("La fecha inicial, no puede ser mayor que la fecha final.");
                    dtdFechaFin.SelectedDate = null;
                }
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
