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
    public partial class FormLichCongTac : Form
    {
        private SqlConnection ketNoiCSDL = new SqlConnection(@"Data Source=PC;Initial Catalog=QuanLyCSVCDaiDoi;Integrated Security=True");
        string currentID = "";
        public FormLichCongTac()
        {
            InitializeComponent();
        }

        private void FormLichCongTac_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_LoadDSLichCongTac", ketNoiCSDL);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ngay", SqlDbType.Date).Value = dtNgayThang.Value;
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

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentID = dgvDanhSach.CurrentRow.Cells[0].Value.ToString();

            ketNoiCSDL.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            SqlCommand command = new SqlCommand("sp_ChiTietSDVC", ketNoiCSDL);
            command.Parameters.Add("@idLich", SqlDbType.Int).Value = Int32.Parse(currentID);
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

            txtMaCT.Text = dgvDanhSach.CurrentRow.Cells[3].Value.ToString();
            txtTenCT.Text = dgvDanhSach.CurrentRow.Cells[2].Value.ToString();
            txtHocVien.Text = dgvDanhSach.CurrentRow.Cells[7].Value.ToString();
            txtCanBo.Text = dgvDanhSach.CurrentRow.Cells[6].Value.ToString();
            txtGhiChu.Text = dgvDanhSach.CurrentRow.Cells[8].Value.ToString();
            txtThoiGian.Text = dgvDanhSach.CurrentRow.Cells[4].Value.ToString();
        }

        private void dtNgayThang_ValueChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_LoadDSLichCongTac", ketNoiCSDL);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ngay", SqlDbType.Date).Value = dtNgayThang.Value;
            SqlDataReader read = command.ExecuteReader();
            dt.Load(read);
            ketNoiCSDL.Close();
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                row["STT"] = i++;
            }
            dgvDanhSach.DataSource = dt;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_CapNhatLichCongTac", ketNoiCSDL);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@hocvien", SqlDbType.NVarChar).Value = txtHocVien.Text;
            command.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
            command.ExecuteNonQuery();
            ketNoiCSDL.Close();

            MessageBox.Show("Cập nhật thành công!");
            Load2();
        }
        public void Load2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_LoadDSLichCongTac", ketNoiCSDL);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ngay", SqlDbType.Date).Value = dtNgayThang.Value;
            SqlDataReader read = command.ExecuteReader();
            dt.Load(read);
            ketNoiCSDL.Close();
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                row["STT"] = i++;
            }
            dgvDanhSach.DataSource = dt;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu();
            this.Hide();
            tc.Show();
        }
    }
}
