using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MVentas
    {
        #region Singleton

        private static MVentas instance;

        public static MVentas GetInstance()
        {
            if (instance == null)
            {
                instance = new MVentas();
            }

            return instance;
        }

        #endregion

        public List<Proveedor> ConsultarProveedor()
        {
            using (var entity = new DBFincaEntities())
            {
                List<Proveedor> lista = new List<Proveedor>()
                {
                    new Proveedor
                    {
                        Nit = 0.ToString(),
                        NombreProveedor = "Seleccione Proveedor"
                    }
                };

                var query = lista.Union(from c in entity.Proveedor
                                        where c.EstadoProveedor == "A"
                                        select c);

                return query.ToList();
            }
        }

        public List<Producto> ConsultarProducto()
        {
            using (var entity = new DBFincaEntities())
            {
                List<Producto> lista = new List<Producto>()
                {
                    new Producto
                    {
                        idProducto = 0,
                        NombreProducto = "Seleccione Producto"
                    }
                };

                var query = lista.Union(from c in entity.Producto
                                        where c.EstadoProducto == "A"
                                        select c);

                return query.ToList();
            }
        }

        public decimal ConsultarProduccion()
        {
            using (var entity = new DBFincaEntities())
            {

                var query = entity.Consultasproduccion().First();
                decimal? valor = query.Cargas;

                if (valor == null)
                {
                    return 0;
                }
                else
                {
                    return query.Cargas.Value;
                }

            }
        }

        public object ConsultarVentas()
        {
            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.Venta
                            join p in entity.Producto on c.idProducto equals p.idProducto
                            group c by new
                            {
                                p.NombreProducto,
                                c.Fecha,
                                c.PrecioCarga,
                                c.CantidadCargas

                            } into result
                            select new
                            {
                                NombreProducto = result.Key.NombreProducto,
                                Fecha = result.Key.Fecha,
                                PrecioCarga = result.Key.PrecioCarga,
                                CantidadCargas = result.Key.CantidadCargas,
                                Total = result.Sum(m => m.PrecioCarga * m.CantidadCargas)
                            };
                return query.ToList();
            }
        }

        public object ConsultaVentasFecha(DateTime parametroInicial, DateTime parametroFinal)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from v in entity.Venta
                            join c in entity.Producto on v.idProducto equals c.idProducto
                            where v.Fecha >= parametroInicial && v.Fecha <= parametroFinal
                            group v by new
                            {
                                c.NombreProducto,
                                v.Fecha,
                                v.PrecioCarga,
                                v.CantidadCargas

                            } into result
                            select new
                            {
                                NombreProducto = result.Key.NombreProducto,
                                Fecha = result.Key.Fecha,
                                PrecioCarga = result.Key.PrecioCarga,
                                CantidadCargas = result.Key.CantidadCargas,
                                Total = result.Sum(m => m.PrecioCarga * m.CantidadCargas)
                            };

                return query.ToList();
            }
        }



        public void GestionVenta(string nit, DateTime fecha, int numeroFactura, int idProducto, decimal PrecioCarga, decimal CantidadCargas, decimal PrecioBeneficio)
        {

            using (var entity = new DBFincaEntities())
            {

                var query = entity.GestionVenta(nit, fecha, numeroFactura, idProducto, PrecioCarga, CantidadCargas, PrecioBeneficio);
            }
        }

        public string ventaProduccion(decimal cantidad)
        {
            try
            {
                using (var entity = new DBFincaEntities())
                {

                    var query = entity.VentaProduccion(cantidad).First();

                    return query.Mensaje;
                }
            }
            catch (Exception ex )
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string filePath = @"" + path + "\\LogCo.txt";

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }

                return "Ha ocurrido un error inesperado, consulte con el administrador del sistema";
            }
          
        }

        public int ValidarFactura(int Factura, string Proveedor)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = (from c in entity.Compra
                             where c.NumeroFactura == Factura && c.NitProveedor == Proveedor
                             select c).Count();

                return query;
            }
        }
    }
}
