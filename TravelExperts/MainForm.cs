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
    // Team 4 - Mehmet Demirci

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public ExplorerForm frmExplorer = new ExplorerForm();
                
        public List<Package> packageList;
        public List<Product> productList;
        public List<Supplier> supplierList;
        public List<SupplierContacts> suppliersContactList;

        private void mainForm_Load(object sender, EventArgs e)
        {
            packageList = PackagesDB.GetAll();
            productList = ProductsDB.GetAll();            
            supplierList = SupplierDB.ListSupplier();
            suppliersContactList = SupplierContactsDB.listSuppliers();
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            frmExplorer.MdiParent = this;
            frmExplorer.Dock = DockStyle.Left;
            frmExplorer.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void DisplayMessage(string msg)
        {
            stsLabel.Text = msg;
            msgTimer.Start();
        }
        
        private void msgTimer_Tick(object sender, EventArgs e)
        {
            stsLabel.Text = "Ready.";
            msgTimer.Stop();
        }
    }
}
