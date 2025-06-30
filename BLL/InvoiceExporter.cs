using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QuanLyCuaHangDoAnNhanh.DTO;
using System.Windows.Forms;
using QRCoder;
using System.Text;

namespace QuanLyCuaHangDoAnNhanh.BLL
{
    class InvoiceExporter
    {
        public static string GenerateVietQRContent(string bankBin, string accountNumber, string accountName, double amount, string description)
        {
            // Xử lý tên chủ tài khoản
            string normalizedAccountName = RemoveDiacritics(accountName).ToUpper(); // Loại bỏ dấu tiếng Việt khỏi tên chủ tài khoản, chuyển sang chữ in hoa.
            if (normalizedAccountName.Length > 50) // Tên có thể dài hơn
                normalizedAccountName = normalizedAccountName.Substring(0, 50); // Nếu tên dài hơn 50 ký tự thì cắt bớt còn 50 ký tự (theo chuẩn VietQR).

            // Làm tròn số tiền về số nguyên (VietQR chỉ nhận số nguyên).
            int amountInt = (int)Math.Round(amount);

            // Nội dung chuyển khoản
            string normalizedDesc = RemoveDiacritics(description); // Loại bỏ dấu tiếng Việt khỏi nội dung chuyển khoản.
            if (normalizedDesc.Length > 50)
                normalizedDesc = normalizedDesc.Substring(0, 50); // Cắt bớt nếu dài hơn 50 ký tự (theo chuẩn VietQR).

            // Merchant Account Information 
            string bankInfo = $"00{bankBin.Length:D2}{bankBin}" +
                              $"01{accountNumber.Length:D2}{accountNumber}"; // Mã BIN ngân hàng + Số tài khoản
            string consumerToBusiness = $"01{bankInfo.Length:D2}{bankInfo}"; // consumerToBusiness: Gói thông tin tài khoản.
            string merchantAccountInfo = $"0010A000000727" + consumerToBusiness; // Mã định danh thương mại (A000000727) + consumerToBusiness

            // Additional Data 
            string purposeField = $"08{normalizedDesc.Length:D2}{normalizedDesc}"; // Mục đích chuyển khoản
            string additionalData = $"62{purposeField.Length:D2}{purposeField}"; // Trường 62: Thông tin bổ sung


            // Ghép chuỗi payload để tính CRC (chưa có trường 63)
            string payload =
                "000201" + // Version
                "010212" + // Init Method: Dynamic QR
                "38" + merchantAccountInfo.Length.ToString("D2") + merchantAccountInfo +
                "5303704" + // Currency: VND
                "54" + amountInt.ToString().Length.ToString("D2") + amountInt + // Amount
                "5802VN" + // Country: VN
                "59" + normalizedAccountName.Length.ToString("D2") + normalizedAccountName + // Merchant Name
                additionalData + // Additional Data
                "6304"; // CRC Field ID and Length

            // Tính CRC trên chuỗi payload 
            string crc = CalculateCRC(payload);

            // Trả về chuỗi cuối cùng
            return payload + crc;
        }

