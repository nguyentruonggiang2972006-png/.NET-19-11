
using System;
using System.Data;
using ProjectTaskSystem.DAL;
using ProjectTaskSystem.DTO;

namespace ProjectTaskSystem.BUS
{
    public class CongViecBUS
    {
        private CongViecDAL dal = new CongViecDAL();

        public DataTable GetAll()
        {
            return dal.GetAll();
        }

        public DataTable GetByProjectAndStatus(int maDuAn, string trangThai)
        {
            return dal.GetByProjectAndStatus(maDuAn, trangThai);
        }

        public string Insert(CongViecDTO cv)
        {
            if (cv.MaDuAn <= 0)
                return "Vui lòng chọn dự án!";

            if (string.IsNullOrWhiteSpace(cv.TenTask))
                return "Tên công việc không được để trống!";

            if (string.IsNullOrWhiteSpace(cv.TrangThai))
                return "Vui lòng chọn trạng thái!";

            if (cv.HanNop.HasValue && cv.HanNop.Value.Date < DateTime.Today)
                return "Hạn nộp không được nhỏ hơn ngày hiện tại!";

            if (dal.Insert(cv))
                return "Thêm công việc thành công!";

            return "Thêm công việc thất bại!";
        }

        public string Update(CongViecDTO cv)
        {
            if (cv.MaTask <= 0)
                return "Mã công việc không hợp lệ!";

            if (string.IsNullOrWhiteSpace(cv.TenTask))
                return "Tên công việc không được để trống!";

            if (cv.MaDuAn <= 0)
                return "Vui lòng chọn dự án!";

            if (dal.Update(cv))
                return "Cập nhật công việc thành công!";

            return "Cập nhật công việc thất bại!";
        }

        public string Delete(int maTask)
        {
            if (maTask <= 0)
                return "Mã công việc không hợp lệ!";

            if (dal.Delete(maTask))
                return "Xóa công việc thành công!";

            return "Không tìm thấy công việc!";
        }

        public string UpdateStatus(int maTask, string trangThai)
        {
            if (maTask <= 0)
                return "Mã công việc không hợp lệ!";

            if (string.IsNullOrWhiteSpace(trangThai))
                return "Vui lòng chọn trạng thái!";

            if (dal.UpdateStatus(maTask, trangThai))
                return "Cập nhật trạng thái thành công!";

            return "Cập nhật trạng thái thất bại!";
        }

        public DataTable GetOverdue()
        {
            return dal.GetOverdue();
        }
    }
}
