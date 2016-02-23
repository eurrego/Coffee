using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MPersona
    {
        #region Singleton

        private static MPersona instance;

        public static MPersona GetInstance()
        {
            if (instance == null)
            {
                instance = new MPersona();
            }

            return instance;
        }

        #endregion

        public List<Persona> ConsultarPersona()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Persona
                            where c.EstadoPersona == "A"
                            select c;

                return query.ToList();
            }

        }

        public List<Persona> ConsultarInactivos()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Persona
                            where c.EstadoPersona == "I"
                            select c;
                return query.ToList();
            }
        }

        public List<Persona> ConsultarParametroPersona(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Persona
                            where c.EstadoPersona == "A" && c.NombrePersona.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }


        //este metodo consulta por medio de la cadena de texto ingresada
        public List<Persona> ConsultarParametroInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Persona
                            where c.EstadoPersona == "I" && c.NombrePersona.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        public string GestionPersona(string nombre, string genero, string telefono, DateTime fechaNacimiento, int id, int opcion, string TipoDocumento, string TipoContrato)
        {

            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.gestionPersona(nombre.ToUpper(), genero.ToUpper(), telefono, fechaNacimiento, id, opcion, TipoDocumento, TipoContrato).First();
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
