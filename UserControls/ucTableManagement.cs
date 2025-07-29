using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using QuanLyCuaHangDoAnNhanh.DTO;
using QuanLyCuaHangDoAnNhanh.DAO;
using System.Globalization;
using QuanLyCuaHangDoAnNhanh.BLL;
using System.IO;
using System.Xml;
using iTextSharp.text.pdf;
using iTextSharp.text;
using RestSharp;
using System.Text.Json.Serialization;
using System.Net;
using System.Text;
using Newtonsoft.Json;

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
            // Khóa không cho người dùng sửa ID
            if (!this.DesignMode)
            {
                LoadTable();
                LoadCategory();
                LoadTableComboBox();
            }
        }

        #region Method
        private TableManagementBLL tableBLL = new TableManagementBLL();
        string employeeName = SessionManager.CurrentAccount.DisplayName;

        void LoadCategory()
        {
            var listCategory = tableBLL.GetCategories();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadFoodListByCategoryID(int id)
        {
            var listFood = tableBLL.GetFoodsByCategory(id);
            cbFoodAndDrinks.DataSource = listFood;
            cbFoodAndDrinks.DisplayMember = "Name";
        }

        void LoadTable()
        {
            flpTable.Controls.Clear();
            var tableList = tableBLL.GetTables();
            foreach (var item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                flpTable.Controls.Add(btn);
                btn.Click += btn_Click;
                btn.Tag = item;
                btn.BackColor = item.Status == "Trống" ? Color.LightGreen : Color.LightCoral;
            }
        }

        void ShowBill(int id)
        {
            // Hiển thị thông tin hóa đơn cho bàn
            lsvBill.Items.Clear();
            var listBillInfo = tableBLL.GetMenuByTable(id);
            float totalPrice = 0;
            foreach (var item in listBillInfo)
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
            var tables = tableBLL.GetTables();
            cbSwitchTable.DataSource = tables;
            cbSwitchTable.DisplayMember = "Name";
        }
        #endregion

        #region Event
        private void ucTableManagement_Load(object sender, EventArgs e)
        {
            btnAdd.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnAdd.Width, btnAdd.Height, 20, 20));
            btnPay.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnPay.Width, btnPay.Height, 15, 15));
            btnSwitchTable.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, btnSwitchTable.Width, btnSwitchTable.Height, 15, 15));
            using (WebClient client = new WebClient())
            {
                var htmlData = client.DownloadData("https://api.vietqr.io/v2/banks");
                var bankRawJson = Encoding.UTF8.GetString(htmlData);
                var listBankData = JsonConvert.DeserializeObject<Bank>(bankRawJson);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            // Lấy đối tượng Table từ Tag của nút
            Table table = (sender as Button).Tag as Table;
            // Kiểm tra nếu table không null
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
            if (table == null)
            {
                MessageBox.Show("Xin vui lòng chọn bàn ăn trước.");
                return;
            }
            int idBill = tableBLL.GetUncheckBillIdByTable(table.ID);
            // Nếu chưa có hóa đơn thì tạo mới
            if (idBill == -1)
            {
                tableBLL.CreateBill(table.ID);
                idBill = tableBLL.GetMaxBillId();
            }
            // Thêm/xoá món ăn vào hóa đơn
            if (cbFoodAndDrinks.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn món ăn trước.");
                return;
            }
            if (!(cbFoodAndDrinks.SelectedItem is FoodAndDrinks))
            {
                MessageBox.Show("Vui lòng chọn món ăn hợp lệ.");
                return;
            }

            int foodID = (cbFoodAndDrinks.SelectedItem as FoodAndDrinks).ID;
            int count = (int)nmFoodCount.Value;

            // Lấy danh sách món hiện tại trên bill
            var billInfoList = tableBLL.GetMenuByTable(table.ID);
            var foodInBill = billInfoList.Find(m => m.FoodName == (cbFoodAndDrinks.SelectedItem as FoodAndDrinks).Name);

            if (count < 0)
            {
                // Nếu món không tồn tại thì không làm gì
                if (foodInBill == null)
                {
                    MessageBox.Show("Không thể xoá món chưa có trong hóa đơn.");
                    return;
                }
                // Nếu số lượng xoá vượt quá số lượng hiện có thì không cho xoá
                if (foodInBill.Count + count < 0)
                {
                    MessageBox.Show("Không thể xoá quá số lượng hiện có.");
                    return;
                }
            }
            else if (count == 0)
            {
                MessageBox.Show("Số lượng phải khác 0.");
                return;
            }

            // Nếu thêm mới món (count > 0) hoặc giảm số lượng hợp lệ (count < 0)
            tableBLL.AddFoodToBill(idBill, foodID, count);

            ShowBill(table.ID);
            LoadTable();
        }


        private void btnPay_Click(object sender, EventArgs e)
        {
            if (lsvBill.Tag == null)
            {
                MessageBox.Show("Không có bàn nào được chọn.");
                return;
            }
            Table table = lsvBill.Tag as Table;
            int idBill = tableBLL.GetUncheckBillIdByTable(table.ID);
            int discount;
            double totalPrice = double.Parse(txtTotalPrice.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("vi-VN"));
            double finalPrice;

            var (image, dataResult) = tableBLL.GenerateQrImage(table, totalPrice);

            if (isDiscountApplied)
            {
                discount = appliedDiscount;
                finalPrice = totalPrice;
            }
            else
            {
                discount = (int)nmDiscount.Value;
                finalPrice = totalPrice - (totalPrice * discount / 100);
                txtTotalPrice.Text = finalPrice.ToString("c", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));
            }

            if (idBill != -1)
            {
                if (MessageBox.Show(
                    string.Format("Bạn có chắc muốn thanh toán & in hóa đơn cho bàn {0}?", table.Name),
                    "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    // Lấy thông tin bàn, tổng tiền, sinh qr
                    double amount = finalPrice; // tổng tiền
                    string description = $"BAN {table.Name} {DateTime.Now:yyyyMMddHHmmss}";


                    // Hiển thị form QR
                    using (var frm = new fPaymentQR(table.Name, amount, image))
                    {
                        // Tạo QR code từ dữ liệu
                        if (frm.ShowDialog() == DialogResult.OK && frm.PaymentConfirmed)
                        {
                            var billInfo = tableBLL.GetMenuByTable(table.ID);
                            // Sau khi xác nhận đã thanh toán, in hóa đơn
                            InvoiceExporter.ExportInvoiceToPdf(table, billInfo, totalPrice, discount, finalPrice, employeeName, image);
                            InvoiceExporter.ExportInvoiceToXml(table, billInfo, totalPrice, discount, finalPrice, employeeName);
                            tableBLL.CheckOut(idBill, discount, totalPrice);
                            ShowBill(table.ID);
                            MessageBox.Show("Thanh toán & in hóa đơn thành công!");
                            LoadTable();
                            isDiscountApplied = false;
                            appliedDiscount = 0;
                        }
                        else
                        {
                            MessageBox.Show("Thanh toán bị hủy hoặc không thành công.");
                        }
                    }
                }
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

            int sourceBillId = tableBLL.GetUncheckBillIdByTable(sourceTable.ID);
            int targetBillId = tableBLL.GetUncheckBillIdByTable(targetTable.ID);

            if (targetTable.Status == "Trống")
            {
                if (sourceBillId == -1)
                {
                    MessageBox.Show("Bàn nguồn không có hóa đơn để chuyển!");
                    return;
                }
                tableBLL.SwitchTable(sourceBillId, targetTable.ID);
                tableBLL.UpdateTableStatus(sourceTable.ID, "Trống");
                tableBLL.UpdateTableStatus(targetTable.ID, "Có người");
                MessageBox.Show($"Chuyển hóa đơn sang bàn {targetTable.Name} thành công!");
            }
            else
            {
                if (sourceBillId == -1 || targetBillId == -1)
                {
                    MessageBox.Show("Cả hai bàn phải có hóa đơn để gộp!");
                    return;
                }
                tableBLL.MergeTable(sourceBillId, targetBillId);
                tableBLL.UpdateTableStatus(sourceTable.ID, "Trống");
                MessageBox.Show($"Gộp hóa đơn bàn {sourceTable.Name} vào bàn {targetTable.Name} thành công!");
            }

            LoadTable();
            LoadTableComboBox();
            ShowBill(targetTable.ID);
            lsvBill.Tag = targetTable;
        }
        #endregion    
    }
}
