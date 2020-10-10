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
    public partial class FormCapMoi : Form
    {
        public FormCapMoi()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gửi đề nghị thành công!");
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            FormDanhSachSuaChua f = new FormDanhSachSuaChua();
            this.Hide();
            f.Show();
        }
    }
}
