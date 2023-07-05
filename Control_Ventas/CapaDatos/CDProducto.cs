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
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;

namespace CapaDatos
{
    public class CDProducto
    {

        private int IdProducto;
        private string nombreProducto;
        private int existencia;
        private double precio;
        private int marca;
        private string Estado;
     
        public CDProducto()
        {
        }

        public CDProducto(int IdProducto, string nombreProducto, int existencia, double precio, int Marca, string Estado)
        {
            this.IdProducto = IdProducto;
            this.nombreProducto = nombreProducto;
            this.existencia = existencia;
            this.precio = precio;
            this.marca = Marca;
            this.Estado = Estado;

        }


        public int _IdProducto { get => IdProducto; set => IdProducto = value; }
        public string _nombreProducto { get => nombreProducto; set => nombreProducto = value; }
        public int _existencia { get => existencia; set => existencia = value; }
        public double _precio { get => precio; set => precio = value; }
        public int _marca { get => marca; set => marca = value; }
        public string _Estado { get => Estado; set => Estado = value; }


        public string ProductoInsertar(CDProducto objProducto)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();


            try
            {
                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("MarcaInsertar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pidProducto", objProducto._IdProducto);
                micomando.Parameters.AddWithValue("@pnombreProducto", objProducto._nombreProducto);
                micomando.Parameters.AddWithValue("@pexistencia", objProducto._existencia);
                micomando.Parameters.AddWithValue("@pPrecio", objProducto._precio);
                micomando.Parameters.AddWithValue("@pmarca", objProducto._marca);
                micomando.Parameters.AddWithValue("@pEstado", objProducto._Estado);

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


        public string ProductoActualizar(CDProducto objProducto)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();


            try
            {
                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("MarcaActualizar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pidProducto", objProducto._IdProducto);
                micomando.Parameters.AddWithValue("@pnombreProducto", objProducto._nombreProducto);
                micomando.Parameters.AddWithValue("@pexistencia", objProducto._existencia);
                micomando.Parameters.AddWithValue("@pPrecio", objProducto._precio);
                micomando.Parameters.AddWithValue("@pmarca", objProducto._marca);
                micomando.Parameters.AddWithValue("@pEstado", objProducto._Estado);

                mensaje = micomando.ExecuteNonQuery() == 1 ? "Los datos han sido actualizados correctamente"
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



        public DataTable DataTableProducto(string miparametro)
        {
            DataTable dt = new DataTable(); // Creacion de la tabla que muestra el cargo
            SqlDataReader leerDatos; //Creacion del data Reader

            try
            {
                SqlCommand sqlCmd = new SqlCommand(); //Establece un comando
                sqlCmd.Connection = new Sistema_Conexion().dbconexion;//Conexion que usara el comando
                sqlCmd.Connection.Open();// Abrir la base de datos
                sqlCmd.CommandText = "ProductoConsultar"; //Nombre de proc. Almacenado
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
