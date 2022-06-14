using System.Windows;
using System.Windows.Controls;

namespace HoaDon
{
	/// <summary>
	/// Interaction logic for pgKhoHang.xaml
	/// </summary>
	public partial class pgKhoHang : Page
	{
		Model.KhoHang khoHang;
		public pgKhoHang()
		{
			InitializeComponent();
			DataContext = khoHang = Helpers.DatabaseHandler.KhoHang();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Helpers.DatabaseHandler.LuuKhoHang(khoHang))
				MessageBox.Show("Lưu thành công");
		}
	}
}
