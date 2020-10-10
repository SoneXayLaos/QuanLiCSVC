using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCSVCDaiDoi
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void danhSáchCSVCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDanhMucCSVC f = new FormDanhMucCSVC();
            this.Hide();
            f.Show();
        }

        private void danhSáchHọcViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDanhMucHV f = new FormDanhMucHV();
            this.Hide();
            f.Show();
        }

        private void lịchCôngTácToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLichCongTac f = new FormLichCongTac();
            this.Hide();
            f.Show();
        }

        private void danhSáchSửaChữaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDanhSachSuaChua f = new FormDanhSachSuaChua();
            this.Hide();
            f.Show();
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTaiKhoan f = new FormTaiKhoan();
            this.Hide();
            f.Show();
        }

        private void hướngDẫnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saoLưuDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void đềNghịCấpMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DangNhap dn = new DangNhap();
            this.Hide();
            dn.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
