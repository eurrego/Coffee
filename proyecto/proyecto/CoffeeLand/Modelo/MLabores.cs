using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Format;

namespace Modelo
{
    public class MLabores
    {
        #region Singleton

        private static MLabores instance;

        public static MLabores GetInstance()
        {
            if (instance == null)
            {
                instance = new MLabores();
            }

            return instance;
        }

        #endregion


        public List<Labor> consultarLabor()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Labor
                            where c.EstadoLabor == "A"
                            select c;

                return query.ToList();
            }

        }

        public List<Labor> ConsultarInactivos()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Labor
                            where c.EstadoLabor == "I"
                            select c;
                return query.ToList();
            }
        }


        public List<Labor> buscarLabor(string NombreLabor)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Labor
                            where c.EstadoLabor == "A" && c.NombreLabor.Contains(NombreLabor)
                            select c;

                return query.ToList();
            }
        }

        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Labor> ConsultarParametroInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Labor
                            where c.EstadoLabor == "I" && c.NombreLabor.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }


        public string GestionLabor(string NombreLabor, bool ModificaArbol, bool RequiereInsumo, string Descripcion, int idLabor, int opc)
        {

            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.gestionLabor(Converter.GetInstance().StringToCapitalsConverter(NombreLabor), ModificaArbol, RequiereInsumo, Converter.GetInstance().StringFirtsLetterToUpper(Descripcion), idLabor, opc).First();

                    return rpta.Mensaje;
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
}
