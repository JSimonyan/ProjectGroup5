using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;
using ProjectGroup5.Common;

namespace ProjectGroup5.Main
{
    class clsMainLogic
    {
        /// <summary>
        /// Currently open invoice. 0 = N/A
        /// </summary>
        public clsInvoice currentInvoice;

        /// <summary>
        /// SQL commands
        /// </summary>
        clsMainSQL msql;

        /// <summary>
        /// Data access class used to execute SQL commands
        /// </summary>
        clsDataAccess da;

        /// <summary>
        /// List of all items
        /// </summary>
        public List<clsItem> itemsList = new List<clsItem>();

        /// <summary>
        /// List of items for the invoice
        /// </summary>
        public List<clsItem> invoiceItems = new List<clsItem>();

        public clsMainLogic()
        {
            try
            {
                msql = new clsMainSQL();
                da = new clsDataAccess();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the stored list of all items
        /// </summary>
        /// <returns></returns>
        public void UpdateAllItems()
        {
            try
            {
                int iRet = 0;
                int costParse;
                itemsList.Clear();

                // Executes query
                DataSet passSet = da.ExecuteSQLStatement(msql.SQLGetItems(), ref iRet);

                // Creates the items with the dataset and adds them to the list
                for (int i = 0; i < passSet.Tables[0].Rows.Count; i++)
                {
                    if (int.TryParse(passSet.Tables[0].Rows[i][2].ToString(), out costParse))
                    {
                        itemsList.Add(new clsItem(passSet.Tables[0].Rows[i][0].ToString(), passSet.Tables[0].Rows[i][1].ToString(), costParse));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates a new invoice and returns the Invoice
        /// </summary>
        /// <returns></returns>
        public clsInvoice NewInvoice()
        {
            try
            {
                int iRet = 0;
                // Makes New Invoice
                string sql = msql.SQLNewInvoice(DateTime.Today.ToString("MM/dd/yyyy"), 0);
                da.ExecuteNonQuery(sql);

                // Returns the Invoice
                return GetInvoice((int)da.ExecuteSQLStatement(msql.SQLGetNewInvoice(), ref iRet).Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Returns an invoice Item based on a given invoice number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public clsInvoice GetInvoice(int invoiceNum)
        {
            try
            {
                int iRet = 0;
                DataSet invoice = da.ExecuteSQLStatement(msql.SQLGetInvoice(invoiceNum), ref iRet);
                return new clsInvoice(invoiceNum, invoice.Tables[0].Rows[0][1].ToString(), (int)invoice.Tables[0].Rows[0][2]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteInvoice()
        {
            try
            {
                // Removes all line item
                da.ExecuteNonQuery(msql.SQLDeleteInvoiceItems(currentInvoice.invoiceNum));
                // Deletes the invoice
                da.ExecuteNonQuery(msql.SQLDeleteInvoice(currentInvoice.invoiceNum));
                // Removes the selected invoice
                currentInvoice = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateItemsList()
        {
            try
            {
                int iRet = 0;
                DataSet items = da.ExecuteSQLStatement(msql.SQLGetLineItems(currentInvoice.invoiceNum), ref iRet);
                invoiceItems.Clear();
                for (int i = 0; i < iRet; i++)
                {
                    invoiceItems.Add(new clsItem(items.Tables[0].Rows[i][0].ToString(), items.Tables[0].Rows[i][1].ToString(), double.Parse(items.Tables[0].Rows[i][2].ToString())));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adds a new line item for the selected invoice
        /// </summary>
        /// <param name="itemCode"></param>
        public void AddLineItem(string itemCode, double itemAmount)
        {
            string sql = msql.SQLInsertItem(currentInvoice.invoiceNum, invoiceItems.Count + 1, itemCode);
            try
            {
                // Adds new line item
                da.ExecuteNonQuery(sql);

                currentInvoice.totalCost += Convert.ToDecimal(itemAmount);

                sql = msql.SQLUpdateTotal(Convert.ToInt32(currentInvoice.totalCost), currentInvoice.invoiceNum);
                da.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + sql + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// Updates the date
        /// </summary>
        /// <param name="date"></param>
        public void UpdateDate(DateTime date)
        {
            try
            {
                string sql = msql.SQLUpdateDate(currentInvoice.invoiceNum, date.ToString("MM/dd/yyyy"));
                da.ExecuteNonQuery(sql);
                currentInvoice = GetInvoice(currentInvoice.invoiceNum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Removes an item from the selected invoice
        /// </summary>
        /// <param name="lineItemNum"></param>
        public void RemoveItem(int lineItemNum, double itemAmount)
        {
            string sql = msql.SQLRemoveItem(currentInvoice.invoiceNum, lineItemNum);
            try
            {
                // Removes line item
                da.ExecuteNonQuery(sql);

                //Updates the existing line item numbers above the removed one
                sql = msql.SQLUpdateLines(currentInvoice.invoiceNum, lineItemNum);
                da.ExecuteNonQuery(sql);

                currentInvoice.totalCost -= Convert.ToDecimal(itemAmount);

                sql = msql.SQLUpdateTotal(Convert.ToInt32(currentInvoice.totalCost), currentInvoice.invoiceNum);
                da.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + sql + " -> " + ex.Message);
            }
        }

    }

}
