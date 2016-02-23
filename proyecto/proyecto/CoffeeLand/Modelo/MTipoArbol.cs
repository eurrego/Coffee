using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MTipoArbol
    {
        #region Singleton

        private static MTipoArbol instance;

        public static MTipoArbol GetInstance()
        {
            if (instance == null)
            {
                instance = new MTipoArbol();
            }

            return instance;
        }

        #endregion

        public object registrarTipoArbol(String NombreArbol, String Descripcion, int idTipoArbol, int opc)
        {

            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.gestionTipoArboles(NombreArbol.ToUpper(), Descripcion.ToUpper(), idTipoArbol, opc).First();
                    return rpta.Mensaje;
                }
                catch (Exception ex)
                {
                    string filePath = @"C:\Users\Snug\LogCoffeeLand.txt";

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

        public List<TipoArbol> ConsultarInactivos()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoArbol
                            where c.EstadoTipoArbol == "I"
                            select c;
                return query.ToList();
            }
        }


        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<TipoArbol> ConsultarParametroInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoArbol
                            where c.EstadoTipoArbol == "I" && c.NombreTipoArbol.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        public List<TipoArbol> buscarTipoArbol(string NombreArbol)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoArbol
                            where c.EstadoTipoArbol == "A" && c.NombreTipoArbol.Contains(NombreArbol)
                            select c;

                return query.ToList();
            }
        }

        public List<TipoArbol> consultarTipoArbol()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoArbol
                            where c.EstadoTipoArbol == "A"
                            select c;
                return query.ToList();
            }
        }
    }
}
