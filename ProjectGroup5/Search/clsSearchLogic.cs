using ProjectGroup5.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows;

namespace ProjectGroup5.Search
{
    class clsSearchLogic
    {
        //getSelectedInvoice return clsInvoice
        //GetInvoices(InvoiceNumber, InvoiceDate, TotalCost) -return DataSet
        //selected invoice can be retrieved using the getSelectedInvoice method to return an object instance of the invoice
        

        public wndSearch searchWnd;

        static string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
        public clsSearchSQL searchSql;
        public clsDataAccess clsDataAccess = new clsDataAccess(connectionString);
        public DataSet vDs = new DataSet();

        public clsInvoice selectedInvoice;

        /// <summary>
        /// Constructor to initilize values and classes
        /// </summary>
        /// <param name="wnd"></param>
        public clsSearchLogic(ref wndSearch wnd) 
        {
            searchWnd = wnd;
            searchSql = new clsSearchSQL();
            selectedInvoice = new clsInvoice();

            string sql = searchSql.selectAllInvoicesSql();

            OleDbConnection conn = new OleDbConnection(connectionString);
            conn.Open();

            OleDbDataAdapter vAdap = new OleDbDataAdapter(sql, conn);
            vAdap.Fill(vDs, "Invoices");
            conn.Close();

        }

        /// <summary>
        /// Function used to retrieve selected invoice 
        /// </summary>
        public clsInvoice getSelectedInvoice()
        {
        
            return selectedInvoice;
        }


        /// <summary>
        /// Function used to retrieve all invoices 
        /// </summary>
        public DataSet getAllInvoices()
        {
            return vDs;
        }

        /// <summary>
        /// Function used to apply filters to return query
        /// </summary>
        public clsInvoice applyFilter(string type, string value)
        {

            return selectedInvoice;
        }

        public void selectInvoice(RoutedEventArgs e) 
        {
            selectedInvoice = new clsInvoice();
        }

        /// <summary>
        /// initilize datagrid with table values and fill filter comboboxes
        /// </summary>
        public void FillValues(ref wndSearch wndSearch)
        {

           
            //having issues with the adapter for 32x systems so using prototype here
            //int rows = 0;
            //DataSet filledDataSet;
            //filledDataSet = clsDataAccess.ExecuteSQLStatement(sql, ref rows);
            //wndSearch.invoiceNumSearchLabel.Content = filledDataSet.Tables["Invoices"].ToString();

            DataView dv = new DataView(vDs.Tables["Invoices"]);

            wndSearch.invoiceDatagrid.ItemsSource = dv;

            foreach (DataRowView dr in dv)
            {
                wndSearch.invoiceNumberComboBox.Items.Add(dr["InvoiceNum"]);
                wndSearch.invoiceDateComboBox.Items.Add(dr["InvoiceDate"]);
                wndSearch.invoiceCostComboBox.Items.Add(dr["TotalCost"]);
            }
        }
    }
}
