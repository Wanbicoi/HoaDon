using System.Windows;

namespace HoaDon
{
	/// <summary>
	/// Interaction logic for wdMaXacNhan.xaml
	/// </summary>
	public partial class wdMaXacNhan : Window
	{
		public wdMaXacNhan()
		{
			InitializeComponent();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			if (Helpers.DatabaseHandler.MaXacNhan(pwbMaXacNhan.Password))
			{
				this.DialogResult = true;
				this.Close();
			}
			else
				MessageBox.Show("Mã xác nhận không đúng");
		}
	}
}
