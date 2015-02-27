using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// created by Garrett
namespace TravelExperts
{
    public partial class frmPackages : Form
    {
        public frmPackages()
        {
            InitializeComponent();
        }

        List<Package> packages = new List<Package>();
        Package package = new Package();

        // loads the form
        private void frmPackages_Load(object sender, EventArgs e)
        {
            try
            {
                packages = PackagesDB.GetAll();
                refreshDGV();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        // to reshesh the DGV
        private void refreshDGV()
        {
            dgvPackages.DataSource = null;
            dgvPackages.AutoGenerateColumns = true;
            dgvPackages.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPackages.MultiSelect = false;
            dgvPackages.DataSource = packages;
            dgvPackages.Columns[0].Visible = false;
            dgvPackages.Columns[1].HeaderText = "Package Name";
            dgvPackages.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


        }

        // button to bring uo the add form
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // creates the new form and sets the options
            frmAddModifyPackages newaddfrm = new frmAddModifyPackages();
            newaddfrm.add = true;
            DialogResult result = newaddfrm.ShowDialog();

            // if the results are good redisplay the dgv
             if(result == DialogResult.OK)
            {
                try     // tries to get the data from the db and refresh the dgv
                {
                    package = newaddfrm.package;
                    packages = PackagesDB.GetAll();

                    refreshDGV();

                    // select the newly made record
                    int n = -1;
                    foreach (Package pack in packages)
                    {
                        if(pack.PackageId == package.PackageId)
                        {
                            n = packages.IndexOf(pack);
                            break;
                        }
                    }
                    dgvPackages.Rows[n].Selected = true;
                    dgvPackages.FirstDisplayedScrollingRowIndex = n;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        // button to edit a package
        private void btnEdit_Click(object sender, EventArgs e)
        {
             //creates the form and sets the options
            int index = dgvPackages.SelectedRows[0].Index;
            frmAddModifyPackages editfrm = new frmAddModifyPackages();
            editfrm.add = false;
            editfrm.package = packages[index];
            DialogResult result = editfrm.ShowDialog();
            if (result == DialogResult.OK)
            {
                try      // if the edit for good redisplay the dgv
                {
                    package = editfrm.package;
                    packages = PackagesDB.GetAll();

                    refreshDGV();

                    // selects the edited record
                    int n = -1;
                    foreach (Package sc in packages)
                    {
                        if (sc.PackageId == package.PackageId)
                        {
                            n = packages.IndexOf(sc);
                            break;
                        }
                    }
                    dgvPackages.Rows[n].Selected = true;
                    dgvPackages.FirstDisplayedScrollingRowIndex = n;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());

                }
            }
        }

        // button to close the form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // button to delete a package from the DB
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvPackages.SelectedRows.Count > 0)
            {
                // sets the object to delete
                int index = dgvPackages.SelectedRows[0].Index;
                package = packages[index];

                // asks user if they are sure
                DialogResult result = MessageBox.Show("Delete the Package " + package.PkgName + "?", "Confirm Delete", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // creates the other table objects
                    List<Packages_Products_Suppliers> newpack_prod_sup = new List<Packages_Products_Suppliers>();
                    try              // tries to delete the table records
                    {
                        // gets the data for the tasble records that need to be deleted
                        newpack_prod_sup = Packages_Products_SuppliersDB.GetList(package.PackageId);
                        // deletes each productsuppliers table record
                        foreach (Packages_Products_Suppliers ps in newpack_prod_sup)
                        {
                            Packages_Products_SuppliersDB.DeletePackProdSup(ps);
                        }

                        // deltes the suppliercontacts record
                        PackagesDB.DeletePackage(package);

                        // deltes the supplier record

                        // redisplays the dgv
                        packages = PackagesDB.GetAll();

                        refreshDGV();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
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
