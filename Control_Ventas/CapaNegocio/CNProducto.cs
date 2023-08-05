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
    public class CNProducto
    {
        public static string InsertarProducto(string nombreProducto, int existencia, double precio, int Marca, string Estado)
        {

            CDProducto objProducto = new CDProducto();

            objProducto._nombreProducto = nombreProducto;
            objProducto._existencia = existencia;
            objProducto._precio = precio;
            objProducto._marca = Marca;
            objProducto._Estado = Estado;

            return objProducto.ProductoInsertar(objProducto);
        }

        public static string ActualizarProducto(int IdProducto, string nombreProducto, int existencia, double precio, int Marca, string Estado)
        {

            CDProducto objProducto = new CDProducto();

            objProducto._IdProducto = IdProducto;
            objProducto._nombreProducto = nombreProducto;
            objProducto._existencia = existencia;
            objProducto._precio = precio;
            objProducto._marca = Marca;
            objProducto._Estado = Estado;

            return objProducto.ProductoActualizar(objProducto);
        }

      public DataTable ObtenerProducto(string miparametro)
        {
            CDProducto objProducto = new CDProducto();
            DataTable dt = new DataTable();

            dt = objProducto.DataTableProducto(miparametro);

            return dt;
        }





    }
}
