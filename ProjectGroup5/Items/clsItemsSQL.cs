using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using ProjectGroup5.Common;

namespace ProjectGroup5.Items
{
    /// <summary>
    /// contains all SQL statements for the Items Window
    /// </summary>
    class clsItemsSQL
    {
        clsDataAccess da;
        DataSet ds;

        /// <summary>
        /// constructor
        /// </summary>
        public clsItemsSQL()
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
        /// Selects items based on the a name entered by user
        /// </summary>
        /// <param name="itemCode"> string containing a name for an item</param>
        /// <returns></returns>
        public string SQLSelectItems(string itemCode)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum FROM LineItems Where ItemCode = '" + itemCode + "';";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        public string SQLItemExists(string itemCode)
        { 
             try
            {
                string sSQL = "SELECT * FROM ItemDesc Where ItemCode = '" + itemCode + "';";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

/// <summary>
/// uses a string input by user in text boxes to update a entry in the item Table 
/// </summary>
/// <param name="itemUpdateSQL"> update entry input by string </param>
/// <returns></returns>
public string SQLupdateItems(string newDescription, string newCost, string itemCode)
        {
            try
            {
                string sSQL = "UPDATE ItemDesc SET " + "ItemDesc = '" + newDescription + "', Cost = " + newCost + " WHERE ItemCode = '" + itemCode + "';";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// updates LineItem From User Input
        /// </summary>
        /// <param name="lineItemUpdateSQL"></param>
        /// <returns></returns>
        public string SQLupdateLineItems(string LineItemCost, string InvoiceNumber, string LineNumber)
        {
            try
            {
                string sSQL = "UPDATE LineItems SET " + " Cost = " + LineItemCost + " WHERE InvoiceNum = " + InvoiceNumber + " and LineItemNum = " + LineNumber + " ;";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// uses a string input by user to delete from the item Table 
        /// </summary>
        /// <param name="itemDeleteSQL"> string containing when thing should be deleted from item table</param>
        public string SQLDeleteItem(string itemDeleteSQL)
        {
            try
            {
                string sSQL = "DELETE FROM ItemDesc WHERE ItemCode =  '" + itemDeleteSQL + "';";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// Creates a Sql Statement to insert into ItemDesc from data entered by user. 
        /// </summary>
        /// <param name="insertItemSQL"></param>
        /// <returns></returns>
        public string SQLInsertItem(string ItemCode, string ItemDescription, string ItemCost)
        {
            string sSQL = " Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values(" + "'" + ItemCode + "', '" + ItemDescription + "', " + ItemCost + ");";
            return sSQL;
        }
        /// <summary>
        /// gets all entries in the ItemDesc Table then inserts the results in a list that is then sent to the window to be displayed
        /// </summary>
        /// <returns></returns>
        public List<clsItem> SQLGetAllitems()
        {
            int iNumRetValues = 0;



            List<clsItem> items = new List<clsItem>();

            string sSQL = "Select ItemCode, ItemDesc, Cost FROM ItemDesc";



            ds = da.ExecuteSQLStatement(sSQL, ref iNumRetValues);

            foreach (DataRow dataRow in ds.Tables[0].Rows) // loop through the data set and add items.
            {
                items.Add(new clsItem(Convert.ToString(dataRow[0]), dataRow[1].ToString(), Convert.ToDouble(dataRow[2])));
            }

            return items;
        }


    }


}
