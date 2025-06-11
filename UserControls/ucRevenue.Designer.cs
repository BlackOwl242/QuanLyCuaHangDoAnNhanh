namespace QuanLyCoffee.UserControls
{
    partial class ucRevenue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRevenue));
            this.pnlContent = new System.Windows.Forms.Panel();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.pnlBot = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.LavenderBlush;
            this.pnlContent.Controls.Add(this.dtpFrom);
            this.pnlContent.Controls.Add(this.dataGridView1);
            this.pnlContent.Controls.Add(this.pnlRight);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(780, 531);
            this.pnlContent.TabIndex = 0;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarFont = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.CalendarMonthBackground = System.Drawing.SystemColors.InactiveBorder;
            this.dtpFrom.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Location = new System.Drawing.Point(17, 34);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(274, 29);
            this.dtpFrom.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 72);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(355, 442);
            this.dataGridView1.TabIndex = 1;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dtpTo);
            this.pnlRight.Controls.Add(this.pnlBot);
            this.pnlRight.Controls.Add(this.pnlTop);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(390, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(390, 531);
            this.pnlRight.TabIndex = 0;
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarFont = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.CalendarMonthBackground = System.Drawing.SystemColors.InactiveBorder;
            this.dtpTo.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Location = new System.Drawing.Point(99, 34);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(274, 29);
            this.dtpTo.TabIndex = 3;
            // 
            // pnlBot
            // 
            this.pnlBot.BackColor = System.Drawing.Color.White;
            this.pnlBot.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBot.Location = new System.Drawing.Point(17, 306);
            this.pnlBot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Size = new System.Drawing.Size(355, 207);
            this.pnlBot.TabIndex = 1;
            this.pnlBot.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.Location = new System.Drawing.Point(17, 72);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(355, 207);
            this.pnlTop.TabIndex = 0;
            this.pnlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(110)))));
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Image = ((System.Drawing.Image)(resources.GetObject("btnView.Image")));
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(338, 34);
            this.btnView.Name = "btnView";
            this.btnView.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnView.Size = new System.Drawing.Size(104, 29);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "Xem";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // ucRevenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.pnlContent);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ucRevenue";
            this.Size = new System.Drawing.Size(780, 531);
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBot;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Button btnView;
    }
}
