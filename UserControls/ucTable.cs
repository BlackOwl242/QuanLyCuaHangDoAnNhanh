using QuanLyCuaHangDoAnNhanh.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDoAnNhanh.UserControls
{
    public partial class ucTable: UserControl
    {
        BindingSource tableList = new BindingSource();
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
        public ucTable()
        {
            InitializeComponent();
            LoadTable();
            dgvTable.DataSource = tableList;
            AddTableBinding();
        }
        void LoadTable()
        {
            tableList.DataSource = TableDAO.Instance.LoadTableList();
        }

        private void ucTable_Load(object sender, EventArgs e)
        {
            btnAdd.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnAdd.Width, btnAdd.Height, 15, 15));
            btnEdit.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnEdit.Width, btnEdit.Height, 15, 15));
            btnDelete.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnDelete.Width, btnDelete.Height, 15, 15));
            btnView.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnView.Width, btnView.Height, 15, 15));
        }
        void AddTableBinding()
        {
            // Xóa binding cũ để tránh lỗi (nếu có)
            txtID.DataBindings.Clear();
            txtTableName.DataBindings.Clear();
            cbStatus.DataBindings.Clear();

            txtID.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTableName.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            cbStatus.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));
            txtID.ReadOnly = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm bàn thành công");
                LoadTable();
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bại");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            string name = txtTableName.Text;
            string status = cbStatus.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên bàn không được để trống!");
                return;
            }

            if (TableDAO.Instance.UpdateTable(id, name, status))
            {
                MessageBox.Show("Sửa bàn thành công");
                LoadTable();
            }

            else
            {
                MessageBox.Show("Sửa bàn thất bại");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa bàn thành công");
                LoadTable();
            }
            else
            {
                MessageBox.Show("Xóa bàn thất bại");
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadTable();
        }
    }
}
