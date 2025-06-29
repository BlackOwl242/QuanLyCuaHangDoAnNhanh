using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace QuanLyCuaHangDoAnNhanh.UserControls
{
    public partial class fPaymentQR: Form
    {
        public bool PaymentConfirmed { get; private set; } = false;
        public fPaymentQR(string tableName, double totalAmount, string qrContent)
        {
            InitializeComponent();

            lblTable.Text = $"Bàn: {tableName}";
            lblTotal.Text = $"Tổng tiền: {totalAmount:N0} VNĐ";

            // Sinh mã QR
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            pictureBoxQR.Image = qrCode.GetGraphic(10);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            PaymentConfirmed = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
