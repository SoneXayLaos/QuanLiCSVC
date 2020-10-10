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
    public partial class DangNhap : Form
    {
        private SqlConnection ketNoiCSDL = new SqlConnection(@"Data Source=PC;Initial Catalog=QuanLyCSVCDaiDoi;Integrated Security=True");
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataTable dt = new DataTable();
            
            ketNoiCSDL.Open();
            SqlCommand command = new SqlCommand("sp_DangNhapTaiKhoan", ketNoiCSDL);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@tendangnhap", SqlDbType.VarChar).Value = tbTenDangNhap.Text;
            command.Parameters.Add("@matkhau", SqlDbType.VarChar).Value = tbMatKhau.Text;
            SqlDataReader read = command.ExecuteReader();
            dt.Load(read);
            ketNoiCSDL.Close();
            if (dt.Rows.Count != 0)
            {
                TrangChu tc = new TrangChu();
                this.Hide();
                tc.Show();
            }
            else
            {
                MessageBox.Show("Nhập sai thông tin");
                i++;
                if (i == 3)
                {
                    MessageBox.Show("Nhập sai quá 3 lần");
                    this.Close();
                }
            }
            //int i = 0;
            //if(tbTenDangNhap.Text == "admin" && tbMatKhau.Text == "1")
            //{
            //    TrangChu tc = new TrangChu();
            //    this.Hide();
            //    tc.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Nhập sai thông tin");
            //    i++;
            //    if (i == 3)
            //    {
            //        MessageBox.Show("Nhập sai quá 3 lần");
            //        this.Close();
            //    }
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
