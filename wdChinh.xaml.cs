using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HoaDon
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class wdChinh : Window
	{
		public wdChinh()
		{
			InitializeComponent();
			btnTrangChu(null, null);
			if (System.IO.File.Exists(Helpers.DatabaseHandler.CaiDat().Logo))
				imgLogo.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(Helpers.DatabaseHandler.CaiDat().Logo));
		}
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			Close();
		}
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void btnHuongDan(object sender, RoutedEventArgs e)
		{
			if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "HuongDan.docx"))
				System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "HuongDan.docx");
		}

		private void btnTrangChu(object sender, MouseButtonEventArgs e)
		{
			nav.Navigate(new pgTrangChu());
		}

		private void btnHoaDon(object sender, MouseButtonEventArgs e)
		{
			nav.Navigate(new pgHoaDon());
		}

		private void btnKhoHang(object sender, MouseButtonEventArgs e)
		{
			if ((new wdMaXacNhan()).ShowDialog() == true)
			{
				nav.Navigate(new pgKhoHang());
				(sender as RadioButton).IsChecked = true;
			}
			else
				e.Handled = false;
		}

		private void btnThongKe(object sender, MouseButtonEventArgs e)
		{
			if ((new wdMaXacNhan()).ShowDialog() == true)
			{
				nav.Navigate(new pgThongKe());
				(sender as RadioButton).IsChecked = true;
			}
			else
				e.Handled = false;
		}

		private void btnCaiDat(object sender, MouseButtonEventArgs e)
		{
			if ((new wdMaXacNhan()).ShowDialog() == true)
			{
				nav.Navigate(new pgCaiDat());
				(sender as RadioButton).IsChecked = true;
			}
			else
				e.Handled = false;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (Helpers.DatabaseHandler.KiemTraMaKichHoat(Helpers.MiscHelper.CreateMD5(
				Helpers.MiscHelper.GetComputerID() + "1lan")))
			{
				Helpers.DatabaseHandler.LuuMaKichHoat("");
			}
		}

		private void btnDuLieuCu(object sender, MouseButtonEventArgs e)
		{
			nav.Navigate(new pgDuLieuCu());
		}
	}
}
