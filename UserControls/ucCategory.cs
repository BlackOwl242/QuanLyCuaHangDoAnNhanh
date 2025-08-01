﻿using QuanLyCuaHangDoAnNhanh.BLL;
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
        }

        #region Method
        private bool isAddNewMode = false;
        private bool isEditMode = false;
        private CategoryBLL categoryBLL = new CategoryBLL();

        void LoadCategory()
        {
            // Tải dữ liệu vào BindingSource, DataGridView sẽ tự động cập nhật
            categoryList.DataSource = categoryBLL.GetListCategory();

            // Đặt lại tên cho các cột
            if (dgvCategory.Columns["ID"] != null)
                dgvCategory.Columns["ID"].HeaderText = "Mã Danh Mục";
            if (dgvCategory.Columns["Name"] != null)
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
            txtID.ReadOnly = true; // Khóa không cho người dùng sửa ID
        }
        #endregion

        #region Event
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

            dgvCategory.DataSource = categoryList;
            LoadCategory();
            AddCategoryBinding();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!isAddNewMode)
            {
                // Chuyển sang chế độ thêm mới
                isAddNewMode = true;
                txtID.Text = "";
                txtID.Enabled = false; // Không cho sửa ID khi thêm mới
                txtCategoryName.Text = "";
                txtCategoryName.ReadOnly = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnAdd.Text = "Lưu";
                return;
            }

            // Đang ở chế độ thêm mới, thực hiện lưu
            string name = txtCategoryName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên danh mục không được để trống!");
                return;
            }

            if (categoryBLL.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công!");
                LoadCategory();
                AddCategoryBinding();
                isAddNewMode = false;
                btnAdd.Text = "Thêm";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                MessageBox.Show("Thêm danh mục thất bại!");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Vui lòng chọn danh mục để sửa!");
                    return;
                }
                isEditMode = true;
                txtCategoryName.ReadOnly = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Text = "Lưu";
                return;
            }

            // Đang ở chế độ sửa, thực hiện lưu
            int id = Convert.ToInt32(txtID.Text);
            string name = txtCategoryName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên danh mục không được để trống!");
                return;
            }

            if (categoryBLL.UpdateCategory(id, name))
            {
                MessageBox.Show("Sửa danh mục thành công!");
                LoadCategory();
                AddCategoryBinding();
                isEditMode = false;
                btnEdit.Text = "Sửa";
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                txtCategoryName.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Sửa danh mục thất bại!");
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
            if (FoodAndDrinksDAO.Instance.GetFoodByCategoryID(id).Count > 0)
            {
                MessageBox.Show("Không thể xóa danh mục này vì vẫn còn món ăn thuộc danh mục.", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa danh mục này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (categoryBLL.DeleteCategory(id))
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

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isAddNewMode)
            {
                isAddNewMode = false;
                AddCategoryBinding();
                btnAdd.Text = "Thêm";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtCategoryName.ReadOnly = true;
            }
            if (isEditMode)
            {
                isEditMode = false;
                AddCategoryBinding();
                btnEdit.Text = "Sửa";
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                txtCategoryName.ReadOnly = true;
            }
        }
        #endregion
    }
}
