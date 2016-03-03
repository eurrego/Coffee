using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Format;

namespace Modelo
{
    public class MConcepto
    {
        #region Singleton

        private static MConcepto instance;

        public static MConcepto GetInstance()
        {
            if (instance == null)
            {
                instance = new MConcepto();
            }

            return instance;
        }

        #endregion

        public List<Concepto> ConsultarConcepto()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Concepto
                            where c.EstadoConcepto == "A"
                            select c;
                return query.ToList();
            }
        }

        public List<Concepto> ConsultarInactivos()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Concepto
                            where c.EstadoConcepto == "I"
                            select c;
                return query.ToList();
            }
        }


        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Concepto> ConsultarParametroConcepto(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Concepto
                            where c.EstadoConcepto == "A" && c.NombreConcepto.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Concepto> ConsultarParametroConceptoInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Concepto
                            where c.EstadoConcepto == "I" && c.NombreConcepto.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        public string GestionConcepto(string NombreConcepto, string Descripcion, byte idConcepto, int opcion)
        {

            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.gestionConcepto(Converter.GetInstance().StringToCapitalsConverter(NombreConcepto), Converter.GetInstance().StringFirtsLetterToUpper(Descripcion), idConcepto, opcion).First();
                    return rpta.Mensaje;
                }
                catch (Exception ex)
                {
                    string filePath = @"C:\Users\Brian\Desktop\LogCoffeeLand.txt";

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
