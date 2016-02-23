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
using System.Collections;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmTerrenos.xaml
    /// </summary>
    public partial class frmTerrenos : UserControl
    {
        public frmTerrenos()
        {
            InitializeComponent();
            mostrar();
        }

        private void mostrar()
        {
            var list = MTerrenos.GetInstance().ConsultarLote() as IEnumerable;

            foreach (var item in list)
            {
                Type v = item.GetType();
                var cantidad = v.GetProperty("Cantidad").GetValue(item).ToString();
                var nombre = v.GetProperty("NombreLote").GetValue(item).ToString();
                var cuadras = v.GetProperty("Cuadras").GetValue(item).ToString();
               


                StackPanel container = new StackPanel();
                container.Children.Add(
                new TextBlock
                {
                    FontSize = 60,
                    Margin = new Thickness(5, 5, 5, 5),
                    Text = nombre
                });

                container.Children.Add(

                new TextBlock
                {
                    FontSize = 10,
                    Margin = new Thickness(5, 5, 5, 5),
                    Text = "Cuadras"
                }

                );

                container.Children.Add(

                new TextBlock
                {
                    FontSize = 35,
                    Margin = new Thickness(5, 5, 5, 5),
                    Text = cuadras
                }

                );

                container.Children.Add(

               new TextBlock
               {
                   FontSize = 10,
                   Margin = new Thickness(5, 5, 5, 5),
                   Text = "Cantidad"
               }

               );

                container.Children.Add(
                new TextBlock
                {
                    FontSize = 18,
                    Margin = new Thickness(5, 5, 5, 5),
                    Text = cantidad
                });

                listLotes.Items.Add(container);
            }

        }
    }
}
