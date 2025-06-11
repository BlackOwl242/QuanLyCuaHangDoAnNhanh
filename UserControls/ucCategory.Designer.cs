namespace QuanLyCoffee.UserControls
{
    partial class ucCategory
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnDgv = new System.Windows.Forms.Panel();
            this.dgvAccount = new System.Windows.Forms.DataGridView();
            this.pnButton = new System.Windows.Forms.Panel();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lbCategoryName = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.pnlContent.SuspendLayout();
            this.pnDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).BeginInit();
            this.pnButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(159)))), ((int)(((byte)(112)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::QuanLyCoffee.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(24, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAdd.Size = new System.Drawing.Size(165, 44);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(81)))), ((int)(((byte)(91)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = global::QuanLyCoffee.Properties.Resources.delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(574, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDelete.Size = new System.Drawing.Size(165, 44);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xoá";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(92)))), ((int)(((byte)(199)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Image = global::QuanLyCoffee.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(299, 19);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnEdit.Size = new System.Drawing.Size(165, 44);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnView
            // 
            this.btnView.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(110)))));
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Image = global::QuanLyCoffee.Properties.Resources.view;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(849, 19);
            this.btnView.Name = "btnView";
            this.btnView.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnView.Size = new System.Drawing.Size(165, 44);
            this.btnView.TabIndex = 3;
            this.btnView.Text = "Xem";
            this.btnView.UseVisualStyleBackColor = false;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnDgv);
            this.pnlContent.Controls.Add(this.pnButton);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1040, 654);
            this.pnlContent.TabIndex = 6;
            // 
            // pnDgv
            // 
            this.pnDgv.BackColor = System.Drawing.Color.LavenderBlush;
            this.pnDgv.Controls.Add(this.txtCategoryName);
            this.pnDgv.Controls.Add(this.dgvAccount);
            this.pnDgv.Controls.Add(this.txtID);
            this.pnDgv.Controls.Add(this.lbID);
            this.pnDgv.Controls.Add(this.lbCategoryName);
            this.pnDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDgv.Location = new System.Drawing.Point(0, 0);
            this.pnDgv.Margin = new System.Windows.Forms.Padding(0);
            this.pnDgv.Name = "pnDgv";
            this.pnDgv.Padding = new System.Windows.Forms.Padding(25, 100, 25, 0);
            this.pnDgv.Size = new System.Drawing.Size(1040, 573);
            this.pnDgv.TabIndex = 7;
            // 
            // dgvAccount
            // 
            this.dgvAccount.BackgroundColor = System.Drawing.Color.White;
            this.dgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccount.Location = new System.Drawing.Point(25, 100);
            this.dgvAccount.Margin = new System.Windows.Forms.Padding(0);
            this.dgvAccount.Name = "dgvAccount";
            this.dgvAccount.Size = new System.Drawing.Size(990, 473);
            this.dgvAccount.TabIndex = 0;
            // 
            // pnButton
            // 
            this.pnButton.BackColor = System.Drawing.Color.LavenderBlush;
            this.pnButton.Controls.Add(this.btnAdd);
            this.pnButton.Controls.Add(this.btnDelete);
            this.pnButton.Controls.Add(this.btnEdit);
            this.pnButton.Controls.Add(this.btnView);
            this.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton.Location = new System.Drawing.Point(0, 573);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(1040, 81);
            this.pnButton.TabIndex = 6;
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.BackColor = System.Drawing.Color.White;
            this.txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtCategoryName.Location = new System.Drawing.Point(150, 50);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(314, 32);
            this.txtCategoryName.TabIndex = 4;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.White;
            this.txtID.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtID.Location = new System.Drawing.Point(150, 12);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(314, 32);
            this.txtID.TabIndex = 3;
            // 
            // lbCategoryName
            // 
            this.lbCategoryName.AutoSize = true;
            this.lbCategoryName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lbCategoryName.Location = new System.Drawing.Point(25, 56);
            this.lbCategoryName.Margin = new System.Windows.Forms.Padding(0);
            this.lbCategoryName.Name = "lbCategoryName";
            this.lbCategoryName.Size = new System.Drawing.Size(114, 21);
            this.lbCategoryName.TabIndex = 1;
            this.lbCategoryName.Text = "Tên danh mục:";
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lbID.Location = new System.Drawing.Point(25, 18);
            this.lbID.Margin = new System.Windows.Forms.Padding(0);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(30, 21);
            this.lbID.TabIndex = 0;
            this.lbID.Text = "ID:";
            // 
            // ucCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.Name = "ucCategory";
            this.Size = new System.Drawing.Size(1040, 654);
            this.Load += new System.EventHandler(this.ucCategory_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnDgv.ResumeLayout(false);
            this.pnDgv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).EndInit();
            this.pnButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnDgv;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.DataGridView dgvAccount;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label lbCategoryName;
        private System.Windows.Forms.Panel pnButton;
    }
}
