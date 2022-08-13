using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using ProjectGroup5.Common;

namespace ProjectGroup5.Main
{
    class clsMainSQL
    {
        clsDataAccess da;
        DataSet ds;

        /// <summary>
        /// Constructor
        /// </summary>
        public clsMainSQL()
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
        /// Returns SQL code for updating the total for an invoice
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLUpdateTotal(int totalCost, int invoiceNum)
        {
            try
            {
                string sSQL = "UPDATE Invoices SET TotalCost = " + totalCost + " WHERE InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns SQL code for inserting an into item an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public string SQLInsertItem(int invoiceNum, int lineItemNum, string ItemCode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (" + invoiceNum + ", " + lineItemNum + ", '" + ItemCode + "')";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns SQL code for making a new invoice
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="invoiceCost"></param>
        /// <returns></returns>
        public string SQLNewInvoice(string invoiceDate, int invoiceCost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#" + invoiceDate + "#, " + invoiceCost + ");";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }


        /// <summary>
        /// Returns SQL code for retrieving an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLGetInvoice(int invoiceNum)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns the SQL query to retrieve the most recent invoice
        /// </summary>
        /// <returns></returns>
        public string SQLGetNewInvoice()
        {
            try
            {
                string sSQL = "SELECT MAX(InvoiceNum) FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns SQL code for getting all times.
        /// </summary>
        /// <returns></returns>
        public string SQLGetItems()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost from ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns SQL code for getting line items
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLGetLineItems(int invoiceNum)
        {
            try
            {
                string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns SQL code for updating date
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public string SQLUpdateDate(int invoiceNum, string invoiceDate)
        {
            try
            {
                string sSQL = "UPDATE Invoices SET InvoiceDate = #" + invoiceDate + "# WHERE InvoiceNum = " + invoiceNum; ;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns SQL code for removing line itmes
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <returns></returns>
        public string SQLRemoveItem(int invoiceNum, int lineItemNum)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems WHERE LineItemNum = " + lineItemNum + " AND InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns SQL code for updating line item numbers
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <returns></returns>
        public string SQLUpdateLines(int invoiceNum, int lineItemNum)
        {
            try
            {
                string sSQL = "UPDATE LineItems SET LineItemNum = LineItemNum - 1 WHERE LineItemNum > " + lineItemNum + " AND InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns SQL code for deleting an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLDeleteInvoice(int invoiceNum)
        {
            try
            {
                string sSQL = "DELETE FROM Invoices WHERE InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Returns SQL code for deleting line items associated with an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLDeleteInvoiceItems(int invoiceNum)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }



    }
}
