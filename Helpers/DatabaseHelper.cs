using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using HoaDon.Model;
namespace HoaDon.Helpers
{
	public static class DatabaseHandler
	{
		#region Misc
		private static string strconn = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "sqlite.db;Version=3;";
		public static bool MaXacNhan(string str)
		{
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "Select data from caidat where rowid = 7";
					return str == cmd.ExecuteScalar().ToString() ? true : false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}
		public static bool KiemTraMaKichHoat(string str)
		{
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "Select data from caidat where rowid = 13";
					return str == cmd.ExecuteScalar().ToString() ? true : false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}
		public static void LuuMaKichHoat(string str)
		{
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "Update caidat set data = @Value where rowid = 13";
					cmd.Parameters.AddWithValue("@Value", str);
					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public static double[] ThongKe()
		{
			double[] arr = new double[12];
			for (int i = 0; i < 12; i++)
			{
				try
				{
					using (var conn = new SQLiteConnection(strconn))
					{
						conn.Open();
						var cmd = new SQLiteCommand(conn);
						cmd.CommandText = "select tien from khachhang where ngay >=" 
							+ (DateTime.Now.Year * 10000 + (i + 1) * 100) + " and ngay <=" 
							+ (DateTime.Now.Year * 10000 + (i + 1) * 100 + 31);
						arr[i] = 0;
						using (var reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								arr[i] += reader.GetInt32(0);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return new double[12] { 0,0,0,0,0,0, 0,0,0,0,0,0};
				}
			}
			return arr;
		}
		public static List<KhachHang> KhachHangCu(string dieuKien)
		{
			List<KhachHang> records = new List<KhachHang>();
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "select rowid, ten, sdt, ngay, data from KhachHangCu " + dieuKien;
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var record = new KhachHang();
							record.ID = reader.GetInt32(0);
							record.Ten = reader.GetString(1);
							record.SDT = reader.GetString(2);
							record.Ngay = reader.GetInt32(3);
							record.Data = reader.GetString(4);
							records.Add(record);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return records;
		}
		#endregion

		#region KhachHang Them Xoa
		public static bool ThemKhachHang(KhachHang _khach)
		{
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "insert into khachhang(ten, sdt, ngay, tien, data)" +
						"values(@Ten, @SDT, @ngay, @TongTien, @Data)";
					cmd.Parameters.AddWithValue("@Ten", _khach.Ten ?? "");
					cmd.Parameters.AddWithValue("@SDT", _khach.SDT ?? "");
					cmd.Parameters.AddWithValue("@ngay", _khach.Ngay);
					cmd.Parameters.AddWithValue("@TongTien", _khach.Tien ?? 0);
					cmd.Parameters.AddWithValue("@Data", _khach.Data);
					cmd.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}
		public static void XoaKhachHang(int ID)
		{
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "Delete from KhachHang where rowid = " + ID;
					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public static List<KhachHang> KhachHang(string dieuKien)
		{
			List<KhachHang> records = new List<KhachHang>();
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "select rowid,* from khachhang " + dieuKien;
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var record = new KhachHang();
							record.ID = reader.GetInt32(0);
							record.Ten = reader.GetString(1);
							record.SDT = reader.GetString(2);
							record.Ngay = reader.GetInt32(3);
							record.Tien = reader.GetInt32(4);
							record.Data = reader.GetString(5);
							records.Add(record);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return records;
		}
		#endregion

		#region CaiDat - KhoHang
		public static CaiDat CaiDat()
		{
			CaiDat qly = new CaiDat();
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "select * from caidat  where rowid = 1";
					qly.TenHoaDon = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 2";
					qly.TenCuaHang = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 3";
					qly.SDTCuaHang = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 4";
					qly.DiaChiCuaHang = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 5";
					qly.Logo = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 6";
					qly.LuuY = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 7";
					qly.MaXacNhan = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 8";
					qly.DiaChiKhachHang = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 9";
					qly.QuaTang = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 10";
					qly.BaoHanh = cmd.ExecuteScalar().ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return qly;
		}
		public static bool LuuCaiDat(CaiDat qly)
		{
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "Update caidat set data = @Value where rowid = 1";
					cmd.Parameters.AddWithValue("@Value", qly.TenHoaDon);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 2";
					cmd.Parameters.AddWithValue("@Value", qly.TenCuaHang);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 3";
					cmd.Parameters.AddWithValue("@Value", qly.SDTCuaHang);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 4";
					cmd.Parameters.AddWithValue("@Value", qly.DiaChiCuaHang);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 5";
					cmd.Parameters.AddWithValue("@Value", qly.Logo);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 6";
					cmd.Parameters.AddWithValue("@Value", qly.LuuY);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 7";
					cmd.Parameters.AddWithValue("@Value", qly.MaXacNhan);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 8";
					cmd.Parameters.AddWithValue("@Value", qly.DiaChiKhachHang);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 9";
					cmd.Parameters.AddWithValue("@Value", qly.QuaTang);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 10";
					cmd.Parameters.AddWithValue("@Value", qly.BaoHanh);
					cmd.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}
		
		public static KhoHang KhoHang()
		{
			KhoHang qly = new KhoHang();
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "select * from caidat  where rowid = 11";
					qly.TenMatHang = cmd.ExecuteScalar().ToString();
					cmd.CommandText = "select * from caidat  where rowid = 12";
					qly.QuyCachGiaTien = cmd.ExecuteScalar().ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return qly;
		}
		public static bool LuuKhoHang(KhoHang qly)
		{
			try
			{
				using (var conn = new SQLiteConnection(strconn))
				{
					conn.Open();
					var cmd = new SQLiteCommand(conn);
					cmd.CommandText = "Update caidat set data = @Value where rowid = 11";
					cmd.Parameters.AddWithValue("@Value", qly.TenMatHang);
					cmd.ExecuteNonQuery();
					cmd.CommandText = "Update caidat set data = @Value where rowid = 12";
					cmd.Parameters.AddWithValue("@Value", qly.QuyCachGiaTien);
					cmd.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		#endregion
	}

}
