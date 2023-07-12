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
    public class CDCabecera_Factura
    {
        //Atributos de Cabecera_Factura

        private int Numero_factura;
        private string Fecha_factura;
        private string Tipo_Factura;
        private int Id_Cliente;
        private int Id_Empleado;
        private int Condicion;
        private string Observacion;
        private string Estado;
        public string _Fecha_Factura;
        public int _Numero_Factura;

        public CDCabecera_Factura()
        {
        }

        public CDCabecera_Factura(int Numero_factura, string Fecha_factura, string Tipo_Factura, int Id_Cliente, int Id_Empleado, int Condicion, string Observacion, string Estado)
        {

            this.Numero_factura = Numero_factura;
            this.Fecha_factura = Fecha_factura;
            this.Tipo_Factura = Tipo_Factura;
            this.Id_Cliente = Id_Cliente;
            this.Id_Empleado = Id_Empleado;
            this.Condicion = Condicion;
            this.Observacion = Observacion;
            this.Estado = Estado;
        }

        // getters y setters
        public int _Numero_factura { get => Numero_factura; set => Numero_factura = value; }
        public string _Fecha_factura { get => Fecha_factura; set => Fecha_factura = value; }
        public string _Tipo_Factura { get => Tipo_Factura; set => Tipo_Factura = value; }
        public int _Id_Cliente { get => Id_Cliente; set => Id_Cliente = value; }
        public int _Id_Empleado { get => Id_Empleado; set => Id_Empleado = value; }
        public int _Condicion { get => Condicion; set => Condicion = value; }
        public string _Observacion { get => Observacion; set => Observacion = value; }
        public string _Estado { get => Estado; set => Estado = value; }



        public string InsertarCabecera_Factura(CDCabecera_Factura objCabecera_Factura)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("Cabecera_FacturaInsertar", sqlCon);
                sqlCon.Open();
                micomando.CommandType = CommandType.StoredProcedure;

                micomando.Parameters.AddWithValue("@pFecha_factura", objCabecera_Factura._Fecha_factura);
                micomando.Parameters.AddWithValue("@pTipo_Factura", objCabecera_Factura._Tipo_Factura);
                micomando.Parameters.AddWithValue("@pId_Cliente", objCabecera_Factura._Id_Cliente);
                micomando.Parameters.AddWithValue("@pId_Empleado", objCabecera_Factura._Id_Empleado);
                micomando.Parameters.AddWithValue("@pCondicion", objCabecera_Factura._Condicion);
                micomando.Parameters.AddWithValue("@pObservacion", objCabecera_Factura._Observacion);
                micomando.Parameters.AddWithValue("@pEstado", objCabecera_Factura._Estado);

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

        public string ActualizarCabecera_Factura(CDCabecera_Factura objCabecera_Factura)
        {

            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {

                sqlCon.ConnectionString = Sistema_Conexion.miconexion;
                SqlCommand micomando = new SqlCommand("Cabecera_FacturaActualizar", sqlCon);
                sqlCon.Open();

                micomando.Parameters.AddWithValue("@pNumero_factura", objCabecera_Factura._Numero_factura);
                micomando.Parameters.AddWithValue("@pFecha_factura", objCabecera_Factura._Fecha_factura);
                micomando.Parameters.AddWithValue("@pTipo_Factura", objCabecera_Factura._Tipo_Factura);
                micomando.Parameters.AddWithValue("@pId_Cliente", objCabecera_Factura._Id_Cliente);
                micomando.Parameters.AddWithValue("@pId_Empleado", objCabecera_Factura._Id_Empleado);
                micomando.Parameters.AddWithValue("@pCondicion", objCabecera_Factura._Condicion);
                micomando.Parameters.AddWithValue("@pObservacion", objCabecera_Factura._Observacion);
                micomando.Parameters.AddWithValue("@pEstado", objCabecera_Factura._Estado);

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


        public DataTable DataTableCabecera_Factura(string miparametro)
        {
            DataTable dt = new DataTable(); // Creacion de la tabla que muestra el carg0
            SqlDataReader leerDatos; //Creacion del data Reader

            try
            {
                SqlCommand sqlCmd = new SqlCommand(); //Establece un comando
                sqlCmd.Connection = new Sistema_Conexion().dbconexion;//Conexion que usara el comando
                sqlCmd.Connection.Open();// Abrir la base de datos
                sqlCmd.CommandText = "Cabecera_Facturaonsultar"; //Nombr3 de proc. Almacenado
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
