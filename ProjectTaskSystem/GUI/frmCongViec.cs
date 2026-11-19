
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectTaskSystem.BUS;
using ProjectTaskSystem.DTO;

namespace ProjectTaskSystem.GUI
{
    public class frmCongViec : Form
    {
        private CongViecBUS bus = new CongViecBUS();
        private DuAnBUS duAnBus = new DuAnBUS();

        private TextBox txtMa, txtTen, txtMoTa;
        private ComboBox cboDuAn, cboUuTien, cboTrangThai, cboLocTrangThai;
        private DateTimePicker dtpHanNop;
        private DataGridView dgv;

        public frmCongViec()
        {
            InitializeComponent();
            LoadProjects();
            LoadData();
        }

        private void InitializeComponent()
        {
            Text = "QUẢN LÝ CÔNG VIỆC";
            Size = new Size(1150, 760);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 250);

            Label title = new Label
            {
                Text = "QUẢN LÝ CÔNG VIỆC",
                Font = new Font("Segoe UI", 19, FontStyle.Bold),
                Location = new Point(25, 15),
                AutoSize = true,
                ForeColor = Color.FromArgb(35, 45, 65)
            };

            Panel panel = new Panel
            {
                Location = new Point(25, 65),
                Size = new Size(1080, 280),
                BackColor = Color.White
            };

            panel.Controls.Add(MakeLabel("Mã công việc:", 20, 15));
            panel.Controls.Add(MakeLabel("Tên công việc:", 20, 55));
            panel.Controls.Add(MakeLabel("Dự án:", 20, 95));
            panel.Controls.Add(MakeLabel("Mô tả:", 20, 135));

            panel.Controls.Add(MakeLabel("Hạn nộp:", 550, 15));
            panel.Controls.Add(MakeLabel("Độ ưu tiên:", 550, 55));
            panel.Controls.Add(MakeLabel("Trạng thái:", 550, 95));

            txtMa = MakeTextBox(145, 15, 300);
            txtMa.ReadOnly = true;
            txtMa.BackColor = Color.LightGray;

            txtTen = MakeTextBox(145, 55, 300);

