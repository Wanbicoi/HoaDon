using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoaDon.Model
{
	public class KhachHang
	{
		public int ID { get; set; }
		public string Ten { get; set; }
		public string SDT { get; set; }
		public string DiaChi { get; set; }
		public int Ngay { get; set; }
		public int? Tien { get; set; }
		public int? DaThanhToan { get; set; }
		public string Data { get; set; }
	}
}
