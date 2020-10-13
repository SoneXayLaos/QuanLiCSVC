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

    