using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectGroup5.Common
{
    /// <summary>
    /// Item class that stores the attributes for an item
    /// </summary>
    class clsItem
    {
        /// <summary>
        /// Char for the code
        /// </summary>
        private string code;

        /// <summary>
        /// Description string
        /// </summary>
        private string description;

        /// <summary>
        /// Item cost
        /// </summary>
        private double cost;

        /// <summary>
        /// Item constructor for the code, description and cost.
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="ItemDescription"></param>
        /// <param name="ItemCost"></param>
        public clsItem(string ItemCode, string ItemDescription, double ItemCost)
        {
            code = ItemCode;
            description = ItemDescription;
            cost = ItemCost;
        }

        /// <summary>
        /// Cost property
        /// </summary>
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        /// <summary>
        /// Description property
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        /// <summary>
        /// Code property
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
    }

}
