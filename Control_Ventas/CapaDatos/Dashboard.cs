using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data;

namespace CapaDatos
{
    public struct RevenueByDate
    {
        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class Dashboard : Sistema_Conexion
    {

        private DateTime startDate;
        private DateTime endDate;
        private int numberDays;

        public int NumCustomers { get; set; }
        public int NumOrders { get; set; }
        public int numProducts { get; set; }
        public List<KeyValuePair<string, int>> TopProductsList { get; private set; }
        public List<KeyValuePair<string, int>> UnderStockList { get; private set; }
        public List<RevenueByDate> GrossRevenueList { get; private set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }

        public Dashboard()
        {

        }

        private void GetNumberItems()
        {
            // Abrir la conexion con la base de datos
            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = miconexion;
            SqlCommand micomando = new SqlCommand();
            sqlCon.Open();

            // Contar todos los clientes
            micomando.CommandText = "select count(id_cliente) from Cliente";
            NumCustomers = (int)micomando.ExecuteScalar();

            // Contar todos los productos

            micomando.CommandText = "select count(id_producto) from Producto";
            numProducts = (int)micomando.ExecuteScalar();

            //Contar todas las ordenes

            micomando.CommandText = @"select count(id_pedido) from pedido" +
                                    "where fecha_pedido between @fromDate and @toDate";
            micomando.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
            micomando.Parameters.Add("@ToDate", System.Data.SqlDbType.DateTime).Value = endDate;
            NumOrders = (int)micomando.ExecuteScalar();
        }

        private void GetProductAnalisys()
        {
            TopProductsList = new List<KeyValuePair<string, int>>();
            UnderStockList = new List<KeyValuePair<string, int>>();

            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = miconexion;
            SqlCommand micomando = new SqlCommand();
            sqlCon.Open();
            SqlDataReader reader;

            micomando.CommandText = @"select top 5 P.nombreProducto, sum(detalle_pedido.cantidad_producto) as Q
                                      from detalle_pedido
                                      inner join Producto P on P.IdProducto = detalle_pedido.id_producto 
                                      inner join 
                                      Pedido PE on PE.id_pedido = detalle_pedido.id_pedido
                                      where fecha_pedido between @fromDate and @toDate
                                      group by P.nombreProducto
                                      order by Q desc";
            micomando.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
            micomando.Parameters.Add("@ToDate", System.Data.SqlDbType.DateTime).Value = endDate;
            reader = micomando.ExecuteReader();
            while (reader.Read())
            {
                TopProductsList.Add(
                    new KeyValuePair<string, int>(reader[0].ToString(), (int)reader[1]));

            }
            reader.Close();

            //Get UnderStock

            micomando.CommandText = @"select nombreProducto, existencia from Producto where existencia <= 6";
            reader = micomando.ExecuteReader();
            while (reader.Read())
            {
                UnderStockList.Add(
                    new KeyValuePair<string, int>(reader[0].ToString(), (int)reader[1]));
            }
            reader.Close();
        }
    
    private void GetOrderAnalisys()
        {
            GrossRevenueList = new List<RevenueByDate>();
            TotalProfit = 0;
            TotalRevenue = 0;

            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = miconexion;
            SqlCommand micomando = new SqlCommand();
            sqlCon.Open();
            SqlDataReader reader;

            micomando.CommandText = @"select P.fecha_pedido, sum(F.precio_venta)
                                    from Pedido P, detalle_factura F
                                    where P.fecha_pedido between @fromDate and @toDate
                                    group by P.fecha_pedido";
            micomando.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
            micomando.Parameters.Add("@ToDate", System.Data.SqlDbType.DateTime).Value = endDate;
        
        // Me quede aqui <-------------------------
        
        }
    
    
    }
}

