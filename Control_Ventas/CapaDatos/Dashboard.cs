using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data;
using System.Globalization;

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

        public int NumCustomers { get; private set; }

        public int NumOrders { get; private set; }
        public int numProducts { get; private set; }

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
            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();
           try
            {
                // Abrir la conexion con la base de datos
                sqlCon.ConnectionString = miconexion;
                var micomando = new SqlCommand();
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
            catch(Exception e)
            {
                mensaje = e.Message;
            }
            finally
            {
                if(sqlCon.State == ConnectionState.Open)
                sqlCon.Close();
            }
        }

        private void GetProductAnalisys()
        {
            TopProductsList = new List<KeyValuePair<string, int>>();
            UnderStockList = new List<KeyValuePair<string, int>>();
            SqlConnection sqlCon = new SqlConnection();
            string mensaje = "";

            try
            {
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
            catch (Exception e)
            {
                mensaje = e.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }
    
        private void GetOrderAnalisys()
        {
            GrossRevenueList = new List<RevenueByDate>();
            TotalProfit = 0;
            TotalRevenue = 0;
            string mensaje = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon.ConnectionString = miconexion;
                SqlCommand micomando = new SqlCommand();
                sqlCon.Open();

                micomando.CommandText = @"select P.fecha_pedido, sum(F.precio_venta)
                                        from Pedido P, detalle_factura F
                                        where P.fecha_pedido between @fromDate and @toDate
                                        group by P.fecha_pedido";
                micomando.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                micomando.Parameters.Add("@ToDate", System.Data.SqlDbType.DateTime).Value = endDate;
               var reader = micomando.ExecuteReader();
               var resultTable = new List<KeyValuePair<DateTime, decimal>>();

               while (reader.Read())
                {
                    resultTable.Add(
                        new KeyValuePair<DateTime, decimal>((DateTime)reader[0], (decimal)reader[1])
                        );
                    TotalRevenue += (decimal)reader[1];
                }
                TotalProfit += TotalRevenue * 0.2m;
                reader.Close();

                //Agrupar por horas
                if(numberDays <= 1)
                {
                    GrossRevenueList = (from orderList in resultTable
                                        group orderList by orderList.Key.ToString("hh tt")
                                        into Pedido
                                        select new RevenueByDate
                                        {
                                            Date = Pedido.Key,
                                            TotalAmount = Pedido.Sum(precio_venta => precio_venta.Value)
                                        }).ToList();
                }

                else if(numberDays <= 30)
                {

                    GrossRevenueList = (from orderList in resultTable
                                        group orderList by orderList.Key.ToString("dd MMM")
                                        into Pedido
                                        select new RevenueByDate
                                        {
                                            Date = Pedido.Key,
                                            TotalAmount = Pedido.Sum(precio_venta => precio_venta.Value)
                                        }).ToList();

                }
                else if(numberDays <= 92)
                {

                    GrossRevenueList = (from orderList in resultTable
                                        group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                                    orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                        into Pedido
                                        select new RevenueByDate
                                        {
                                            Date = "Week" + Pedido.Key.ToString(),
                                            TotalAmount = Pedido.Sum(precio_venta => precio_venta.Value)
                                        }).ToList();

                }
                else if(numberDays <= 365 * 2)
                {
                    bool isYear = numberDays <= 365 ? true : false;

                    GrossRevenueList = (from orderList in resultTable
                                        group orderList by orderList.Key.ToString("MMM yyyy")
                                        into Pedido
                                        select new RevenueByDate
                                        {
                                            Date = isYear ? Pedido.Key.Substring(0, Pedido.Key.IndexOf(" ")) : Pedido.Key,
                                            TotalAmount = Pedido.Sum(precio_venta => precio_venta.Value)
                                        }).ToList();

            }
            else
            {

                GrossRevenueList = (from orderList in resultTable
                                    group orderList by orderList.Key.ToString("yyyy")
                                    into Pedido
                                    select new RevenueByDate
                                    {
                                        Date = Pedido.Key,
                                        TotalAmount = Pedido.Sum(precio_venta => precio_venta.Value)
                                    }).ToList();
            }

            }catch (Exception e)
            {
                mensaje = e.Message;
            }
            finally
            {
                if(sqlCon.State == ConnectionState.Open) 
                { 
                    sqlCon.Close(); 
                }
            }
           
        }
        
        public bool LoadData(DateTime startDate, DateTime endDate)
        {
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 59);
            if(startDate != this.startDate || endDate != this.endDate)
            {
                this.startDate = startDate;
                this.endDate = endDate;
                numberDays = (endDate - startDate).Days;

                GetNumberItems();
                GetProductAnalisys();
                GetOrderAnalisys();
                Console.WriteLine("Refreshed Data : {0} - {1}", startDate.ToString(), endDate.ToString());
                return true;

            }
            else
            {
                Console.WriteLine("Data not refreshed, same query: {0} - {1}", startDate.ToString(), endDate.ToString());
                return false;
            }
        }
    }
}

