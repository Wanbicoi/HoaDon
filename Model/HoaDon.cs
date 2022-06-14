using System.Collections.Generic;

namespace HoaDon.Model
{
	public class HoaDon
	{
		public KhachHang KhachHang { get; set; }
		public bool DatTruoc { get; set; }
		public int? TienDatTruoc { get; set; }

		public string BaoHanh { get; set; }
		public string QuaTang { get; set; }
		public bool SoDoLapTheoYeuCau { get; set; }

		public bool ThongSoTrongKinh { get; set; }
		public string MatPhai { get; set; }
		public string MatTrai { get; set; }

		public bool HienThiThiLuc { get; set; }
		public string[] ThiLuc { get; set; }

		public string Data { get; set; }
		public System.Collections.ObjectModel.ObservableCollection<MatHang> MatHangs { get; set; }
		public HoaDon()
		{
			ThiLuc = new string[9];
			MatHangs = new System.Collections.ObjectModel.ObservableCollection<MatHang>();
			KhachHang = new KhachHang();
		}
	}
}
