
using System.Data;
using ProjectTaskSystem.DAL;
using ProjectTaskSystem.DTO;

namespace ProjectTaskSystem.BUS
{
    public class ThanhVienBUS
    {
        private ThanhVienDAL dal = new ThanhVienDAL();

        public DataTable GetAll()
        {
            return dal.GetAll();
        }

        public string Insert(ThanhVienDTO tv)
        {
            if (string.IsNullOrWhiteSpace(tv.HoTen))
                return "Họ tên thành viên không được để trống!";

            if (dal.Insert(tv))
                return "Thêm thành viên thành công!";

            return "Thêm thành viên thất bại!";
        }

        public string Update(ThanhVienDTO tv)
        {
            if (tv.MaThanhVien <= 0)
                return "Mã thành viên không hợp lệ!";

            if (string.IsNullOrWhiteSpace(tv.HoTen))
                return "Họ tên không được để trống!";

            if (dal.Update(tv))
                return "Cập nhật thành viên thành công!";

            return "Cập nhật thành viên thất bại!";
        }

        public string Delete(int maThanhVien)
        {
            if (maThanhVien <= 0)
                return "Mã thành viên không hợp lệ!";

            if (dal.Delete(maThanhVien))
                return "Xóa thành viên thành công!";

            return "Không tìm thấy thành viên!";
        }
    }
}
