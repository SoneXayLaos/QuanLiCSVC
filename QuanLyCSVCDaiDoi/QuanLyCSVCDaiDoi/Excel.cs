using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyCSVCDaiDoi
{
    class Excel1
    {

        #region In Đơn Thuốc
        public void ExportDTToCOMExcelDonThuoc(DataTable dt, string maDon, string maKH, string tenKH, string gt, string ngayKham, string hoTenNV, int thanhTien)
        {
            // Khởi động chương trình COMExcel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình COMExcel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["B1"].Value = "Phòng Khám Nhi Khoa";
            exRange.Range["B1"].Font.Size = 12;
            exRange.Range["B1"].Font.Bold = true;
            exRange.Range["B1"].Font.ColorIndex = 5; //Màu xanh da trời            
            exRange.Range["B1"].MergeCells = true;
            exRange.Range["B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B2"].Value = "236 Hoàng Quốc Việt - Hà Nội";
            exRange.Range["B2"].Font.Size = 12;
            exRange.Range["B2"].Font.Bold = true;
            exRange.Range["B2"].Font.ColorIndex = 5; //Màu xanh da trời            
            exRange.Range["B2"].MergeCells = true;
            exRange.Range["B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B3"].Value = "Điện thoại: (+84)904841221";
            exRange.Range["B3"].Font.Size = 12;
            exRange.Range["B3"].Font.Bold = true;
            exRange.Range["B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["B3"].MergeCells = true;
            exRange.Range["B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D5"].Value = "BIÊN LAI BÁN THUỐC";
            exRange.Range["D5"].Font.Size = 16;
            exRange.Range["D5"].Font.Bold = true;
            exRange.Range["D5"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["D5"].ColumnWidth = 60;
            exRange.Range["D5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

            // Biểu diễn thông tin chung của hóa đơn bán


            exRange.Range["B7"].Value = "Mã đơn thuốc:";
            exRange.Range["B8"].Value = maDon;
            exRange.Range["B7"].ColumnWidth = 20;
            exRange.Range["B7"].MergeCells = true;

            exRange.Range["B9"].Value = "Mã khách hàng:";
            exRange.Range["B10"].Value = maKH;
            exRange.Range["B9"].ColumnWidth = 20;
            exRange.Range["B9"].MergeCells = true;


            exRange.Range["B11"].Value = "Tên Khách Hàng:";
            exRange.Range["B12"].Value = tenKH;
            exRange.Range["B11"].ColumnWidth = 20;
            exRange.Range["B11"].MergeCells = true;

            exRange.Range["E9"].Value = "Giới Tính:";
            exRange.Range["E10"].Value = gt;
            exRange.Range["E9"].MergeCells = true;


            exRange.Range["E11"].Value = "Ngày Khám:";
            exRange.Range["E12"].Value = ngayKham;
            exRange.Range["E11"].MergeCells = true;

            //Lấy thông tin các mặt hàng

            //Tạo dòng tiêu đề bảng
            exRange.Range["B14"].Value = "STT";
            exRange.Range["B14"].Font.Bold = true;
            exRange.Range["B14"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B14"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["B14"].ColumnWidth = 12;

            exRange.Range["C14"].Value = "Tên thuốc";
            exRange.Range["C14"].Font.Bold = true;
            exRange.Range["C14"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C14"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["C14"].ColumnWidth = 25;

            exRange.Range["D14"].Value = "Số lượng";
            exRange.Range["D14"].Font.Bold = true;
            exRange.Range["D14"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D14"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["D14"].ColumnWidth = 15;

            exRange.Range["E14"].Value = "Đơn giá";
            exRange.Range["E14"].Font.Bold = true;
            exRange.Range["E14"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E14"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["E14"].ColumnWidth = 15;

            exRange.Range["F14"].Value = "Thành tiền";
            exRange.Range["F14"].Font.Bold = true;
            exRange.Range["F14"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["F14"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["F14"].ColumnWidth = 15;

            int i = 15;
            foreach (DataRow row in dt.Rows)
            {
                exRange.Range["B" + i.ToString()].Value = row["STT"];
                exRange.Range["B" + i.ToString()].Font.Bold = true;
                exRange.Range["B" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C" + i.ToString()].Value = row["TenThuoc"];
                exRange.Range["C" + i.ToString()].Font.Bold = true;
                exRange.Range["C" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["D" + i.ToString()].Value = row["SoLuongBan"];
                exRange.Range["D" + i.ToString()].Font.Bold = true;
                exRange.Range["D" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["E" + i.ToString()].Value = row["GiaThuoc"];
                exRange.Range["E" + i.ToString()].Font.Bold = true;
                exRange.Range["E" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["F" + i.ToString()].Value = row["Tiền Thuốc"];
                exRange.Range["F" + i.ToString()].Font.Bold = true;
                i++;
            }



            exRange.Range["E24"].Value = "Tổng tiền";
            exRange.Range["E24"].Font.Bold = true;
            exRange.Range["E24"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E24"].ColumnWidth = 15;
            exRange.Range["F24"].Value = thanhTien;
            exRange.Range["F24"].Font.Bold = true;
            exRange.Range["F24"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            ////exRange.Value2 = tongtien;              

            exRange.Range["E27"].MergeCells = true;
            exRange.Range["E27"].Font.Italic = true;
            exRange.Range["E27"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = DateTime.Now;
            exRange.Range["E27"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["E29"].MergeCells = true;
            exRange.Range["E29"].Font.Bold = true;
            exRange.Range["E29"].Font.Italic = true;
            exRange.Range["E29"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E29"].Value = "Người lập phiếu";
            exRange.Range["E31"].MergeCells = true;
            exRange.Range["E31"].Font.Italic = true;
            exRange.Range["E31"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E31"].Value = hoTenNV;
            exSheet.Name = "Hóa Đơn";

            exApp.Visible = true;

        }
        #endregion

        #region In Danh sách xét nghiệm
        public void ExportDTToCOMExcelDSXetNghiem(DataTable dt, string maPK, string maKH, string tenKH, string gt, string ngayKham, string hoTenNV, int thanhTien)
        {
            // Khởi động chương trình COMExcel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình COMExcel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["B1"].Value = "Phòng Khám Nhi Khoa";
            exRange.Range["B1"].Font.Size = 12;
            exRange.Range["B1"].Font.Bold = true;
            exRange.Range["B1"].Font.ColorIndex = 5; //Màu xanh da trời            
            exRange.Range["B1"].MergeCells = true;
            exRange.Range["B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B2"].Value = "236 Hoàng Quốc Việt - Hà Nội";
            exRange.Range["B2"].Font.Size = 12;
            exRange.Range["B2"].Font.Bold = true;
            exRange.Range["B2"].Font.ColorIndex = 5; //Màu xanh da trời            
            exRange.Range["B2"].MergeCells = true;
            exRange.Range["B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B3"].Value = "Điện thoại: (+84)904841221";
            exRange.Range["B3"].Font.Size = 12;
            exRange.Range["B3"].Font.Bold = true;
            exRange.Range["B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["B3"].MergeCells = true;
            exRange.Range["B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C5"].Value = "DANH SÁCH XÉT NGHIỆM";
            exRange.Range["C5"].Font.Size = 16;
            exRange.Range["C5"].Font.Bold = true;
            exRange.Range["C5"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C5"].ColumnWidth = 60;
            exRange.Range["C5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

            // Biểu diễn thông tin chung của hóa đơn bán


            exRange.Range["B7"].Value = "Mã Phiếu Khám:";
            exRange.Range["B8"].Value = maPK;
            exRange.Range["B7"].ColumnWidth = 20;
            exRange.Range["B7"].MergeCells = true;

            exRange.Range["B9"].Value = "Mã khách hàng:";
            exRange.Range["B10"].Value = maKH;
            exRange.Range["B9"].ColumnWidth = 20;
            exRange.Range["B9"].MergeCells = true;


            exRange.Range["B11"].Value = "Tên Khách Hàng:";
            exRange.Range["B12"].Value = tenKH;
            exRange.Range["B11"].ColumnWidth = 20;
            exRange.Range["B11"].MergeCells = true;

            exRange.Range["E9"].Value = "Giới Tính:";
            exRange.Range["E10"].Value = gt;
            exRange.Range["E9"].MergeCells = true;


            exRange.Range["E11"].Value = "Ngày Khám:";
            exRange.Range["E12"].Value = ngayKham;
            exRange.Range["E11"].MergeCells = true;

            //Lấy thông tin các mặt hàng

            //Tạo dòng tiêu đề bảng
            exRange.Range["B14"].Value = "STT";
            exRange.Range["B14"].Font.Bold = true;
            exRange.Range["B14"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B14"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["B14"].ColumnWidth = 12;

            exRange.Range["C14"].Value = "Tên Xét Nghiệm";
            exRange.Range["C14"].Font.Bold = true;
            exRange.Range["C14"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C14"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["C14"].ColumnWidth = 25;


            exRange.Range["D14"].Value = "Đơn giá";
            exRange.Range["D14"].Font.Bold = true;
            exRange.Range["D14"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D14"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["D14"].ColumnWidth = 15;


            int i = 15;
            foreach (DataRow row in dt.Rows)
            {
                exRange.Range["B" + i.ToString()].Value = row["STT"];
                exRange.Range["B" + i.ToString()].Font.Bold = true;
                exRange.Range["B" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C" + i.ToString()].Value = row["TenXetNghiem"];
                exRange.Range["C" + i.ToString()].Font.Bold = true;
                exRange.Range["C" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["D" + i.ToString()].Value = row["DonGia"];
                exRange.Range["D" + i.ToString()].Font.Bold = true;
                exRange.Range["D" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

                i++;
            }



            exRange.Range["D24"].Value = "Tổng tiền";
            exRange.Range["D24"].Font.Bold = true;
            exRange.Range["D24"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D24"].ColumnWidth = 15;
            exRange.Range["E24"].Value = thanhTien;
            exRange.Range["E24"].Font.Bold = true;
            exRange.Range["E24"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            ////exRange.Value2 = tongtien;              
            exRange.Range["E27"].MergeCells = true;
            exRange.Range["E27"].Font.Italic = true;
            exRange.Range["E27"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = DateTime.Now;
            exRange.Range["E27"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["E29"].MergeCells = true;
            exRange.Range["E29"].Font.Bold = true;
            exRange.Range["E29"].Font.Italic = true;
            exRange.Range["E29"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E29"].Value = "Người lập phiếu";
            exRange.Range["E31"].MergeCells = true;
            exRange.Range["E31"].Font.Italic = true;
            exRange.Range["E31"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E31"].Value = hoTenNV;
            exSheet.Name = "Xét nghiệm";

            exApp.Visible = true;

        }
        #endregion

        #region In Phiếu Khám
        public void ExportDTToCOMExcelPhieuKham(string maPK, string maKH, string tenKH, string gt, string ngayKham, string hoTenNV, string ketLuan)
        {
            // Khởi động chương trình COMExcel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình COMExcel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["B1"].Value = "Phòng Khám Nhi Khoa";
            exRange.Range["B1"].Font.Size = 12;
            exRange.Range["B1"].Font.Bold = true;
            exRange.Range["B1"].Font.ColorIndex = 5; //Màu xanh da trời            
            exRange.Range["B1"].MergeCells = true;
            exRange.Range["B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B2"].Value = "236 Hoàng Quốc Việt - Hà Nội";
            exRange.Range["B2"].Font.Size = 12;
            exRange.Range["B2"].Font.Bold = true;
            exRange.Range["B2"].Font.ColorIndex = 5; //Màu xanh da trời            
            exRange.Range["B2"].MergeCells = true;
            exRange.Range["B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B3"].Value = "Điện thoại: (+84)904841221";
            exRange.Range["B3"].Font.Size = 12;
            exRange.Range["B3"].Font.Bold = true;
            exRange.Range["B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["B3"].MergeCells = true;
            exRange.Range["B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C5"].Value = "KẾT LUẬN CỦA BÁC SĨ";
            exRange.Range["C5"].Font.Size = 16;
            exRange.Range["C5"].Font.Bold = true;
            exRange.Range["C5"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C5"].ColumnWidth = 40;
            exRange.Range["C5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

            // Biểu diễn thông tin chung của hóa đơn bán

            exRange.Range["B7"].Value = "Mã Phiếu Khám:";
            exRange.Range["B8"].Value = maPK;
            exRange.Range["B7"].ColumnWidth = 20;
            exRange.Range["B7"].MergeCells = true;

            exRange.Range["B9"].Value = "Mã khách hàng:";
            exRange.Range["B10"].Value = maKH;
            exRange.Range["B9"].ColumnWidth = 20;
            exRange.Range["B9"].MergeCells = true;


            exRange.Range["B11"].Value = "Tên Khách Hàng:";
            exRange.Range["B12"].Value = tenKH;
            exRange.Range["B11"].ColumnWidth = 20;
            exRange.Range["B11"].MergeCells = true;

            exRange.Range["E9"].Value = "Giới Tính:";
            exRange.Range["E10"].Value = gt;
            exRange.Range["E9"].MergeCells = true;


            exRange.Range["E11"].Value = "Ngày Khám:";
            exRange.Range["E12"].ColumnWidth = 30;
            exRange.Range["E12"].Value = ngayKham;
            exRange.Range["E11"].MergeCells = true;

            exRange.Range["B14"].Value = "KẾT LUẬN CỦA BÁC SĨ:";
            exRange.Range["B14"].ColumnWidth = 20;
            exRange.Range["B14"].MergeCells = true;

            exRange.Range["B15"].Value = ketLuan;
            exRange.Range["B15"].ColumnWidth = 30;
            exRange.Range["B15"].MergeCells = true;
            exRange.Range["B15"].Font.Bold = true;
            //Lấy thông tin các mặt hàng

            DateTime d = DateTime.Now;
            exRange.Range["E20"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["E22"].MergeCells = true;
            exRange.Range["E22"].Font.Bold = true;
            exRange.Range["E22"].Font.Italic = true;
            exRange.Range["E22"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E22"].Value = "Người lập phiếu";
            exRange.Range["E24"].MergeCells = true;
            exRange.Range["E24"].Font.Italic = true;
            exRange.Range["E24"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E24"].Value = hoTenNV;
            exSheet.Name = "Phiếu Khám";

            exApp.Visible = true;
            exApp.Quit();

        }
        #endregion

    }

    class Excel
    {
        #region In Chi tiết danh sách sửa chữa
        public void ExportDTToCOMExcelChiTietDanhSach(DataTable dt, string idLich, string tenCanBo, string ngayThongBao)
        {
            // Khởi động chương trình COMExcel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình COMExcel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["B1"].Value = "HỌC VIỆN KĨ THUẬT QUÂN SỰ";
            exRange.Range["B1"].Font.Size = 12;
            exRange.Range["B1"].Font.Bold = true;
            exRange.Range["B1"].Font.ColorIndex = 5; //Màu xanh da trời            
            exRange.Range["B1"].MergeCells = true;
            exRange.Range["B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B2"].Value = "d1-236 Hoàng Quốc Việt - Hà Nội";
            exRange.Range["B2"].Font.Size = 12;
            exRange.Range["B2"].Font.Bold = true;
            exRange.Range["B2"].Font.ColorIndex = 5; //Màu xanh da trời            
            exRange.Range["B2"].MergeCells = true;
            exRange.Range["B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B3"].Value = "Điện thoại: (+84)904841221";
            exRange.Range["B3"].Font.Size = 12;
            exRange.Range["B3"].Font.Bold = true;
            exRange.Range["B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["B3"].MergeCells = true;
            exRange.Range["B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D5"].Value = "DANH SÁCH HỎNG HÓC ĐẠI ĐỘI 153";
            exRange.Range["D5"].Font.Size = 16;
            exRange.Range["D5"].Font.Bold = true;
            exRange.Range["D5"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["D5"].ColumnWidth = 60;
            exRange.Range["D5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

            // Biểu diễn thông tin chung của hóa đơn bán


            exRange.Range["B7"].Value = "Mã Danh Sách:";
            exRange.Range["B8"].Value = idLich;
            exRange.Range["B7"].ColumnWidth = 20;
            exRange.Range["B7"].MergeCells = true;

            exRange.Range["B9"].Value = "Chỉ huy Đại đội:";
            exRange.Range["B10"].Value = tenCanBo;
            exRange.Range["B9"].ColumnWidth = 20;
            exRange.Range["B9"].MergeCells = true;


            exRange.Range["E9"].Value = "Ngày Thông Báo:";
            exRange.Range["E10"].Value = ngayThongBao;
            exRange.Range["E19"].MergeCells = true;

            //Lấy thông tin các mặt hàng

            //Tạo dòng tiêu đề bảng
            exRange.Range["B12"].Value = "STT";
            exRange.Range["B12"].Font.Bold = true;
            exRange.Range["B12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["B12"].ColumnWidth = 12;

            exRange.Range["C12"].Value = "Phòng";
            exRange.Range["C12"].Font.Bold = true;
            exRange.Range["C12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["C12"].ColumnWidth = 25;

            exRange.Range["D12"].Value = "Mã vật chất";
            exRange.Range["D12"].Font.Bold = true;
            exRange.Range["D12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["D12"].ColumnWidth = 15;

            exRange.Range["E12"].Value = "Tên vật chất";
            exRange.Range["E12"].Font.Bold = true;
            exRange.Range["E12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["E12"].ColumnWidth = 15;

            exRange.Range["F12"].Value = "Tình trạng";
            exRange.Range["F12"].Font.Bold = true;
            exRange.Range["F12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["F12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
            exRange.Range["F12"].ColumnWidth = 15;

            int i = 13;
            foreach (DataRow row in dt.Rows)
            {
                exRange.Range["B" + i.ToString()].Value = row["STT"];
                exRange.Range["B" + i.ToString()].Font.Bold = true;
                exRange.Range["B" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C" + i.ToString()].Value = row["SoPhong"];
                exRange.Range["C" + i.ToString()].Font.Bold = true;
                exRange.Range["C" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["D" + i.ToString()].Value = row["ID"];
                exRange.Range["D" + i.ToString()].Font.Bold = true;
                exRange.Range["D" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["E" + i.ToString()].Value = row["TenVC"];
                exRange.Range["E" + i.ToString()].Font.Bold = true;
                exRange.Range["E" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["F" + i.ToString()].Value = row["TinhTrang"];
                exRange.Range["F" + i.ToString()].Font.Bold = true;
                i++;
            }
            i++;
            ////exRange.Value2 = tongtien;              

            exRange.Range["D" + i.ToString()].MergeCells = true;
            exRange.Range["D" + i.ToString()].Font.Italic = true;
            exRange.Range["D" + (i + 2).ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = DateTime.Now;
            exRange.Range["D" + i.ToString()].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["D" + (i + 2).ToString()].MergeCells = true;
            exRange.Range["D" + (i + 2).ToString()].Font.Bold = true;
            exRange.Range["D" + (i + 2).ToString()].Font.Italic = true;
            exRange.Range["D" + (i + 2).ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E" + (i + 2).ToString()].Value = "Người lập phiếu";
            exRange.Range["E" + (i + 4).ToString()].MergeCells = true;
            exRange.Range["E" + (i + 4).ToString()].Font.Italic = true;
            exRange.Range["E" + (i + 4).ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E" + (i + 4).ToString()].Value = tenCanBo;
            exSheet.Name = "PHIẾU";

            exApp.Visible = true;

        }
        #endregion
    }
}
