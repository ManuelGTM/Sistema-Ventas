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
    public class CDDetalle_Factura
    {
        //Atributos de Detalle_Factura

        private int Id_Detalle_Factura;
        private int Numero_Factura;
        private int Id_Producto;
        private int Cantidad_Vendida;
        private float Precio_Venta;


        public CDDetalle_Factura()
        {
        }

        public CDDetalle_Factura(int Id_Detalle_Factura, int Numero_Factura, int Id_Producto, int Cantidad_Vendida, float Precio_Venta)
        {

            this.Id_Detalle_Factura = Id_Detalle_Factura;
            this.Numero_Factura = Numero_Factura;
            this.Id_Producto = Id_Producto;
            this.Cantidad_Vendida = Cantidad_Vendida;
            this.Precio_Venta = Precio_Venta;

        }

        // getters y setters
        public int _Id_Detalle_Factura { get => Id_Detalle_Factura; set => Id_Detalle_Factura = value; }
        public int _Numero_Factura { get => Numero_Factura; set => Numero_Factura = value; }
        public int _Id_Producto { get => Id_Producto; set => Id_Producto = value; }
        public int _Cantidad_Vendida { get => Cantidad_Vendida; set => Cantidad_Vendida = value; }
        public float _Precio_Venta { get => Precio_Venta; set => Precio_Venta = value; }




        public string InsertarDetalle_Factura(CDDetalle_Factura objDetalle_Factura)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("Detalle_FacturaInsertar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pNumero_Factura", objDetalle_Factura._Numero_Factura);
                micomando.Parameters.AddWithValue("@pId_Producto", objDetalle_Factura._Id_Producto);
                micomando.Parameters.AddWithValue("@pCantidad_Vendida", objDetalle_Factura._Cantidad_Vendida);
                micomando.Parameters.AddWithValue("@pPrecio_Venta", objDetalle_Factura._Precio_Venta);


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

        public string ActualizarDetalle_Factura(CDDetalle_Factura objDetalle_Factura)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("Detalle_FacturaActualizar", sqlCon);
                sqlCon.Open();

                micomando.Parameters.AddWithValue("@pId_Detalle_Factura", objDetalle_Factura._Id_Detalle_Factura);
                micomando.Parameters.AddWithValue("@pNumero_Factura", objDetalle_Factura._Numero_Factura);
                micomando.Parameters.AddWithValue("@pId_Producto", objDetalle_Factura._Id_Producto);
                micomando.Parameters.AddWithValue("@pCantidad_Vendida", objDetalle_Factura._Cantidad_Vendida);
                micomando.Parameters.AddWithValue("@pPrecio_Venta", objDetalle_Factura._Precio_Venta);


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


        public DataTable DataTableDetalle_Factura(string miparametro)
        {
            DataTable dt = new DataTable(); // Creacion de la tabla que muestra el carg0
            SqlDataReader leerDatos; //Creacion del data Reader

            try
            {
                SqlCommand sqlCmd = new SqlCommand(); //Establece un comando
                sqlCmd.Connection = new Sistema_Conexion().dbconexion;//Conexion que usara el comando
                sqlCmd.Connection.Open();// Abrir la base de datos
                sqlCmd.CommandText = "Detalle_FacturaConsultar"; //Nombr3 de proc. Almacenado
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
