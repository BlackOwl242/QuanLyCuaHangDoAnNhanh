using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangDoAnNhanh.DTO;

namespace QuanLyCuaHangDoAnNhanh.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;
        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return instance; }
            private set { instance = value; }
        }

        public static int TableWidth = 115;
        public static int TableHeight = 115;

        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public void UpdateTableStatus(int tableId, string status)
        {
            string query = "UPDATE TableFood SET status = @status WHERE id = @tableId";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { status, tableId });
        }
    }
}
