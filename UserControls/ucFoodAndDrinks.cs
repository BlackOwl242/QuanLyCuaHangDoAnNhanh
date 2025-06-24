using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangDoAnNhanh.DAO;

namespace QuanLyCuaHangDoAnNhanh.UserControls
{
    public partial class ucFoodAndDrinks: UserControl
    {
        // Sử dụng BindingSource để quản lý dữ liệu một cách hiệu quả
        readonly BindingSource foodList = new BindingSource();

        public ucFoodAndDrinks()
        {
            InitializeComponent();
            // Gán sự kiện Load để đảm bảo các control đã được khởi tạo
            this.Load += ucFoodAndDrinks_Load;
        }

        private void ucFoodAndDrinks_Load(object sender, EventArgs e)
        {
            // Thiết lập DataGridView và gán DataSource
            SetupDataGridView();

            // Tải dữ liệu từ CSDL
            LoadFoodList();

            // Thiết lập liên kết dữ liệu cho các TextBox
            AddFoodBinding();
        }

        // Cấu hình các cột cho DataGridView một cách thủ công.
        // Điều này đảm bảo các cột luôn tồn tại và tránh lỗi NullReferenceException.
        void SetupDataGridView()
        {
            // Quan trọng: Tắt tính năng tự động tạo cột
            dataGridView2.AutoGenerateColumns = false;

            // Gán foodList làm nguồn dữ liệu
            dataGridView2.DataSource = foodList;

            // Xóa các cột cũ (nếu có) trước khi thêm cột mới
            dataGridView2.Columns.Clear();

            // Thêm các cột theo cách thủ công và chỉ định DataPropertyName
            // DataPropertyName phải khớp chính xác với tên cột trong DataTable trả về từ FoodDAO
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "Mã", DataPropertyName = "ID" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenMon", HeaderText = "Tên Món Ăn", DataPropertyName = "TenMon", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDDanhMuc", HeaderText = "Mã Danh Mục", DataPropertyName = "ID Danh mục" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "Gia", HeaderText = "Đơn Giá", DataPropertyName = "Giá" });
        }

        /// <summary>
        /// Tải danh sách món ăn từ CSDL và gán vào BindingSource.
        /// </summary>

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadFoodList();
        }
        void LoadFoodList()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void AddFoodBinding()
        {
            // Xóa các binding cũ để tránh lỗi
            txtID.DataBindings.Clear();
            txtDish.DataBindings.Clear();
            txtCategory.DataBindings.Clear();
            txtPrice.DataBindings.Clear();

            // Thêm các binding mới, liên kết trực tiếp với BindingSource
            txtID.DataBindings.Add(new Binding("Text", foodList, "ID", true, DataSourceUpdateMode.Never));
            txtDish.DataBindings.Add(new Binding("Text", foodList, "TenMon", true, DataSourceUpdateMode.Never));
            txtCategory.DataBindings.Add(new Binding("Text", foodList, "IDDanhMuc", true, DataSourceUpdateMode.Never));
            txtPrice.DataBindings.Add(new Binding("Text", foodList, "Gia", true, DataSourceUpdateMode.Never));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường nhập liệu
                string name = txtDish.Text;
                int categoryID = Convert.ToInt32(txtCategory.Text);
                float price = (float)Convert.ToDouble(txtPrice.Text);

                if (FoodDAO.Instance.InsertFood(name, categoryID, price))
                {
                    MessageBox.Show("Thêm món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFoodList(); // Tải lại danh sách để cập nhật DataGridView
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm món ăn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Kiểm tra các trường nhập liệu
                int id = Convert.ToInt32(txtID.Text);
                string name = txtDish.Text;
                int categoryID = Convert.ToInt32(txtCategory.Text);
                float price = (float)Convert.ToDouble(txtPrice.Text);

                if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
                {
                    MessageBox.Show("Cập nhật món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFoodList();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi cập nhật món ăn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int id = Convert.ToInt32(txtID.Text);

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa món này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (FoodDAO.Instance.DeleteFood(id))
                    {
                        MessageBox.Show("Xóa món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadFoodList();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi xóa món ăn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            foodList.DataSource = FoodDAO.Instance.SearchFoodByName(txtSearch.Text);
        }
    }
}


