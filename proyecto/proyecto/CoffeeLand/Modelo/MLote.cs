using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Format;


namespace Modelo
{
    public class MLote
    {
        #region Singleton

        private static MLote instance;

        public static MLote GetInstance()
        {
            if (instance == null)
            {
                instance = new MLote();
            }

            return instance;
        }

        #endregion

        public List<Lote> ConsultarLotes()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Lote
                            where c.EstadoLote == "A"
                            select c;

                return query.ToList();
            }
        }

        public List<Lote> ConsultarInactivos()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Lote
                            where c.EstadoLote == "I"
                            select c;
                return query.ToList();
            }
        }

        public List<Lote> tamañoLotes(int idLote)
        {

            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.Lote
                            where c.idLote != idLote
                            select c;

                return query.ToList();
            }
        }

        public List<Lote> tamañoLotes()
        {

            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.Lote
                            where c.EstadoLote == "A"
                            select c;

                return query.ToList();
            }
        }

        public List<Lote> tamañoLotesCompletos()
        {

            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.Lote                            
                            select c;

                return query.ToList();
            }
        }

        public List<Arboles> ConsultarArboles(int idLote)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Arboles
                            where c.idLote == idLote 
                            select c;
                return query.ToList();
            }
        }

        public List<Lote> ConsultarParametroLote(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Lote
                            where c.EstadoLote == "A" && c.NombreLote.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Lote> ConsultarParametroInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Lote
                            where c.EstadoLote == "I" && c.NombreLote.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        public string GestionLote(string NombreLote, string observaciones, string cuadras, int idLote, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.gestionLotes(Converter.GetInstance().StringToCapitalsConverter(NombreLote), Converter.GetInstance().StringFirtsLetterToUpper(observaciones), cuadras, idLote, opcion).First();
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
