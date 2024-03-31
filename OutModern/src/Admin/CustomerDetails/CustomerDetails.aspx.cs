﻿using OutModern.src.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace OutModern.src.Admin.CustomerDetails
{
    public partial class CustomerDetails : System.Web.UI.Page, IFilter
    {

        protected static readonly string OrderDetails = "OrderDetails";
        protected static readonly string CustomerEdit = "CustomerEdit";

        // Side menu urls
        protected Dictionary<string, string> urls = new Dictionary<string, string>()
        {
            { OrderDetails , "~/src/Admin/OrderDetails/OrderDetails.aspx" },
            { CustomerEdit , "~/src/Admin/CustomerEdit/CustomerEdit.aspx" }
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptAddress.DataSource = GenerateDummyAddresses();
                rptAddress.DataBind();

                lvOrders.DataSource = GetOrders();
                lvOrders.DataBind();
                Page.DataBind();
            }
        }

        protected void lvOrders_PagePropertiesChanged(object sender, EventArgs e)
        {
            lvOrders.DataSource = GetOrders();
            lvOrders.DataBind();
        }

        private DataTable GenerateDummyAddresses()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AddressName", typeof(string));
            dt.Columns.Add("AddressLine", typeof(string));
            dt.Columns.Add("PostalCode", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("Country", typeof(string));

            // Add some dummy rows
            for (int i = 0; i < 2; i++)
            {
                dt.Rows.Add("Address " + i, "123 Fake Street " + i, "12345", "Anytown", "CA", "USA");
            }

            return dt;
        }

        //dummy data
        protected DataTable GetOrders()
        {
            DataTable dtOrders = new DataTable();
            dtOrders.Columns.AddRange(new DataColumn[] {
                new DataColumn("OrderId", typeof(int)),
                new DataColumn("ProductDetails", typeof(DataTable)),
                new DataColumn("OrderDateTime", typeof(DateTime)),
                new DataColumn("SubTotal", typeof(decimal)),
                new DataColumn("OrderStatus", typeof(string))
              });

            // Generate 5 dummy orders with random statuses
            string[] orderStatuses = { "Order Placed", "Shipped", "Cancelled", "Received" };
            for (int i = 0; i < 20; i++)
            {
                DataRow drOrder = dtOrders.NewRow();
                drOrder["OrderId"] = i + 1;

                // Nested DataTable for products
                DataTable dtProducts = new DataTable();
                dtProducts.Columns.AddRange(new DataColumn[] {
                   new DataColumn("ProductName", typeof(string)),
                   new DataColumn("Quantity", typeof(int))
                  });
                dtProducts.Rows.Add("Product A", 2);
                dtProducts.Rows.Add("Product B", 1);

                drOrder["ProductDetails"] = dtProducts;
                drOrder["OrderDateTime"] = DateTime.Now.AddDays(-i);
                drOrder["SubTotal"] = 10.00m * (i + 1);
                drOrder["OrderStatus"] = orderStatuses[i % orderStatuses.Length];

                dtOrders.Rows.Add(drOrder);
            }

            return dtOrders;
        }

        protected void lvOrders_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem) return;


            DataRowView rowView = (DataRowView)e.Item.DataItem;
            HtmlGenericControl statusSpan = (HtmlGenericControl)e.Item.FindControl("orderStatus");
            string status = rowView["OrderStatus"].ToString();

            switch (status)
            {
                case "Order Placed":
                    statusSpan.Attributes["class"] += " order-placed";
                    break;
                case "Shipped":
                    statusSpan.Attributes["class"] += " shipped";
                    break;
                case "Cancelled":
                    statusSpan.Attributes["class"] += " cancelled";
                    break;
                case "Received":
                    statusSpan.Attributes["class"] += " received";
                    break;
                default:
                    // Handle cases where status doesn't match any of the above
                    break;
            }
        }

        //search logic
        public void FilterListView(string searchTerm)
        {
            lvOrders.DataSource = FilterDataTable(GetOrders(), searchTerm);
            lvOrders.DataBind();
        }

        private DataTable FilterDataTable(DataTable dataTable, string searchTerm)
        {
            // Escape single quotes for safety
            string safeSearchTerm = searchTerm.Replace("'", "''");

            // Build the filter expression with relevant fields
            string expression = string.Format(
                "Convert(OrderId, 'System.String') LIKE '%{0}%' OR " +
                "CustomerName LIKE '%{0}%' OR " +
                "Convert(OrderDateTime, 'System.String') LIKE '%{0}%' OR " +
                "Convert(SubTotal, 'System.String') LIKE '%{0}%' OR " +
                "OrderStatus LIKE '%{0}%'",
                safeSearchTerm);

            // Filter the rows
            DataRow[] filteredRows = dataTable.Select(expression);

            // Create a new DataTable for the filtered results
            DataTable filteredDataTable = dataTable.Clone();

            // Import the filtered rows
            foreach (DataRow row in filteredRows)
            {
                filteredDataTable.ImportRow(row);
            }

            return filteredDataTable;
        }
    }
}