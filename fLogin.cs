using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCoffee
{
    public partial class fLogin: Form
    {
        private bool isDragging = false; // Biến cờ để kiểm tra xem form có đang được kéo hay không
        private Point lastLocation;    // Lưu trữ vị trí cuối cùng của chuột

        public fLogin()
        {
            InitializeComponent();
        }

        private void pnlDangNhap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Chỉ xử lý khi nhấn chuột trái
            {
                isDragging = true;         // Đặt cờ kéo là true
                lastLocation = e.Location; // Ghi lại vị trí hiện tại của chuột trên form
            }
        }

        private void pnlDangNhap_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging) // Chỉ di chuyển form nếu đang trong trạng thái kéo
            {
                // Cập nhật vị trí của form bằng cách cộng dồn sự thay đổi của chuột
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update(); // Cập nhật hiển thị form
            }
        }

        private void pnlDangNhap_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false; // Đặt cờ kéo là false
        }

        private void pbOnOff_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
