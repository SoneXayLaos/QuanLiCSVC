using QuanLyCSVCDaiDoi.Data;
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
    public partial class FormDanhMucCSVC : Form
    {
        private Model1 db = new Model1();
        public FormDanhMucCSVC()
        {
            InitializeComponent();
        }

        private void FormDanhMucCSVC_Load(object sender, EventArgs e)
        {
           
            this.loaiCSVCTableAdapter.Fill(this.quanLyCSVCDaiDoiDataSet.LoaiCSVC);
            while (dgvDanhSach.Rows.Count != 0)
            {
                dgvDanhSach.Rows.RemoveAt(0);
            }
            var list = db.DanhMucCSVCs.Where(x => x.TenVC != null).ToList();
            foreach (var item in list)
            {
                dgvDanhSach.Rows.Add(item.TenVC, item.LoaiCSVC.TenLoai, item.SoPhong, item.TinhTrang, item.GhiChu, item.ID);
            }
            var listLoai = db.LoaiCSVCs.Where(x => x.TenLoai != null).ToList();
            foreach (var item in listLoai)
            {
                cbLoaiCSVC.Items.Add(item.TenLoai);
            }
        }

        private void Clear()
        {
            txtMACSVC.Text = "";
            txtTenCSVC.Text = "";
            txtTinhTrang.Text = "";
            cbLoaiCSVC.Text = "";
            txtGhiChu.Text = "";
        }
        private void dgvDanhSach(object sender, DataGridViewCellEventArgs e)
        {
            currentIDLich = dgvDanhSach.CurrentRow.Cells[0].Value.ToString();

            ketNoiCSDL.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            SqlCommand command = new SqlCommand("sp_ThongTinDSCS", ketNoiCSDL);
            command.Parameters.Add("@idLich", SqlDbType.Int).Value = Int32.Parse(currentIDLich);
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
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu();
            this.Hide();
            tc.Show();
        }

        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbLoaiCSVC.Text = dgvDanhSach.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTenCSVC.Text = dgvDanhSach.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtPhong.Text = dgvDanhSach.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTinhTrang.Text = dgvDanhSach.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtGhiChu.Text = dgvDanhSach.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtMACSVC.Text = dgvDanhSach.Rows[e.RowIndex].Cells[5].Value.ToString();
            btnXoa.Enabled = true;
            btnXoa.BackColor = Color.Red;
            btnThem.Enabled = false;
            btnThem.BackColor = Color.SteelBlue;
            btnCapNhat.Enabled = false;
            btnCapNhat.BackColor = Color.SteelBlue;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtTinhTrang_TextChanged(object sender, EventArgs e)
        {
            btnCapNhat.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnCapNhat.BackColor = Color.Blue;
            btnThem.BackColor = Color.Blue;
            btnXoa.BackColor = Color.LightCoral;
        }

        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {
            btnCapNhat.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnCapNhat.BackColor = Color.Blue;
            btnThem.BackColor = Color.Blue;
            btnXoa.BackColor = Color.LightCoral;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtTenCSVC_TextChanged(object sender, EventArgs e)
        {
            btnCapNhat.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnCapNhat.BackColor = Color.Blue;
            btnThem.BackColor = Color.Blue;
            btnXoa.BackColor = Color.LightCoral;
        }

        private void txtLoaiCSVC_TextChanged(object sender, EventArgs e)
        {
            btnCapNhat.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnCapNhat.BackColor = Color.Blue;
            btnThem.BackColor = Color.Blue;
            btnXoa.BackColor = Color.LightCoral;
        }

        private void txtPhong_TextChanged(object sender, EventArgs e)
        {
            btnCapNhat.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnCapNhat.BackColor = Color.Blue;
            btnThem.BackColor = Color.Blue;
            btnXoa.BackColor = Color.LightCoral;
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDanhSach_CellContentClick(sender, e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                db.DanhMucCSVCs.Remove(db.DanhMucCSVCs.Find(int.Parse(txtMACSVC.Text)));
                db.SaveChanges();
                Clear();
                FormDanhMucCSVC_Load(sender, e);
            }
        }

        private void cbLoaiCSVC_TextChanged(object sender, EventArgs e)
        {
            btnCapNhat.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnCapNhat.BackColor = Color.Blue;
            btnThem.BackColor = Color.Blue;
            btnXoa.BackColor = Color.LightCoral;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtGhiChu.Text.Trim() == "" || txtTenCSVC.Text.Trim() == "" || txtTinhTrang.Text.Trim() == "" || txtGhiChu.Text.Trim() == "" || cbLoaiCSVC.Text.Trim() == "")
            {
                MessageBox.Show("Bạn cần điển đầy đủ thông tin ở tất cả các trường", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Thêm vật chất?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    var VC = new DanhMucCSVC();
                    VC.TenVC = txtTenCSVC.Text;
                    var idloai = db.LoaiCSVCs.FirstOrDefault(x => x.TenLoai == cbLoaiCSVC.Text).IDLoai;
                    VC.IDLoai = idloai;
                    VC.TinhTrang = txtTinhTrang.Text;
                    VC.GhiChu = txtGhiChu.Text;
                    VC.SoPhong = int.Parse(txtPhong.Text);
                    db.DanhMucCSVCs.Add(VC);
                    db.SaveChanges();
                    Clear();
                    FormDanhMucCSVC_Load(sender, e);
                }
            }
        }
        //nut cap nhat
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtGhiChu.Text.Trim() == "" || txtTenCSVC.Text.Trim() == "" || txtTinhTrang.Text.Trim() == "" || txtGhiChu.Text.Trim() == "" || cbLoaiCSVC.Text.Trim() == "")
            {
                MessageBox.Show("Bạn cần điển đầy đủ thông tin ở tất cả các trường", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn sửa?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    var VC = db.DanhMucCSVCs.Find(int.Parse(txtMACSVC.Text));
                    VC.TenVC = txtTenCSVC.Text;
                    var idloai = db.LoaiCSVCs.FirstOrDefault(x => x.TenLoai == cbLoaiCSVC.Text).IDLoai;
                    VC.IDLoai = idloai;
                    VC.TinhTrang = txtTinhTrang.Text;
                    VC.GhiChu = txtGhiChu.Text;
                    VC.SoPhong = int.Parse(txtPhong.Text);
                    db.SaveChanges();
                    Clear();
                    FormDanhMucCSVC_Load(sender, e);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
