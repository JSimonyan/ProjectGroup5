using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;

namespace ProjectGroup5.Items
{   /// <summary>
    /// contains all the business logic for the items window
    /// </summary>
    class clsItemsLogic
    {
        clsItemsSQL isql;
        clsDataAccess da;
        DataSet ds;

        
        /// <summary>
        /// constructor
        /// </summary>
        public clsItemsLogic()
        {
            try
            {

                isql = new clsItemsSQL();
                da = new clsDataAccess();

                
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// Checks if item is on a invoice, if no invoice exists then item is deleted from dataBase
        /// </summary>
        /// <param name="itemDelete"> item object that contains item desc, itemCode, and cost </param>
        public void DeleteItems(string ItemCode)
        {
            try
            {

                string invoiceResults;
                string testSQL = isql.SQLSelectItems(ItemCode);
                invoiceResults = da.ExecuteScalarSQL(testSQL);

                if (invoiceResults == "")
                {
                    string sql = isql.SQLDeleteItem(ItemCode);
                    int rowsAffected;
                    rowsAffected = da.ExecuteNonQuery(sql);
                }
                else if (invoiceResults != "")
                {
                    MessageBox.Show("Selected item is on invoice Number " + invoiceResults + ". It cannot be deleted");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// creates and executes a line item update sql Statement
        /// </summary>
        /// <param name="InvoiceNumber"></param>
        /// <param name="LineNumber"></param>
        /// <param name="LineItemCost"></param>
        public void updateLineItems(string InvoiceNumber, string LineNumber, string LineItemCost)
        {
            try
            {
              
                string execSQL = isql.SQLupdateLineItems(LineItemCost,InvoiceNumber,LineNumber);
                da.ExecuteScalarSQL(execSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// updates an items description and cost using a sql Statement generated from user input
        /// </summary>
        /// <param name="newDescription"></param>
        /// <param name="newCost"></param>
        /// <param name="item"></param>
        public void updateDesc(string newDescription, string newCost, string ItemCode)
        {
            try
            {
               
                string execSQL = isql.SQLupdateItems(newDescription, newCost, ItemCode);
                da.ExecuteScalarSQL(execSQL);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// adds a new item to the ItemDesc Table
        /// </summary>
        /// <param name="ItemDescription"></param>
        /// <param name="ItemCost"></param>
        /// <param name="ItemCode"></param>
        public void addItem(string ItemDescription, string ItemCost, string ItemCode)
        {
            try
            {
                
               
               
                string execSQL = isql.SQLInsertItem(ItemCode,ItemDescription,ItemCost);
                da.ExecuteScalarSQL(execSQL);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
