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
    public class CNDetalle_Pedido
    {
        public static string InsertarDetalle_Pedido(int Detalle_Pedido, int Id_Pedido, int Id_Producto, int Cantidad_Producto, string Estado)
        {
            CDDetalle_Pedido objDetalle_Pedido = new CDDetalle_Pedido();

            objDetalle_Pedido._Id_Pedido = Id_Pedido;
            objDetalle_Pedido._Id_Producto = Id_Producto;
            objDetalle_Pedido._Cantidad_Producto = Cantidad_Producto;
            objDetalle_Pedido._Estado = Estado;

            return objDetalle_Pedido.InsertarDetalle_Pedido(objDetalle_Pedido);
        }

        public static string ActualizarDetalle_Pedido(int Detalle_Pedido, int Id_Pedido, int Id_Producto, int Cantidad_Producto, string Estado)
        {
            CDDetalle_Pedido objDetalle_Pedido = new CDDetalle_Pedido();

            objDetalle_Pedido._Detalle_Pedido = Detalle_Pedido;
            objDetalle_Pedido._Id_Pedido = Id_Pedido;
            objDetalle_Pedido._Id_Producto = Id_Producto;
            objDetalle_Pedido._Cantidad_Producto = Cantidad_Producto;
            objDetalle_Pedido._Estado = Estado;

            return objDetalle_Pedido.ActualizarDetalle_Pedido(objDetalle_Pedido);
        }

        public DataTable ObtenerDetalle_Pedido(string miparametro)
        {
            CDDetalle_Pedido objDetalle_Pedido = new CDDetalle_Pedido();
            DataTable dt = new DataTable();

            dt = objDetalle_Pedido.DataTableDetalle_Pedido(miparametro);

            return dt;
        }





    }
}