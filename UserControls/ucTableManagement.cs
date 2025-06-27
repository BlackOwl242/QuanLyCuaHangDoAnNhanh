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
using QuanLyCuaHangDoAnNhanh.DTO;
using QuanLyCuaHangDoAnNhanh.DAO;
using System.Globalization;

namespace QuanLyCuaHangDoAnNhanh.UserControls
{
    public partial class ucTableManagement: UserControl
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

        private bool isDiscountApplied = false;
        private int appliedDiscount = 0;

        public ucTableManagement()
        {
            InitializeComponent();
            LoadTable();
            LoadCategory();
            LoadTableComboBox();
        }

        #region Method
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name"; // Hiển thị tên danh mục
        }

        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFoodAndDrinks.DataSource = listFood;
            cbFoodAndDrinks.DisplayMember = "Name"; // Hiển thị tên món ăn
        }

        void LoadTable()
        {
            flpTable.Controls.Clear(); // Xóa các nút bàn cũ trước khi tải lại
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                flpTable.Controls.Add(btn);
                btn.Click += btn_Click;
                btn.Tag = item; // Lưu cả Table object vào Tag của nút

                if (item.Status == "Trống")
                {
                    btn.BackColor = Color.LightGreen; // Màu xanh lá cho bàn trống
                }
                else
                {
                    btn.BackColor = Color.LightCoral; // Màu đỏ cho bàn đã có người
                }
            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();

            List<DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;

            foreach (DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            txtTotalPrice.Text = totalPrice.ToString("c", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));

        }
        void LoadTableComboBox()
        {
            var tables = TableDAO.Instance.LoadTableList();
            cbSwitchTable.DataSource = tables; // Gán danh sách bàn vào ComboBox
            cbSwitchTable.DisplayMember = "Name"; // Hiển thị tên bàn
        }

        #endregion

        #region Event
        private void ucTableManagement_Load(object sender, EventArgs e)
        {
            btnAdd.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnAdd.Width, btnAdd.Height, 20, 20));
            btnPay.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnPay.Width, btnPay.Height, 15, 15));
            btnDiscount.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnDiscount.Width, btnDiscount.Height, 15, 15));
            btnSwitchTable.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnSwitchTable.Width, btnSwitchTable.Height, 15, 15));
        }

        private void btn_Click(object sender, EventArgs e)
        {
            //int tableID = ((sender as Button).Tag as Table).ID;
            Table table = (sender as Button).Tag as Table;

            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(table.ID);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodListByCategoryID(id);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            //Nếu bàn chưa có hóa đơn thì tạo hóa đơn mới
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), (cbFoodAndDrinks.SelectedItem as Food).ID, (int)nmFoodCount.Value);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, (cbFoodAndDrinks.SelectedItem as Food).ID, (int)nmFoodCount.Value);
            }
            ShowBill(table.ID);
            LoadTable(); // Tải lại danh sách bàn sau khi thêm món
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int discount;
            double totalPrice = double.Parse(txtTotalPrice.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("vi-VN"));
            double finalPrice;

            if (isDiscountApplied)
            {
                discount = appliedDiscount;
                finalPrice = totalPrice; // Đã giảm giá rồi, không giảm nữa
            }
            else
            {
                discount = (int)nmDiscount.Value;
                finalPrice = totalPrice - (totalPrice * discount / 100);
                txtTotalPrice.Text = finalPrice.ToString("c", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));
            }

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán hóa đơn cho bàn {0}?", table.Name), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, totalPrice);
                    ShowBill(table.ID);
                    MessageBox.Show("Thanh toán thành công!");
                    LoadTable();
                    // Reset trạng thái giảm giá sau khi thanh toán
                    isDiscountApplied = false;
                    appliedDiscount = 0;
                }
            }
        }
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            if (idBill != -1)
            {
                int discount = (int)nmDiscount.Value;
                double totalPrice = double.Parse(txtTotalPrice.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("vi-VN"));
                double finalPrice = totalPrice - (totalPrice * discount / 100);
                txtTotalPrice.Text = finalPrice.ToString("c", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));
                MessageBox.Show(string.Format("Giảm giá {0}% cho bàn {1} thành công!", discount, table.Name));
                isDiscountApplied = true;
                appliedDiscount = discount;
            }
            else
            {
                MessageBox.Show("Bàn này chưa có hóa đơn để áp dụng giảm giá!");
            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            Table sourceTable = lsvBill.Tag as Table;
            Table targetTable = cbSwitchTable.SelectedItem as Table;
            if (sourceTable == null || targetTable == null || sourceTable.ID == targetTable.ID)
            {
                MessageBox.Show("Vui lòng chọn bàn hợp lệ để chuyển/gộp!");
                return;
            }

            int sourceBillId = BillDAO.Instance.GetUncheckBillIDByTableID(sourceTable.ID);
            int targetBillId = BillDAO.Instance.GetUncheckBillIDByTableID(targetTable.ID);

            // Nếu bàn đích trống => chuyển bàn
            if (targetTable.Status == "Trống")
            {
                if (sourceBillId == -1)
                {
                    MessageBox.Show("Bàn nguồn không có hóa đơn để chuyển!");
                    return;
                }
                BillDAO.Instance.SwitchTable(sourceBillId, targetTable.ID);
                TableDAO.Instance.UpdateTableStatus(sourceTable.ID, "Trống");
                TableDAO.Instance.UpdateTableStatus(targetTable.ID, "Có người");
                MessageBox.Show($"Chuyển hóa đơn sang bàn {targetTable.Name} thành công!");
            }
            // Nếu bàn đích có người => gộp bàn
            else
            {
                if (sourceBillId == -1 || targetBillId == -1)
                {
                    MessageBox.Show("Cả hai bàn phải có hóa đơn để gộp!");
                    return;
                }
                BillDAO.Instance.MergeTable(sourceBillId, targetBillId);
                TableDAO.Instance.UpdateTableStatus(sourceTable.ID, "Trống");
                MessageBox.Show($"Gộp hóa đơn bàn {sourceTable.Name} vào bàn {targetTable.Name} thành công!");
            }

            LoadTable();
            LoadTableComboBox();
            ShowBill(targetTable.ID);
            lsvBill.Tag = targetTable; // Cập nhật lsvBill.Tag sang bàn đích sau khi chuyển/gộp
        }
        #endregion
    }
}
