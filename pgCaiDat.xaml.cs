using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace HoaDon
{
	/// <summary>
	/// Interaction logic for pgCaiDat.xaml
	/// </summary>
	public partial class pgCaiDat : Page
	{
		Model.CaiDat caiDat;
		public pgCaiDat()
		{
			InitializeComponent();
			DataContext = caiDat = Helpers.DatabaseHandler.CaiDat();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if(Helpers.DatabaseHandler.LuuCaiDat(caiDat))
				MessageBox.Show("Lưu thành công");
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			var open = new OpenFileDialog();
			open.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpg|All files (*.*)|*.*";
			if (open.ShowDialog() == true)
			{
				caiDat.Logo = open.FileName;
				imgLogo.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(caiDat.Logo));
			}
		}
	}
}
