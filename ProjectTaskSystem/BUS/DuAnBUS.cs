
using System.Data;
using ProjectTaskSystem.DAL;
using ProjectTaskSystem.DTO;

namespace ProjectTaskSystem.BUS
{
    public class DuAnBUS
    {
        private DuAnDAL dal = new DuAnDAL();

        public DataTable GetAll()
        {
            return dal.GetAll();
        }

        public string Insert(DuAnDTO da)
        {
            if (string.IsNullOrWhiteSpace(da.TenDuAn))
                return "Tên dự án không được để trống!";

            if (dal.Insert(da))
                return "Thêm dự án thành công!";

            return "Thêm dự án thất bại!";
        }

        public string Update(DuAnDTO da)
        {
            if (da.MaDuAn <= 0)
                return "Mã dự án không hợp lệ!";

            if (string.IsNullOrWhiteSpace(da.TenDuAn))
                return "Tên dự án không được để trống!";

            if (dal.Update(da))
                return "Cập nhật dự án thành công!";

            return "Cập nhật dự án thất bại!";
        }

        public string Delete(int maDuAn)
        {
            if (maDuAn <= 0)
                return "Mã dự án không hợp lệ!";

            if (dal.Delete(maDuAn))
                return "Xóa dự án thành công!";

            return "Không tìm thấy dự án!";
        }
    }
}
