using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace HoaDon
{
	/// <summary>
	/// Interaction logic for pgDuLieuCu.xaml
	/// </summary>
	public partial class pgDuLieuCu : Page
	{
		public pgDuLieuCu()
		{
			InitializeComponent();
		}
		private void lbKhachHangs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (lbKhachHangs.SelectedItems.Count > 0)
			{
				wdInHoaDon wd = new wdInHoaDon(Helpers.MiscHelper.DeSerializeXAML(
					(lbKhachHangs.SelectedItem as Model.KhachHang).Data));
				wd.Show();
			}
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var txb = sender as TextBox;
			if (txb.Text.Length > 2 && !txb.Text.Contains("\""))
			{
				lbKhachHangs.ItemsSource = Helpers.DatabaseHandler.KhachHangCu(
					" where ten like \"%" + txb.Text + "%\" or sdt like \"%" + txb.Text + "%\"");
			}
		}
	}
}
