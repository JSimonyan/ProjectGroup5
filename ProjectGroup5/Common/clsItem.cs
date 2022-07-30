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
        /// Single letter code for an item. Always a capital letter
        /// </summary>
        public char itemCode;

        /// <summary>
        /// 1-4 word description of the item
        /// </summary>
        public string itemDesc;

        /// <summary>
        /// Cost of the item. All initial items have an even .00 cost
        /// </summary>
        public decimal itemCost;

        /// <summary>
        /// Overridden to return the description
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return itemDesc;
        }
    }

}
