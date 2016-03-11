using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MInicio
    {
        #region Singleton

        private static MInicio instance;

        public static MInicio GetInstance()
        {
            if (instance == null)
            {
                instance = new MInicio();
            }

            return instance;
        }

        #endregion

        public List<Lote> ConsultarLote()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Lote
                            where c.EstadoLote == "A"
                            select c;

                return query.ToList();
            }
        }

        public List<Persona> ConsultarEmpleados()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Persona
                            where c.EstadoPersona == "A"
                            select c;

                return query.ToList();
            }
        }

        public int ConsultarCantidadArboles()
        {
            int cantidad = 0;

            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.Arboles
                            select new { c.Cantidad };

                if (query.Count() == 0)
                {
                    cantidad = 0;
                }
                else
                {
                    cantidad = query.Sum(total => total.Cantidad);
                }

                return cantidad;
            }
        }

        public List<Producto> ConsultarProductos()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Producto
                            where c.EstadoProducto == "A"
                            select c;

                return query.ToList();
            }
        }
    }
}
