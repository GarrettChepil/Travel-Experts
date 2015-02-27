using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//created by Garrett
namespace TravelExperts
{
    public partial class frmAddModifySupplier : Form
    {
        public frmAddModifySupplier()
        {
            InitializeComponent();
        }

        public bool add;
        public Supplier supplier = new Supplier();
        public SupplierContacts supplierContact = new SupplierContacts();
        public List<Products_Suppliers> productSuppliers = new List<Products_Suppliers>();

        // sets up the form on load
        private void frmAddModifySupplier_Load(object sender, EventArgs e)
        {
                      
            this.LoadProductComboBox();
            // does the fiollowing if its a add
            if(this.add)
            {
                // find the next availble supplierid and suppliercontactid
                supplierContact.SupplierContactId = SupplierContactsDB.GetNextAvailableID();
                txtSCID.Text = supplierContact.SupplierContactId.ToString();

                supplierContact.SupplierId = SupplierDB.GetNextAvailableID();
                supplier.SupplierId = SupplierDB.GetNextAvailableID();
                this.Text = "Add a Supplier";
                lblTitle.Text = "Add Supplier Information";
                cmbProductBName.SelectedIndex = -1;
            }
            //else does this for modify
            else
            {
                this.Text = "Modify a Supplier";
                lblTitle.Text = "Modify Supplier Information";

                try
                {
                    supplier = SupplierDB.GetSupplier(supplierContact.SupplierId);
                    productSuppliers = Products_SuppliersDB.GetAllProdSupOnID(supplierContact.SupplierId);
                    this.DisplaySupplier();
                    this.RedisplayList();
                    cmbProductBName.SelectedIndex = -1;

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());

                }
            }
        }

        // a method to dispaly the supplier being modified
        private void DisplaySupplier()
        {
            txtSCID.Text = supplierContact.SupplierContactId.ToString();
            //txtSCID.ReadOnly = true;
            txtCName.Text = supplierContact.SupConCompany.ToString();
            txtFirst.Text = supplierContact.SupConFirstName;
            txtLast.Text = supplierContact.SupConLastName;
            txtAddress.Text = supplierContact.SupConAddress;
            txtCity.Text = supplierContact.SupConCity;
            txtProv.Text = supplierContact.SupConProv;
            txtPO.Text = supplierContact.SupConPostal;
            txtCountry.Text = supplierContact.SupConCountry;
            txtPhone.Text = supplierContact.SupConBusPhone;
            txtFax.Text = supplierContact.SupConFax;
            txtEmail.Text = supplierContact.SupConEmail;
            txtURL.Text = supplierContact.SupConURL;
            
        }

