
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectTaskSystem.BUS;
using ProjectTaskSystem.DTO;

namespace ProjectTaskSystem.GUI
{
    public class frmDuAn : Form
    {
        private DuAnBUS bus = new DuAnBUS();

        private TextBox txtMaDuAn;
        private TextBox txtTenDuAn;
        private DateTimePicker dtpNgayBatDau;
        private DateTimePicker dtpNgayKetThuc;
        private ComboBox cboTrangThai;

        private DataGridView dgvDuAn;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;

        public frmDuAn()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "QUẢN LÝ DỰ ÁN";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            Label lblTitle = new Label();
            lblTitle.Text = "QUẢN LÝ DỰ ÁN";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(35, 45, 65);
            lblTitle.Location = new Point(30, 20);
            lblTitle.AutoSize = true;

            Panel pnlInput = new Panel();
            pnlInput.Location = new Point(30, 75);
            pnlInput.Size = new Size(920, 210);
            pnlInput.BackColor = Color.White;

            Label lblMa = CreateLabel("Mã dự án:", 20, 20);
            Label lblTen = CreateLabel("Tên dự án:", 20, 65);
            Label lblBatDau = CreateLabel("Ngày bắt đầu:", 470, 20);
            Label lblKetThuc = CreateLabel("Ngày kết thúc:", 470, 65);
            Label lblTrangThai = CreateLabel("Trạng thái:", 20, 110);

            txtMaDuAn = new TextBox();
            txtMaDuAn.Location = new Point(140, 20);
            txtMaDuAn.Size = new Size(250, 28);
            txtMaDuAn.ReadOnly = true;
            txtMaDuAn.BackColor = Color.LightGray;

            txtTenDuAn = new TextBox();
            txtTenDuAn.Location = new Point(140, 65);
            txtTenDuAn.Size = new Size(250, 28);

            dtpNgayBatDau = new DateTimePicker();
            dtpNgayBatDau.Location = new Point(600, 20);
            dtpNgayBatDau.Size = new Size(250, 28);
            dtpNgayBatDau.Format = DateTimePickerFormat.Short;
            dtpNgayBatDau.ShowCheckBox = true;
            dtpNgayBatDau.Checked = false;

            dtpNgayKetThuc = new DateTimePicker();
            dtpNgayKetThuc.Location = new Point(600, 65);
            dtpNgayKetThuc.Size = new Size(250, 28);
            dtpNgayKetThuc.Format = DateTimePickerFormat.Short;
            dtpNgayKetThuc.ShowCheckBox = true;
            dtpNgayKetThuc.Checked = false;

            cboTrangThai = new ComboBox();
            cboTrangThai.Location = new Point(140, 110);
            cboTrangThai.Size = new Size(250, 28);
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Items.AddRange(new object[]
            {
                "Chưa bắt đầu",
                "Đang thực hiện",
                "Hoàn thành",
                "Tạm dừng"
            });
            cboTrangThai.SelectedIndex = 0;

            btnThem = CreateButton("Thêm", 140, 155, Color.SeaGreen);
            btnSua = CreateButton("Sửa", 270, 155, Color.RoyalBlue);
            btnXoa = CreateButton("Xóa", 400, 155, Color.IndianRed);
            btnLamMoi = CreateButton("Làm mới", 530, 155, Color.Gray);

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;

            pnlInput.Controls.AddRange(new Control[]
            {
                lblMa, lblTen, lblBatDau, lblKetThuc, lblTrangThai,
                txtMaDuAn, txtTenDuAn,
                dtpNgayBatDau, dtpNgayKetThuc,
                cboTrangThai,
                btnThem, btnSua, btnXoa, btnLamMoi
            });

            dgvDuAn = new DataGridView();
            dgvDuAn.Location = new Point(30, 310);
            dgvDuAn.Size = new Size(920, 270);
            dgvDuAn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                             AnchorStyles.Left | AnchorStyles.Right;
            dgvDuAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDuAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDuAn.MultiSelect = false;
            dgvDuAn.ReadOnly = true;
            dgvDuAn.AllowUserToAddRows = false;
            dgvDuAn.RowHeadersVisible = false;
            dgvDuAn.BackgroundColor = Color.White;
            dgvDuAn.CellClick += DgvDuAn_CellClick;

