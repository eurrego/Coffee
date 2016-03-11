using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Format;


namespace Modelo
{
    public class MPrestamosEmpleados
    {

        #region Singleton

        private static MPrestamosEmpleados instance;

        public static MPrestamosEmpleados GetInstance()
        {
            if (instance == null)
            {
                instance = new MPrestamosEmpleados();
            }

            return instance;
        }

        #endregion

        public List<Persona> ConsultarEmpleado()
        {
            using (var entity = new DBFincaEntities())
            {
                List<Persona> lista = new List<Persona>()
                {
                    new Persona
                    {
                        DocumentoPersona = 0.ToString(),
                        NombrePersona = "Seleccione un Empleado..."
                    }
                };

                var query = lista.Union(from c in entity.Persona
                                        where c.EstadoPersona == "A"
                                        select c);

                return query.ToList();
            }
        }

        public List<Persona> ConsultarParametroPersona(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Persona
                            where c.EstadoPersona == "A" && c.DocumentoPersona.Equals(parametro)
                            select c;

                return query.ToList();
            }
        }

        public List<DeudaPersona> ConsultarDetalleDeudasEmpleado(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.DeudaPersona
                             where c.DocumentoPersona.Equals(parametro)
                             orderby c.Fecha
                             select c;

                return query.ToList();
            }
        }

        public List<AbonoDeuda> ConsultarDetalleAbonosEmpleado(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.AbonoDeuda
                             join t in entity.DeudaPersona on c.idDeudaPersona equals t.idDeudaPersona
                             orderby c.Fecha
                             select c;

                return query.ToList();
            }
        }


        public List<DeudaPersona> ConsultarDeudaEmpleado(string parametro)
        {

            var list = new List<DeudaPersona>();
            decimal valor = 0;


            using (var entity = new DBFincaEntities())
            {

                var deudas = from c in entity.DeudaPersona
                             where c.EstadoCuenta == "D" && c.DocumentoPersona.Equals(parametro)
                             select c;

                var abonos = from c in entity.AbonoDeuda
                             join t in entity.DeudaPersona on c.idDeudaPersona equals t.idDeudaPersona
                             where t.EstadoCuenta == "D"
                             select c;

                foreach (var itemDeudas in deudas)
                {
                    valor = itemDeudas.Valor;

                    foreach (var itemAbonos in abonos)
                    {
                        if (itemDeudas.idDeudaPersona == itemAbonos.idDeudaPersona)
                        {
                            valor -= itemAbonos.Valor;
                        }
                    }

                    list.Add(new DeudaPersona
                    {
                        idDeudaPersona = itemDeudas.idDeudaPersona,
                        DocumentoPersona = itemDeudas.DocumentoPersona,
                        Valor = valor,
                        Fecha = itemDeudas.Fecha,
                        Descripcion = itemDeudas.Descripcion
                    });
                }

                return list.ToList();
            }
        }

        public string insercionDeudaEmpleado(string documento, decimal valor, DateTime fecha, string descripcion)
        {

            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.insercionDeudaEmpleado(documento, valor, fecha,Converter.GetInstance().StringFirtsLetterToUpper(descripcion)).First();
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

        public string insercionAbonoDeuda(decimal valor, DateTime fecha, int idDeuda, int opcion)
        {

            using (var entity = new DBFincaEntities())
            {
                try
                {
                    var rpta = entity.insercionAbonoDeuda(valor, fecha, idDeuda, opcion).First();
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

