
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectTaskSystem.BUS;
using ProjectTaskSystem.DTO;

namespace ProjectTaskSystem.GUI
{
    public class frmThanhVien : Form
    {
        private ThanhVienBUS bus = new ThanhVienBUS();

        private TextBox txtMa, txtHoTen, txtEmail, txtChuyenMon;
        private DataGridView dgv;

        public frmThanhVien()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            Text = "QUẢN LÝ THÀNH VIÊN";
            Size = new Size(950, 620);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 250);

            Label title = new Label
            {
                Text = "QUẢN LÝ THÀNH VIÊN",
                Font = new Font("Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 45, 65),
                Location = new Point(25, 20),
                AutoSize = true
            };

            Panel panel = new Panel
            {
                Location = new Point(25, 75),
                Size = new Size(880, 190),
                BackColor = Color.White
            };

            panel.Controls.Add(MakeLabel("Mã thành viên:", 20, 20));
            panel.Controls.Add(MakeLabel("Họ tên:", 20, 60));
            panel.Controls.Add(MakeLabel("Email:", 450, 20));
            panel.Controls.Add(MakeLabel("Chuyên môn:", 450, 60));

            txtMa = MakeTextBox(145, 20, 240);
            txtMa.ReadOnly = true;
            txtMa.BackColor = Color.LightGray;

            txtHoTen = MakeTextBox(145, 60, 240);
            txtEmail = MakeTextBox(575, 20, 250);
            txtChuyenMon = MakeTextBox(575, 60, 250);

            Button btnThem = MakeButton("Thêm", 145, 115, Color.SeaGreen);
            Button btnSua = MakeButton("Sửa", 280, 115, Color.RoyalBlue);
            Button btnXoa = MakeButton("Xóa", 415, 115, Color.IndianRed);
            Button btnMoi = MakeButton("Làm mới", 550, 115, Color.Gray);

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnMoi.Click += (s, e) => { ClearForm(); LoadData(); };

            panel.Controls.AddRange(new Control[]
            {
                txtMa, txtHoTen, txtEmail, txtChuyenMon,
                btnThem, btnSua, btnXoa, btnMoi
            });

            dgv = new DataGridView
            {
                Location = new Point(25, 290),
                Size = new Size(880, 260),
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
            Controls.Add(dgv);
        }

        private Label MakeLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
        }

        private TextBox MakeTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 28),
                Font = new Font("Segoe UI", 10)
            };
        }

        private Button MakeButton(string text, int x, int y, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(115, 38),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void LoadData()
        {
            try
            {
                dgv.DataSource = bus.GetAll();

                if (dgv.Columns["MaThanhVien"] != null)
                    dgv.Columns["MaThanhVien"].HeaderText = "Mã thành viên";

                if (dgv.Columns["HoTen"] != null)
                    dgv.Columns["HoTen"].HeaderText = "Họ tên";

                if (dgv.Columns["Email"] != null)
                    dgv.Columns["Email"].HeaderText = "Email";

                if (dgv.Columns["ChuyenMon"] != null)
                    dgv.Columns["ChuyenMon"].HeaderText = "Chuyên môn";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private ThanhVienDTO GetData()
        {
            ThanhVienDTO tv = new ThanhVienDTO();

            if (!string.IsNullOrWhiteSpace(txtMa.Text))
                tv.MaThanhVien = int.Parse(txtMa.Text);

            tv.HoTen = txtHoTen.Text.Trim();
            tv.Email = txtEmail.Text.Trim();
            tv.ChuyenMon = txtChuyenMon.Text.Trim();

            return tv;
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
                MessageBox.Show("Lỗi thêm thành viên: " + ex.Message);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMa.Text))
                {
                    MessageBox.Show("Vui lòng chọn thành viên cần sửa!");
                    return;
                }

                MessageBox.Show(bus.Update(GetData()));
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa thành viên: " + ex.Message);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn thành viên cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa thành viên này?",
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
                    MessageBox.Show("Không thể xóa thành viên: " + ex.Message);
                }
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgv.Rows[e.RowIndex];

            txtMa.Text = Convert.ToString(row.Cells["MaThanhVien"].Value);
            txtHoTen.Text = Convert.ToString(row.Cells["HoTen"].Value);
            txtEmail.Text = Convert.ToString(row.Cells["Email"].Value);
            txtChuyenMon.Text = Convert.ToString(row.Cells["ChuyenMon"].Value);
        }

        private void ClearForm()
        {
            txtMa.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtChuyenMon.Clear();
            dgv.ClearSelection();
            txtHoTen.Focus();
        }
    }
}
