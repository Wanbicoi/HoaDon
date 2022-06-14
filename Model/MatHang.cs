using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoaDon.Model
{
	public class MatHang 
	{
		public int ID { get; set; }
		public string TenMatHang { get; set; }
		public string QuyCach { get; set; }
		public int? GiaTien { get; set; }
		public string TrietKhau { get; set; }
	}
}
