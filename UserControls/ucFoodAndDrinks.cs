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
using QuanLyCuaHangDoAnNhanh.DTO;

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

            LoadCategoryIntoCombobox(cbCategory);
        }

        // Cấu hình các cột cho DataGridView một cách thủ công.
        // Điều này đảm bảo các cột luôn tồn tại và tránh lỗi NullReferenceException.
        void SetupDataGridView()
        {
            // Tắt tính năng tự động tạo cột
            dataGridView2.AutoGenerateColumns = false;

            // Gán foodList làm nguồn dữ liệu
            dataGridView2.DataSource = foodList;

            // Xóa các cột cũ (nếu có) trước khi thêm cột mới
            dataGridView2.Columns.Clear();

            // Thêm các cột theo cách thủ công và chỉ định DataPropertyName
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "Mã",
                DataPropertyName = "ID"
            });

            // Thêm cột tên món ăn
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenMon",
                HeaderText = "Tên Món Ăn",
                DataPropertyName = "TenMon",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Thêm cột danh mục
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDanhMuc",                   
                HeaderText = "Danh Mục",              
                DataPropertyName = "TenDanhMuc"      
            });

            // Thêm cột Đơn Giá
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Gia",
                HeaderText = "Đơn Giá",
                DataPropertyName = "Gia"
            });
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
            cbCategory.DataBindings.Clear();
            numpPrice.DataBindings.Clear();

            // Thêm các binding mới, liên kết trực tiếp với BindingSource
            txtID.DataBindings.Add(new Binding("Text", foodList, "ID", true, DataSourceUpdateMode.Never));
            txtDish.DataBindings.Add(new Binding("Text", foodList, "TenMon", true, DataSourceUpdateMode.Never));
            cbCategory.DataBindings.Add(new Binding("SelectedValue", foodList, "IDDanhMuc", true, DataSourceUpdateMode.Never));
            numpPrice.DataBindings.Add(new Binding("Value", foodList, "Gia", true, DataSourceUpdateMode.Never));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem tên món ăn có được nhập hay không
            if (string.IsNullOrWhiteSpace(txtDish.Text))
            {
                MessageBox.Show("Tên món ăn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiểm tra các trường nhập liệu
                string name = txtDish.Text;
                int categoryID = (int)cbCategory.SelectedValue;
                float price = (float)numpPrice.Value;

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
            // Kiểm tra xem ID có hợp lệ không
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một món ăn để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tên món ăn không được để trống
            if (string.IsNullOrWhiteSpace(txtDish.Text))
            {
                MessageBox.Show("Tên món ăn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiểm tra các trường nhập liệu
                string name = txtDish.Text;
                int categoryID = (int)cbCategory.SelectedValue; 
                float price = (float)numpPrice.Value;


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
            // Kiểm tra xem ID có hợp lệ không
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một món ăn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
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

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {

        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name"; // Hiển thị tên danh mục
            cb.ValueMember = "ID";   // Nhưng giá trị thực sự là ID
        }
    }
}


