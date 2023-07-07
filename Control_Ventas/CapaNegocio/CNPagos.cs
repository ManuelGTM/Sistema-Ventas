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
    public class CNPagos
    {

        public static string InsertarPagos(int id_Cliente, int numeroFactura, string fechaPago, double total, int metodoPago)
        {

            CDPagos objPagos = new CDPagos();  

            objPagos._numeroFactura = numeroFactura;
            objPagos._fechapago = fechaPago;
            objPagos._total = total;
            objPagos._metodoPago = metodoPago;


            return objPagos.PagosInsertar(objPagos);

        }

        public static string ActualizarPagos(int id_Cliente, int numeroFactura, string fechaPago, double total, int metodoPago)
        {

            CDPagos objPagos = new CDPagos();  

            objPagos._IdCliente = id_Cliente;
            objPagos._numeroFactura = numeroFactura;
            objPagos._fechapago = fechaPago;
            objPagos._total = total;
            objPagos._metodoPago = metodoPago;


            return objPagos.ActualizarPagos(objPagos);

        }

      public DataTable ObtenerPagos(string miparametro)
        {
            CDPagos objPagos = new CDPagos();
            DataTable dt = new DataTable();

            dt = objPagos.DataTablePagos(miparametro);

            return dt;
        }








    }
}
