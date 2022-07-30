using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;

namespace ProjectGroup5.Main
{
    class clsMainLogic
    {
        /// <summary>
        /// Currently open invoice. 0 = N/A
        /// </summary>
        public int currentInvoice = 0;

        /// <summary>
        /// Currently opened invoice date
        /// </summary>
        public DateTime invoiceDate;

        /// <summary>
        /// Currently opened invoice cost
        /// </summary>
        public decimal invoiceCost;

        /// <summary>
        /// SQL commands
        /// </summary>
        clsMainSQL msql;

        /// <summary>
        /// Data access class used to execute SQL commands
        /// </summary>
        clsDataAccess da;

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
        /// Updates currently selected invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void update(int invoiceNum)
        {
            try
            {
                //Gets string from mainsql
                //Executes SQL query with da
                //Updates internal info
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