            cboDuAn = new ComboBox
            {
                Location = new Point(145, 95),
                Size = new Size(300, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            txtMoTa = MakeTextBox(145, 135, 850);
            txtMoTa.Multiline = true;
            txtMoTa.Height = 55;

            dtpHanNop = new DateTimePicker
            {
                Location = new Point(680, 15),
                Size = new Size(300, 28),
                Format = DateTimePickerFormat.Short
            };

            cboUuTien = MakeComboBox(680, 55, 300,
                "Thấp", "Trung bình", "Cao", "Khẩn cấp");

            cboTrangThai = MakeComboBox(680, 95, 300,
                "Chưa bắt đầu", "Đang thực hiện", "Hoàn thành");

            Button btnThem = MakeButton("Thêm", 145, 210, Color.SeaGreen);
            Button btnSua = MakeButton("Sửa", 275, 210, Color.RoyalBlue);
            Button btnXoa = MakeButton("Xóa", 405, 210, Color.IndianRed);
            Button btnMoi = MakeButton("Làm mới", 535, 210, Color.Gray);

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnMoi.Click += (s, e) => { ClearForm(); LoadData(); };

            panel.Controls.AddRange(new Control[]
            {
                txtMa, txtTen, cboDuAn, txtMoTa,
                dtpHanNop, cboUuTien, cboTrangThai,
                btnThem, btnSua, btnXoa, btnMoi
            });

            Label lblFilter = MakeLabel("Lọc theo dự án:", 25, 365);
            cboLocTrangThai = MakeComboBox(650, 360, 250,
                "Tất cả", "Chưa bắt đầu", "Đang thực hiện", "Hoàn thành");

            Label lblStatus = MakeLabel("Trạng thái:", 550, 365);

            Button btnLoc = MakeButton("Lọc", 920, 357, Color.RoyalBlue);
            Button btnTatCa = MakeButton("Tất cả", 1020, 357, Color.Gray);
            Button btnQuaHan = MakeButton("Xem quá hạn", 25, 405, Color.DarkOrange);

            btnLoc.Click += (s, e) => FilterData();
            btnTatCa.Click += (s, e) => LoadData();
            btnQuaHan.Click += BtnQuaHan_Click;

            dgv = new DataGridView
            {
                Location = new Point(25, 455),
                Size = new Size(1080, 245),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            dgv.CellClick += Dgv_CellClick;

            Controls.Add(title);
            Controls.Add(panel);
            Controls.Add(lblFilter);
            Controls.Add(lblStatus);
            Controls.Add(cboLocTrangThai);
            Controls.Add(btnLoc);
            Controls.Add(btnTatCa);
            Controls.Add(btnQuaHan);
            Controls.Add(dgv);
        }

        private Label MakeLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
        }

        private TextBox MakeTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 28),
                Font = new Font("Segoe UI", 9)
            };
        }

        private ComboBox MakeComboBox(int x, int y, int width, params string[] items)
        {
            ComboBox cbo = new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cbo.Items.AddRange(items);
            if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;

            return cbo;
        }

        private Button MakeButton(string text, int x, int y, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(100, 36),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void LoadProjects()
        {
            try
            {
                DataTable dt = duAnBus.GetAll();

                cboDuAn.DataSource = dt.Copy();
                cboDuAn.DisplayMember = "TenDuAn";
                cboDuAn.ValueMember = "MaDuAn";

                DataTable filterTable = dt.Copy();
                DataRow allRow = filterTable.NewRow();
                allRow["MaDuAn"] = 0;
                allRow["TenDuAn"] = "Tất cả dự án";
                filterTable.Rows.InsertAt(allRow, 0);

                cboLocTrangThai.DataSource = null;
                cboLocTrangThai.Items.Clear();

                // ComboBox lọc trạng thái được tạo riêng ở phần giao diện.
                // Dự án lọc được chọn qua hộp chọn dự án chính.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dự án: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                dgv.DataSource = bus.GetAll();
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải công việc: " + ex.Message);
            }
        }

        private void FilterData()
        {
            try
            {
                int maDuAn = cboDuAn.SelectedValue == null
                    ? 0 : Convert.ToInt32(cboDuAn.SelectedValue);

                string trangThai = cboLocTrangThai.Text;

                dgv.DataSource = bus.GetByProjectAndStatus(maDuAn, trangThai);
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc dữ liệu: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            if (dgv.Columns["MaTask"] != null)
                dgv.Columns["MaTask"].HeaderText = "Mã công việc";

            if (dgv.Columns["MaDuAn"] != null)
                dgv.Columns["MaDuAn"].HeaderText = "Mã dự án";

            if (dgv.Columns["TenTask"] != null)
                dgv.Columns["TenTask"].HeaderText = "Tên công việc";

            if (dgv.Columns["MoTa"] != null)
                dgv.Columns["MoTa"].HeaderText = "Mô tả";

            if (dgv.Columns["HanNop"] != null)
                dgv.Columns["HanNop"].HeaderText = "Hạn nộp";

            if (dgv.Columns["DoUuTien"] != null)
                dgv.Columns["DoUuTien"].HeaderText = "Độ ưu tiên";

            if (dgv.Columns["TrangThai"] != null)
                dgv.Columns["TrangThai"].HeaderText = "Trạng thái";

            if (dgv.Columns["TenDuAn"] != null)
                dgv.Columns["TenDuAn"].HeaderText = "Tên dự án";
        }

        private CongViecDTO GetData()
        {
            CongViecDTO cv = new CongViecDTO();

            if (!string.IsNullOrWhiteSpace(txtMa.Text))
                cv.MaTask = int.Parse(txtMa.Text);

            cv.MaDuAn = Convert.ToInt32(cboDuAn.SelectedValue);
            cv.TenTask = txtTen.Text.Trim();
            cv.MoTa = txtMoTa.Text.Trim();
            cv.HanNop = dtpHanNop.Value;
            cv.DoUuTien = cboUuTien.Text;
            cv.TrangThai = cboTrangThai.Text;

            return cv;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(bus.Insert(GetData()));
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm công việc: " + ex.Message);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn công việc cần sửa!");
                return;
            }

            try
            {
                MessageBox.Show(bus.Update(GetData()));
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa công việc: " + ex.Message);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn công việc cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa công việc này?",
                "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    MessageBox.Show(bus.Delete(int.Parse(txtMa.Text)));
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa công việc: " + ex.Message);
                }
            }
        }

        private void BtnQuaHan_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.DataSource = bus.GetOverdue();
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải công việc quá hạn: " + ex.Message);
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgv.Rows[e.RowIndex];

            if (!dgv.Columns.Contains("MaTask")) return;

            txtMa.Text = Convert.ToString(row.Cells["MaTask"].Value);
            txtTen.Text = Convert.ToString(row.Cells["TenTask"].Value);
            txtMoTa.Text = Convert.ToString(row.Cells["MoTa"].Value);
            dtpHanNop.Value = Convert.ToDateTime(row.Cells["HanNop"].Value);

            cboUuTien.Text = Convert.ToString(row.Cells["DoUuTien"].Value);
            cboTrangThai.Text = Convert.ToString(row.Cells["TrangThai"].Value);

            if (row.Cells["MaDuAn"].Value != DBNull.Value)
                cboDuAn.SelectedValue = Convert.ToInt32(row.Cells["MaDuAn"].Value);
        }

        private void ClearForm()
        {
            txtMa.Clear();
            txtTen.Clear();
            txtMoTa.Clear();

            if (cboDuAn.Items.Count > 0)
                cboDuAn.SelectedIndex = 0;

            cboUuTien.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            dtpHanNop.Value = DateTime.Today;

            dgv.ClearSelection();
            txtTen.Focus();
        }
    }
}
