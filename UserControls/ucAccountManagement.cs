using System;
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
            // Thêm binding để liên kết dữ liệu từ DataGridView với các TextBox
            AddAccountBinding();
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

            // Thiết lập các thuộc tính cho ComboBox
            cbType.Items.Add("Admin");
            cbType.Items.Add("Nhân viên");
            cbType.SelectedIndex = 1; // Mặc định chọn "Nhân viên"

            // Gán sự kiện Click cho các nút
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnView.Click += btnView_Click;
        }

        void LoadAccountList()
        {
            string query = "SELECT UserName AS [Tên tài khoản], DisplayName AS [Tên hiển thị], CASE Type WHEN 1 THEN 'Admin' ELSE 'Nhân viên' END AS [Loại tài khoản] FROM dbo.Account";
            dgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void AddAccountBinding()
        {
            // Xóa các binding cũ (nếu có)
            txtUserName.DataBindings.Clear();
            txtDisplayName.DataBindings.Clear();
            cbType.DataBindings.Clear();

            // Thêm binding mới
            txtUserName.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "Tên tài khoản", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "Tên hiển thị", true, DataSourceUpdateMode.Never));
            cbType.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "Loại tài khoản", true, DataSourceUpdateMode.Never));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserName.Text;
                string displayName = txtDisplayName.Text;
                // Kiểm tra loại tài khoản
                int type = (cbType.SelectedItem.ToString() == "Admin") ? 1 : 0;

                if (string.IsNullOrWhiteSpace(userName))
                {
                    MessageBox.Show("Tên tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
                {
                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccountList();
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại! Tên tài khoản có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có tài khoản nào được chọn không
                string userName = txtUserName.Text;
                string displayName = txtDisplayName.Text;
                int type = (cbType.SelectedItem.ToString() == "Admin") ? 1 : 0;

                if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccountList();
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserName.Text;

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (AccountDAO.Instance.DeleteAccount(userName))
                    {
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAccountList();
                    }
                    else
                    {
                        MessageBox.Show("Xóa tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadAccountList();
        }
    }
}
