using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MCompra
    {
        #region Singleton

        private static MCompra instance;

        public static MCompra GetInstance()
        {
            if (instance == null)
            {
                instance = new MCompra();
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
                        NombreProveedor = "Seleccione un Proveedor..."
                    }
                };

                var query = lista.Union(from c in entity.Proveedor
                                        where c.EstadoProveedor == "A"
                                        select c);

                return query.ToList();
            }
        }

        public List<TipoInsumo> ConsultarTipoInsumo()
        {
            using (var entity = new DBFincaEntities())
            {
                List<TipoInsumo> lista = new List<TipoInsumo>()
                {
                    new TipoInsumo
                    {
                        idTipoInsumo = 0,
                        NombreTipoInsumo = "Seleccione un Tipo de insumo..."
                    }
                };

                var query = lista.Union(from c in entity.TipoInsumo
                                        where c.EstadoTipoInsumo == "A"
                                        select c);

                return query.ToList();
            }
        }

        public List<Insumo> ConsultarInsumo(byte idTipoInsumo)
        {
            using (var entity = new DBFincaEntities())
            {
                List<Insumo> lista = new List<Insumo>()
                {
                    new Insumo
                    {
                        idInsumo = 0,
                        NombreInsumo = "Seleccione un Insumo..."
                    }
                };

                var query = lista.Union(from c in entity.Insumo
                                        where c.EstadoInsumo == "A" && c.idTipoInsumo == idTipoInsumo
                                        select c);

                return query.ToList();
            }
        }

        public object ConsultaComprasEstadoCuenta(int parametro)
        {
            using (var entity = new DBFincaEntities())
            {

                if (parametro == 1)
                {
                    var query = from c in entity.Compra
                                join p in entity.Proveedor on c.NitProveedor equals p.Nit
                                join d in entity.Compra_Insumo on c.idCompra equals d.idCompra
                                where c.EstadoCuenta == "D"
                                group d by new
                                {
                                    d.Precio,
                                    c.NitProveedor,
                                    p.NombreProveedor,
                                    c.Fecha,
                                    c.NumeroFactura,
                                    c.EstadoCuenta
                                } into total
                                select new
                                {
                                    NitProveedor = total.Key.NitProveedor,
                                    NombreProveedor = total.Key.NombreProveedor,
                                    Fecha = total.Key.Fecha,
                                    NumeroFactura = total.Key.NumeroFactura,
                                    Total = total.Sum(m => m.Precio * m.Cantidad),
                                    EstadoCuenta = total.Key.EstadoCuenta
                                };
                    return query.ToList();
                }
                else if (parametro == 2)
                {
                    var query = from c in entity.Compra
                                join p in entity.Proveedor on c.NitProveedor equals p.Nit
                                join d in entity.Compra_Insumo on c.idCompra equals d.idCompra
                                where c.EstadoCuenta == "P"
                                group d by new
                                {
                                    d.Precio,
                                    c.NitProveedor,
                                    p.NombreProveedor,
                                    c.Fecha,
                                    c.NumeroFactura,
                                    c.EstadoCuenta
                                } into total
                                select new
                                {
                                    NitProveedor = total.Key.NitProveedor,
                                    NombreProveedor = total.Key.NombreProveedor,
                                    Fecha = total.Key.Fecha,
                                    NumeroFactura = total.Key.NumeroFactura,
                                    Total = total.Sum(m => m.Precio * m.Cantidad),
                                    EstadoCuenta = total.Key.EstadoCuenta
                                };
                    return query.ToList();
                }
                else if (parametro == 3)
                {
                    var query = from c in entity.Compra
                                join p in entity.Proveedor on c.NitProveedor equals p.Nit
                                join d in entity.Compra_Insumo on c.idCompra equals d.idCompra
                                group d by new
                                {
                                    d.Precio,
                                    c.NitProveedor,
                                    p.NombreProveedor,
                                    c.Fecha,
                                    c.NumeroFactura,
                                    c.EstadoCuenta
                                } into total
                                select new
                                {
                                    NitProveedor = total.Key.NitProveedor,
                                    NombreProveedor = total.Key.NombreProveedor,
                                    Fecha = total.Key.Fecha,
                                    NumeroFactura = total.Key.NumeroFactura,
                                    Total = total.Sum(m => m.Precio * m.Cantidad),
                                    EstadoCuenta = total.Key.EstadoCuenta
                                };
                    return query.ToList();
                }

                return null;
            }
        }

        public object ConsultaComprasFecha(DateTime parametroInicial, DateTime parametroFinal)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Compra
                            join p in entity.Proveedor on c.NitProveedor equals p.Nit
                            join d in entity.Compra_Insumo on c.idCompra equals d.idCompra
                            where c.Fecha >= parametroInicial && c.Fecha <= parametroFinal
                            group d by new
                            {
                                d.Precio,
                                c.NitProveedor,
                                p.NombreProveedor,
                                c.Fecha,
                                c.NumeroFactura,
                                c.EstadoCuenta
                            } into total
                            select new
                            {
                                NitProveedor = total.Key.NitProveedor,
                                NombreProveedor = total.Key.NombreProveedor,
                                Fecha = total.Key.Fecha,
                                NumeroFactura = total.Key.NumeroFactura,
                                Total = total.Sum(m => m.Precio * m.Cantidad),
                                EstadoCuenta = total.Key.EstadoCuenta
                            };
                return query.ToList();
            }
        }


        public object ConsultaComprasNombre(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Compra
                            join p in entity.Proveedor on c.NitProveedor equals p.Nit
                            join d in entity.Compra_Insumo on c.idCompra equals d.idCompra
                            where p.NombreProveedor == parametro
                            group d by new
                            {
                                d.Precio,
                                c.NitProveedor,
                                p.NombreProveedor,
                                c.Fecha,
                                c.NumeroFactura,
                                c.EstadoCuenta
                            } into total
                            select new
                            {
                                NitProveedor = total.Key.NitProveedor,
                                NombreProveedor = total.Key.NombreProveedor,
                                Fecha = total.Key.Fecha,
                                NumeroFactura = total.Key.NumeroFactura,
                                Total = total.Sum(m => m.Precio * m.Cantidad),
                                EstadoCuenta = total.Key.EstadoCuenta
                            };
                return query.ToList();
            }
        }




        public string RegistroCompra(string nit, DateTime fecha, int numeroFactura)
        {

            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.RegistrarCompra(nit, fecha, numeroFactura).First();
                return rpta.Mensaje;
            }
        }

        public void RegistroDetalleCompra(DataTable DetalleCompra)
        {

            using (var entity = new DBFincaEntities())
            {
                entity.SP_InsertarDetalleCompra(DetalleCompra);

            }
        }
    }
}
