using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data;
using System.Reflection.Emit;

namespace CapaDatos
{
    public class CDPedido
    {
        //Atributos de Pedido

        private int Id_Pedido;
        private DateTime Fecha_Pedido;
        private int Id_Cliente;
        private int Id_Empleado;
        private string Observacion;
        private string Estado;


        public CDPedido()
        {
        }

        public CDPedido(int Id_Pedido, DateTime Fecha_Pedido, int Id_Cliente, int Id_Empleado, string Observacion, string Estado)
        {

            this.Id_Pedido = Id_Pedido;
            this.Fecha_Pedido = Fecha_Pedido;
            this.Id_Cliente = Id_Cliente;
            this.Id_Empleado = Id_Empleado;
            this.Observacion = Observacion;
            this.Estado = Estado;

        }

        // getters y setters
        public int _Id_Pedido { get => Id_Pedido; set => Id_Pedido = value; }
        public DateTime _Fecha_Pedido { get => Fecha_Pedido; set => Fecha_Pedido = value; }
        public int _Id_Cliente { get => Id_Cliente; set => Id_Cliente = value; }
        public int _Id_Empleado { get => Id_Empleado; set => Id_Empleado = value; }
        public string _Observacion { get => Observacion; set => Observacion = value; }
        public string _Estado { get => Estado; set => Estado = value; }




        public string InsertarPedido(CDPedido objPedido)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("PedidoInsertar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pFecha_Pedido", objPedido._Fecha_Pedido);
                micomando.Parameters.AddWithValue("@pId_Cliente", objPedido._Id_Cliente);
                micomando.Parameters.AddWithValue("@pId_Empleado", objPedido._Id_Empleado);
                micomando.Parameters.AddWithValue("@pObservacion", objPedido._Observacion);
                micomando.Parameters.AddWithValue("@pEstado", objPedido._Estado);


                mensaje = micomando.ExecuteNonQuery() == 1 ? "Insercion de datos completada correctamente"
                                                             : "No se pudo insertar correctamente los nuevos datos";

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

            return "";
        }

        public string ActualizarPedido(CDPedido objPedido)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("PedidoActualizar", sqlCon);
                sqlCon.Open();

                micomando.Parameters.AddWithValue("@pId_Pedido", objPedido._Id_Pedido);
                micomando.Parameters.AddWithValue("@pFecha_Pedido", objPedido._Fecha_Pedido);
                micomando.Parameters.AddWithValue("@pId_Cliente", objPedido._Id_Cliente);
                micomando.Parameters.AddWithValue("@pId_Empleado", objPedido._Id_Empleado);
                micomando.Parameters.AddWithValue("@pObservacion", objPedido._Observacion);
                micomando.Parameters.AddWithValue("@pEstado", objPedido._Estado);


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


        public DataTable DataTablePedido(string miparametro)
        {
            DataTable dt = new DataTable(); // Creacion de la tabla que muestra el carg0
            SqlDataReader leerDatos; //Creacion del data Reader

            try
            {
                SqlCommand sqlCmd = new SqlCommand(); //Establece un comando
                sqlCmd.Connection = new Sistema_Conexion().dbconexion;//Conexion que usara el comando
                sqlCmd.Connection.Open();// Abrir la base de datos
                sqlCmd.CommandText = "PedidoConsultar"; //Nombr3 de proc. Almacenado
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