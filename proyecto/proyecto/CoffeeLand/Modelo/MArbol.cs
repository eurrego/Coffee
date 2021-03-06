﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MArbol
    {
        #region Singleton

        private static MArbol instance;

        public static MArbol GetInstance()
        {
            if (instance == null)
            {
                instance = new MArbol();
            }

            return instance;
        }

        #endregion

        public string NombreTipoArbol { get; set; }
        public int Cantidad { get; set; }
        public int idTipoArbol { get; set; }

        public List<Lote> ConsultarLote()
        {
            using (var entity = new DBFincaEntities())
            {
                List<Lote> lista = new List<Lote>()
                {
                    new Lote
                    {
                        idLote = 0,
                        NombreLote = "Seleccione un Lote..."
                    }
                };

                var query = lista.Union(from c in entity.Lote
                                        where c.EstadoLote == "A"
                                        select c);

                return query.ToList();
            }
        }

        public List<TipoArbol> ConsultarTipoArbol()
        {
            using (var entity = new DBFincaEntities())
            {
                List<TipoArbol> lista = new List<TipoArbol>()
                {
                    new TipoArbol
                    {
                        idTipoArbol = 0,
                        NombreTipoArbol = "Seleccione un Tipo de Arbol..."
                    }
                };

                var query = lista.Union(from c in entity.TipoArbol
                                        where c.EstadoTipoArbol == "A"
                                        select c);

                return query.ToList();
            }
        }

        public int ConsultarCantidad(int parametroLote)
        {
            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.Arboles
                            where c.idLote == parametroLote
                            select new { c.Cantidad };

                int cantidad = query.Sum(total => total.Cantidad);

                return cantidad;
            }
        }

        public object ConsultarArboles(int parametroLote)
        {
            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.Arboles
                            join t in entity.TipoArbol on c.idTIpoArbol equals t.idTipoArbol
                            where c.idLote == parametroLote
                            select new
                            {
                                idArboles = c.idArboles,
                                idTipoArbol = c.idTIpoArbol,
                                NombreTipoArbol = t.NombreTipoArbol,
                                Cantidad = c.Cantidad
                            };

                return query.ToList();
            }
        }


        public List<MovimientosArboles> ConsultarMovimiento(int parametroArboles)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.MovimientosArboles
                            where c.idArboles == parametroArboles
                            select c;

                return query.ToList();
            }
        }


        public List<Labor_Lote> ConsultarLabor(int idLote)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Labor_Lote
                            where c.idLote == idLote && c.Labor.ModificaArboles == true
                            select c;

                return query.ToList();
            }
        }

        public string gestionArboles(short idLote, byte idTipoArbol, int cantidad, DateTime fecha, int idMovimiento, int opcion)
        {
            try
            {
                using (var entity = new DBFincaEntities())
                {
                    var rpta = entity.gestionArboles(idLote, idTipoArbol, cantidad, fecha, idMovimiento, opcion).First();
                    return rpta.Mensaje;
                }
            }
            catch (Exception ex)
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
    }
}
