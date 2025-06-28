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
using System.IO;

namespace QuanLyCuaHangDoAnNhanh.UserControls
{
    public partial class ucFoodAndDrinks: UserControl
    {
        // Sử dụng BindingSource để quản lý dữ liệu một cách hiệu quả
        readonly BindingSource foodList = new BindingSource();
        private string _currentImagePath = null;

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
            foodList.CurrentChanged += FoodList_CurrentChanged;
        }
        private void FoodList_CurrentChanged(object sender, EventArgs e)
        {
            // Lấy đường dẫn ảnh từ món ăn đang được chọn
            if (foodList.Current is DataRowView rowView)
            {
                string imagePath = rowView["ImagePath"] != DBNull.Value ? rowView["ImagePath"].ToString() : null;

                // Hiển thị ảnh
                ShowImage(imagePath);
                _currentImagePath = imagePath; // Cập nhật đường dẫn hiện tại
            }
        }

        void ShowImage(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                pictureBox1.Image = null; // Xóa ảnh nếu không có đường dẫn
                return;
            }

            try
            {
                // Ghép đường dẫn tương đối với thư mục chạy của ứng dụng
                string fullPath = Path.Combine(Application.StartupPath, relativePath);
                if (File.Exists(fullPath))
                {
                    // Sử dụng Image.FromFile để không khóa file ảnh
                    pictureBox1.Image = Image.FromFile(fullPath);
                }
                else
                {
                    pictureBox1.Image = null; // Xóa ảnh nếu file không tồn tại
                }
            }
            catch
            {
                pictureBox1.Image = null; // Xóa ảnh nếu có lỗi xảy ra
            }
        }

        // Cấu hình các cột cho DataGridView thủ công.
        // Đảm bảo các cột luôn tồn tại và tránh lỗi NullReferenceException.
        void SetupDataGridView()
        {
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = foodList;
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "Mã", DataPropertyName = "ID" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenMon", HeaderText = "Tên Món Ăn", DataPropertyName = "TenMon", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenDanhMuc", HeaderText = "Danh Mục", DataPropertyName = "TenDanhMuc" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "Gia", HeaderText = "Đơn Giá", DataPropertyName = "Gia" });
        }

        /// <summary>
        /// Tải danh sách món ăn từ CSDL và gán vào BindingSource.
        /// </summary>         

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Tạo thư mục "Images"
                    string imageDir = Path.Combine(Application.StartupPath, "Images");
                    if (!Directory.Exists(imageDir))
                    {
                        Directory.CreateDirectory(imageDir);
                    }

                    // Tạo tên file duy nhất và copy vào thư mục Images
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(openFile.FileName);
                    string destinationPath = Path.Combine(imageDir, fileName);
                    File.Copy(openFile.FileName, destinationPath);

                    // Hiển thị ảnh vừa tải lên
                    pictureBox1.Image = Image.FromFile(destinationPath);

                    // Lưu đường dẫn tương đối để lưu vào CSDL
                    _currentImagePath = Path.Combine("Images", fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                }
            }
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
            // Kiểm tra xem có ảnh được tải lên không
            if (string.IsNullOrWhiteSpace(txtDish.Text))
            {
                MessageBox.Show("Tên món ăn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string name = txtDish.Text;
                int categoryID = (int)cbCategory.SelectedValue;
                float price = (float)numpPrice.Value;

                // Thêm món ăn với đường dẫn ảnh
                if (FoodDAO.Instance.InsertFood(name, categoryID, price, _currentImagePath))
                {
                    MessageBox.Show("Thêm món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFoodList();
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

            if (string.IsNullOrWhiteSpace(txtDish.Text))
            {
                MessageBox.Show("Tên món ăn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem có ảnh mới được tải lên không
            try
            {
                string name = txtDish.Text;
                int categoryID = (int)cbCategory.SelectedValue;
                float price = (float)numpPrice.Value;

                // Cập nhật món ăn với đường dẫn ảnh
                if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price, _currentImagePath))
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
            // Xác nhận xóa món ăn
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


