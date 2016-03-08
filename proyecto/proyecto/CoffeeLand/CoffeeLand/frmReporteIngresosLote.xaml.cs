using System;
using System.Windows;
using System.Windows.Controls;
using Modelo;
using CoffeeLand.Reportes;
using CrystalDecisions.Shared;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls.Primitives;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmReporteIngresosLote.xaml
    /// </summary>
    public partial class frmReporteIngresosLote : UserControl
    {

        rptIngresosLote rptDoc = new rptIngresosLote();


        string name = Environment.MachineName;
        string name1 = System.Net.Dns.GetHostName();

        string name2 = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

        public frmReporteIngresosLote()
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
            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = "C:\\Users\\"+name+"\\Desktop\\Informe Ingeresos Lote.pdf";
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




            if (cmbLotes.SelectedIndex != 0)
            {
                int idlote = int.Parse(cmbLotes.SelectedValue.ToString());
                if (!dtdFechaInicio.SelectedDate.Equals(null) && !dtdFechaFin.SelectedDate.Equals(null))
                {
                    if (dtdFechaInicio.SelectedDate <= dtdFechaFin.SelectedDate)
                    {


                        rptDoc.Load("C:\\Users\\Naits\\Documents\\GitHub\\coffee\\proyecto\\proyecto\\CoffeeLand\\CoffeeLand\\Reportes\\rptIngresosLote.rpt");
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
                else
                {
                    mensajeError("Por favor seleccione la fecha inicial.");
                    dtdFechaFin.SelectedDate = null;
                }
            }
            else
            {
                mensajeError("Por favor seleccione un Lote.");
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

            pnlPrincipal.Height = height - 220;
        }

       
    }
}
