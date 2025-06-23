﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using QuanLyCuaHangDoAnNhanh.DAO;

namespace QuanLyCuaHangDoAnNhanh.UserControls
{
    public partial class ucAccountManagement: UserControl
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeft,
            int nTop,
            int nRight,
            int nBottom,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public ucAccountManagement()
        {
            InitializeComponent();
            LoadAccountList();
        }

        private void ucAccountManagement_Load(object sender, EventArgs e)
        {
            btnAdd.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnAdd.Width, btnAdd.Height, 15, 15));
            btnEdit.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnEdit.Width, btnEdit.Height, 15, 15));
            btnDelete.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnDelete.Width, btnDelete.Height, 15, 15));
            btnView.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnView.Width, btnView.Height, 15, 15));
            btnViewAccount.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnViewAccount.Width, btnViewAccount.Height, 15, 15));
        }

        void LoadAccountList()
        {
            string query = "EXEC USP_GetAccountByUserName @userName";

            dgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { "admin" });
        }
    }
}
