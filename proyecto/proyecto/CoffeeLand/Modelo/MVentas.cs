﻿using System;
using System.Collections.Generic;
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

                return query.Cargas.Value;

            }
        }


        public void GestionVenta(int nit, DateTime fecha, int numeroFactura, int idProducto, decimal PrecioCarga, decimal CantidadCargas, decimal PrecioBeneficio)
        {

            using (var entity = new DBFincaEntities())
            {

                var query = entity.GestionVenta(nit, fecha, numeroFactura, idProducto, PrecioCarga, CantidadCargas, PrecioBeneficio);


            }
        }

        public string ventaProduccion(decimal cantidad)

        {
            using (var entity = new DBFincaEntities())
            {

                var query = entity.VentaProduccion(cantidad).First();

                return query.Mensaje;

            }
        }
    }
}
