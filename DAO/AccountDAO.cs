using QuanLyCuaHangDoAnNhanh.DAO;
using System;
using System.Collections.Generic;
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

        
        // Thêm một tài khoản mới. Mật khẩu mặc định là "123".
        public bool InsertAccount(string userName, string displayName, int type)
        {
            
            string query = "INSERT dbo.Account ( UserName, DisplayName, PassWord, Type ) VALUES ( @userName , @displayName , @password , @type )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, displayName, "123", type });
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
    }
}