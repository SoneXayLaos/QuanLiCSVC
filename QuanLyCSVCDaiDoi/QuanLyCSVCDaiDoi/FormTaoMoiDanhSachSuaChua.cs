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

        private void txtMaCSVC_TextChanged(object sender, EventArgs e)
        {
            if (txtMaCSVC.Text != "" && txtMaCSVC.Text != null)
            {
                DataTable dt = new DataTable();
                ketNoiCSDL.Open();
                SqlCommand command = new SqlCommand("sp_TimKiemMaDSSC", ketNoiCSDL);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@maVC", SqlDbType.Int).Value = Int32.Parse(txtMaCSVC.Text);
                SqlDataReader read = command.ExecuteReader();
                dt.Load(read);
                ketNoiCSDL.Close();
                if (dt.Rows.Count > 0)
                    dgvKetQuaTimKiem.DataSource = dt;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != null)
            {
                DataTable dt = new DataTable();
                ketNoiCSDL.Open();
                SqlCommand command = new SqlCommand("sp_TimKiemTenDSVC", ketNoiCSDL);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@tenVC", SqlDbType.NVarChar).Value = textBox1.Text;
                SqlDataReader read = command.ExecuteReader();
                dt.Load(read);
                ketNoiCSDL.Close();
                if (dt.Rows.Count > 0)
                    dgvKetQuaTimKiem.DataSource = dt;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox2.Text != null)
            {
                DataTable dt = new DataTable();
                ketNoiCSDL.Open();
                SqlCommand command = new SqlCommand("sp_TimKiemSoPhong", ketNoiCSDL);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@soPhong", SqlDbType.Int).Value = Int32.Parse(textBox2.Text);
                SqlDataReader read = command.ExecuteReader();
                dt.Load(read);
                ketNoiCSDL.Close();
                if (dt.Rows.Count > 0)
                    dgvKetQuaTimKiem.DataSource = dt;
            }
        }

        private void dgvKetQuaTimKiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormTaoMoiDanhSachSuaChua_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_LoadDSVatChat", ketNoiCSDL);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader read = command.ExecuteReader();
            dt.Load(read);
            ketNoiCSDL.Close();

            dgvKetQuaTimKiem.DataSource = dt;
            dgvKetQuaTimKiem.RowHeadersVisible = false;
            dgvKetQuaTimKiem.Columns[0].Width = 40;
            dgvKetQuaTimKiem.Columns[1].Width = 40;
            dgvKetQuaTimKiem.Columns[3].Width = 80;
            Load2();
        }
        public void Load2()
        {
            Random rd = new Random();
            idLichSuaChua = rd.Next();
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_ThemMoiLichSC", ketNoiCSDL);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@idLich", SqlDbType.Int).Value = idLichSuaChua;
            command.ExecuteNonQuery();
            ketNoiCSDL.Close();
            lbID.Text = idLichSuaChua.ToString();
        }

        private void dgvKetQuaTimKiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThem.Enabled = true;
            currentIDtimkiem = dgvKetQuaTimKiem.CurrentRow.Cells[0].Value.ToString();
        }



    }

   
}
