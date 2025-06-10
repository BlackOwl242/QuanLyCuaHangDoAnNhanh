using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace QuanLyCoffee
{
    public partial class fMain: Form
    {
        // Import hàm AnimateWindow từ thư viện user32.dll
        [DllImport("user32.dll")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, uint flags);

        // Các cờ (flags) để điều khiển kiểu animation
        private const uint AW_ACTIVATE = 0x20000; // Kích hoạt cửa sổ
        private const uint AW_BLEND = 0x80000;    // Hiệu ứng mờ dần (Fade)
        private const uint AW_CENTER = 0x0010;    // Bung ra/Thu vào từ giữa
        private const uint AW_HIDE = 0x10000;     // Ẩn cửa sổ

        // Khai báo biến toàn cục trong MainForm
        bool isMenuExpanded = true; // Ban đầu menu đang mở rộng
        private bool isDragging = false;
        private Point lastLocation;

        public fMain()
        {
            InitializeComponent();
            // Tạm thời ẩn Form đi để chuẩn bị cho animation
            this.Opacity = 0.0;
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            // Gọi AnimateWindow để làm Form hiện ra mờ dần trong 300 mili-giây
            AnimateWindow(this.Handle, 300, AW_BLEND | AW_ACTIVATE);
        }

        private void pbToggleMenu_Click(object sender, EventArgs e)
        {
            timerMenu.Start();
        }

        private void timerMenu_Tick(object sender, EventArgs e)
        {
            if (isMenuExpanded)
            {
                pnlTopMenu.Width -= 15;
                pnlMenu.Width -= 15;
                if (pnlMenu.Width <= 80 && pnlTopMenu.Width <= 80)
                {
                    isMenuExpanded = false;
                    timerMenu.Stop(); 
                }
            }
            else
            {
                pnlTopMenu.Width += 15;
                pnlMenu.Width += 15;
                if (pnlMenu.Width >= 240 && pnlTopMenu.Width <= 240)
                {
                    isMenuExpanded = true;
                    timerMenu.Stop(); 
                }
            }
        }

        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            // Nếu người dùng nhấn chuột trái
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;      // Bắt đầu kéo
                lastLocation = e.Location; // Lưu lại vị trí ban đầu của con trỏ chuột
            }
        }

        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            // Chỉ thực hiện khi đang trong trạng thái kéo
            if (isDragging)
            {
                // Tính toán vị trí mới của Form
                this.Left += e.X - lastLocation.X;
                this.Top += e.Y - lastLocation.Y;
            }
        }

        private void pnlTop_MouseUp(object sender, MouseEventArgs e)
        {
            // Khi người dùng nhả chuột, kết thúc việc kéo
            isDragging = false;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            // Gọi AnimateWindow để làm Form mờ đi trong 300 mili-giây
            AnimateWindow(this.Handle, 300, AW_BLEND | AW_HIDE);

            this.Close();
        }
    }
}
