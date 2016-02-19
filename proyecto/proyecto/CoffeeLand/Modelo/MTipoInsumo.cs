using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MTipoInsumo
    {
        #region Singleton

        private static MTipoInsumo instance;

        public static MTipoInsumo GetInstance()
        {
            if (instance == null)
            {
                instance = new MTipoInsumo();
            }

            return instance;
        }

        #endregion

        public List<TipoInsumo> ConsultarTipoInsumo()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoInsumo
                            where c.EstadoTipoInsumo == "A"
                            select c;
                return query.ToList();
            }
        }

        public List<TipoInsumo> ConsultarInactivos()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoInsumo
                            where c.EstadoTipoInsumo == "I"
                            select c;
                return query.ToList();
            }
        }

        public List<TipoInsumo> ConsultarParametroTipoInsumo(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoInsumo
                            where c.EstadoTipoInsumo == "A" && c.NombreTipoInsumo.Contains(parametro)
                            select c;

                return query.ToList();
            }

        }

        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<TipoInsumo> ConsultarParametroInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoInsumo
                            where c.EstadoTipoInsumo == "I" && c.NombreTipoInsumo.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        public string GestionTipoInsumo(string nombre, string descripcion, int id, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.gestionTipoInsumo(nombre.ToUpper(), descripcion.ToUpper(), id, opcion).First();
                    return rpta.Mensaje;
                }
                catch (Exception ex)
                {
                    string filePath = @"C:\Users\Susy\Desktop\LogCoffeeLand.txt";

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

}
