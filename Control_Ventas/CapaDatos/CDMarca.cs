using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Security.Policy;

namespace CapaDatos
{
    public class CDMarca
    {

        private int id_Marca;
        private string nombreMarca;
        private string descripcion;

        public CDMarca()
        {

        }

        public CDMarca(int IdMarca, string nombreMarca, string descripcion)
        {
            id_Marca = IdMarca;
            this.nombreMarca = nombreMarca;
            this.descripcion = descripcion;
        }

        /*
         Tipos de datos primitivos c#
          
        int = 21;
        double = 2.5;
        string = "hola"; => es lo mismo que varchar();
        char = 'C';
        bool = false;
        */

        // Getter y setter --> sirve para obtener o modificar los datos del atributo
        // de clase.

        public int _idMarca { get => id_Marca; set => id_Marca = value; }
        public string _nombreMarca { get => nombreMarca;set => nombreMarca = value; }
        public string _descripcion { get => descripcion; set => descripcion = value; }
        
        
        public string MarcaInsertar(CDMarca objMarca)
        {
            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("MarcaInsertar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pIdMarca", objMarca._idMarca);
                micomando.Parameters.AddWithValue("@pnombreMarca", objMarca._nombreMarca);
                micomando.Parameters.AddWithValue("@pDescripcion", objMarca._descripcion);

                mensaje = micomando.ExecuteNonQuery() == 1 ? "Insercion de datos completada correctamente"
                                                              : "No se pudo insertar correctamente los nuevos datos";
            }
            catch (Exception e)
            {
                mensaje = e.Message;
            }
            finally 
            { 
                
                if(sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }

            }

            return $"{mensaje}";
        }


        public string ActualizarCargo(CDMarca objMarca)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("MarcaActualizar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pIdCargo", objMarca._idMarca);
                micomando.Parameters.AddWithValue("@pNombreCargo", objMarca._nombreMarca);
                micomando.Parameters.AddWithValue("@pEstado", objMarca._descripcion);

                mensaje = micomando.ExecuteNonQuery() == 1 ? "Datos actualizados correctamente"
                                                             : "No se pudo actualizar correctamente los nuevos datos";

            }
            catch (Exception e)
            {
                mensaje = e.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }


            return $"{mensaje}";
        }


        public DataTable DataTableMarca(string miparametro)
        {
            DataTable dt = new DataTable(); // Creacion de la tabla que muestra el cargo
            SqlDataReader leerDatos; //Creacion del data Reader

            try
            {
                SqlCommand sqlCmd = new SqlCommand(); //Establece un comando
                sqlCmd.Connection = new Sistema_Conexion().dbconexion;//Conexion que usara el comando
                sqlCmd.Connection.Open();// Abrir la base de datos
                sqlCmd.CommandText = "MarcaConsultar"; //Nombre de proc. Almacenado
                sqlCmd.CommandType = CommandType.StoredProcedure; // Se trata de un Proc. Almacenado
                sqlCmd.Parameters.AddWithValue("@pvalor", miparametro); // Se pasa el valor a buscar 
                leerDatos = sqlCmd.ExecuteReader(); // Lenamos el data reader con los datos resultantes
                dt.Load(leerDatos); // Se cargan los registros devueltos al DataTable
                sqlCmd.Connection.Close(); // Se cierra la conexion
            }
            catch (Exception e)
            {
                dt = null; //si ocurre un erro se anula el DataTable
            }

            return dt;


        }
    }
}
