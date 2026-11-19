
using System;
using System.Data;
using System.Data.SqlClient;
using ProjectTaskSystem.DTO;

namespace ProjectTaskSystem.DAL
{
    public class DuAnDAL
    {
        public DataTable GetAll()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "SELECT * FROM DuAn";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool Insert(DuAnDTO da)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"INSERT INTO DuAn
                    (TenDuAn, NgayBatDau, NgayKetThuc, TrangThai)
                    VALUES (@TenDuAn, @NgayBatDau, @NgayKetThuc, @TrangThai)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenDuAn", da.TenDuAn);
                cmd.Parameters.AddWithValue("@NgayBatDau",
                    da.NgayBatDau.HasValue ? (object)da.NgayBatDau.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayKetThuc",
                    da.NgayKetThuc.HasValue ? (object)da.NgayKetThuc.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", da.TrangThai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(DuAnDTO da)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"UPDATE DuAn SET
                    TenDuAn = @TenDuAn,
                    NgayBatDau = @NgayBatDau,
                    NgayKetThuc = @NgayKetThuc,
                    TrangThai = @TrangThai
                    WHERE MaDuAn = @MaDuAn";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDuAn", da.MaDuAn);
                cmd.Parameters.AddWithValue("@TenDuAn", da.TenDuAn);
                cmd.Parameters.AddWithValue("@NgayBatDau",
                    da.NgayBatDau.HasValue ? (object)da.NgayBatDau.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayKetThuc",
                    da.NgayKetThuc.HasValue ? (object)da.NgayKetThuc.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", da.TrangThai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int maDuAn)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "DELETE FROM DuAn WHERE MaDuAn = @MaDuAn";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDuAn", maDuAn);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
