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
    public class CNEmpleado
    {
        public static string InsertarEmpleados(int Empleado, string Nombre, string Apellido, string Telefono, string Direccion,  string email, int Cargo, string Estado)
        {
            CDEmpleado objEmpleado = new CDEmpleado();
            
            objEmpleado._Nombre = Nombre;
            objEmpleado._Apellido = Apellido;
            objEmpleado._Telefono = Telefono;
            objEmpleado._Direccion = Direccion;
            objEmpleado._Email = email;
            objEmpleado._Cargo = Cargo;
            objEmpleado._Estado = Estado;

            return objEmpleado.InsertarEmpleado(objEmpleado);
        }

        public static string ActualizarEmpleados(int Empleado, string Nombre, string Apellido, string Telefono, string Direccion,  string email, int Cargo, string Estado)
        {
            CDEmpleado objEmpleado = new CDEmpleado();
            
            objEmpleado._IdEmpleado = Empleado;
            objEmpleado._Nombre = Nombre;
            objEmpleado._Apellido = Apellido;
            objEmpleado._Telefono = Telefono;
            objEmpleado._Direccion = Direccion;
            objEmpleado._Email = email;
            objEmpleado._Cargo = Cargo;
            objEmpleado._Estado = Estado;

            return objEmpleado.ActualizarEmpleado(objEmpleado);
        }

        public DataTable ObtenerEmpleado(string miparametro)
        {
            CDEmpleado objEmpleado = new CDEmpleado();
            DataTable dt = new DataTable();

            dt = objEmpleado.DataTableEmpleado(miparametro);

            return dt;
        }





    }
}
