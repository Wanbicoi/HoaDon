using System;
using System.Windows.Controls;

namespace HoaDon
{
	/// <summary>
	/// Interaction logic for pgTrangChu.xaml
	/// </summary>
	public partial class pgTrangChu : Page
	{
		public pgTrangChu()
		{
			InitializeComponent();
			var list = Helpers.DatabaseHandler.KhachHang(
				" where ngay = " + Helpers.MiscHelper.FormatDate(DateTime.Now));
			lbKhachHangs.ItemsSource = list;
			txbTongDonHang.Text += " " + list.Count;
			int? tong = 0;
			foreach (var item in list)
			{
				tong += item.Tien;
			}
			txbTongDoangThu.Text += " " + String.Format("{0:#,0}", tong) + ",000 VNĐ";
		}


		private void lbKhachHangs_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (lbKhachHangs.SelectedItems.Count > 0)
			{
				wdInHoaDon wd = new wdInHoaDon(Helpers.MiscHelper.DeserializeFromJson(
					(lbKhachHangs.SelectedItem as Model.KhachHang).Data));
				wd.Show();
			}
		}

		private void Click_btnXuatExcel(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lbKhachHangs.ItemsSource != null)
				Helpers.MiscHelper.ExportToExcel(lbKhachHangs.ItemsSource as System.Collections.Generic.List<Model.KhachHang>);
		}
	}
}
