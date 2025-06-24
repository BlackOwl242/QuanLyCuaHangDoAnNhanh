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
        }

        #region Event
        private void ucTableManagement_Load(object sender, EventArgs e)
        {
            btnAdd.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnAdd.Width, btnAdd.Height, 30, 30));
            btnPay.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnPay.Width, btnPay.Height, 30, 30));
            btnDiscount.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnDiscount.Width, btnDiscount.Height, 15, 15));
            btnSwitchTable.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnSwitchTable.Width, btnSwitchTable.Height, 15, 15));
        }
        private void btn_Click(object sender, EventArgs e)
        {
            int tableID = (int)(sender as Button).Tag;
            ShowBill(tableID);
        }
        #endregion

        #region Method
        void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                flpTable.Controls.Add(btn);
                btn.Click += btn_Click;
                btn.Tag = item.ID; // Lưu ID của bàn vào Tag của nút

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

            if (listBillInfo == null || listBillInfo.Count == 0)
            {
                MessageBox.Show("Bàn này chưa có món nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());

                lsvBill.Items.Add(lsvItem);
            }
        }
        #endregion
    }
}
