using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MProducto
    {
        #region Singleton

        private static MProducto instance;

        public static MProducto GetInstance()
        {
            if (instance == null)
            {
                instance = new MProducto();
            }

            return instance;
        }

        #endregion

        public List<Producto> ConsultarProducto()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Producto
                            where c.EstadoProducto == "A"
                            select c;
                return query.ToList();
            }

        }

        public List<Producto> ConsultarInactivos()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Producto
                            where c.EstadoProducto == "I"
                            select c;
                return query.ToList();
            }
        }

        public List<Producto> ConsultarParametroProducto(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Producto
                            where c.EstadoProducto == "A" && c.NombreProducto.Contains(parametro)
                            select c;

                return query.ToList();
            }

        }


        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Producto> ConsultarParametroInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Producto
                            where c.EstadoProducto == "I" && c.NombreProducto.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        public string GestionProducto(string NombreProducto, string Descripcion, byte idProducto, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.gestionProducto(NombreProducto, Descripcion, idProducto, opcion).First();
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
    }
}
