using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MPagos
    {
        #region Singleton
        private static MPagos instance;

        public static MPagos GetInstance()
        {
            if (instance == null)
            {
                instance = new MPagos();
            }
            return instance;
        }

        #endregion


        public object ConsultarSalario(int opc)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = entity.PagosPersona(opc);
                return query.ToList();
            }
        }

        public object DetalleSalario(int cedula)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = entity.DetalleSalario(cedula);
                return query.ToList();
            }
        }


        public void insertarMultiplesSalarios(DataTable dt, int opc)
        {
            try
            {
                using (var entity = new DBFincaEntities())
                {
                    if (opc == 1)//permanente
                    {

                        entity.SP_InsertMultiplesSalariosPersonaPermanente(dt);
                    }
                    else if (opc == 2)//temporal
                    {
                        entity.Insercion_RegistroPago_SalarioPersonaTemporal();
                    }
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

                //return "Ha ocurrido un error inesperado, consulte con el administrador del sistema";
            }
        }
    }
}
