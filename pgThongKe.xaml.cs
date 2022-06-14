using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace HoaDon
{
	/// <summary>
	/// Interaction logic for pgThongKe.xaml
	/// </summary>
	public partial class pgThongKe : Page
	{
		public SeriesCollection SeriesCollection { get; set; }
		public string[] Labels { get; set; }
		public Func<double, string> Formatter { get; set; }

		public pgThongKe()
		{
			InitializeComponent();
			//Biểu đồ
			var doanhThu = new ChartValues<double>();
			for (int i = 0; i < 12; i++)
			{
				doanhThu.Add(Helpers.DatabaseHandler.ThongKe()[i]);
			}
			SeriesCollection = new SeriesCollection
			{
				new ColumnSeries
				{
					Title="",
					Values = doanhThu
				}
			};

			Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
							"Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
			Formatter = value => value.ToString("N");

			DataContext = this;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			lbKhachHangs.ItemsSource = Helpers.DatabaseHandler.KhachHang(
					"where ngay >= " + Helpers.MiscHelper.FormatDate(dpNgayBatDau.SelectedDate ?? DateTime.Now)
					+ " and ngay <= " + Helpers.MiscHelper.FormatDate(dpNgayCuoi.SelectedDate ?? DateTime.Now));
			int? tong = 0;
			int dem = 0;

			foreach (var item in lbKhachHangs.ItemsSource as List<Model.KhachHang>)
			{
				dem++;
				tong += item.Tien;
			}
			txbSoLuongVaDoanhThu.Text = "Số lượng khách hàng: " + dem + " - Tổng doanh thu: " + String.Format("{0:#,0}", tong) + ",000 VNĐ";

			
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			if (lbKhachHangs.SelectedItems.Count > 0)
			{
				Helpers.DatabaseHandler.XoaKhachHang((lbKhachHangs.SelectedItem as Model.KhachHang).ID);
				Button_Click(null, null);
			}
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

		private void Click_btnXuatExcel(object sender, RoutedEventArgs e)
		{
			if(lbKhachHangs.ItemsSource != null)
				Helpers.MiscHelper.ExportToExcel(lbKhachHangs.ItemsSource as List<Model.KhachHang>);
		}
	}
}
