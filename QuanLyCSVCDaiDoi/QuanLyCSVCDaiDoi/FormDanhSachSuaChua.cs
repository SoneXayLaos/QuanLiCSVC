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
        
        private SqlConnection ketNoiCSDL = new SqlConnection(@"Data Source=.\VANANH;Initial Catalog=QuanLyCSVCDaiDoi;Integrated Security=True");
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

      
       
}
