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

    public partial class ExplorerForm : Form
    {
        public ExplorerForm()
        {
            InitializeComponent();
        }

        private MainForm frmMain = null;        

        private void ExplorerForm_Load(object sender, EventArgs e)
        {
            frmMain = (MainForm)this.MdiParent;            
        }

        private void ExplorerForm_Shown(object sender, EventArgs e)
        {
            PopulateTree();
            uxTreeView.SelectedNode.Name = "rootPackages";
        }

        private void PopulateTree()
        {
            PopulateTreeNode("rootPackages");
            PopulateTreeNode("rootProducts");
            PopulateTreeNode("rootSuppliers");
        }

        public void PopulateTreeNode(string NodeName)
        {
            switch (NodeName)
            {
                case "rootPackages":
                    uxTreeView.Nodes["rootPackages"].Nodes.Clear();
                    foreach (Package item in frmMain.packageList)
                    {
                        uxTreeView.Nodes["rootPackages"].Nodes.Add(item.PkgName, item.PkgName);
                    }
                    break;
                case "rootProducts":
                    uxTreeView.Nodes["rootProducts"].Nodes.Clear();
                    foreach (Product item in frmMain.productList)
                    {
                        uxTreeView.Nodes["rootProducts"].Nodes.Add(item.ProdName, item.ProdName);
                    }
                    break;
                case "rootSuppliers":
                    uxTreeView.Nodes["rootSuppliers"].Nodes.Clear();
                    foreach (Supplier item in frmMain.supplierList)
                    {
                        uxTreeView.Nodes["rootSuppliers"].Nodes.Add(item.SupName, item.SupName);
                    }
                    break;
            }
        }

        private void uxTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (uxTreeView.SelectedNode.Level == 0)
            {
                addToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                PopulateViewer(uxTreeView.SelectedNode.Name);
            }
            else
            {
                addToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                PopulateViewer(uxTreeView.SelectedNode.Name, uxTreeView.SelectedNode.Parent.Name);
            }
        }

        private void PopulateViewer(string NodeName)
        {
            uxListView.Columns.Clear();
            uxListView.Items.Clear();

            switch (NodeName)
            {
                case "rootPackages":
                    uxListView.Columns.Add("ID", 40);
                    uxListView.Columns.Add("Package Name", 150);
                    uxListView.Columns.Add("Start Date", 80);
                    uxListView.Columns.Add("End Date", 80);
                    uxListView.Columns.Add("Description", 200);
                    uxListView.Columns.Add("Base Price", 75);
                    uxListView.Columns.Add("Agency Commission", 75);
                    foreach (Package item in frmMain.packageList)
                    {
                        uxListView.Items.Add(new ListViewItem(new string[] { item.PackageId.ToString(), item.PkgName, 
                                                                             item.PkgStartDate.ToString(), item.PkgEndDate.ToString(), item.PkgDesc, 
                                                                             item.PkgBasePrice.ToString(), item.PkgAgencyCommission.ToString() }));
                    }
                    break;
                case "rootProducts":
                    uxListView.Columns.Add("ID", 40);
                    uxListView.Columns.Add("Product Name", 150);
                    foreach (Product item in frmMain.productList)
                    {
                        uxListView.Items.Add( new ListViewItem(new string[] { item.ProductId.ToString(), item.ProdName }));
                    }
                    break;
                case "rootSuppliers":
                    uxListView.Columns.Add("ID", 40);
                    uxListView.Columns.Add("Supplier Name", 200);
                    foreach (Supplier item in frmMain.supplierList)
                    {
                        uxListView.Items.Add(new ListViewItem(new string[] { item.SupplierId.ToString(), item.SupName }));
                    }
                    break;
                default:
                    ;
                    break;
            }
        }

        private void PopulateViewer(string NodeName, string ParentNodeName)
        {
            uxListView.Columns.Clear();
            uxListView.Items.Clear();

            switch (ParentNodeName)
            {
                case "rootPackages":
                    uxListView.Columns.Add("Product Name", 150);
                    uxListView.Columns.Add("Supplier Name", 150);
                    List<Products_Suppliers> package_products = Products_SuppliersDB.GetProductsByPackageName(NodeName);
                    foreach (Products_Suppliers item in package_products)
                    {
                        uxListView.Items.Add(new ListViewItem(new string[] { item.productName, item.SupName }));
                    }
                    break;
                case "rootProducts":
                    uxListView.Columns.Add("Product Name", 75);
                    uxListView.Columns.Add("Supplier Name", 150);
                    List<Products_Suppliers> product_suppliers = Products_SuppliersDB.GetSuppliersByProductName(NodeName);
                    foreach (Products_Suppliers item in product_suppliers)
                    {
                        uxListView.Items.Add(new ListViewItem(new string[] { item.productName, item.SupName }));
                    }
                    break;
                case "rootSuppliers":
                    uxListView.Columns.Add("Supplier Name", 250);
                    uxListView.Columns.Add("Product Name", 150);
                    List<Products_Suppliers> supplier_products = Products_SuppliersDB.GetProductsBySupplierName(NodeName);
                    foreach (Products_Suppliers item in supplier_products)
                    {
                        uxListView.Items.Add(new ListViewItem(new string[] { item.SupName, item.productName }));
                    }
                    break;
                default:
                    ;
                    break;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (uxTreeView.SelectedNode.Name)
            {
                case "rootPackages":    // open AddEditPackage form in NewPackage mode
                    frmAddModifyPackages frmNewPackage = new frmAddModifyPackages();
                    frmNewPackage.add = true;
                    if (frmNewPackage.ShowDialog() == DialogResult.OK)
                    {
                        RefreshPackages();  // Refresh the Supplier List and the TreeView Node
                        frmMain.DisplayMessage("A new package has been added successfully.");
                    }
                    break;
                case "rootProducts":    // open AddEditProduct form in NewProduct mode
                    frmProductAddEdit frmNewProduct = new frmProductAddEdit();
                    frmNewProduct.xNewProduct = true;
                    if (frmNewProduct.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProducts();  // Refresh the Product List and the TreeView Node
                        frmMain.DisplayMessage("A new product has been added successfully.");
                    }
                    break;
                case "rootSuppliers":   // open AddEditSupplier form in NewSupplier mode
                    frmAddModifySupplier frmNewSupplier = new frmAddModifySupplier();
                    frmNewSupplier.add = true;
                    if (frmNewSupplier.ShowDialog() == DialogResult.OK)
                    {
                        RefreshSuppliers();  // Refresh the Supplier List and the TreeView Node
                        frmMain.DisplayMessage("A new supplier has been added successfully.");
                    }
                    break;
                default:
                    break;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (uxTreeView.SelectedNode.Parent.Name)
            {
                case "rootPackages":    // open AddEditPackage form with related Package object in Edit mode
                    Package pkg = frmMain.packageList[frmMain.packageList.FindIndex(pk => pk.PkgName == uxTreeView.SelectedNode.Name)];
                    frmAddModifyPackages frmEditPackage = new frmAddModifyPackages();
                    frmEditPackage.add = false;
                    frmEditPackage.package = pkg;
                    if (frmEditPackage.ShowDialog() == DialogResult.OK)
                    {
                        RefreshPackages();  // Refresh the Package List and the TreeView Node
                        frmMain.DisplayMessage("The package has been modified successfully.");
                    }
                    break;
                case "rootProducts":    // open AddEditProduct form with related Product object in Edit mode
                    Product prd = frmMain.productList[frmMain.productList.FindIndex(pr => pr.ProdName == uxTreeView.SelectedNode.Name)];
                    frmProductAddEdit frmEditProduct = new frmProductAddEdit();
                    frmEditProduct.xNewProduct = false;
                    frmEditProduct.txbProductID.Text = prd.ProductId.ToString();
                    frmEditProduct.txbProductName.Text = prd.ProdName;
                    if (frmEditProduct.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProducts();  // Refresh the Product List and the TreeView Node
                        frmMain.DisplayMessage("The product has been modified successfully.");
                    }
                    break;
                case "rootSuppliers":   // open AddEditSupplier form with related Supplier object in Edit mode
                    Supplier sp = frmMain.supplierList[frmMain.supplierList.FindIndex(s => s.SupName == uxTreeView.SelectedNode.Name)];
                    frmAddModifySupplier frmEditSupplier = new frmAddModifySupplier();
                    frmEditSupplier.add = false;
                    frmEditSupplier.supplierContact = SupplierContactsDB.GetSupplierbySupID(sp.SupplierId);
                    if (frmEditSupplier.ShowDialog() == DialogResult.OK)
                    {
                        RefreshSuppliers();  // Refresh the Supplier List and the TreeView Node
                        frmMain.DisplayMessage("The supplier information have been modified successfully.");
                    }
                    break;
                default:
                    break;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (uxTreeView.SelectedNode.Parent.Name)
            {
                case "rootPackages":
                    Package pkg = frmMain.packageList[frmMain.packageList.FindIndex(pk => pk.PkgName == uxTreeView.SelectedNode.Name)];
                    DeletePackage(pkg);
                    break;
                case "rootProducts":
                    Product prd = frmMain.productList[frmMain.productList.FindIndex(pr => pr.ProdName == uxTreeView.SelectedNode.Name)];
                    DeleteProduct(prd);
                    break;
                case "rootSuppliers":
                    Supplier sp = frmMain.supplierList[frmMain.supplierList.FindIndex(s => s.SupName == uxTreeView.SelectedNode.Name)];
                    DeleteSupplier(sp);
                    break;
                default:
                    break;
            }
        }

        private void DeletePackage(Package pkg)
        {
            DialogResult result = MessageBox.Show("Delete the Package " + pkg.PkgName + "?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // creates the other table objects
                List<Packages_Products_Suppliers> newpack_prod_sup = new List<Packages_Products_Suppliers>();
                try              // tries to delete the table records
                {
                    // gets the data for the tasble records that need to be deleted
                    newpack_prod_sup = Packages_Products_SuppliersDB.GetList(pkg.PackageId);
                    // deletes each productsuppliers table record
                    foreach (Packages_Products_Suppliers ps in newpack_prod_sup)
                    {
                        Packages_Products_SuppliersDB.DeletePackProdSup(ps);
                    }
                    // deletes the suppliercontacts record
                    PackagesDB.DeletePackage(pkg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
                RefreshPackages();
            }
        }

        private void DeleteProduct(Product prd)
        {
            DialogResult result = MessageBox.Show("Delete Product: " + prd.ProdName + "?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!ProductsDB.DeleteProduct(prd))
                    {
                        MessageBox.Show("Another user has updated or deleted " +
                            "that product.", "Database Error");
                    }
                    else
                    {
                        RefreshProducts();
                        frmMain.DisplayMessage("Product has been deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void DeleteSupplier(Supplier sp)
        {
            SupplierContacts suplierContact = SupplierContactsDB.GetSupplierbySupID(sp.SupplierId);

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

                    // deletes the suppliercontacts record
                    SupplierContactsDB.DeleteSup(suplierContact);

                    // deletes the supplier record
                    SupplierDB.DeleteSup(supplier);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
                RefreshSuppliers();
            }
        }

        private void RefreshPackages()
        {
            frmMain.packageList = null;
            frmMain.packageList = PackagesDB.GetAll();
            PopulateTreeNode("rootPackages");
            PopulateViewer("rootPackages");
        }
        
        private void RefreshProducts()
        {
            frmMain.productList = null;
            frmMain.productList = ProductsDB.GetAll();
            PopulateTreeNode("rootProducts");
            PopulateViewer("rootProducts");
        }

        private void RefreshSuppliers()
        {
            frmMain.supplierList = null;
            frmMain.supplierList = SupplierDB.ListSupplier();
            PopulateTreeNode("rootSuppliers");
            PopulateViewer("rootSuppliers");
        }
    }
}
