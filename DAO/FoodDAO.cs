using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangDoAnNhanh.DTO;

namespace QuanLyCuaHangDoAnNhanh.DAO
{
    internal class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();

            string query = "select * from Food where idCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

        public DataTable GetListFood()
        {
            // Lấy danh sách món ăn từ cơ sở dữ liệu
            string query = "SELECT id AS ID, name AS TenMon, idCategory AS IDDanhMuc, price AS Gia FROM dbo.Food";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
