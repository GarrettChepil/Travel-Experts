using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// created by Garrett, main menu for program
namespace TravelExperts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnExplorer_Click(object sender, EventArgs e)
        {
            MainForm explorer = new MainForm();
            explorer.Show();

        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            frmSuppliers frmSuppliers = new frmSuppliers();
            frmSuppliers.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            frmProductExplorer frmproducts = new frmProductExplorer();
            frmproducts.Show();
        }

        private void btnPackages_Click(object sender, EventArgs e)
        {
            frmPackages frmPackages = new frmPackages();
            frmPackages.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
