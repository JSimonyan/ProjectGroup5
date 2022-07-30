using System;
using System.Collections.Generic;
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
using ProjectGroup5.Search;
using ProjectGroup5.Items;
using ProjectGroup5.Common;

namespace ProjectGroup5.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        clsMainLogic mLogic;
        clsMainSQL msql;

        /// <summary>
        /// Initializes window
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();
                mLogic = new clsMainLogic();
                msql = new clsMainSQL();
                UpdateItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Opens the search tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndSearch search = new wndSearch();
                search.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Opens the edit items tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndItems wm = new wndItems();
                wm.ShowDialog();
                UpdateItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Opens an invoice and displays the data on the menu. Called by Search window before closing
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void OpenInvoice(int invoiceNum)
        {
            // Calls open function in logic to update invoice info
            // Updates the display
        }

        private void UpdateItems()
        {
            // Gets SQL query from main sql
            // Executes SQL query with da
            // Updates the item display in the combo box
        }
    }
}
