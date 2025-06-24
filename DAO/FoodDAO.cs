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

        public bool InsertFood(string name, int idCategory, float price)
        {
            // Sử dụng tham số @ để tránh lỗi SQL Injection
            string query = "INSERT dbo.Food (name, idCategory, price) VALUES ( @name , @idCategory , @price )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, idCategory, price });
            return result > 0;
        }
        public bool UpdateFood(int idFood, string name, int idCategory, float price)
        {
            string query = "UPDATE dbo.Food SET name = @name , idCategory = @idCategory , price = @price WHERE id = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, idCategory, price, idFood });
            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {

            /* Trước khi xóa một món ăn thì cần phải xóa các bản ghi liên quan trong bảng BillInfo
            để tránh lỗi khóa ngoại. Cần tạo một phương thức tương tự trong BillInfoDAO.
            // BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood); 
            */
            string query = "DELETE Food WHERE id = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idFood });
            return result > 0;
        }

        public DataTable SearchFoodByName(string name)
        {
            // Sử dụng hàm dbo.fuConvertToUnsign1 để tìm kiếm không phân biệt chữ hoa chữ thường và dấu
            string query = "SELECT id AS ID, name AS TenMon, idCategory AS IDDanhMuc, price AS Gia FROM dbo.Food WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1( @name ) + '%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { name });
            return data;
        }
    }
}
