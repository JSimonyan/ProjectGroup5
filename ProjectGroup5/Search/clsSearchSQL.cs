using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Text;

namespace ProjectGroup5.Search
{
    class clsSearchSQL
    {

        static string selectAllInvoices = "SELECT * FROM Invoices";
        static string selectAllInvoicesWhereInvoiceNum = "SELECT * FROM Invoices WHERE InvoiceNum = ";
        static string selectAllInvoicesWhereInvoiceDate = "SELECT * FROM Invoices WHERE InvoiceDate = ";
        static string selectAllInvoicesWhereTotalCost = "SELECT * FROM Invoices WHERE TotalCost = ";
        //- SELECT * FROM Invoices WHERE InvoiceNum = 5000
        //- SELECT * FROM Invoices WHERE InvoiceNum = 5000 AND InvoiceDate = #4/13/2018#
        //-SELECT * FROM Invoices WHERE InvoiceNum = 5000 AND InvoiceDate = #4/13/2018# AND TotalCost = 120
        //-SELECT * FROM Invoices WHERE TotalCost = 1200
        //- SELECT * FROM Invoices WHERE TotalCost = 1300 and InvoiceDate = #4/13/2018# 
        //-SELECT * FROM Invoices WHERE InvoiceDate = #4/13/2018#


        clsDataAccess da;
        DataSet ds;

        /// <summary>
        /// constructor to set search cls
        /// </summary>
        public clsSearchSQL()
        {
            try
            {
                da = new clsDataAccess();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }


        /// <summary>
        /// Returns SQL code for getting all Invoices.
        /// </summary>
        /// <returns></returns>
        public string selectAllInvoicesSql()
        {
            try
            {
                return selectAllInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }        
        
        
        /// <summary>
        /// Returns SQL code for getting all Invoices by in.
        /// </summary>
        /// <returns></returns>
        public string searchByInvoiceNumberSql()
        {
            try
            {
                return selectAllInvoicesWhereInvoiceNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }        
        
        /// <summary>
        /// Returns SQL code for getting all Invoices by date.
        /// </summary>
        /// <returns></returns>
        public string searchByInvoiceDateSql()
        {
            try
            {
                return selectAllInvoicesWhereInvoiceDate;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns SQL code for getting all Invoices.
        /// </summary>
        /// <returns></returns>
        public string searchByInvoiceTotalCostSql()
        {
            try
            {
                return selectAllInvoicesWhereTotalCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        
    }
}
