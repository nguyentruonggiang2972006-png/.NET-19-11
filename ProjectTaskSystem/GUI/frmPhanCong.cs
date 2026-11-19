
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectTaskSystem.BUS;

namespace ProjectTaskSystem.GUI
{
    public class frmPhanCong : Form
    {
        private PhanCongBUS phanCongBus = new PhanCongBUS();
        private CongViecBUS congViecBus = new CongViecBUS();
        private ThanhVienBUS thanhVienBus = new ThanhVienBUS();
        private DuAnBUS duAnBus = new DuAnBUS();

        private ComboBox cboCongViec;
        private ComboBox cboThanhVien;
        private ComboBox cboDuAnThongKe;

        private DataGridView dgvPhanCong;
        private DataGridView dgvThongKe;

        public frmPhanCong()
        {
            InitializeComponent();
            LoadComboBox();
        }

        private void InitializeComponent()
        {
            Text = "PHÂN CÔNG CÔNG VIỆC";
            Size = new Size(1100, 720);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 250);

            Label title = new Label
            {
                Text = "PHÂN CÔNG VÀ THEO DÕI TIẾN ĐỘ",
                Location = new Point(25, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 45, 65)
            };

            Panel pnlAssign = new Panel
            {
                Location = new Point(25, 65),
                Size = new Size(1030, 185),
                BackColor = Color.White
            };

            Label lblTask = MakeLabel("Công việc:", 20, 25);
            Label lblMember = MakeLabel("Thành viên:", 20, 75);

            cboCongViec = MakeComboBox(130, 20, 650);
            cboThanhVien = MakeComboBox(130, 70, 650);

            Button btnPhanCong = MakeButton(
                "Phân công", 800, 20, Color.SeaGreen);

            Button btnXem = MakeButton(
                "Xem phân công", 800, 70, Color.RoyalBlue);

            btnPhanCong.Click += BtnPhanCong_Click;
            btnXem.Click += BtnXem_Click;

            pnlAssign.Controls.AddRange(new Control[]
            {
                lblTask, lblMember,
                cboCongViec, cboThanhVien,
                btnPhanCong, btnXem
            });

            Label lblDanhSach = MakeLabel(
                "DANH SÁCH THÀNH VIÊN ĐƯỢC PHÂN CÔNG", 25, 265);

            dgvPhanCong = CreateGrid(25, 295, 1030, 125);

            Panel pnlThongKe = new Panel
            {
                Location = new Point(25, 440),
                Size = new Size(1030, 220),
                BackColor = Color.White
            };

            Label lblThongKe = MakeLabel(
                "THỐNG KÊ TIẾN ĐỘ THEO THÀNH VIÊN", 20, 15);

            Label lblDuAn = MakeLabel("Chọn dự án:", 20, 60);

            cboDuAnThongKe = MakeComboBox(130, 55, 450);

            Button btnThongKe = MakeButton(
                "Thống kê", 610, 53, Color.DarkOrange);

            btnThongKe.Click += BtnThongKe_Click;

            dgvThongKe = CreateGrid(20, 105, 990, 100);

            pnlThongKe.Controls.AddRange(new Control[]
            {
                lblThongKe, lblDuAn,
                cboDuAnThongKe, btnThongKe, dgvThongKe
            });

            Controls.Add(title);
            Controls.Add(pnlAssign);
            Controls.Add(lblDanhSach);
            Controls.Add(dgvPhanCong);
            Controls.Add(pnlThongKe);
        }

        private Label MakeLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
        }

        private ComboBox MakeComboBox(int x, int y, int width)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private Button MakeButton(string text, int x, int y, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(150, 38),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private DataGridView CreateGrid(int x, int y, int width, int height)
        {
            return new DataGridView
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                ReadOnly = true,
                AllowUserToAddRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };
        }

        private void LoadComboBox()
        {
            try
            {
                DataTable dtTask = congViecBus.GetAll();

                cboCongViec.DataSource = dtTask;
                cboCongViec.DisplayMember = "TenTask";
                cboCongViec.ValueMember = "MaTask";

                DataTable dtThanhVien = thanhVienBus.GetAll();

                cboThanhVien.DataSource = dtThanhVien;
                cboThanhVien.DisplayMember = "HoTen";
                cboThanhVien.ValueMember = "MaThanhVien";

                DataTable dtDuAn = duAnBus.GetAll();

                cboDuAnThongKe.DataSource = dtDuAn;
                cboDuAnThongKe.DisplayMember = "TenDuAn";
                cboDuAnThongKe.ValueMember = "MaDuAn";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void BtnPhanCong_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboCongViec.SelectedValue == null ||
                    cboThanhVien.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn công việc và thành viên!");
                    return;
                }

                int maTask = Convert.ToInt32(cboCongViec.SelectedValue);
                int maThanhVien = Convert.ToInt32(cboThanhVien.SelectedValue);

                string result = phanCongBus.Assign(maTask, maThanhVien);

                MessageBox.Show(result);
                LoadAssignments();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi phân công: " + ex.Message);
            }
        }

        private void BtnXem_Click(object sender, EventArgs e)
        {
            LoadAssignments();
        }

        private void LoadAssignments()
        {
            try
            {
                if (cboCongViec.SelectedValue == null)
                    return;

                int maTask = Convert.ToInt32(cboCongViec.SelectedValue);

                dgvPhanCong.DataSource = phanCongBus.GetByTask(maTask);

                if (dgvPhanCong.Columns["MaThanhVien"] != null)
                    dgvPhanCong.Columns["MaThanhVien"].HeaderText = "Mã thành viên";

                if (dgvPhanCong.Columns["HoTen"] != null)
                    dgvPhanCong.Columns["HoTen"].HeaderText = "Họ tên";

                if (dgvPhanCong.Columns["Email"] != null)
                    dgvPhanCong.Columns["Email"].HeaderText = "Email";

                if (dgvPhanCong.Columns["NgayGiao"] != null)
                    dgvPhanCong.Columns["NgayGiao"].HeaderText = "Ngày giao";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phân công: " + ex.Message);
            }
        }

        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboDuAnThongKe.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn dự án!");
                    return;
                }

                int maDuAn = Convert.ToInt32(cboDuAnThongKe.SelectedValue);

                dgvThongKe.DataSource =
                    phanCongBus.GetCompletionStatistics(maDuAn);

                if (dgvThongKe.Columns["MaThanhVien"] != null)
                    dgvThongKe.Columns["MaThanhVien"].HeaderText = "Mã thành viên";

                if (dgvThongKe.Columns["HoTen"] != null)
                    dgvThongKe.Columns["HoTen"].HeaderText = "Họ tên";

                if (dgvThongKe.Columns["TongCongViec"] != null)
                    dgvThongKe.Columns["TongCongViec"].HeaderText = "Tổng công việc";

                if (dgvThongKe.Columns["CongViecHoanThanh"] != null)
                    dgvThongKe.Columns["CongViecHoanThanh"].HeaderText = "Đã hoàn thành";

                if (dgvThongKe.Columns["TyLeHoanThanh"] != null)
                    dgvThongKe.Columns["TyLeHoanThanh"].HeaderText = "Tỷ lệ hoàn thành (%)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê: " + ex.Message);
            }
        }
    }
}
