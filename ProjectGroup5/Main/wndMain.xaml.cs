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
        /// <summary>
        /// Main logic class
        /// </summary>
        clsMainLogic mLogic;
        
        /// <summary>
        /// Current group selection for the item display
        /// </summary>
        int selection;

        /// <summary>
        /// Initializes window
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();
                mLogic = new clsMainLogic();
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
                if (wm.itemChanged)
                {
                    UpdateItems();
                }
                wm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates all items in the add items box
        /// </summary>
        private void UpdateItems()
        {
            try
            {
                // Gets the items from db
                mLogic.UpdateAllItems();

                // Clears items in the combo box
                CbAddItems.Items.Clear();

                // Adds the new items to the combo box
                for (int i = 0; i < mLogic.itemsList.Count; i++)
                {
                    CbAddItems.Items.Add(mLogic.itemsList[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Clicked on new invoice button. Creates new invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DisplayInvoice(mLogic.NewInvoice());
                // Enters edit mode
                SpAddRemove.IsEnabled = true;
                BtnEditInvoice.IsEnabled = false;
                BtnSaveInvoice.IsEnabled = true;
                BtnSearch.IsEnabled = false;
                BtnEditItems.IsEnabled = false;
                BtnNewInvoice.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Displays an invoice on the GUI
        /// </summary>
        /// <param name="invoice"></param>
        private void DisplayInvoice(clsInvoice invoice)
        {
            try
            {
                // Updates Invoice in Logic
                mLogic.currentInvoice = invoice;

                // Updates Invoice displays
                LblInvoice.Content = "Invoice " + invoice.invoiceNum;
                LblDate.Content = invoice.invoiceDate;
                LblTotalAmount.Content = invoice.totalCost;

                // Updates Item Display
                DisplayItems();

                // Updates calendar
                cldDate.DisplayDate = DateTime.Parse(invoice.invoiceDate);

                // Enables Buttons
                if (CbAddItems.SelectedIndex != -1)
                {
                    BtnAdd.IsEnabled = true;
                }
                BtnEditInvoice.IsEnabled = true;
                BtnDeleteInvoice.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the items display for a newly selected invoice
        /// </summary>
        private void DisplayItems(int newSelection = 1)
        {
            try
            {
                // Updates the list of items for the current invoice
                mLogic.UpdateItemsList();

                // Enables up/down buttons for items display
                if(newSelection == 1)
                {
                    BtnDown.IsEnabled = false;
                }

                // Updates the remove items combo box
                UpdateDeleteItems();

                // Sets the starting list
                selection = newSelection;

                // Displays the current set of items
                DisplaySelection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the table to display a certain set of 5 items
        /// </summary>
        private void DisplaySelection(bool clearAll = false) {
            try
            {
                if(clearAll)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        // Clears all rows
                        ((Label)((Border)GridItems.Children[(i + 1) * 4]).Child).Content = "";
                        ((Label)((Border)GridItems.Children[(i + 1) * 4 + 1]).Child).Content = "";
                        ((Label)((Border)GridItems.Children[(i + 1) * 4 + 2]).Child).Content = "";
                        ((Label)((Border)GridItems.Children[(i + 1) * 4 + 3]).Child).Content = "";
                    }
                }
                else
                {
                    //Numbers the rows
                    for (int i = 0; i < 5; i++)
                    {
                        // Checks to see if the row is empty
                        if (i < mLogic.invoiceItems.Count - (selection - 1) * 5)
                        {
                            // Adds item info to a row
                            ((Label)((Border)GridItems.Children[(i + 1) * 4]).Child).Content = (i + 1) + (selection - 1) * 5;
                            ((Label)((Border)GridItems.Children[(i + 1) * 4 + 1]).Child).Content = mLogic.invoiceItems[i + (selection - 1) * 5].Code;
                            ((Label)((Border)GridItems.Children[(i + 1) * 4 + 2]).Child).Content = mLogic.invoiceItems[i + (selection - 1) * 5].Description;
                            ((Label)((Border)GridItems.Children[(i + 1) * 4 + 3]).Child).Content = "$" + mLogic.invoiceItems[i + (selection - 1) * 5].Cost + ".00";
                        }
                        else
                        {
                            // Clears empty rows
                            ((Label)((Border)GridItems.Children[(i + 1) * 4]).Child).Content = "";
                            ((Label)((Border)GridItems.Children[(i + 1) * 4 + 1]).Child).Content = "";
                            ((Label)((Border)GridItems.Children[(i + 1) * 4 + 2]).Child).Content = "";
                            ((Label)((Border)GridItems.Children[(i + 1) * 4 + 3]).Child).Content = "";
                        }

                    }
                }
                

                //Enables the up button if needed
                if (mLogic.invoiceItems.Count > 5 * selection)
                {
                    BtnUP.IsEnabled = true;
                }
                else
                {
                    BtnUP.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// User clicked on the button to add an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Updates DB
                mLogic.AddLineItem(((clsItem)CbAddItems.SelectedItem).Code, ((clsItem)CbAddItems.SelectedItem).Cost);

                // Updates total display
                LblTotalAmount.Content = lblAddPrice.Content = "$" + mLogic.currentInvoice.totalCost + ".00";


                // Updates the items display BUT keeps the grid on the current page
                DisplayItems(selection);

                // Resets the add items UI
                CbAddItems.SelectedIndex = -1;
                BtnAdd.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Enables the "Add" Button if an invoice is selected and updates the cost display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbAddItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(mLogic.currentInvoice != null)
                {
                    BtnAdd.IsEnabled = true;
                }
                if(CbAddItems.SelectedIndex == -1)
                {
                    lblAddPrice.Content = "$0.00";
                }
                else
                {
                    lblAddPrice.Content = "$" + ((clsItem)CbAddItems.SelectedItem).Cost + ".00";
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Up button clicked on the grid submenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selection++;
                BtnDown.IsEnabled = true;
                DisplaySelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Down button clicked on the grid submenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selection--;
                if (selection == 1)
                {
                    BtnDown.IsEnabled = false;
                }
                DisplaySelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Enables the add/remove items and date controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SpAddRemove.IsEnabled = true;
                BtnEditInvoice.IsEnabled = false;
                BtnSaveInvoice.IsEnabled = true;
                BtnSearch.IsEnabled = false;
                BtnEditItems.IsEnabled = false;
                BtnNewInvoice.IsEnabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Saves the date and disables the add/remove items and date controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cldDate.SelectedDate != null)
                {
                    mLogic.UpdateDate((DateTime)cldDate.SelectedDate);
                    DisplayInvoice(mLogic.currentInvoice);
                }

                SpAddRemove.IsEnabled = false;
                BtnEditInvoice.IsEnabled = true;
                BtnSaveInvoice.IsEnabled = false;
                BtnSearch.IsEnabled = true;
                BtnEditItems.IsEnabled = true;
                BtnNewInvoice.IsEnabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the delete items combo box
        /// </summary>
        private void UpdateDeleteItems()
        {
            try
            {
                CbRemoveItems.Items.Clear();
                for (int i = 0; i < mLogic.invoiceItems.Count; i++)
                {
                    CbRemoveItems.Items.Add(mLogic.invoiceItems[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Enables the "Remove" Button and updates the cost display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbRemoveItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CbRemoveItems.SelectedIndex == -1)
                {
                    lblRemovePrice.Content = "$0.00";
                    BtnRemove.IsEnabled = false;
                }
                else
                {
                    lblRemovePrice.Content = "$" + ((clsItem)CbRemoveItems.SelectedItem).Cost + ".00";
                    BtnRemove.IsEnabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Removes the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Removes from DB
                mLogic.RemoveItem(CbRemoveItems.SelectedIndex + 1, ((clsItem)CbRemoveItems.SelectedItem).Cost);

                // Updates total display
                LblTotalAmount.Content = lblAddPrice.Content = "$" + mLogic.currentInvoice.totalCost + ".00";

                // Updates the items display
                DisplayItems();

                // Resets the remove items UI
                CbRemoveItems.SelectedIndex = -1;
                BtnRemove.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the selected invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this invoice?",
                    "Delete invoice",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Deletes invoice from DB
                mLogic.DeleteInvoice();

                // Clears UI
                SpAddRemove.IsEnabled = false;
                BtnEditInvoice.IsEnabled = false;
                BtnSaveInvoice.IsEnabled = false;
                BtnSearch.IsEnabled = true;
                BtnEditItems.IsEnabled = true;
                BtnNewInvoice.IsEnabled = true;
                CbAddItems.Items.Clear();
                CbRemoveItems.Items.Clear();
                DisplaySelection(true);
                LblInvoice.Content = "Invoice ####";
                LblDate.Content = "N/A";
                LblTotalAmount.Content = "N/A";
            }
        }
    }
}
