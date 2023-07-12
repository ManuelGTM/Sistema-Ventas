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
    public class CNCabecera_Factura
    {
        public static string InsertarCabecera_Factura(int Numero_Factura, string Fecha_Factura, string Tipo_Factura, int Id_Cliente, int Id_Empleado, int Condicion, string Observacion, string Estado)
        {
            CDCabecera_Factura objCabecera_Factura = new CDCabecera_Factura();

            objCabecera_Factura._Fecha_Factura = Fecha_Factura;
            objCabecera_Factura._Tipo_Factura = Tipo_Factura;
            objCabecera_Factura._Id_Cliente = Id_Cliente;
            objCabecera_Factura._Id_Empleado = Id_Empleado;
            objCabecera_Factura._Condicion = Condicion;
            objCabecera_Factura._Observacion = Observacion;
            objCabecera_Factura._Estado = Estado;

            return objCabecera_Factura.InsertarCabecera_Factura(objCabecera_Factura);
        }

        public static string ActualizarCabecera_Factura(int Numero_Factura, string Fecha_Factura, string Tipo_Factura, int Id_Cliente, int Id_Empleado, int Condicion, string Observacion, string Estado)
        {
            CDCabecera_Factura objCabecera_Factura = new CDCabecera_Factura();

            objCabecera_Factura._Numero_Factura = Numero_Factura;
            objCabecera_Factura._Fecha_Factura = Fecha_Factura;
            objCabecera_Factura._Tipo_Factura = Tipo_Factura;
            objCabecera_Factura._Id_Cliente = Id_Cliente;
            objCabecera_Factura._Id_Empleado = Id_Empleado;
            objCabecera_Factura._Condicion = Condicion;
            objCabecera_Factura._Observacion = Observacion;
            objCabecera_Factura._Estado = Estado;

            return objCabecera_Factura.ActualizarCabecera_Factura(objCabecera_Factura);
        }

        public DataTable ObtenerCabecera_Factura(string miparametro)
        {
            CDCabecera_Factura objCabecera_Factura = new CDCabecera_Factura();
            DataTable dt = new DataTable();

            dt = objCabecera_Factura.DataTableCabecera_Factura(miparametro);

            return dt;
        }





    }
}