        // method to load the combo box for products
        private void LoadProductComboBox()
        {
            List<Product> cbproducts = new List<Product>();
            try
            {
                cbproducts = ProductsDB.GetAll();
                cmbProductBName.DataSource = cbproducts;
                cmbProductBName.DisplayMember = "ProdName";
                cmbProductBName.ValueMember = "ProductId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        
        // button for accepting the form
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtCName.IsPresent())
            {
                //for add
                if(add)
                {
                    this.createSupplierObjects();
                    try
                    {
                        SupplierDB.AddSupp(supplier);
                        SupplierContactsDB.AddSupp(supplierContact);

                        foreach(Products_Suppliers ps in productSuppliers)
                        {
                            ps.supplierid = supplier.SupplierId;
                            Products_SuppliersDB.AddProdSupp(ps);
                        }

                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                    
                }
            
                // for modify
                else
                {
                    SupplierContacts newSupplierinfo = new SupplierContacts();
                    newSupplierinfo.SupplierContactId = Convert.ToInt32(txtSCID.Text);
                    newSupplierinfo.SupConFirstName = txtFirst.Text.ToString();
                    newSupplierinfo.SupConLastName = txtLast.Text.ToString();
                    newSupplierinfo.SupConCompany = txtCName.Text.ToString();
                    newSupplierinfo.SupConAddress = txtAddress.Text.ToString();
                    newSupplierinfo.SupConCity = txtCity.Text.ToString();
                    newSupplierinfo.SupConProv = txtProv.Text.ToString();
                    newSupplierinfo.SupConPostal = txtPO.Text.ToString();
                    newSupplierinfo.SupConCountry = txtCountry.Text.ToString();
                    newSupplierinfo.SupConBusPhone = txtPhone.Text.ToString();
                    newSupplierinfo.SupConFax = txtFax.Text.ToString();
                    newSupplierinfo.SupConEmail = txtEmail.Text.ToString();
                    newSupplierinfo.SupConURL = txtURL.Text.ToString();
                    newSupplierinfo.AffiliationId = "";
                    newSupplierinfo.SupplierId = Convert.ToInt32(txtSCID.Text);

                    try
                    {
                        if(!SupplierContactsDB.UpdateSupplier(supplierContact, newSupplierinfo))
                        {
                            MessageBox.Show("That Supplier has been updated or deleted already.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            supplierContact = newSupplierinfo;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }

                }

            }
        }

        // created the supplier objects for adding to the DB
        private void createSupplierObjects()
        {
            // create supplier
            supplier.SupName = txtCName.Text.ToString();

            // create suppliercontact
            supplierContact.SupConFirstName = txtFirst.Text.ToString();
            supplierContact.SupConLastName = txtLast.Text.ToString();
            supplierContact.SupConCompany = txtCName.Text.ToString();
            supplierContact.SupConAddress = txtAddress.Text.ToString();
            supplierContact.SupConCity = txtCity.Text.ToString();
            supplierContact.SupConProv = txtProv.Text.ToString();
            supplierContact.SupConPostal = txtPO.Text.ToString();
            supplierContact.SupConCountry = txtCountry.Text.ToString();
            supplierContact.SupConBusPhone = txtPhone.Text.ToString();
            supplierContact.SupConFax = txtFax.Text.ToString();
            supplierContact.SupConEmail = txtEmail.Text.ToString();
            supplierContact.SupConURL = txtURL.Text.ToString();
            supplierContact.AffiliationId = "";
        }

        // button to cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // button to add a product to the supplier
        private void btnProduct_Click(object sender, EventArgs e)
        {
            if (cmbProductBName.IsPresent())
            {
                // for adding a supplier
                if (add)
                {
                    Products_Suppliers product = new Products_Suppliers();
                    product.productid = (int)cmbProductBName.SelectedValue;
                    product.productName = cmbProductBName.Text;

                    productSuppliers.Add(product);

                    RedisplayList();
                    cmbProductBName.SelectedIndex = -1;
                }
                //for modifing a supplier
                else
                {
                    Products_Suppliers product = new Products_Suppliers();
                    product.productid = (int)cmbProductBName.SelectedValue;
                    product.productName = cmbProductBName.Text;
                    product.supplierid = supplier.SupplierId;
                    try
                    {
                        Products_SuppliersDB.AddProdSupp(product);
                        productSuppliers = Products_SuppliersDB.GetAllProdSupOnID(supplierContact.SupplierId);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());

                    }
                    RedisplayList();
                    cmbProductBName.SelectedIndex = -1;



                }
            }
        }

        // redispaly the product list
        private void RedisplayList()
        {
            // clears the listbox
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = productSuppliers;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;


            dgvProducts.Columns[0].Visible = false;
            dgvProducts.Columns[1].Visible = false;
            dgvProducts.Columns[2].Visible = false;
            dgvProducts.Columns[3].Width = 195;
        }

        // button to delete fa product from the list of products
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                // for adding
                if (add)
                {
                    productSuppliers.RemoveAt(this.dgvProducts.SelectedRows[0].Index);
                    RedisplayList();
                }
                //for modifing
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this Product offered by Supplier?", 
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        int index = this.dgvProducts.SelectedRows[0].Index;
                        try
                        {
                            if(!Products_SuppliersDB.DeleteProdSup(productSuppliers[index]))
                            {
                                MessageBox.Show("That product has been updated or deleted already.", "Database Error");
                            }
                            else
                            {
                                productSuppliers = Products_SuppliersDB.GetAllProdSupOnID(supplierContact.SupplierId);
                                this.RedisplayList();
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, ex.GetType().ToString());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a Product");
            }
        }


    }
}
