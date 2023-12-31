﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private BindingSource ShowProductList;
        public frmAddProduct()
        {
            InitializeComponent();

            ShowProductList = new BindingSource();

        }

        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        
        private int _Quantity;
        private double _SellPrice;

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z ]+$"))

                throw new StringFormatException("Invalid Product Name");

                return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^\d+$"))

                throw new NumberFormatException("Invalid Quantity");

                return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))

                throw new CurrencyFormatException("Invalid Selling Price");

                return Convert.ToDouble(price);
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = { "Beverages", "Bread/Bakery\r\n", "Canned/Jarred Goods\r\n", "Dairy", "Frozen Goods\r\n", "Meat", "Personal Care\r\n", "Other" };

            foreach (string x in ListOfProductCategory) 
            {
                cbCategory.Items.Add(x);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                ShowProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
                _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = ShowProductList;
            }
            catch (NumberFormatException x)
            {
                MessageBox.Show("Message: " + x.Message);
            }
            catch (StringFormatException x)
            {
                MessageBox.Show("Message: " + x.Message);
            }
            catch (CurrencyFormatException x)
            {
                MessageBox.Show("Message: " + x.Message);
            }
            finally 
            {
                MessageBox.Show("Nice Work!!");
            }
        }

        public class NumberFormatException : Exception 
        {
            public NumberFormatException(string qty) : base(qty) 
            {
            
            }
        }

        public class StringFormatException : Exception 
        {
            public StringFormatException(string pname) : base(pname) 
            {
            
            }
        }

        public class CurrencyFormatException : Exception 
        {
            public CurrencyFormatException(string sprice) : base(sprice) 
            {
            
            }
        }
    }    
}
    

