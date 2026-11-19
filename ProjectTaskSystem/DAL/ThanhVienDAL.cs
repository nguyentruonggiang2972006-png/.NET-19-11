
using System.Data;
using System.Data.SqlClient;
using ProjectTaskSystem.DTO;

namespace ProjectTaskSystem.DAL
{
    public class ThanhVienDAL
    {
        public DataTable GetAll()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM ThanhVien", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool Insert(ThanhVienDTO tv)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"INSERT INTO ThanhVien
                    (HoTen, Email, ChuyenMon)
                    VALUES (@HoTen, @Email, @ChuyenMon)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HoTen", tv.HoTen);
                cmd.Parameters.AddWithValue("@Email",
                    string.IsNullOrWhiteSpace(tv.Email)
                    ? (object)System.DBNull.Value : tv.Email);
                cmd.Parameters.AddWithValue("@ChuyenMon", tv.ChuyenMon);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(ThanhVienDTO tv)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"UPDATE ThanhVien SET
                    HoTen = @HoTen,
                    Email = @Email,
                    ChuyenMon = @ChuyenMon
                    WHERE MaThanhVien = @MaThanhVien";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaThanhVien", tv.MaThanhVien);
                cmd.Parameters.AddWithValue("@HoTen", tv.HoTen);
                cmd.Parameters.AddWithValue("@Email",
                    string.IsNullOrWhiteSpace(tv.Email)
                    ? (object)System.DBNull.Value : tv.Email);
                cmd.Parameters.AddWithValue("@ChuyenMon", tv.ChuyenMon);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int maThanhVien)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "DELETE FROM ThanhVien WHERE MaThanhVien = @MaThanhVien";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaThanhVien", maThanhVien);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
