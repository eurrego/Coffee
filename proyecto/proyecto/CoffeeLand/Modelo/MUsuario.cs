﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace Modelo
{
    public class MUsuario
    {
        #region Singleton

        private static MUsuario instance;

        public static MUsuario GetInstance()
        {
            if (instance == null)
            {
                instance = new MUsuario();
            }

            return instance;
        }

        #endregion


        public string rol { get; set; }

        public List<Usuario> ConsultarUsuario()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Usuario
                            where c.EstadoUsuario == "A"
                            select c;
                return query.ToList();
            }

            
        }

        public List<Usuario> ConsultarUsuarioParametro(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Usuario
                            where c.EstadoUsuario == "A" && c.Nickname == parametro
                            select c;
                return query.ToList();
            }


        }

        public List<Usuario> ConsultarInactivos()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Usuario
                            where c.EstadoUsuario == "I"
                            select c;
                return query.ToList();
            }
        }


        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Usuario> ConsultarParametro(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Usuario
                            where c.EstadoUsuario == "A" && c.Nickname.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Usuario> ConsultarParametroInhabilitado(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Usuario
                            where c.EstadoUsuario == "I" && c.Nickname.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }



        public string GestionUsuario(int idUsuario, string nickName, string rol, string contrasena, string preguntaSeguridad, string respuesta, int opc)
        {
            using (var entity = new DBFincaEntities())
            {
                string mensaje = entity.GestionUsuario(idUsuario, nickName, rol, contrasena, preguntaSeguridad, respuesta, opc).First().Mensaje;
                return mensaje;
            }

        }

        public IEnumerable<Usuario> InciarSesion(string nickName)
        {
            using (var entity = new DBFincaEntities())
            {
                var contra = from c in entity.Usuario
                             where c.Nickname == nickName && c.EstadoUsuario == "A"
                             select c;
                return contra.ToList();
            }
        }

        #region Inicio de sesión, para conexión dimanico
        //public string bb { get; set; }

        //private string connectionString =
        //  "Data Source=DESKTOP-QI4E0BK;Initial Catalog=Usuarios;persist security info=True;"
        //  + "user id=sa;password=123";

        //public void RegistrarUsuarios(string usuario, string contrasena, string rol, string db)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT * FROM Usuario";
        //        SqlCommand command = new SqlCommand(query, conn);
        //        conn.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //    }
        //}


        //public void ValidarInicioSesion(string usuario, string contrasena)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT BaseDatos FROM Usuario where Usuario = @Usuario and Contrasena = @Contrasena";
        //        SqlCommand command = new SqlCommand(query, conn);
        //        command.Parameters.AddWithValue("@Usuario", usuario);
        //        command.Parameters.AddWithValue("@Contrasena", contrasena);

        //        conn.Open();

        //        SqlDataReader reader = command.ExecuteReader();
        //        string datos = "";
        //        while (reader.Read())
        //        {
        //                datos = reader[0].ToString();
        //        }
        //        bb = datos;
        //    }
        #endregion

    }
}