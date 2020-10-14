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
    public partial class FormTaiKhoan : Form
    {
        private SqlConnection ketNoiCSDL = new SqlConnection(@"Data Source=.\VANANH;Initial Catalog=QuanLyCSVCDaiDoi;Integrated Security=True");
        DataSet ds = new DataSet("DuLieu");
        public FormTaiKhoan()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnXacNHan_Click(object sender, EventArgs e)
        {
            FormPass1 f = new FormPass1();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu();
            this.Hide();
            tc.Show();
        }

        private void FormTaiKhoan_Load(object sender, EventArgs e)
        {

        }

       public void DoDuLieu()
        {
            ketNoiCSDL.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT *FROM DanhMucCSVC", ketNoiCSDL);
            da.Fill(ds, "DanhMucCSVC");
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT *FROM LichCongTac", ketNoiCSDL);
            da1.Fill(ds, "LichCongTac");
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT *FROM LichSuaChua", ketNoiCSDL);
            da2.Fill(ds, "LichSuaChua");
            ketNoiCSDL.Close();
        }

        public void ExportToExcelBangDanhMucCSVC(DataTable dt, string nametable)
        {
            #region
            COMExcel.Application app = new COMExcel.Application();
            COMExcel.Workbook wb = app.Workbooks.Add(Type.Missing);
            COMExcel.Worksheet ws = (COMExcel.Worksheet)wb.ActiveSheet;
            //ws.Name = nametable;
            ws.Columns.AutoFit();
            for (int i = 2; i < dt.Columns.Count + 2; i++)
            {
                //ws.Cells[7, i] = dt.Columns[i - 2].ToString();
                ws.Cells[7, i].Borders.Weight = COMExcel.XlBorderWeight.xlThin;
                // căn giữa 
                ws.Cells[7, i].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                ws.Cells[7, i].Font.Bold = true;
                ws.Cells[7, i].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                ws.Rows.RowHeight = 23;
                ws.Cells[7, 2].Value = "Mã Vật Chất";
                ws.Columns[2].ColumnWidth = 15;
                ws.Cells[7, 3].Value = "Mã Loại Vật Chất";
                ws.Columns[3].ColumnWidth = 25;
                ws.Cells[7, 4].Value = "Tên Vật Chất";
                ws.Columns[4].ColumnWidth = 15;
                ws.Cells[7, 5].Value = "Số Phòng";
                ws.Columns[5].ColumnWidth = 25;
                ws.Cells[7, 6].Value = "Tình Trạng";
                ws.Columns[6].ColumnWidth = 30;
                ws.Cells[7, 7].Value = "Ghi Chú";
                ws.Columns[7].ColumnWidth = 40;
            }
            //add Content
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != null)
                    {

                        //add datatable
                        COMExcel.Range xlRange =
                             (COMExcel.Range)ws.Cells[i + 8, j + 2];
                        xlRange.Borders.Weight = COMExcel.XlBorderWeight.xlThin;
                        // căn giữa theo chiều ngang của Cột STT
                        xlRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                        // căn giữa theo chiều dọc của cột STT
                        xlRange.VerticalAlignment = COMExcel.XlVAlign.xlVAlignTop;
                        xlRange.Borders.LineStyle = COMExcel.XlLineStyle.xlContinuous;
                        ws.Cells[i + 8, j + 2] = dt.Rows[i][j].ToString();
                        if (dt.Rows[i][3].ToString() == "True") ws.Cells[i + 8, 4] = "Nam";
                        else ws.Cells[i + 8, 4] = "Nữ";
                    }
                }
            }

            app.Visible = true;
            #endregion
        }