        // Hàm tính CRC-CCITT (0xFFFF)
        // CRC-CCITT là một thuật toán kiểm tra lỗi thường được sử dụng trong các giao thức truyền thông và mã hóa dữ liệu. Trong trường hợp này, chúng ta sẽ sử dụng nó để tính toán mã CRC cho nội dung của mã QR theo chuẩn VietQR.
        private static string CalculateCRC(string input)
        {
            ushort crc = 0xFFFF; // Giá trị khởi tạo CRC-CCITT
            byte[] bytes = Encoding.ASCII.GetBytes(input); // Chuyển đổi chuỗi sang mảng byte
            // Tính toán CRC
            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8);
                // Dịch trái 8 bit
                for (int i = 0; i < 8; i++) 
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc <<= 1;
                }
            }
            return crc.ToString("X4"); // Trả về CRC dưới dạng chuỗi hex 4 ký tự
        }

        private static string RemoveDiacritics(string text)
        {
            string normalized = text.Normalize(System.Text.NormalizationForm.FormD); // Chuyển đổi sang dạng chuẩn để loại bỏ dấu
            var sb = new StringBuilder(); 
            foreach (var c in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark) // Kiểm tra xem ký tự có phải là dấu hay không
                    sb.Append(c); // Nếu không phải là dấu thì thêm vào StringBuilder
            }
            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC); // Chuyển đổi lại về dạng chuẩn C
        }

        private string GetInvoiceDirectory()
        {
            // Gốc chương trình
            string root = Application.StartupPath;
            // Lấy ngày hiện tại
            DateTime now = DateTime.Now;
            // Tạo đường dẫn: Invoices\yyyy\MM\dd
            string invoiceDir = Path.Combine(root, "Invoices", now.Year.ToString(), now.Month.ToString("D2"), now.Day.ToString("D2"));
            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(invoiceDir))
            {
                Directory.CreateDirectory(invoiceDir);
            }
            return invoiceDir;
        }

        public static void ExportInvoiceToXml(Table table, List<DTO.Menu> billInfo, double total, double discount, double finalTotal)
        {
            InvoiceExporter exporter = new InvoiceExporter();
            string invoiceDir = exporter.GetInvoiceDirectory();
            string fileName = $"HoaDon_{table.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.xml";
            string filePath = Path.Combine(invoiceDir, fileName);

            using (XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("HoaDon");

                writer.WriteElementString("Ban", table.Name);
                writer.WriteElementString("Ngay", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                writer.WriteStartElement("DanhSachMon");

                foreach (var item in billInfo)
                {
                    writer.WriteStartElement("Mon");
                    writer.WriteElementString("TenMon", item.FoodName);
                    writer.WriteElementString("SoLuong", item.Count.ToString());
                    writer.WriteElementString("DonGia", item.Price.ToString());
                    writer.WriteElementString("ThanhTien", item.TotalPrice.ToString());
                    writer.WriteEndElement(); // Mon
                }

                writer.WriteEndElement(); // DanhSachMon

                writer.WriteElementString("TongCong", total.ToString());
                writer.WriteElementString("GiamGia", discount.ToString());
                writer.WriteElementString("ThanhTien", finalTotal.ToString());

                writer.WriteEndElement(); // HoaDon
                writer.WriteEndDocument();
            }
        }

        public static void ExportInvoiceToPdf(Table table, List<DTO.Menu> billInfo, double total, double discount, double finalTotal)
        {
            InvoiceExporter exporter = new InvoiceExporter();
            string invoiceDir = exporter.GetInvoiceDirectory();
            string fileName = $"HoaDon_{table.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string filePath = Path.Combine(invoiceDir, fileName);

            string fontPath = Path.Combine(Application.StartupPath, "Fonts", "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var fontTitle = new Font(bf, 16, Font.BOLD);
            var fontNormal = new Font(bf, 12, Font.NORMAL);

            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                Paragraph title = new Paragraph("HÓA ĐƠN THANH TOÁN", fontTitle);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(new Paragraph($"Bàn: {table.Name}", fontNormal));
                doc.Add(new Paragraph($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", fontNormal));
                doc.Add(new Paragraph(" ", fontNormal));

                PdfPTable tablePdf = new PdfPTable(4);
                tablePdf.WidthPercentage = 100;
                tablePdf.SetWidths(new float[] { 4, 2, 2, 2 });

                tablePdf.AddCell(new PdfPCell(new Phrase("Tên món", fontNormal)));
                tablePdf.AddCell(new PdfPCell(new Phrase("Số lượng", fontNormal)));
                tablePdf.AddCell(new PdfPCell(new Phrase("Đơn giá", fontNormal)));
                tablePdf.AddCell(new PdfPCell(new Phrase("Thành tiền", fontNormal)));

                foreach (var item in billInfo)
                {
                    tablePdf.AddCell(new PdfPCell(new Phrase(item.FoodName, fontNormal)));
                    tablePdf.AddCell(new PdfPCell(new Phrase(item.Count.ToString(), fontNormal)));
                    tablePdf.AddCell(new PdfPCell(new Phrase(item.Price.ToString("N0"), fontNormal)));
                    tablePdf.AddCell(new PdfPCell(new Phrase(item.TotalPrice.ToString("N0"), fontNormal)));
                }

                doc.Add(tablePdf);

                doc.Add(new Paragraph(" ", fontNormal));
                doc.Add(new Paragraph($"Tổng cộng: {total:N0} VNĐ", fontNormal));
                doc.Add(new Paragraph($"Giảm giá: {discount}%", fontNormal));
                doc.Add(new Paragraph($"Thành tiền: {finalTotal:N0} VNĐ", fontNormal));

                // --- QR PAYMENT --- Chuẩn VietQR
                string bankBin = "970432"; // Mã BIN ngân hàng (VPBANK)
                string bankAccount = "263696255"; // Số tài khoản nhận
                string accountName = "LE QUOC HUY"; // Tên chủ tài khoản (không dấu, viết hoa)
                double amount = finalTotal; // Số tiền cần thanh toán
                string description = $"BAN {table.Name} {DateTime.Now:yyyyMMddHHmmss}"; // Nội dung chuyển khoản

                string qrContent = GenerateVietQRContent(bankBin, bankAccount, accountName, amount, description);

                // Sinh mã QR
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q); // Sử dụng mức ECC Q để đảm bảo mã QR có thể đọc được ngay cả khi bị hỏng một phần
                QRCode qrCode = new QRCode(qrCodeData); // Tạo mã QR từ dữ liệu đã tạo
                using (var qrBitmap = qrCode.GetGraphic(10)) // Tạo hình ảnh mã QR với kích thước 10x10 pixel cho mỗi module
                using (var ms = new MemoryStream()) // Lưu mã QR vào MemoryStream để thêm vào PDF
                {
                    qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Lưu mã QR vào MemoryStream dưới dạng PNG
                    iTextSharp.text.Image qrImage = iTextSharp.text.Image.GetInstance(ms.ToArray()); // Tạo đối tượng Image từ mảng byte của mã QR
                    qrImage.Alignment = Element.ALIGN_CENTER; 
                    qrImage.ScaleAbsolute(120, 120); // Kích thước QR 
                    doc.Add(new Paragraph("Quét mã QR để thanh toán:", fontNormal)); 
                    doc.Add(qrImage);
                }

                doc.Close();
            }

            MessageBox.Show($"Hóa đơn PDF đã được lưu vào:\n{filePath}", "Thông báo");
        }
    }
}

