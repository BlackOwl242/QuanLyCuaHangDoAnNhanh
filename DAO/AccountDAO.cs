using QuanLyCuaHangDoAnNhanh.DAO;
using QuanLyCuaHangDoAnNhanh.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangDoAnNhanh.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        // Lấy thông tin tài khoản bằng UserName
        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM account WHERE userName = @userName", new object[] { userName });

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        // Thêm một tài khoản mới. Mật khẩu mặc định là "123".
        public bool InsertAccount(string userName, string displayName, int type)
        {
            string query = "INSERT dbo.Account ( UserName, DisplayName, PassWord, Type ) VALUES ( @userName , @displayName , '123' , @type )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, displayName, type });
            return result > 0;
        }
        
        /// Cập nhật thông tin tài khoản.
        public bool UpdateAccount(string userName, string displayName, int type)
        {
            string query = "UPDATE dbo.Account SET DisplayName = @displayName , Type = @type WHERE UserName = @userName";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { displayName, type, userName });
            return result > 0;
        }


        /// Xóa một tài khoản.
        public bool DeleteAccount(string userName)
        {
            string query = "DELETE dbo.Account WHERE UserName = @userName";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName });
            return result > 0;
        }

        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, passWord });

            return result.Rows.Count>0; // Trả về true nếu có ít nhất một tài khoản khớp với thông tin đăng nhập
        }
    }
}
