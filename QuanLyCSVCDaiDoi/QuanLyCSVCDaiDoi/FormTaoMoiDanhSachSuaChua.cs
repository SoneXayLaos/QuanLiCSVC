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
    public partial class FormTaoMoiDanhSachSuaChua : Form
    {
        private SqlConnection ketNoiCSDL = new SqlConnection(@"Data Source=DESKTOP-SAU51IQ;Initial Catalog=QuanLyCSVCDaiDoi;Integrated Security=True");
        string currentIDtimkiem = "";
        string currentIDSuaChua = "";
        int idLichSuaChua = 0;

        public FormTaoMoiDanhSachSuaChua()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormDanhSachSuaChua f = new FormDanhSachSuaChua();
            this.Hide();
            f.Show();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_ThemVCHong", ketNoiCSDL);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@idLich", SqlDbType.Int).Value = idLichSuaChua;
            command.Parameters.Add("@idVC", SqlDbType.Int).Value = Int32.Parse(currentIDtimkiem);
            command.ExecuteNonQuery();
            ketNoiCSDL.Close();

            Load3();
        }

        public void Load3()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_ThongTinDSCS", ketNoiCSDL);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@idLich", SqlDbType.Int).Value = idLichSuaChua;
            SqlDataReader read = command.ExecuteReader();
            dt.Load(read);
            ketNoiCSDL.Close();
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                row["STT"] = i++;
            }
            if (dt.Rows.Count == 0)
            {
                DataTable t = new DataTable();
                dgvDanhSachSuaChua.DataSource = t;
            }
            dgvDanhSachSuaChua.DataSource = dt;
        }




        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_XoaVCHong", ketNoiCSDL);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@idLich", SqlDbType.Int).Value = idLichSuaChua;
            command.Parameters.Add("@idVC", SqlDbType.Int).Value = Int32.Parse(currentIDSuaChua);
            command.ExecuteNonQuery();
            ketNoiCSDL.Close();

            Load3();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_ChotLichSC", ketNoiCSDL);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@idLich", SqlDbType.Int).Value = idLichSuaChua;
            command.Parameters.Add("@ghiChu", SqlDbType.NVarChar).Value = rtbGhiChu.Text;
            command.Parameters.Add("@ngaySua", SqlDbType.Date).Value = dtNgaySua.Value;
            command.ExecuteNonQuery();
            ketNoiCSDL.Close();
            
            MessageBox.Show("Lưu thành công!");


            Excel COMExcel = new Excel();

            DataTable dt = new DataTable();
            dt = (DataTable)dgvDanhSachSuaChua.DataSource;

            COMExcel.ExportDTToCOMExcelChiTietDanhSach(dt, idLichSuaChua.ToString(), "1// Nguyễn Quốc Nhân", DateTime.Today.ToString());
        }



    }

   
}
