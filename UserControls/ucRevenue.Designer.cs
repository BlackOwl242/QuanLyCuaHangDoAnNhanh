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
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlBot = new System.Windows.Forms.Panel();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.btnView = new System.Windows.Forms.Button();
            this.pnlContent.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.LavenderBlush;
            this.pnlContent.Controls.Add(this.dateFrom);
            this.pnlContent.Controls.Add(this.dataGridView1);
            this.pnlContent.Controls.Add(this.pnlRight);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1040, 654);
            this.pnlContent.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dateTo);
            this.pnlRight.Controls.Add(this.pnlBot);
            this.pnlRight.Controls.Add(this.pnlTop);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(520, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(520, 654);
            this.pnlRight.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(23, 88);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(473, 544);
            this.dataGridView1.TabIndex = 1;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.Location = new System.Drawing.Point(23, 88);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(473, 255);
            this.pnlTop.TabIndex = 0;
            this.pnlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // pnlBot
            // 
            this.pnlBot.BackColor = System.Drawing.Color.White;
            this.pnlBot.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBot.Location = new System.Drawing.Point(23, 377);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Size = new System.Drawing.Size(473, 255);
            this.pnlBot.TabIndex = 1;
            this.pnlBot.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // dateTo
            // 
            this.dateTo.CalendarFont = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTo.CalendarMonthBackground = System.Drawing.SystemColors.InactiveBorder;
            this.dateTo.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.dateTo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTo.Location = new System.Drawing.Point(132, 42);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(364, 29);
            this.dateTo.TabIndex = 3;
            // 
            // dateFrom
            // 
            this.dateFrom.CalendarFont = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFrom.CalendarMonthBackground = System.Drawing.SystemColors.InactiveBorder;
            this.dateFrom.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.dateFrom.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFrom.Location = new System.Drawing.Point(23, 42);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(364, 29);
            this.dateFrom.TabIndex = 4;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(110)))));
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Image = ((System.Drawing.Image)(resources.GetObject("btnView.Image")));
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(450, 42);
            this.btnView.Margin = new System.Windows.Forms.Padding(4);
            this.btnView.Name = "btnView";
            this.btnView.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnView.Size = new System.Drawing.Size(138, 29);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "Xem";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // ucRevenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.pnlContent);
            this.Name = "ucRevenue";
            this.Size = new System.Drawing.Size(1040, 654);
            this.pnlContent.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBot;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.Button btnView;
    }
}
