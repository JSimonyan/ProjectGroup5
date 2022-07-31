using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectGroup5.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        clsSearchSQL clsSearchSQL;
        clsSearchLogic searchLogic;
        wndSearch searchWnd;

        /// <summary>
        /// constructor to initilize search window
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            searchWnd = this;
            clsSearchSQL = new clsSearchSQL();
            searchLogic = new clsSearchLogic(ref searchWnd);
            searchLogic.FillValues(ref searchWnd);
        }

        /// <summary>
        /// set search InvoiceNumber filters when selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void invoiceNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                searchLogic.applyFilter("InvoiceNum", e.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set invoiceDate search filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void invoiceDateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                searchLogic.applyFilter("InvoiceDate", e.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set TotalCost search filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void invoiceCostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                searchLogic.applyFilter("TotalCost", e.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice and set new invoice object from selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                searchLogic.selectInvoice(e);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
