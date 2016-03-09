using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Format;

namespace Modelo
{
    public class MInsumo
    {
        #region Singleton

        private static MInsumo instance;

        public static MInsumo GetInstance()
        {
            if (instance == null)
            {
                instance = new MInsumo();
            }

            return instance;
        }

        #endregion

        public List<Insumo> ConsultarInsumo()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Insumo
                            where c.EstadoInsumo == "A"
                            select c;

                return query.ToList();
            }
        }

        public List<Insumo> ConsultarParametroInsumo(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Insumo
                            where c.EstadoInsumo == "A" && c.NombreInsumo.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        public List<Insumo> ConsultarInactivos()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Insumo
                            where c.EstadoInsumo == "I"
                            select c;
                return query.ToList();
            }
        }

        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Insumo> ConsultarParametroInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Insumo
                            where c.EstadoInsumo == "I" && c.NombreInsumo.Contains(parametro)
                            select c;

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
                        NombreTipoInsumo = "Seleccione un Tipo de Insumo...",
                        Descripcion = "",
                        EstadoTipoInsumo= ""
                    }
                };

                var query = lista.Union(from c in entity.TipoInsumo
                                        where c.EstadoTipoInsumo == "A"
                                        select c);
                return query.ToList();
            }
        }

        public string GestionInsumo(byte IdTipoInsumo, string nombre, string descripcion, string marca, string unidaMedida, int id, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.gestionInsumo(IdTipoInsumo, Converter.GetInstance().StringToCapitalsConverter(nombre), Converter.GetInstance().StringFirtsLetterToUpper(descripcion), Converter.GetInstance().StringToCapitalsConverter(marca), Converter.GetInstance().StringToCapitalsConverter(unidaMedida), id, opcion).First();
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
