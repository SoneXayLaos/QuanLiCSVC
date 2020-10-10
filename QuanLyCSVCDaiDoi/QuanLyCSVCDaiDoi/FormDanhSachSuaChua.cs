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
    public partial class FormDanhSachSuaChua : Form
    {
        
        private SqlConnection ketNoiCSDL = new SqlConnection(@"Data Source=PC;Initial Catalog=QuanLyCSVCDaiDoi;Integrated Security=True");
        string currentIDLich = "";
        public FormDanhSachSuaChua()
        {
            InitializeComponent();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu();
            this.Hide();
            tc.Show();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            FormTaoMoiDanhSachSuaChua f = new FormTaoMoiDanhSachSuaChua();
            this.Hide();
            f.Show();
        }

        private void FormDanhSachSuaChua_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_LoadDSLichSuaChua", ketNoiCSDL);
        
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader read = command.ExecuteReader();
            dt.Load(read);
            ketNoiCSDL.Close();
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                row["STT"] = i++;
            }
            dgvDanhSach.DataSource = dt;
            dgvDanhSach.RowHeadersVisible = false;
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {
            Excel COMExcel = new Excel();

            DataTable dt = new DataTable();
            dt = (DataTable)dgvChiTietDanhSach.DataSource;

            COMExcel.ExportDTToCOMExcelChiTietDanhSach(dt, currentIDLich, "1// Nguyễn Quốc Nhân", DateTime.Today.ToString());
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentIDLich = dgvDanhSach.CurrentRow.Cells[0].Value.ToString();
            
            ketNoiCSDL.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            SqlCommand command = new SqlCommand("sp_ThongTinDSCS", ketNoiCSDL);
            command.Parameters.Add("@idLich", SqlDbType.Int).Value = Int32.Parse(currentIDLich);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader read = command.ExecuteReader();
            dt.Load(read);
            ketNoiCSDL.Close();
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                row["STT"] = i++;
            }
            dgvChiTietDanhSach.DataSource = dt;
            btnInRa.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormCapMoi f = new FormCapMoi();
            this.Hide();
            f.Show();
        }
    }

    //class Excel
    //{
    //    #region In Chi tiết danh sách sửa chữa
    //    public void ExportDTToCOMExcelChiTietDanhSach(DataTable dt, string idLich, string tenCanBo, string ngayThongBao)
    //    {
    //        // Khởi động chương trình COMExcel
    //        COMExcel.Application exApp = new COMExcel.Application();
    //        COMExcel.Workbook exBook; //Trong 1 chương trình COMExcel có nhiều Workbook
    //        COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
    //        COMExcel.Range exRange;

    //        exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
    //        exSheet = exBook.Worksheets[1];
    //        // Định dạng chung
    //        exRange = exSheet.Cells[1, 1];
    //        exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
    //        exRange.Range["B1"].Value = "HỌC VIỆN KĨ THUẬT QUÂN SỰ";
    //        exRange.Range["B1"].Font.Size = 12;
    //        exRange.Range["B1"].Font.Bold = true;
    //        exRange.Range["B1"].Font.ColorIndex = 5; //Màu xanh da trời            
    //        exRange.Range["B1"].MergeCells = true;
    //        exRange.Range["B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["B2"].Value = "d1-236 Hoàng Quốc Việt - Hà Nội";
    //        exRange.Range["B2"].Font.Size = 12;
    //        exRange.Range["B2"].Font.Bold = true;
    //        exRange.Range["B2"].Font.ColorIndex = 5; //Màu xanh da trời            
    //        exRange.Range["B2"].MergeCells = true;
    //        exRange.Range["B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["B3"].Value = "Điện thoại: (+84)904841221";
    //        exRange.Range["B3"].Font.Size = 12;
    //        exRange.Range["B3"].Font.Bold = true;
    //        exRange.Range["B3"].Font.ColorIndex = 5; //Màu xanh da trời
    //        exRange.Range["B3"].MergeCells = true;
    //        exRange.Range["B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["D5"].Value = "DANH SÁCH HỎNG HÓC ĐẠI ĐỘI 153";
    //        exRange.Range["D5"].Font.Size = 16;
    //        exRange.Range["D5"].Font.Bold = true;
    //        exRange.Range["D5"].Font.ColorIndex = 3; //Màu đỏ
    //        exRange.Range["D5"].ColumnWidth = 60;
    //        exRange.Range["D5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

    //        // Biểu diễn thông tin chung của hóa đơn bán


    //        exRange.Range["B7"].Value = "Mã Danh Sách:";
    //        exRange.Range["B8"].Value = idLich;
    //        exRange.Range["B7"].ColumnWidth = 20;
    //        exRange.Range["B7"].MergeCells = true;

    //        exRange.Range["B9"].Value = "Chỉ huy Đại đội:";
    //        exRange.Range["B10"].Value = tenCanBo;
    //        exRange.Range["B9"].ColumnWidth = 20;
    //        exRange.Range["B9"].MergeCells = true;


    //        exRange.Range["E9"].Value = "Ngày Thông Báo:";
    //        exRange.Range["E10"].Value = ngayThongBao;
    //        exRange.Range["E19"].MergeCells = true;

    //        //Lấy thông tin các mặt hàng

    //        //Tạo dòng tiêu đề bảng
    //        exRange.Range["B12"].Value = "STT";
    //        exRange.Range["B12"].Font.Bold = true;
    //        exRange.Range["B12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["B12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
    //        exRange.Range["B12"].ColumnWidth = 12;

    //        exRange.Range["C12"].Value = "Phòng";
    //        exRange.Range["C12"].Font.Bold = true;
    //        exRange.Range["C12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["C12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
    //        exRange.Range["C12"].ColumnWidth = 25;

    //        exRange.Range["D12"].Value = "Mã vật chất";
    //        exRange.Range["D12"].Font.Bold = true;
    //        exRange.Range["D12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["D12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
    //        exRange.Range["D12"].ColumnWidth = 15;

    //        exRange.Range["E12"].Value = "Tên vật chất";
    //        exRange.Range["E12"].Font.Bold = true;
    //        exRange.Range["E12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["E12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
    //        exRange.Range["E12"].ColumnWidth = 15;

    //        exRange.Range["F12"].Value = "Tình trạng";
    //        exRange.Range["F12"].Font.Bold = true;
    //        exRange.Range["F12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["F12"].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
    //        exRange.Range["F12"].ColumnWidth = 15;

    //        int i = 13;
    //        foreach (DataRow row in dt.Rows)
    //        {
    //            exRange.Range["B" + i.ToString()].Value = row["STT"];
    //            exRange.Range["B" + i.ToString()].Font.Bold = true;
    //            exRange.Range["B" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //            exRange.Range["C" + i.ToString()].Value = row["SoPhong"];
    //            exRange.Range["C" + i.ToString()].Font.Bold = true;
    //            exRange.Range["C" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //            exRange.Range["D" + i.ToString()].Value = row["ID"];
    //            exRange.Range["D" + i.ToString()].Font.Bold = true;
    //            exRange.Range["D" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //            exRange.Range["E" + i.ToString()].Value = row["TenVC"];
    //            exRange.Range["E" + i.ToString()].Font.Bold = true;
    //            exRange.Range["E" + i.ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //            exRange.Range["F" + i.ToString()].Value = row["TinhTrang"];
    //            exRange.Range["F" + i.ToString()].Font.Bold = true;
    //            i++;
    //        }
    //        i++;
    //        ////exRange.Value2 = tongtien;              

    //        exRange.Range["E" + i.ToString()].MergeCells = true;
    //        exRange.Range["E" + i.ToString()].Font.Italic = true;
    //        exRange.Range["E" + (i + 2).ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        DateTime d = DateTime.Now;
    //        exRange.Range["E" + i.ToString()].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
    //        exRange.Range["E" + (i + 2).ToString()].MergeCells = true;
    //        exRange.Range["E" + (i + 2).ToString()].Font.Bold = true;
    //        exRange.Range["E" + (i + 2).ToString()].Font.Italic = true;
    //        exRange.Range["E" + (i + 2).ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["E" + (i + 2).ToString()].Value = "Người lập phiếu";
    //        exRange.Range["E" + (i + 4).ToString()].MergeCells = true;
    //        exRange.Range["E" + (i + 4).ToString()].Font.Italic = true;
    //        exRange.Range["E" + (i + 4).ToString()].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
    //        exRange.Range["E" + (i + 4).ToString()].Value = tenCanBo;
    //        exSheet.Name = "PHIẾU";

    //        exApp.Visible = true;

    //    }
    //    #endregion
    //}
}
