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
    public partial class FormPass1 : Form
    {
        public FormPass1()
        {
            InitializeComponent();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if(txtPass.Text == "1")
            {
                MessageBox.Show("Đổi thành công");
                this.Hide();
            }
        }
    }
}
