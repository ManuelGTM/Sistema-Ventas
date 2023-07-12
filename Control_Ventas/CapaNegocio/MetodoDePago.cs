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
    public class CNMetodoPago
    {
        public static string InsertarMetodoPago(int Id_MetodoPago, string Contado, string Credito, string Tarjeta, string Plazo)
        {
            CDMetodoPago objMetodoPago = new CDMetodoPago();

            objMetodoPago._contado = Contado;
            objMetodoPago._credito = Credito;
            objMetodoPago._tarjeta = Tarjeta;
            objMetodoPago._plazo = Plazo;

            return objMetodoPago.InsertarMetodoPago(objMetodoPago);
        }

        public static string ActualizarMetodoPago(int Id_MetodoPago, string Contado, string Credito, string Tarjeta, string Plazo)
        {
            CDMetodoPago objMetodoPago = new CDMetodoPago();

            objMetodoPago._Id_MetodoPago = Id_MetodoPago;
            objMetodoPago._contado = Contado;
            objMetodoPago._credito = Credito;
            objMetodoPago._tarjeta = Tarjeta;
            objMetodoPago._plazo = Plazo;

            return objMetodoPago.ActualizarMetodoPago(objMetodoPago);
        }

        public DataTable ObtenerMetodoPago(string miparametro)
        {
            CDMetodoPago objMetodoPago = new CDMetodoPago();
            DataTable dt = new DataTable();

            dt = objMetodoPago.DataTableMetodoPago(miparametro);

            return dt;
        }





    }
}