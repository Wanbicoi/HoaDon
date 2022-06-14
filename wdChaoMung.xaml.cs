using System;
using System.Windows;

namespace HoaDon
{
	/// <summary>
	/// Interaction logic for wdChaoMung.xaml
	/// </summary>
	public partial class wdChaoMung : Window
	{
		public wdChaoMung()
		{
			InitializeComponent();
			tbMa.Text = Helpers.MiscHelper.GetComputerID();
			if (System.IO.File.Exists(Helpers.DatabaseHandler.CaiDat().Logo))
				imgLogo.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(Helpers.DatabaseHandler.CaiDat().Logo));
			string maKichHoat = Helpers.MiscHelper.GetComputerID();
			if (Helpers.DatabaseHandler.KiemTraMaKichHoat(Helpers.MiscHelper.CreateMD5(maKichHoat + "vinhvien")))
			{
				var w = new wdChinh();
				w.Show(); Close();
			}
		}

		private void btnCopy(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(tbMa.Text);
			MessageBox.Show("Đã copy!");
		}

		private void btnThoat(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void btnKichHoat(object sender, RoutedEventArgs e)
		{
			if (Helpers.MiscHelper.CreateMD5(Helpers.MiscHelper.GetComputerID() + "1lan") == tbMaKichHoat.Text)
			{
				var w = new wdChinh();
				w.Show();
				if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "HuongDan.pdf"))
					System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "HuongDan.pdf");
				Close();
			}
			else if (Helpers.MiscHelper.CreateMD5(Helpers.MiscHelper.GetComputerID() + "vinhvien") == tbMaKichHoat.Text)
			{
				Helpers.DatabaseHandler.LuuMaKichHoat(tbMaKichHoat.Text);
				var w = new wdChinh();
				w.Show();
				if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "HuongDan.pdf"))
					System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "HuongDan.pdf");
				Close();
			}
			else
				MessageBox.Show("Mã kích hoạt không đúng!");
		}

		private void btnDan(object sender, RoutedEventArgs e)
		{
			tbMaKichHoat.Text = Clipboard.GetText();
		}
	}
}
