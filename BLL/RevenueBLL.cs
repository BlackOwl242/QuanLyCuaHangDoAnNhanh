using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QuanLyCuaHangDoAnNhanh.DAO;

namespace QuanLyCuaHangDoAnNhanh.BLL
{
    public class RevenueBLL
    {
        public DataTable GetBillsByDate(DateTime checkIn, DateTime checkOut)
        {
            return BillDAO.Instance.GetListBillByDate(checkIn, checkOut);
        }


        public Dictionary<DateTime, double> GetRevenueByDate(DataTable billTable)
        {
            var revenueByDate = new Dictionary<DateTime, double>();
            foreach (DataRow row in billTable.Rows)
            {
                if (row["Ngày ra"] != DBNull.Value && row["Tổng tiền"] != DBNull.Value)
                {
                    DateTime date = Convert.ToDateTime(row["Ngày ra"]);
                    double total = Convert.ToDouble(row["Tổng tiền"]);
                    if (revenueByDate.ContainsKey(date.Date))
                        revenueByDate[date.Date] += total;
                    else
                        revenueByDate[date.Date] = total;
                }
            }
            return revenueByDate;
        }
    }
}
