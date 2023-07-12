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
    public class CDMetodoPago
    {
        private int Id_MetodoPago;
        private string contado;
        private string credito;
        private string tarjeta;
        private string plazo;
        public int _Id_MetodoPago;

        public CDMetodoPago()
        {
        }

        public CDMetodoPago(int Id_metodoPago, string contado, string credito,string tarjeta, string plazo)
        {
            Id_MetodoPago = Id_metodoPago;
            this.contado = contado;
            this.credito = credito;
            this.tarjeta = tarjeta;
            this.plazo = plazo;
        }
       
        
        public int _IdMetodoPago { get => Id_MetodoPago; set => Id_MetodoPago = value; }
        public string _contado { get => contado; set => contado = value; }
        public string _credito { get => credito; set => credito = value; }
        public string _tarjeta { get => tarjeta; set => tarjeta = value; }
        public string  _plazo  { get => plazo; set => plazo = value; }


        public string MetodoPagoInsertar(CDMetodoPago objMetodoPago)
        {
            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("MarcaInsertar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pcontado", objMetodoPago._contado);
                micomando.Parameters.AddWithValue("@pcredito", objMetodoPago._credito);
                micomando.Parameters.AddWithValue("@ptarjeta", objMetodoPago._tarjeta);
                micomando.Parameters.AddWithValue("@pPlazo", objMetodoPago._plazo);

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


        public string ActualizarMetodoPago(CDMetodoPago objMetodoPago)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("MetodoPagoActualizar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pid_MetodoPago", objMetodoPago._IdMetodoPago);
                micomando.Parameters.AddWithValue("@pcontado", objMetodoPago._contado);
                micomando.Parameters.AddWithValue("@pcredito", objMetodoPago._credito);
                micomando.Parameters.AddWithValue("@ptarjeta", objMetodoPago._tarjeta);
                micomando.Parameters.AddWithValue("@pPlazo", objMetodoPago._plazo);

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


        public DataTable DataTableMetodoPago(string miparametro)
        {
            DataTable dt = new DataTable(); // Creacion de la tabla que muestra el cargo
            SqlDataReader leerDatos; //Creacion del data Reader

            try
            {
                SqlCommand sqlCmd = new SqlCommand(); //Establece un comando
                sqlCmd.Connection = new Sistema_Conexion().dbconexion;//Conexion que usara el comando
                sqlCmd.Connection.Open();// Abrir la base de datos
                sqlCmd.CommandText = "MetodoPagoConsultar"; //Nombre de proc. Almacenado
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

        public string InsertarMetodoPago(CDMetodoPago objMetodoPago)
        {
            throw new NotImplementedException();
        }
    }
}
