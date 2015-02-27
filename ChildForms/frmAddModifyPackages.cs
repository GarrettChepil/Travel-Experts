using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//created By Garrett
namespace TravelExperts
{
    public partial class frmAddModifyPackages : Form
    {
        public frmAddModifyPackages()
        {
            InitializeComponent();
        }

        // sets up the form on load
        private void frmAddModifyPackages_Load(object sender, EventArgs e)
        {
            this.LoadProductComboBox();

            // does the fiollowing if its a add
            if(add)
            {
                this.Text = "Add a Package";
                lblTitle.Text = "Add Package Information";
                cmbProductBName.SelectedIndex = -1;
                cmbSuppliers.DataSource = null;
            }

             //else does this for modify
            else
            {
                this.Text = "Modify a Package";
                lblTitle.Text = "Modify Package Information";
                cmbProductBName.SelectedIndex = -1;
                cmbSuppliers.DataSource = null;


                try
                {
                    pack_prod_sup = Packages_Products_SuppliersDB.GetList(package.PackageId);
                    this.DisplayPackages();
                    this.RedisplayList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());

                }
            }
            
        }

        // displays the package to be edited
        private void DisplayPackages()
        {
            txtPkgName.Text = package.PkgName;
            dtpPkgStartDate.Value = package.PkgStartDate;
            dtpPkgEndDate.Value = package.PkgEndDate;
            txtDescription.Text = package.PkgDesc;
            txtPrice.Text = package.PkgBasePrice.ToString();
            txtCommision.Text = package.PkgAgencyCommission.ToString();
        }

        public bool add;
        public Package package = new Package();
        public List<Packages_Products_Suppliers> pack_prod_sup = new List<Packages_Products_Suppliers>();

        // loads the product combo box
        private void LoadProductComboBox()
        {
            List<Product> cbproducts = new List<Product>();
            try
            {
                cmbProductBName.SelectedIndex = -1;

                cbproducts = ProductsDB.GetAll();
                cmbProductBName.DisplayMember = "ProdName";
                cmbProductBName.ValueMember = "ProductId";
                cmbProductBName.DataSource = cbproducts;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        // if product combo box selected item changes updates the suppliers combobox
        private void cmbProductBName_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Products_Suppliers> cbSuppliers = new List<Products_Suppliers>();
            try
            {
                if(cmbProductBName.SelectedIndex != -1)
                {

                    cbSuppliers = Products_SuppliersDB.GetAllProdSupOnProdID((int)cmbProductBName.SelectedValue);
                    cmbSuppliers.DataSource = cbSuppliers;
                    cmbSuppliers.DisplayMember = "SupName";
                    cmbSuppliers.ValueMember = "ProductSupplierId";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());

            }
            
        }

        //button to cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // button to add a product supplier to the package
        private void btnProduct_Click(object sender, EventArgs e)
        {
            if (cmbProductBName.IsPresent())
            {
                if (add)
                {
                    Packages_Products_Suppliers product = new Packages_Products_Suppliers();
                    product.ProductSupplierId = (int)cmbSuppliers.SelectedValue;
                    product.ProdName = cmbProductBName.Text;
                    product.SupName = cmbSuppliers.Text;



                    pack_prod_sup.Add(product);
                    RedisplayList();
                    cmbProductBName.SelectedIndex = -1;
                    cmbSuppliers.SelectedIndex = -1;
                }
                else
                {
                    Packages_Products_Suppliers product = new Packages_Products_Suppliers();
                    product.ProductSupplierId = (int)cmbSuppliers.SelectedValue;
                    product.ProdName = cmbProductBName.Text;
                    product.SupName = cmbSuppliers.Text;
                    product.PackageId = package.PackageId;
                    try
                    {
                        Packages_Products_SuppliersDB.AddPackProdSupp(product);
                        pack_prod_sup = Packages_Products_SuppliersDB.GetList(package.PackageId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());

                    }
                    RedisplayList();
                    cmbProductBName.SelectedIndex = -1;
                }
            }
        }

        // redraws the list pof products
        private void RedisplayList()
        {
            // clears the listbox
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = pack_prod_sup;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;


            dgvProducts.Columns[0].Visible = false;
            dgvProducts.Columns[1].Visible = false;
            //dgvProducts.Columns[2].Visible = false;
            dgvProducts.Columns[2].Width = 140;

            dgvProducts.Columns[3].Width = 140;
        }

        // removes a product supplier from the list
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvProducts.SelectedRows.Count > 0)
            {
                if (add)
                {
                    pack_prod_sup.RemoveAt(this.dgvProducts.SelectedRows[0].Index);
                    RedisplayList();
                }
                 else
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this Product in the Package?", 
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        int index = this.dgvProducts.SelectedRows[0].Index;
                        try
                        {
                            if (!Packages_Products_SuppliersDB.DeletePackProdSup(pack_prod_sup[index]))
                            {
                                MessageBox.Show("That product has been updated or deleted already.", "Database Error");
                            }
                            else
                            {
                                pack_prod_sup = Packages_Products_SuppliersDB.GetList(package.PackageId);

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

        // button the add the data to the DB
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                if (add)
                {
                    package = this.createPackageObjects();
                    try
                    {
                        package.PackageId = PackagesDB.AddPackage(package);
                        

                        foreach (Packages_Products_Suppliers ps in pack_prod_sup)
                        {
                            ps.PackageId = package.PackageId;
                            Packages_Products_SuppliersDB.AddPackProdSupp(ps);
                        }

                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }

                }
                else
                {
                    Package newPackage = new Package();
                    newPackage = this.createPackageObjects();

                    try
                    {
                        if (!PackagesDB.UpdatePackage (package, newPackage))
                        {
                            MessageBox.Show("That Package has been updated or deleted already.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            package = newPackage;
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

        // checks if the data is valid before adding to the DB
        private bool isValid()
        {
            if (txtPkgName.IsPresent() && txtDescription.IsPresent() && txtPrice.IsPresent() && txtPrice.IsDecimal() && txtCommision.IsPresent() && txtCommision.IsDecimal())
            {
                decimal baseprice, commsion;
                baseprice = Convert.ToDecimal(txtPrice.Text);
                commsion = Convert.ToDecimal(txtCommision.Text);
                if (commsion > baseprice)
                {
                    MessageBox.Show(txtPrice.Tag + " must be greater than " + txtCommision.Tag, "Entry Error");
                    txtPrice.Focus();
                    return false;
                }


                if (dtpPkgEndDate.Value < dtpPkgStartDate.Value)
                {
                    MessageBox.Show("End date can not be before start date", "Entry Error");
                    dtpPkgStartDate.Focus();
                    return false;
                }
                return true;
            }
            else
                return false;
        }

        // creates the package objects
        private Package createPackageObjects()
        {
            Package newpackage = new Package();
            if (!add)
            {
                newpackage.PackageId = package.PackageId;
            }
            newpackage.PkgName = txtPkgName.Text;
            newpackage.PkgStartDate = dtpPkgStartDate.Value;
            newpackage.PkgEndDate = dtpPkgEndDate.Value;
            newpackage.PkgDesc = txtDescription.Text;
            newpackage.PkgBasePrice = Convert.ToDecimal(txtPrice.Text);
            newpackage.PkgAgencyCommission = Convert.ToDecimal(txtCommision.Text);
            return newpackage;


        }
    }


    
}
