
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectTaskSystem.GUI
{
    public class frmMain : Form
    {
        private Panel pnlMenu;
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblWelcome;

        private Button btnDuAn;
        private Button btnThanhVien;
        private Button btnCongViec;
        private Button btnPhanCong;
        private Button btnQuaHan;
        private Button btnThoat;

        public frmMain()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "PROJECT & TASK MANAGEMENT";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.MinimumSize = new Size(900, 600);

            pnlMenu = new Panel();
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Width = 230;
            pnlMenu.BackColor = Color.FromArgb(35, 45, 65);

            pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 85;
            pnlHeader.BackColor = Color.FromArgb(245, 247, 250);

            lblTitle = new Label();
            lblTitle.Text = "QUẢN LÝ CÔNG VIỆC";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(35, 45, 65);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 20);

            lblWelcome = new Label();
            lblWelcome.Text = "PROJECT & TASK MANAGEMENT SYSTEM";
            lblWelcome.Font = new Font("Segoe UI", 10);
            lblWelcome.ForeColor = Color.Gray;
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(35, 65);

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblWelcome);

            Label lblLogo = new Label();
            lblLogo.Text = "PROJECT MANAGER";
            lblLogo.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Height = 100;

            pnlMenu.Controls.Add(lblLogo);

            btnDuAn = CreateMenuButton("Quản lý dự án", 120);
            btnThanhVien = CreateMenuButton("Quản lý thành viên", 180);
            btnCongViec = CreateMenuButton("Quản lý công việc", 240);
            btnPhanCong = CreateMenuButton("Phân công công việc", 300);
            btnQuaHan = CreateMenuButton("Công việc quá hạn", 360);
            btnThoat = CreateMenuButton("Thoát chương trình", 440);

            btnDuAn.Click += (s, e) =>
            {
                using (frmDuAn f = new frmDuAn())
                    f.ShowDialog(this);
            };

            btnThanhVien.Click += (s, e) =>
            {
                using (frmThanhVien f = new frmThanhVien())
                    f.ShowDialog(this);
            };

            btnCongViec.Click += (s, e) =>
            {
                using (frmCongViec f = new frmCongViec())
                    f.ShowDialog(this);
            };

            btnPhanCong.Click += (s, e) =>
            {
                using (frmPhanCong f = new frmPhanCong())
                    f.ShowDialog(this);
            };

            btnQuaHan.Click += (s, e) =>
            {
                using (frmCongViec f = new frmCongViec())
                {
                    f.ShowDialog(this);
                }
            };
            btnThoat.Click += (s, e) =>
            {
                if (MessageBox.Show("Bạn có muốn thoát chương trình?",
                    "Xác nhận", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };

            pnlMenu.Controls.AddRange(new Control[]
            {
                btnDuAn,
                btnThanhVien,
                btnCongViec,
                btnPhanCong,
                btnQuaHan,
                btnThoat
            });

            Panel pnlContent = new Panel();
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.BackColor = Color.FromArgb(248, 249, 252);

            Label lblContent = new Label();
            lblContent.Text = "CHÀO MỪNG BẠN ĐẾN VỚI HỆ THỐNG\n\n" +
                              "PROJECT & TASK MANAGEMENT\n\n" +
                              "Quản lý dự án - Phân công - Theo dõi tiến độ";
            lblContent.Font = new Font("Segoe UI", 17, FontStyle.Bold);
            lblContent.ForeColor = Color.FromArgb(50, 65, 90);
            lblContent.TextAlign = ContentAlignment.MiddleCenter;
            lblContent.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(lblContent);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlMenu);
        }

        private Button CreateMenuButton(string text, int top)
        {
            Button btn = new Button();

            btn.Text = text;
            btn.Location = new Point(10, top);
            btn.Size = new Size(210, 48);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(35, 45, 65);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.FromArgb(55, 75, 110);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = Color.FromArgb(35, 45, 65);
            };

            return btn;
        }

        private void OpenForm(string title)
        {
            Form form = new Form();

            form.Text = title;
            form.Size = new Size(900, 600);
            form.StartPosition = FormStartPosition.CenterParent;
            form.BackColor = Color.White;

            Label lbl = new Label();
            lbl.Text = title + "\n\nGiao diện đang được xây dựng.";
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            form.Controls.Add(lbl);
            form.ShowDialog(this);
        }
    }
}
