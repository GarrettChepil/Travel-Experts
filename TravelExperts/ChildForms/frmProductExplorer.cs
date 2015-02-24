using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelExperts
{
    public partial class frmProductExplorer : Form
    {
        // Team 4 - Mehmet Demirci 

        public frmProductExplorer()
        {
            InitializeComponent();
        }

        List<Product> productList = null;
        Product product;

        private void Form1_Load(object sender, EventArgs e)
        {
            productList = ProductsDB.GetAll();
            productDGV.MultiSelect = false;
            productDGV.DataSource = productList;

            cmbProducts.DataSource = productList;
            cmbProducts.DisplayMember = "ProdName";
            cmbProducts.ValueMember = "ProductId";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProductAddEdit productAddForm = new frmProductAddEdit();
            productAddForm.xNewProduct = true;

            if (productAddForm.ShowDialog() == DialogResult.OK)
            {
                RefreshList();
                cmbProducts.Text = productAddForm.txbProductName.Text;  // Reposition the selection (cursor)
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmProductAddEdit productEditForm = new frmProductAddEdit();
            productEditForm.xNewProduct = false;

            productEditForm.txbProductID.Text = this.txbProductID.Text;
            productEditForm.txbProductName.Text = this.txbProductName.Text;

            DialogResult result = productEditForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                RefreshList();
                cmbProducts.SelectedValue = Convert.ToInt32(productEditForm.txbProductID.Text); // Reposition the selection (cursor)
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            product = new Product();
            product.ProductId = Convert.ToInt32(txbProductID.Text);
            product.ProdName = txbProductName.Text;
            DialogResult result = MessageBox.Show("Delete Product ID: " + product.ProductId.ToString() + "?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!ProductsDB.DeleteProduct(product))
                    {
                        MessageBox.Show("Another user has updated or deleted " +
                            "that product.", "Database Error");
                    }
                    else
                        RefreshList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void productDGV_SelectionChanged(object sender, EventArgs e)
        {
            int index = productDGV.CurrentRow.Index;
            txbProductID.Text = productDGV.Rows[index].Cells[0].Value.ToString();   // Copy selected item to related TextBoxes
            txbProductName.Text = productDGV.Rows[index].Cells[1].Value.ToString();
        }

        private void RefreshList()
        {
            GC.SuppressFinalize(productList);   // remove existing List object 
            productList = ProductsDB.GetAll();  // and get new one              
            RefreshDGV();                       // refresh the DataGridView Control
        }

        private void RefreshDGV()
        {
            productDGV.DataSource = null;           
            productDGV.DataSource = productList;

            cmbProducts.DataSource = null;              // refresh ComboBox too
            cmbProducts.DataSource = productList;
            cmbProducts.DisplayMember = "ProdName";
            cmbProducts.ValueMember = "ProductId";
        }
    }
}
