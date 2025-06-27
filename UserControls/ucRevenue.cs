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
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyCuaHangDoAnNhanh.UserControls
{
    public partial class ucRevenue: UserControl
    {
        public ucRevenue()
        {
            InitializeComponent();
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpCheckIn.Value, dtpCheckOut.Value);
        }

        #region Method
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dgvRevenue.DataSource = BillDAO.Instance.GetListBillByDate(checkIn, checkOut);
            LoadRevenueChart();
        }
        void LoadRevenueChart()
        {
            // Xóa dữ liệu cũ
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();

            // Tạo ChartArea và Series
            ChartArea chartArea = new ChartArea("RevenueArea");
            chart2.ChartAreas.Add(chartArea);

            Series series = new Series("Doanh thu");
            series.ChartType = SeriesChartType.Column; // hoặc Line

            // Lấy dữ liệu từ DataGridView
            var revenueByDate = new Dictionary<DateTime, double>();
            foreach (DataGridViewRow row in dgvRevenue.Rows)
            {
                if (row.Cells["Ngày vào"].Value != null && row.Cells["Ngày ra"].Value != null)
                {
                    DateTime date = Convert.ToDateTime(row.Cells["Ngày ra"].Value);
                    double total = Convert.ToDouble(row.Cells["Tổng tiền"].Value);

                    if (revenueByDate.ContainsKey(date.Date))
                        revenueByDate[date.Date] += total;
                    else
                        revenueByDate[date.Date] = total;
                }
            }

            // Thêm dữ liệu vào Series
            foreach (var item in revenueByDate.OrderBy(x => x.Key))
            {
                series.Points.AddXY(item.Key.ToString("dd/MM/yyyy"), item.Value);
            }

            chart2.Series.Add(series);
        }
        #endregion

        #region Event
        private void btnView_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpCheckIn.Value, dtpCheckOut.Value);
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpCheckIn.Value = new DateTime(today.Year, today.Month, 1);
            dtpCheckOut.Value = dtpCheckIn.Value.AddMonths(1).AddDays(-1); 
        }
        #endregion
    }
}
