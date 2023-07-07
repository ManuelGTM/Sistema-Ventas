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
    public class CNMarca
    {
        public static string InsertarMarca(int IdMarca, string nombreMarca, string descripcion)
        {
            CDMarca objMarca = new CDMarca();
            objMarca._nombreMarca = nombreMarca;
            objMarca._descripcion = descripcion;

            return objMarca.MarcaInsertar(objMarca);
        }


        public static string ActualizarMarca(int IdMarca, string nombreMarca, string descripcion)
        {
            CDMarca objMarca = new CDMarca();
            objMarca._idMarca = IdMarca;
            objMarca._nombreMarca = nombreMarca;
            objMarca._descripcion = descripcion;

            return objMarca.ActualizarMarca(objMarca);
        }
        public DataTable ObtenerMarca(string miparametro)
        {
            CDMarca objMarca = new CDMarca();
            DataTable dt = new DataTable();

            dt = objMarca.DataTableMarca(miparametro);

            return dt;
        }



    }
}
