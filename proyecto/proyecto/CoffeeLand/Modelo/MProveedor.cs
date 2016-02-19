using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MProveedor
    {
        #region Singleton

        private static MProveedor instance;

        public static MProveedor GetInstance()
        {
            if (instance == null)
            {
                instance = new MProveedor();
            }

            return instance;
        }

        #endregion

        public List<Proveedor> ConsultarProveedor()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Proveedor
                            where c.EstadoProveedor == "A"
                            select c;

                return query.ToList();
            }
        }

        public List<Proveedor> ConsultarInactivos()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Proveedor
                            where c.EstadoProveedor == "I"
                            select c;
                return query.ToList();
            }
        }

        public object consultarDeuda(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = entity.ComprasProveedor(parametro);
                          
                return query.ToList();
            }
        }

        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Proveedor> ConsultarParametroProveedor(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Proveedor
                            where c.EstadoProveedor == "A" && c.NombreProveedor.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        //este metodo consulta por medio de la cadena de texto ingresada
        public List<Proveedor> ConsultarParametroInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Proveedor
                            where c.EstadoProveedor == "I" && c.NombreProveedor.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }


        public string GestionProveedor(string nit, string nombreProveedor, string telefono, string direccionProveedor, string tipoDocumento, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.gestionProveedor(nit, nombreProveedor.ToUpper(), telefono, direccionProveedor.ToUpper(), tipoDocumento, opcion).First();
                    return rpta.Mensaje;
                }
                catch (Exception ex)
                {
                    string filePath = @"C:\LogCoffeeLand.txt";

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
