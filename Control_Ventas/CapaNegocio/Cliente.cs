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
    public class CNCliente
    {
        public static string InsertarCliente(int Id_Cliente, string Nombre, string Apellido, string Telefono, string Direccion, string Cuidad, string Departamento, string Pais)
        {
            CDCliente objCliente = new CDCliente();

            objCliente._Nombre = Nombre;
            objCliente._Apellido = Apellido;
            objCliente._Telefono = Telefono;
            objCliente._Direccion = Direccion;
            objCliente._Cuidad = Cuidad;
            objCliente._Departamento = Departamento;
            objCliente._Pais = Pais;

            return objCliente.InsertarCliente(objCliente);
        }

        public static string ActualizarCliente(int Id_Cliente, string Nombre, string Apellido, string Telefono, string Direccion, string Cuidad, string Departamento, string Pais)
        {
            CDCliente objCliente = new CDCliente();

            objCliente._Id_Cliente = Id_Cliente;
            objCliente._Nombre = Nombre;
            objCliente._Apellido = Apellido;
            objCliente._Telefono = Telefono;
            objCliente._Direccion = Direccion;
            objCliente._Cuidad = Cuidad;
            objCliente._Departamento = Departamento;
            objCliente._Pais = Pais;

            return objCliente.ActualizarCliente(objCliente);
        }

        public DataTable ObtenerCliente(string miparametro)
        {
            CDCliente objCliente = new CDCliente();
            DataTable dt = new DataTable();

            dt = objCliente.DataTableCliente(miparametro);

            return dt;
        }





    }
}