            this.Controls.Add(lblTitle);
            this.Controls.Add(pnlInput);
            this.Controls.Add(dgvDuAn);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 10);
            return lbl;
        }

        private Button CreateButton(string text, int x, int y, Color color)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(115, 38);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private void LoadData()
        {
            try
            {
                dgvDuAn.DataSource = bus.GetAll();

                if (dgvDuAn.Columns["MaDuAn"] != null)
                    dgvDuAn.Columns["MaDuAn"].HeaderText = "Mã dự án";

                if (dgvDuAn.Columns["TenDuAn"] != null)
                    dgvDuAn.Columns["TenDuAn"].HeaderText = "Tên dự án";

                if (dgvDuAn.Columns["NgayBatDau"] != null)
                    dgvDuAn.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";

                if (dgvDuAn.Columns["NgayKetThuc"] != null)
                    dgvDuAn.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";

                if (dgvDuAn.Columns["TrangThai"] != null)
                    dgvDuAn.Columns["TrangThai"].HeaderText = "Trạng thái";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private DuAnDTO GetDuAnFromForm()
        {
            DuAnDTO da = new DuAnDTO();

            if (!string.IsNullOrWhiteSpace(txtMaDuAn.Text))
                da.MaDuAn = int.Parse(txtMaDuAn.Text);

            da.TenDuAn = txtTenDuAn.Text.Trim();

            da.NgayBatDau = dtpNgayBatDau.Checked
                ? (DateTime?)dtpNgayBatDau.Value : null;

            da.NgayKetThuc = dtpNgayKetThuc.Checked
                ? (DateTime?)dtpNgayKetThuc.Value : null;

            da.TrangThai = cboTrangThai.Text;

            return da;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DuAnDTO da = GetDuAnFromForm();

                if (da.NgayBatDau.HasValue && da.NgayKetThuc.HasValue &&
                    da.NgayKetThuc.Value.Date < da.NgayBatDau.Value.Date)
                {
                    MessageBox.Show("Ngày kết thúc không được trước ngày bắt đầu!");
                    return;
                }

                string result = bus.Insert(da);
                MessageBox.Show(result);

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm dự án: " + ex.Message);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDuAn.Text))
                {
                    MessageBox.Show("Vui lòng chọn dự án cần sửa!");
                    return;
                }

                DuAnDTO da = GetDuAnFromForm();

                if (da.NgayBatDau.HasValue && da.NgayKetThuc.HasValue &&
                    da.NgayKetThuc.Value.Date < da.NgayBatDau.Value.Date)
                {
                    MessageBox.Show("Ngày kết thúc không được trước ngày bắt đầu!");
                    return;
                }

                string result = bus.Update(da);
                MessageBox.Show(result);

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa dự án: " + ex.Message);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDuAn.Text))
                {
                    MessageBox.Show("Vui lòng chọn dự án cần xóa!");
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa dự án này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    int maDuAn = int.Parse(txtMaDuAn.Text);
                    string result = bus.Delete(maDuAn);

                    MessageBox.Show(result);
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa dự án: " + ex.Message);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadData();
        }

        private void DgvDuAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvDuAn.Rows[e.RowIndex];

            txtMaDuAn.Text = row.Cells["MaDuAn"].Value.ToString();
            txtTenDuAn.Text = row.Cells["TenDuAn"].Value.ToString();

            if (row.Cells["NgayBatDau"].Value != DBNull.Value)
            {
                dtpNgayBatDau.Value = Convert.ToDateTime(
                    row.Cells["NgayBatDau"].Value);
                dtpNgayBatDau.Checked = true;
            }
            else
            {
                dtpNgayBatDau.Checked = false;
            }

            if (row.Cells["NgayKetThuc"].Value != DBNull.Value)
            {
                dtpNgayKetThuc.Value = Convert.ToDateTime(
                    row.Cells["NgayKetThuc"].Value);
                dtpNgayKetThuc.Checked = true;
            }
            else
            {
                dtpNgayKetThuc.Checked = false;
            }

            cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
        }

        private void ClearForm()
        {
            txtMaDuAn.Clear();
            txtTenDuAn.Clear();

            dtpNgayBatDau.Checked = false;
            dtpNgayKetThuc.Checked = false;

            cboTrangThai.SelectedIndex = 0;

            dgvDuAn.ClearSelection();
            txtTenDuAn.Focus();
        }
    }
}
