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

        public ucTableManagement()
        {
            InitializeComponent();
            LoadTable();
            LoadCategory();
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
            List<Food> listFood = FoodDAO.Instance.GetListFood(id);
            cbFoodAndDrinks.DataSource = listFood;
            cbFoodAndDrinks.DisplayMember = "Name"; // Hiển thị tên món ăn
        }

        void LoadTable()
        {
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
        }
        #endregion
    }
}
