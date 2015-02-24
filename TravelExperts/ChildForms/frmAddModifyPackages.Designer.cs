namespace TravelExperts
{
    partial class frmAddModifyPackages
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label13 = new System.Windows.Forms.Label();
            this.txtPkgName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpPkgStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpPkgEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCommision = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.lblProducts = new System.Windows.Forms.Label();
            this.cmbProductBName = new System.Windows.Forms.ComboBox();
            this.btnProduct = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSuppliers = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(42, 95);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 13);
            this.label13.TabIndex = 57;
            this.label13.Text = "Package Name";
            // 
            // txtPkgName
            // 
            this.txtPkgName.Location = new System.Drawing.Point(161, 92);
            this.txtPkgName.Name = "txtPkgName";
            this.txtPkgName.Size = new System.Drawing.Size(200, 20);
            this.txtPkgName.TabIndex = 58;
            this.txtPkgName.Tag = "pkgName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Start Date";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpPkgStartDate
            // 
            this.dtpPkgStartDate.Location = new System.Drawing.Point(161, 152);
            this.dtpPkgStartDate.Name = "dtpPkgStartDate";
            this.dtpPkgStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpPkgStartDate.TabIndex = 60;
            this.dtpPkgStartDate.Tag = "PkgStartDate";
            // 
            // dtpPkgEndDate
            // 
            this.dtpPkgEndDate.Location = new System.Drawing.Point(552, 154);
            this.dtpPkgEndDate.Name = "dtpPkgEndDate";
            this.dtpPkgEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpPkgEndDate.TabIndex = 62;
            this.dtpPkgEndDate.Tag = "PkgEndDate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(436, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "End Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(161, 226);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(591, 20);
            this.txtDescription.TabIndex = 64;
            this.txtDescription.Tag = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Description";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(161, 292);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(200, 20);
            this.txtPrice.TabIndex = 66;
            this.txtPrice.Tag = "Base Price";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "Base Price";
            // 
            // txtCommision
            // 
            this.txtCommision.Location = new System.Drawing.Point(552, 292);
            this.txtCommision.Name = "txtCommision";
            this.txtCommision.Size = new System.Drawing.Size(200, 20);
            this.txtCommision.TabIndex = 68;
            this.txtCommision.Tag = "Agency Commission";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(426, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 67;
            this.label5.Text = "Agency Commission";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(215, 483);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(112, 38);
            this.btnDelete.TabIndex = 73;
            this.btnDelete.Text = "Delete Product";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvProducts
            // 
            this.dgvProducts.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(346, 371);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.Size = new System.Drawing.Size(280, 150);
            this.dgvProducts.TabIndex = 72;
            // 
            // lblProducts
            // 
            this.lblProducts.AutoSize = true;
            this.lblProducts.Location = new System.Drawing.Point(67, 374);
            this.lblProducts.Name = "lblProducts";
            this.lblProducts.Size = new System.Drawing.Size(87, 13);
            this.lblProducts.TabIndex = 71;
            this.lblProducts.Text = "Products Offered";
            // 
            // cmbProductBName
            // 
            this.cmbProductBName.FormattingEnabled = true;
            this.cmbProductBName.Location = new System.Drawing.Point(161, 371);
            this.cmbProductBName.Name = "cmbProductBName";
            this.cmbProductBName.Size = new System.Drawing.Size(166, 21);
            this.cmbProductBName.TabIndex = 69;
            this.cmbProductBName.Tag = "Products Offered";
            this.cmbProductBName.SelectedIndexChanged += new System.EventHandler(this.cmbProductBName_SelectedIndexChanged);
            // 
            // btnProduct
            // 
            this.btnProduct.Location = new System.Drawing.Point(70, 483);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(112, 38);
            this.btnProduct.TabIndex = 70;
            this.btnProduct.Text = "Add Product";
            this.btnProduct.UseVisualStyleBackColor = true;
            this.btnProduct.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(104, 429);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 75;
            this.label6.Text = "Suppliers";
            // 
            // cmbSuppliers
            // 
            this.cmbSuppliers.FormattingEnabled = true;
            this.cmbSuppliers.Location = new System.Drawing.Point(161, 429);
            this.cmbSuppliers.Name = "cmbSuppliers";
            this.cmbSuppliers.Size = new System.Drawing.Size(166, 21);
            this.cmbSuppliers.TabIndex = 74;
            this.cmbSuppliers.Tag = "Products Offered";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(643, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 38);
            this.btnCancel.TabIndex = 76;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(643, 483);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(112, 38);
            this.btnAccept.TabIndex = 77;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(300, 35);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 24);
            this.lblTitle.TabIndex = 78;
            this.lblTitle.Text = "Add Package Information";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(343, 355);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 13);
            this.label14.TabIndex = 79;
            this.label14.Text = "List of Products";
            // 
            // frmAddModifyPackages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(856, 567);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbSuppliers);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.lblProducts);
            this.Controls.Add(this.cmbProductBName);
            this.Controls.Add(this.btnProduct);
            this.Controls.Add(this.txtCommision);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpPkgEndDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpPkgStartDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPkgName);
            this.Controls.Add(this.label13);
            this.Name = "frmAddModifyPackages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddModifyPackages";
            this.Load += new System.EventHandler(this.frmAddModifyPackages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPkgName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpPkgStartDate;
        private System.Windows.Forms.DateTimePicker dtpPkgEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCommision;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Label lblProducts;
        private System.Windows.Forms.ComboBox cmbProductBName;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSuppliers;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label14;
    }
}