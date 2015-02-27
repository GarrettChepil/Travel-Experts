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
    public partial class frmSuppliers : Form
    {
        public frmSuppliers()
        {
            InitializeComponent();
        }

        List<SupplierContacts> suppliersContacts;
        SupplierContacts suplierContact;

        //sets up the form pn load and displays the datagridview
        private void frmSuppliers_Load(object sender, EventArgs e)
        {
            try       // tries to get info from the db and refreshes the dgv and loads the combobox
            {
                suppliersContacts = SupplierContactsDB.listSuppliers();
                refreshDGV();
                loadComboBox();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }


        // button for filtering the dgv
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try     // tries to get datat from the db wuith the filter
            {
                suppliersContacts = new List<SupplierContacts>();
                suppliersContacts = SupplierContactsDB.listSuppliersByFilter(((Item)cbFilter.SelectedItem).Value, txtFilter.Text);
                refreshDGV();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        // resets the dgv and redisplays it
        private void refreshDGV()
        {
            dgvSuppliers.DataSource = null;
            dgvSuppliers.AutoGenerateColumns = true;
            dgvSuppliers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSuppliers.MultiSelect = false;
            dgvSuppliers.DataSource = suppliersContacts;
            dgvSuppliers.Columns[0].Visible = false;
            dgvSuppliers.Columns[1].HeaderText = "Company Name";
            dgvSuppliers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSuppliers.Columns[2].HeaderText = "First Name";
            dgvSuppliers.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSuppliers.Columns[3].HeaderText = "Last Name";
            dgvSuppliers.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSuppliers.Columns[4].HeaderText = "Address";
            dgvSuppliers.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSuppliers.Columns[5].HeaderText = "City";
            dgvSuppliers.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSuppliers.Columns[6].HeaderText = "Prov";
            dgvSuppliers.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSuppliers.Columns[7].Visible = false;
            dgvSuppliers.Columns[8].Visible = false;
            dgvSuppliers.Columns[8].HeaderText = "Country";
            dgvSuppliers.Columns[9].HeaderText = "Phone";
            dgvSuppliers.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSuppliers.Columns[10].HeaderText = "Fax";
            dgvSuppliers.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSuppliers.Columns[11].HeaderText = "Email";
            dgvSuppliers.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgvSuppliers.Columns[12].Visible = false;
            dgvSuppliers.Columns[12].HeaderText = "Supplier URL";
            dgvSuppliers.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSuppliers.Columns[13].Visible = false;
            dgvSuppliers.Columns[14].Visible = false;
        }

        // loads the values in the combobox for filtering
        private void loadComboBox()
        {
            cbFilter.Items.Add(new Item("Company Name", "SupConCompany"));
            cbFilter.Items.Add(new Item("First Name", "SupConFirstName"));
            cbFilter.Items.Add(new Item("Last Name", "SupConLastName"));
            cbFilter.Items.Add(new Item("Address", "SupConAddress"));
            cbFilter.Items.Add(new Item("City", "SupConCity"));
            cbFilter.Items.Add(new Item("Province", "SupConProv"));
            cbFilter.Items.Add(new Item("Phone", "SupConBusPhone"));
            cbFilter.Items.Add(new Item("Fax", "SupConFax"));
            cbFilter.Items.Add(new Item("Email", "SupConEmail"));
            cbFilter.Items.Add(new Item("URL", "SupConURL"));


        }


        // class for the combobox items
        private class Item
        {
            public string Name;
            public string Value;
            public Item(string name, string value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }

        // button to bring up the addmodify form to add data to the DB
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // creates the new form and sets the options
            frmAddModifySupplier newaddfrm = new frmAddModifySupplier();
            newaddfrm.add = true;
            DialogResult result = newaddfrm.ShowDialog();

            // if the results are good redisplay the dgv
            if(result == DialogResult.OK)
            {
                try     // tries to get the data from the db and refresh the dgv
                {
                    suplierContact = newaddfrm.supplierContact;
                    suppliersContacts = SupplierContactsDB.listSuppliers();

                    refreshDGV();

                    // select the newly made record
                    int n = -1;
                    foreach (SupplierContacts sc in suppliersContacts)
                    {
                        if (sc.SupplierContactId == suplierContact.SupplierContactId)
                        {
                            n = suppliersContacts.IndexOf(sc);
                            break;
                        }
                    }
                    dgvSuppliers.Rows[n].Selected = true;
                    dgvSuppliers.FirstDisplayedScrollingRowIndex = n;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        // btn to bring up the addmodify form to edit a record
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // checks if there is a record selected
            if(dgvSuppliers.SelectedRows.Count > 0)
            {
                //creates the form and sets the options
                int index = dgvSuppliers.SelectedRows[0].Index;
                frmAddModifySupplier editsupplier = new frmAddModifySupplier();
                editsupplier.add = false;
                editsupplier.supplierContact = suppliersContacts[index];
                DialogResult result = editsupplier.ShowDialog();
                if(result == DialogResult.OK)
                {
                    try      // if the edit for good redisplay the dgv
                    {
                        suplierContact = editsupplier.supplierContact;
                        suppliersContacts = SupplierContactsDB.listSuppliers();

                        refreshDGV();

                        // selects the edited record
                        int n = -1;
                        foreach (SupplierContacts sc in suppliersContacts)
                        {
                            if (sc.SupplierContactId == suplierContact.SupplierContactId)
                            {
                                n = suppliersContacts.IndexOf(sc);
                                break;
                            }
                        }
                        dgvSuppliers.Rows[n].Selected = true;
                        //dgvSuppliers.FirstDisplayedScrollingRowIndex = n;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());

                    }
                }
            }
        }

        // btn to delete a record
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvSuppliers.SelectedRows.Count > 0)
            {
                // sets the object to delete
                int index = dgvSuppliers.SelectedRows[0].Index;
                suplierContact = suppliersContacts[index];

                // asks user if they are sure
                DialogResult result = MessageBox.Show("Delete the supplier " + suplierContact.SupConCompany + "?", "Confirm Delete", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // creates the other table objects
                    Supplier supplier = new Supplier();
                    List<Products_Suppliers> products_suppliers = new List<Products_Suppliers>();
                    try              // tries to delete the table records
                    {
                        // gets the data for the tasble records that need to be deleted
                        supplier = SupplierDB.GetSupplier(suplierContact.SupplierId);
                        products_suppliers = Products_SuppliersDB.GetAllProdSupOnID(suplierContact.SupplierId);

                        // deletes each productsuppliers table record
                        foreach (Products_Suppliers ps in products_suppliers)
                        {
                            Products_SuppliersDB.DeleteProdSup(ps);
                        }

                        // deltes the suppliercontacts record
                        SupplierContactsDB.DeleteSup(suplierContact);

                        // deltes the supplier record
                        SupplierDB.DeleteSup(supplier);

                        // redisplays the dgv
                        suppliersContacts = SupplierContactsDB.listSuppliers();

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
