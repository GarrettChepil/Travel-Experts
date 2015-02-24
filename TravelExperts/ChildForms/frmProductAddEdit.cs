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
    public partial class frmProductAddEdit : Form
    {
        // Team 4 - Mehmet Demirci 

        public frmProductAddEdit()
        {
            InitializeComponent();
        }

        public bool xNewProduct;
        public Product product;

        private void frmProductAddEdit_Load(object sender, EventArgs e)
        {
            if (xNewProduct)
            {
                this.Text = "Add Product";
            }
            else
            {
                this.Text = "Edit Product";
                txbProductID.Text = product.ProductId.ToString(); ;
                txbProductName.Text = product.ProdName;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            product = new Product();

            if (string.IsNullOrEmpty(txbProductName.Text))
            {
                MessageBox.Show("Please use numeric characters for 'Product ID'",
                    "Input Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            product.ProductId = 0;
            product.ProdName = txbProductName.Text;

            if (xNewProduct)
            {
                try
                {
                    ProductsDB.AddProduct(product);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
            else
            {
                Product newProduct = new Product();
                newProduct.ProductId = Convert.ToInt32(txbProductID.Text);
                newProduct.ProdName = txbProductName.Text;

                product.ProductId = newProduct.ProductId;   // The same ID

                try
                {
                    if (!ProductsDB.UpdateProduct(product, newProduct))
                    {
                        MessageBox.Show("Another user has updated or " +
                            "deleted that product.", "Database Error");
                        this.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        product = newProduct;
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        
    }
}
