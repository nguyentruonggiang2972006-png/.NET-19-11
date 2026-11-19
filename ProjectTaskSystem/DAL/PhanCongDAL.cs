
using System.Data;
using System.Data.SqlClient;

namespace ProjectTaskSystem.DAL
{
    public class PhanCongDAL
    {
        public bool Assign(int maTask, int maThanhVien)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"INSERT INTO PhanCong
                    (MaTask, MaThanhVien)
                    VALUES (@MaTask, @MaThanhVien)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTask", maTask);
                cmd.Parameters.AddWithValue("@MaThanhVien", maThanhVien);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetByTask(int maTask)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"SELECT tv.MaThanhVien, tv.HoTen,
                                      tv.Email, pc.NgayGiao
                               FROM PhanCong pc
                               INNER JOIN ThanhVien tv
                               ON pc.MaThanhVien = tv.MaThanhVien
                               WHERE pc.MaTask = @MaTask";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTask", maTask);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetCompletionStatistics(int maDuAn)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"SELECT
                                   tv.MaThanhVien,
                                   tv.HoTen,
                                   COUNT(cv.MaTask) AS TongCongViec,
                                   SUM(CASE
                                       WHEN cv.TrangThai = N'Hoàn thành'
                                       THEN 1 ELSE 0
                                   END) AS CongViecHoanThanh,
                                   CAST(
                                       SUM(CASE
                                           WHEN cv.TrangThai = N'Hoàn thành'
                                           THEN 1.0 ELSE 0
                                       END) * 100 /
                                       NULLIF(COUNT(cv.MaTask), 0)
                                   AS DECIMAL(5,2)) AS TyLeHoanThanh
                               FROM ThanhVien tv
                               INNER JOIN PhanCong pc
                                   ON tv.MaThanhVien = pc.MaThanhVien
                               INNER JOIN CongViec cv
                                   ON pc.MaTask = cv.MaTask
                               WHERE cv.MaDuAn = @MaDuAn
                               GROUP BY tv.MaThanhVien, tv.HoTen";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDuAn", maDuAn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
