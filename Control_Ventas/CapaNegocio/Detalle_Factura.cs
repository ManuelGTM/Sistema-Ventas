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
    public class CNDetalle_Factura
    {
        public static string InsertarDetalle_Factura(int Id_Detalle_Factura, int Numero_Factura, int Id_Producto, int Cantidad_Vendida, int Precio_Venta)

        {
            CDDetalle_Factura objDetalle_Factura = new CDDetalle_Factura();

            objDetalle_Factura._Numero_Factura = Numero_Factura;
            objDetalle_Factura._Id_Producto = Id_Producto;
            objDetalle_Factura._Cantidad_Vendida = Cantidad_Vendida;
            objDetalle_Factura._Precio_Venta = Precio_Venta;


            return objDetalle_Factura.InsertarDetalle_Factura(objDetalle_Factura);
        }

        public static string ActualizarDetalle_Factura(int Id_Detalle_Factura, int Numero_Factura, int Id_Producto, int Cantidad_Vendida, int Precio_Venta)

        {
            CDDetalle_Factura objDetalle_Factura = new CDDetalle_Factura();

            objDetalle_Factura._Id_Detalle_Factura = Id_Detalle_Factura;
            objDetalle_Factura._Numero_Factura = Numero_Factura;
            objDetalle_Factura._Id_Producto = Id_Producto;
            objDetalle_Factura._Cantidad_Vendida = Cantidad_Vendida;
            objDetalle_Factura._Precio_Venta = Precio_Venta;


            return objDetalle_Factura.ActualizarDetalle_Factura(objDetalle_Factura);
        }

        public DataTable ObtenerDetalle_Factura(string miparametro)
        {
            CDDetalle_Factura objDetalle_Factura = new CDDetalle_Factura();
            DataTable dt = new DataTable();

            dt = objDetalle_Factura.DataTableDetalle_Factura(miparametro);

            return dt;
        }





    }
}