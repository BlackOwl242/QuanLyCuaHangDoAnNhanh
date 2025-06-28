using QuanLyCuaHangDoAnNhanh.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDoAnNhanh.UserControls
{
    public partial class ucCategory : UserControl
    {
        BindingSource categoryList = new BindingSource();
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

        public ucCategory()
        {
            InitializeComponent();
            txtID.ReadOnly = true; // Khóa không cho người dùng sửa ID
            LoadCategory();

        }

        private void ucCategory_Load(object sender, EventArgs e)
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
        void LoadAndBindData()
        {
            // Gán BindingSource cho DataGridView
            dgvCategory.DataSource = categoryList;
            LoadCategory();
            AddCategoryBinding();
        }
        void LoadCategory()
        {
            // Tải dữ liệu và gán vào BindingSource. DataGridView sẽ tự động cập nhật.
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();

            // Đặt lại tên cho các cột để hiển thị thân thiện hơn
            dgvCategory.Columns["ID"].HeaderText = "Mã Danh Mục";
            dgvCategory.Columns["Name"].HeaderText = "Tên Danh Mục";
        }
        void AddCategoryBinding()
        {
            // Xóa các liên kết cũ trước khi thêm mới
            txtID.DataBindings.Clear();
            txtCategoryName.DataBindings.Clear();

            // Liên kết các TextBox với BindingSource
            txtID.DataBindings.Add(new Binding("Text", categoryList, "ID", true, DataSourceUpdateMode.Never));
            txtCategoryName.DataBindings.Add(new Binding("Text", categoryList, "Name", true, DataSourceUpdateMode.Never));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên danh mục không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công!");
                LoadCategory(); // Sửa tên phương thức gọi
            }
            else
            {
                MessageBox.Show("Thêm danh mục thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("Mã danh mục không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtCategoryName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên danh mục không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Cập nhật danh mục thành công!");
                LoadCategory(); // Tải lại danh sách sau khi sửa
            }
            else
            {
                MessageBox.Show("Cập nhật danh mục thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một danh mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra ràng buộc khóa ngoại trước khi xóa
            if (FoodDAO.Instance.GetFoodByCategoryID(id).Count > 0)
            {
                MessageBox.Show("Không thể xóa danh mục này vì vẫn còn món ăn thuộc danh mục.", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa danh mục này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (CategoryDAO.Instance.DeleteCategory(id))
                    {
                        MessageBox.Show("Xóa danh mục thành công!");
                        LoadCategory(); // Tải lại danh sách sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Xóa danh mục thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi xóa danh mục: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

    }
}
