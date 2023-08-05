using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CapaDatos;


namespace CapaNegocio
{
    internal class CNPedido
    {

        public static string InsertarPedido(int Id_Pedido, DateTime Fecha_Pedido, int Id_Cliente, int Id_Empleado, string Observacion, string Estado)
        {
            CDPedido objPedido = new CDPedido();
            
            objPedido._Fecha_Pedido = Fecha_Pedido;
            objPedido._Id_Cliente = Id_Cliente;
            objPedido._Id_Empleado = Id_Empleado;
            objPedido._Observacion = Observacion;
            objPedido._Estado = Estado;

            return objPedido.InsertarPedido(objPedido);
        }

        public static string ActualizarPedido(int Id_Pedido, DateTime Fecha_Pedido, int Id_Cliente, int Id_Empleado, string Observacion, string Estado)
        {
            CDPedido objPedido = new CDPedido();
            
            objPedido._Id_Pedido = Id_Pedido;
            objPedido._Fecha_Pedido = Fecha_Pedido;
            objPedido._Id_Cliente = Id_Cliente;
            objPedido._Id_Empleado = Id_Empleado;
            objPedido._Observacion = Observacion;
            objPedido._Estado = Estado;

            return objPedido.ActualizarPedido(objPedido);
        }


        public DataTable ObtenerPedido(string miparametro)
        {
            CDPedido objPedido  = new CDPedido ();
            DataTable dt = new DataTable();

            dt = objPedido.DataTablePedido(miparametro);

            return dt;
        }











    }
}
