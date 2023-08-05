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
    public class CDPagos
    {
       
        private int IdCliente;
        private int numeroFactura;
        private DateTime fechaPago;
        private double total;
        private int metodoPago;

        public CDPagos()
        {
        }

        public CDPagos(int id_Cliente, int numeroFactura, DateTime fechaPago, double total, int metodoPago)
        {

            IdCliente = id_Cliente;
            this.numeroFactura = numeroFactura;
            this.fechaPago = fechaPago;
            this.total = total;
            this.metodoPago = metodoPago;
            
        }
      
        
        public int _IdCliente { get => IdCliente; set => IdCliente = value; } 
        public int _numeroFactura { get => numeroFactura; set => numeroFactura = value; } 
        public DateTime _fechapago { get => fechaPago; set => fechaPago = value; } 
        public double _total { get => total; set => total = value; } 
        public int _metodoPago { get => metodoPago; set => metodoPago = value; } 


        
        public string PagosInsertar(CDPagos objPagos)
        {
            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("PagosInsertar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pnumeroFactura", objPagos._numeroFactura);
                micomando.Parameters.AddWithValue("@pfechaPago", objPagos._fechapago);
                micomando.Parameters.AddWithValue("@ptotal", objPagos._total);
                micomando.Parameters.AddWithValue("@pmetodoPago", objPagos._metodoPago);

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


        public string ActualizarPagos(CDPagos objPagos)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("PagosActualizar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pid_Cliente", objPagos._IdCliente);
                micomando.Parameters.AddWithValue("@pnumeroFactura", objPagos._numeroFactura);
                micomando.Parameters.AddWithValue("@pfechaPago", objPagos._fechapago);
                micomando.Parameters.AddWithValue("@ptotal", objPagos._total);
                micomando.Parameters.AddWithValue("@pmetodoPago", objPagos._metodoPago);


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


        public DataTable DataTablePagos(string miparametro)
        {
            DataTable dt = new DataTable(); // Creacion de la tabla que muestra el cargo
            SqlDataReader leerDatos; //Creacion del data Reader

            try
            {
                SqlCommand sqlCmd = new SqlCommand(); //Establece un comando
                sqlCmd.Connection = new Sistema_Conexion().dbconexion;//Conexion que usara el comando
                sqlCmd.Connection.Open();// Abrir la base de datos
                sqlCmd.CommandText = "PagosConsultar"; //Nombre de proc. Almacenado
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
