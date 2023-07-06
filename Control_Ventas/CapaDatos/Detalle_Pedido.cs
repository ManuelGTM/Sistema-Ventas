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
    public class CDDetalle_Pedido
    {
        //Atributos de Detalle_Pedido

        private int Detalle_Pedido;
        private int Id_Pedido;
        private int Id_Producto;
        private int Cantidad_Producto;
        private string Estado;


        public CDDetalle_Pedido()
        {
        }

        public CDDetalle_Pedido(int Detalle_Pedido, int Id_Pedido, int Id_Producto, int Cantidad_Producto, string Estado)
        {

            this.Detalle_Pedido = Detalle_Pedido;
            this.Id_Pedido = Id_Pedido;
            this.Id_Producto = Id_Producto;
            this.Cantidad_Producto = Cantidad_Producto;
            this.Estado = Estado;

        }

        // getters y setters
        public int _Detalle_Pedido { get => Detalle_Pedido; set => Detalle_Pedido = value; }
        public int _Id_Pedido { get => Id_Pedido; set => Id_Pedido = value; }
        public int _Id_Producto { get => Id_Producto; set => Id_Producto = value; }
        public int _Cantidad_Producto { get => Cantidad_Producto; set => Cantidad_Producto = value; }
        public string _Estado { get => Estado; set => Estado = value; }




        public string InsertarDetalle_Pedido(CDDetalle_Pedido objDetalle_Pedido)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("Detalle_PedidoInsertar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pId_Pedido", objDetalle_Pedido._Id_Pedido);
                micomando.Parameters.AddWithValue("@pId_Producto", objDetalle_Pedido._Id_Producto);
                micomando.Parameters.AddWithValue("@pCantidad_Producto", objDetalle_Pedido._Cantidad_Producto);
                micomando.Parameters.AddWithValue("@pEstado", objDetalle_Pedido._Estado);


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

        public string ActualizarDetalle_Pedido(CDDetalle_Pedido objDetalle_Pedido)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("Detalle_PedidoActualizar", sqlCon);
                sqlCon.Open();

                micomando.Parameters.AddWithValue("@pDetalle_Pedido", objDetalle_Pedido._Detalle_Pedido);
                micomando.Parameters.AddWithValue("@pId_Pedido", objDetalle_Pedido._Id_Pedido);
                micomando.Parameters.AddWithValue("@pId_Producto", objDetalle_Pedido._Id_Producto);
                micomando.Parameters.AddWithValue("@pCantidad_Producto", objDetalle_Pedido._Cantidad_Producto);
                micomando.Parameters.AddWithValue("@pEstado", objDetalle_Pedido._Estado);


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


        public DataTable DataTableDetalle_Pedido(string miparametro)
        {
            DataTable dt = new DataTable(); // Creacion de la tabla que muestra el carg0
            SqlDataReader leerDatos; //Creacion del data Reader

            try
            {
                SqlCommand sqlCmd = new SqlCommand(); //Establece un comando
                sqlCmd.Connection = new Sistema_Conexion().dbconexion;//Conexion que usara el comando
                sqlCmd.Connection.Open();// Abrir la base de datos
                sqlCmd.CommandText = "Detalle_PedidoConsultar"; //Nombr3 de proc. Almacenado
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