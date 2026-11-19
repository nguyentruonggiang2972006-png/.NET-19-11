
using System.Data;
using ProjectTaskSystem.DAL;

namespace ProjectTaskSystem.BUS
{
    public class PhanCongBUS
    {
        private PhanCongDAL dal = new PhanCongDAL();

        public string Assign(int maTask, int maThanhVien)
        {
            if (maTask <= 0)
                return "Vui lòng chọn công việc!";

            if (maThanhVien <= 0)
                return "Vui lòng chọn thành viên!";

            if (dal.Assign(maTask, maThanhVien))
                return "Phân công công việc thành công!";

            return "Phân công thất bại!";
        }

        public DataTable GetByTask(int maTask)
        {
            return dal.GetByTask(maTask);
        }

        public DataTable GetCompletionStatistics(int maDuAn)
        {
            return dal.GetCompletionStatistics(maDuAn);
        }
    }
}
