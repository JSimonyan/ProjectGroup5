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
using ProjectGroup5.Main;
using ProjectGroup5.Common;

namespace ProjectGroup5.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        clsItemsLogic IL;
        clsItemsSQL isql;
        int Selection = 0;
        clsItem item;
        public bool itemChanged;//default false but changes to true if any items are changed 
        public wndItems()
        {
            InitializeComponent();
            IL = new clsItemsLogic();
            isql = new clsItemsSQL();
            itemChanged = false;
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Selection = 3;
                UpdateDescCanvas.Visibility = Visibility.Hidden;
                AddItemCanvas.Visibility = Visibility.Visible;
                UpdateLineItemCanvas.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// all canvas besides the update Description canvas 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDescBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Selection = 1;

                UpdateDescCanvas.Visibility = Visibility.Visible;
                AddItemCanvas.Visibility = Visibility.Hidden;
                UpdateLineItemCanvas.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void UpdateLineItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Selection = 2;
                UpdateDescCanvas.Visibility = Visibility.Hidden;
                AddItemCanvas.Visibility = Visibility.Hidden;
                UpdateLineItemCanvas.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ItemDataGrid.SelectedItem != null || Selection == 3)
                {
                    item = (clsItem)ItemDataGrid.SelectedItem;
                    switch (Selection)
                    {

                        case 1:
                            IL.updateDesc(ItemDescriptionTxtbx.Text, ItemCostTxtbx.Text, item.Description);

                            itemChanged = true;

                            break;
                        case 2:


                            IL.updateLineItems(InvoiceNumberTxtbx.Text, LineNumberTxtbx.Text, LineItemCostTextBox.Text);
                            itemChanged = true;

                            break;
                        case 3:


                            IL.addItem(AddItemDescriptionTxtbx.Text, AddItemCostTxtbx.Text, AddItemCodeTxtbx.Text);
                            itemChanged = true;

                            break;
                        case 0:

                            MessageBox.Show("Please Select an Action before saving. ");

                            break;

                    }

                }
                else if (ItemDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Please select an Item first. ");

                }
                //clear source then refresh to reflect any changes made. 
                ItemDataGrid.ItemsSource = null;
                ItemDataGrid.ItemsSource = isql.SQLGetAllitems();

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ItemDataGrid.SelectedItem != null)
                {
                    IL.DeleteItems(item.Code);
                    //clear source then refresh to reflect any changes made. 
                    itemChanged = true;
                    ItemDataGrid.ItemsSource = null;
                    ItemDataGrid.ItemsSource = isql.SQLGetAllitems();
                }
                else
                {
                    MessageBox.Show("Please select an item before attempting to delete");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void ItemDB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ItemDataGrid.SelectedItem != null)
                {
                    item = (clsItem)ItemDataGrid.SelectedItem;
                    ItemDescriptionTxtbx.Text = item.Description;
                    ItemCostTxtbx.Text = Convert.ToString(item.Cost);
                }
                else
                    return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// fills the ItemDataGrid with all the items in the itemDesc table when the item table is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wndItems_loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemDataGrid.ItemsSource = isql.SQLGetAllitems();

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// close this window and show main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
