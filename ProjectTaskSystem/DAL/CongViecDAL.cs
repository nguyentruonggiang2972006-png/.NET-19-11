
using System;
using System.Data;
using System.Data.SqlClient;
using ProjectTaskSystem.DTO;

namespace ProjectTaskSystem.DAL
{
    public class CongViecDAL
    {
        public DataTable GetAll()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"SELECT cv.*, da.TenDuAn
                               FROM CongViec cv
                               INNER JOIN DuAn da ON cv.MaDuAn = da.MaDuAn";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetByProjectAndStatus(int maDuAn, string trangThai)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"SELECT * FROM CongViec
                               WHERE (@MaDuAn = 0 OR MaDuAn = @MaDuAn)
                               AND (@TrangThai = N'Tất cả' OR TrangThai = @TrangThai)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDuAn", maDuAn);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool Insert(CongViecDTO cv)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"INSERT INTO CongViec
                    (MaDuAn, TenTask, MoTa, HanNop, DoUuTien, TrangThai)
                    VALUES
                    (@MaDuAn, @TenTask, @MoTa, @HanNop, @DoUuTien, @TrangThai)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDuAn", cv.MaDuAn);
                cmd.Parameters.AddWithValue("@TenTask", cv.TenTask);
                cmd.Parameters.AddWithValue("@MoTa",
                    string.IsNullOrWhiteSpace(cv.MoTa) ? (object)DBNull.Value : cv.MoTa);
                cmd.Parameters.AddWithValue("@HanNop",
                    cv.HanNop.HasValue ? (object)cv.HanNop.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@DoUuTien", cv.DoUuTien);
                cmd.Parameters.AddWithValue("@TrangThai", cv.TrangThai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(CongViecDTO cv)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"UPDATE CongViec SET
                    MaDuAn = @MaDuAn,
                    TenTask = @TenTask,
                    MoTa = @MoTa,
                    HanNop = @HanNop,
                    DoUuTien = @DoUuTien,
                    TrangThai = @TrangThai
                    WHERE MaTask = @MaTask";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTask", cv.MaTask);
                cmd.Parameters.AddWithValue("@MaDuAn", cv.MaDuAn);
                cmd.Parameters.AddWithValue("@TenTask", cv.TenTask);
                cmd.Parameters.AddWithValue("@MoTa",
                    string.IsNullOrWhiteSpace(cv.MoTa) ? (object)DBNull.Value : cv.MoTa);
                cmd.Parameters.AddWithValue("@HanNop",
                    cv.HanNop.HasValue ? (object)cv.HanNop.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@DoUuTien", cv.DoUuTien);
                cmd.Parameters.AddWithValue("@TrangThai", cv.TrangThai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int maTask)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "DELETE FROM CongViec WHERE MaTask = @MaTask";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTask", maTask);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateStatus(int maTask, string trangThai)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"UPDATE CongViec
                               SET TrangThai = @TrangThai
                               WHERE MaTask = @MaTask";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.Parameters.AddWithValue("@MaTask", maTask);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetOverdue()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"SELECT MaTask, TenTask, HanNop, TrangThai
                               FROM CongViec
                               WHERE HanNop < GETDATE()
                               AND TrangThai <> N'Hoàn thành'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }

                return dt;
            }
        }
    }
}
