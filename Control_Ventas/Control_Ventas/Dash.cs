using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using CapaNegocio;

namespace Control_Ventas
{
    public partial class Dash : Form
    {
        private readonly Dashboard model;
        public Dash()
        {
            InitializeComponent();

            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7days.Select();

            model = new Dashboard();
            LoadData();
        }

        private void LoadData()
        {
            var refreshData = model.LoadData(dtpStartDate.Value, dtpEndDate.Value);
            string mensaje = ""; 
            try
            {
                if (refreshData == true)
                {
                    lblNumOrders.Text = model.NumOrders.ToString();
                    lblTotalRevenue.Text = "$" + model.TotalRevenue.ToString();
                    lblTotalProfit.Text = "$" + model.TotalProfit.ToString();

                    lblNumCustomers.Text = model.NumCustomers.ToString();
                    lblNumProducts.Text = model.numProducts.ToString();

                    chartGrossRevenue.DataSource = model.GrossRevenueList;
                    chartGrossRevenue.Series[0].XValueMember = "Date";
                    chartGrossRevenue.Series[0].YValueMembers = "Total amount";
                    chartGrossRevenue.DataBind();

                    chartTopProducts.DataSource = model.TopProductsList.ToString();
                    chartTopProducts.Series[0].XValueMember = "Key";
                    chartTopProducts.Series[0].YValueMembers = "Value";
                    chartTopProducts.DataBind();

                    dgvUnderstock.DataSource = model.UnderStockList;
                    dgvUnderstock.Columns[0].HeaderText = "Item";
                    dgvUnderstock.Columns[1].HeaderText = "Units";
                    Console.WriteLine("Loaded view :)");

                }
                else Console.WriteLine("View not loaded, same query");

            }catch (Exception ex)
            {
                mensaje = ex.Message;  
            }
        }

        private void DisableCustomDates()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOk.Visible = false;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnLast7days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnLast30days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnLastMonth_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnCustomDate_Click(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;
            btnOk.Visible = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
