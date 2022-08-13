using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectGroup5.Common
{
    /// <summary>
    /// Invoice class that stores the invoice attributes
    /// </summary>
    class clsInvoice
    {
        /// <summary>
        /// Unique invoice number. Starts with 5000 and increments by 1
        /// </summary>
        public int invoiceNum;

        /// <summary>
        /// Date the invoice was created
        /// </summary>
        public string invoiceDate;

        /// <summary>
        /// Total cost for the invoice
        /// </summary>
        public decimal totalCost;

        /// <summary>
        /// Overridden to return the number
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Invoice #" + invoiceNum;
        }

        public clsInvoice(int num, string date, int cost)
        {
            invoiceNum = num;
            invoiceDate = date;
            totalCost = cost;
        }

        public clsInvoice()
        {
            invoiceNum = -1;
            invoiceDate = DateTime.Today.ToString("MM/dd/yyyy");
            totalCost = 0;
        }
    }

    
}
